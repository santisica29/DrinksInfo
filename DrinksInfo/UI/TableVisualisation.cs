using DrinksInfo.Models;
using Spectre.Console;

namespace DrinksInfo.UI;
internal static class TableVisualisation
{
    internal static void PrintDrinkTable(List<Drink> list)
    {
        var table = new Table();

        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Category");
        table.AddColumn("Is Alcoholic?");
        table.AddColumn("Instructions");
        table.AddColumn("Ingredient 1");
        table.AddColumn("Ingredient 2");
        table.AddColumn("Ingredient 3");

        foreach (var item in list)
        {
            table.AddRow(item.DrinkId.ToString(), item.DrinkName, item.DrinkCategory, item.IsAlcoholic, item.DrinkInstructions, item.Ingredient1, item.Ingredient2, item.Ingredient3 ?? "-");
            table.ShowRowSeparators();
        }

        AnsiConsole.Write(table);
    }

    internal static void PrintCategoryTable(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Category");

        foreach (var c in categories)
        {
            table.AddRow(c.CategoryName);
        }

        AnsiConsole.Write(table);
    }

    internal static void PrintDrinkFromCategories(List<DrinkFromCategory> drinks)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var d in drinks)
        {
            table.AddRow(d.Id, d.Name);
        }

        AnsiConsole.Write(table);
    }
}
