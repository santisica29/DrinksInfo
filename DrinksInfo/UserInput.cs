using Spectre.Console;

namespace DrinksInfo;

internal class UserInput
{
    DrinksService drinksService = new();

    internal async Task GetCategoriesInput()
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

            AnsiConsole.Write(table);
            
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
                await GetCategoriesInput();
                return;
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
            var drinks = await drinksService.GetDrinksByCategory(category);

            if (drinks == null)
            {
                Console.WriteLine("No drinks");
                Console.ReadKey();
                return;
            }

            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Name");

            foreach (var d in drinks)
            {
                table.AddRow(d.IdDrink, d.StrDrink);
            }

            AnsiConsole.Write(table);

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
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex}[/]");
        }
    }
}