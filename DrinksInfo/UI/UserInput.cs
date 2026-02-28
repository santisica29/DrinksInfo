using DrinksInfo.Controllers;
using DrinksInfo.Models;
using DrinksInfo.Services;
using Spectre.Console;

namespace DrinksInfo.UI;

internal class UserInput
{
    DrinksService drinksService = new();
    DrinkController drinkController = new();

    internal async Task MainMenu()
    {
        bool appRunning = true;
        while (appRunning)
        {
            Console.Clear();

            Console.WriteLine("1 - See drinks from cocktail API");
            Console.WriteLine("2 - See your favorite drinks saved in the database");
            Console.WriteLine("3 - See how many times the drinks have been viewed");
            Console.WriteLine("0 - Exit");

            string input = Console.ReadLine();

            while (String.IsNullOrEmpty(input))
            {
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "1":
                    await GetCategoriesInput();
                    break;
                case "2":
                    ShowFavoriteDrinks();
                    break;
                case "3":
                    ShowViewedDrinks();
                    break;
                case "0":
                    appRunning = false;
                    return;
            }
        }
    }

    private void ShowViewedDrinks()
    {
        var list = drinkController.GetViewedDrinks();

        if (list == null)
        {
            AnsiConsole.MarkupLine("[red]no data found[/]");
            Console.ReadKey();
            return;
        }

        var table = new Table();
        table.AddColumns("Name", "Counter");
        foreach (var drink in list)
        {
            table.AddRow(drink.Drinkname, drink.Counter.ToString());
        }

        Console.WriteLine("Press any key to go back");
        Console.ReadKey();
            
    }

    internal void ShowFavoriteDrinks()
    {
        Console.Clear();

        var list = drinkController.Get();

        if (list == null)
        {
            AnsiConsole.MarkupLine("[red]There are not items in the list[/]");
        }

        TableVisualisation.PrintDrinkTable(list);

        Console.ReadKey();
    }

    internal async Task GetCategoriesInput()
    {
        try
        {
            Console.Clear();

            var categories = await drinksService.GetCategories();

            //TableVisualisation.PrintCategoryTable(categories);

            var category = AnsiConsole.Prompt(new SelectionPrompt<Category>()
            .Title("Choose a category")
            .UseConverter(x => x.CategoryName)
            .AddChoices(categories)
            );

            //while (!Validator.IsStringValid(category.CategoryName))
            //{
            //    AnsiConsole.MarkupLine("[red]Invalid input. Try again[/]");

            //    category = AnsiConsole.Prompt(new SelectionPrompt<Category>()
            //.Title("Choose a category")
            //.UseConverter(x => x.CategoryName)
            //.AddChoices(categories)
            //);
            //}

            while (!categories.Any(x => string.Equals(x.CategoryName, category.CategoryName, StringComparison.OrdinalIgnoreCase)))
            {
                AnsiConsole.MarkupLine("[red]Category doesn't exist. Try again[/]");

                category = AnsiConsole.Prompt(new SelectionPrompt<Category>()
            .Title("Choose a category")
            .UseConverter(x => x.CategoryName)
            .AddChoices(categories)
            );
            }

            await GetDrinksInput(category.CategoryName);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex}[/]");
        }
    }

    internal async Task GetDrinksInput(string? category)
    {
        try
        {
            Console.Clear();

            var drinks = await drinksService.GetDrinksByCategory(category);

            //TableVisualisation.PrintDrinkFromCategories(drinks);

            var drink = AnsiConsole.Prompt(new SelectionPrompt<DrinkFromCategory>()
                    .Title("Choose a drink")
                    .UseConverter(x => x.Name)
                    .AddChoices(drinks));

            //while (!Validator.IsIdValid(drinkId))
            //{
            //    Console.WriteLine("\nInvalid drink");
            //    drinkId = Console.ReadLine();
            //}

            if (!drinks.Any(x => x.Id == drink.Id))
            {
                Console.WriteLine("Category doesn't exist. Press any key to try again.");
                Console.ReadKey();
                await GetDrinksInput(category);
                return;
            }

            var drinkDetail = await drinksService.GetDrink(drink.Id);

            var list = new List<Drink>()
            {
                drinkDetail
            };

            //adds drink to viewed drinks table
            drinkController.AddViewDrink(drinkDetail.DrinkName);

            Console.Clear();

            TableVisualisation.PrintDrinkTable(list);

            var saveToDB = AnsiConsole.Confirm("Do you want to save this drink to your favortie drinks list?");

            if (!saveToDB)
            {
                Console.WriteLine("The drink won't be saved");
                return;
            }
            int affectedRows = drinkController.Add(drinkDetail);

            if (affectedRows > 0)
                Console.WriteLine("Drink saved");
            else
                Console.WriteLine("Drink could not be saved to the database.");

            Console.WriteLine("Press any key to go back to main menu.");
            Console.ReadKey();

            if (!Console.KeyAvailable) return;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
}