using DrinksInfo.Models;
using System.Web;
using System.Text.Json;

namespace DrinksInfo;
internal class DrinksService
{
    private static readonly HttpClient _client = new();
    internal async Task<List<Category>> GetCategories()
    {
        string url = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";

        HttpResponseMessage response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Categories? result = JsonSerializer.Deserialize<Categories>(json);

        List<Category> categories = result.CategoriesList;

        return categories ?? new List<Category>();
    }

    internal async Task<DrinkDetail?> GetDrink(string? drink)
    {
        var url = $"http://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drink}";

        HttpResponseMessage response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        DrinkDetailObject? result = JsonSerializer.Deserialize<DrinkDetailObject>(json);

        List<DrinkDetail> returnedList = result.DrinkDetailList;

        DrinkDetail? drinkDetail = returnedList.FirstOrDefault();

        return drinkDetail;

        //List<object> prepList = new();

        //string formattedName = String.Empty;

        //foreach (PropertyInfo prop in drinkDetail.GetType().GetProperties())
        //{
        //    if (prop.Name.Contains("Str"))
        //        formattedName = prop.Name.Substring(3);

        //    if (!string.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
        //    {
        //        prepList.Add(new
        //        {
        //            Key = formattedName,
        //            Value = prop.GetValue(drinkDetail)
        //        });
        //    }
        //}

        //TableVisualisationEngine.ShowTable(prepList, drinkDetail.StrDrink);

    }

    internal async Task<List<Drink>> GetDrinksByCategory(string? category)
    {
        var url = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={HttpUtility.UrlEncode(category)}";

        HttpResponseMessage response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Drinks result = JsonSerializer.Deserialize<Drinks>(json);

        List<Drink> drinks = result.DrinksList;

        return drinks;
    }
}
