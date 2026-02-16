using Spectre.Console;

namespace DrinksInfo;

internal class UserInput
{
    DrinksService drinksService = new();

    internal async void GetCategoriesInput()
    {
        try
        {
            var categories = await drinksService.GetCategories();

            var table = new Table();
            table.AddColumn("Category");

            foreach (var c in categories)
            {
                table.AddRow(c.CategoryName);
            }

            Console.WriteLine("Choose a category:");
            string category = Console.ReadLine();

            while (!Validator.IsStringValid(category))
            {
                Console.WriteLine("\nInvalid category");
                category = Console.ReadLine();
            }

            if (!categories.Any(x => String.Equals(x.CategoryName, category, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Category doesn't exist");
                GetCategoriesInput();
                return;
            }

            GetDrinksInput(category);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex}[/]");
        }
    }

    internal async void GetDrinksInput(string? category)
    {
        var drinks = drinksService.GetDrinksByCategory(category);

        Console.WriteLine("Choose a drink by typing it's id or go back to category menu by typing 0:");
        string drink = Console.ReadLine();

        if (drink == "0")
        {
            GetCategoriesInput();
            return;
        }

        while (!Validator.IsIdValid(drink))
        {
            Console.WriteLine("\nInvalid drink");
            drink = Console.ReadLine();
        }

        if (!drinks.Any(x => x.IdDrink == drink))
        {
            Console.WriteLine("Category doesn't exist. Press any key to try again.");
            Console.ReadKey();
            GetDrinksInput(category); 
            return;
        }

        var drinkList = await drinksService.GetDrink(drink);

        Console.WriteLine(drinkList.ToString());

        Console.WriteLine("Press any key to go back to categories menu.");
        Console.ReadKey();
        if (!Console.KeyAvailable) GetCategoriesInput();
    }
}