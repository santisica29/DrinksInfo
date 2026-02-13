using Spectre.Console;

namespace DrinksInfo;

internal class UserInput
{
    DrinksService drinksService = new();

    internal void GetCategoriesInput()
    {
        drinksService.GetCategories();

        Console.WriteLine("Choose a category:");
        var category = Console.ReadLine();  

        while (!Validator.IsStringValid(category))
        {
            Console.WriteLine("\nInvalid category");
            category = Console.ReadLine();
        }

        GetDrinksInput(category);
    }

    private void GetDrinksInput(string? category)
    {
        drinksService.GetDrinksByCategory(category);
    }
}