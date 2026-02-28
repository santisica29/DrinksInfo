using DrinksInfo.Controllers;
using DrinksInfo.Models;
using DrinksInfo.Services;
using DrinksInfo.Validation;
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
                case "0":
                    appRunning = false;
                    return;
            }
        }
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

            TableVisualisation.PrintCategoryTable(categories);
            
            Console.WriteLine("Choose a category:");
            string category = Console.ReadLine();

            while (!Validator.IsStringValid(category))
            {
                Console.WriteLine("\nInvalid category");
                category = Console.ReadLine();
            }

            while (!categories.Any(x => string.Equals(x.CategoryName, category, StringComparison.OrdinalIgnoreCase)))
            {
                AnsiConsole.MarkupLine("[red]Category doesn't exist. Try again[/]");

                Console.WriteLine("Choose a category:");
                category = Console.ReadLine();
            }

            await GetDrinksInput(category);
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

            if (drinks == null)
            {
                Console.WriteLine("No drinks");
                Console.ReadKey();
                return;
            }

            TableVisualisation.PrintDrinkFromCategories(drinks);

            Console.WriteLine("Choose a drink by typing it's id or go back to main menu by typing 0:");
            string drinkId = Console.ReadLine();

            if (drinkId == "0")
            {
                return;
            }

            while (!Validator.IsIdValid(drinkId))
            {
                Console.WriteLine("\nInvalid drink");
                drinkId = Console.ReadLine();
            }

            if (!drinks.Any(x => x.Id == drinkId))
            {
                Console.WriteLine("Category doesn't exist. Press any key to try again.");
                Console.ReadKey();
                await GetDrinksInput(category);
                return;
            }

            var drinkDetail = await drinksService.GetDrink(drinkId);

            var list = new List<Drink>()
            {
                drinkDetail
            };

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