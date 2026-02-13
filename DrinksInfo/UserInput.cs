using Spectre.Console;

namespace DrinksInfo;

internal class UserInput
{
    DrinksService drinksService = new();

    internal void GetCategoriesInput()
    {
        var categories = drinksService.GetCategories();

        Console.WriteLine("Choose a category:");
        string category = Console.ReadLine();  

        while (!Validator.IsStringValid(category))
        {
            Console.WriteLine("\nInvalid category");
            category = Console.ReadLine();
        }

        if (!categories.Any(x => String.Equals(x.StrCategory, category, StringComparison.InvariantCultureIgnoreCase)))
        {
            Console.WriteLine("Category doesn't exist");
            GetCategoriesInput();
            return;
        }

        GetDrinksInput(category);
    }

    private void GetDrinksInput(string? category)
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

        drinksService.GetDrink(drink);
    }
}