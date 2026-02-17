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
                table.AddRow(d.Id, d.Name);
            }

            AnsiConsole.Write(table);

            Console.WriteLine("Choose a drink by typing it's id or go back to category menu by typing 0:");
            string drinkId = Console.ReadLine();

            if (drinkId == "0")
            {
                GetCategoriesInput();
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
                GetDrinksInput(category);
                return;
            }

            var drinkDetail = await drinksService.GetDrink(drinkId);

            var table2 = new Table();

            table2.AddColumn("Category");
            table2.AddColumn("Name");
            table2.AddColumn("Instructions");
            table2.AddColumn("Ingredient 1");
            table2.AddColumn("Ingredient 2");
            table2.AddColumn("Ingredient 3");
            table2.AddColumn("Is alcoholic?");
            table2.AddRow(
                drinkDetail.Category, 
                drinkDetail.Name, 
                drinkDetail.Instructions,
                drinkDetail.Ingredient1 ?? "-",
                drinkDetail.Ingredient2 ?? "-",
                drinkDetail.Ingredient3 ?? "-",
                drinkDetail.IsAlcoholic ?? "-");

            AnsiConsole.Write(table2);

            Console.WriteLine("Press any key to go back to categories menu.");
            Console.ReadKey();
            if (!Console.KeyAvailable) await GetCategoriesInput();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex}[/]");
        }
    }
}