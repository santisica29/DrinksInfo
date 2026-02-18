using DrinksInfo.Models;
using System.Web;
using System.Text.Json;

namespace DrinksInfo.Services;
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

    internal async Task<Drink?> GetDrink(string? drinkId)
    {
        var url = $"http://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drinkId}";

        HttpResponseMessage response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        DrinkDetail? result = JsonSerializer.Deserialize<DrinkDetail>(json);

        List<Drink> returnedList = result.DrinkDetailList;

        Drink? drinkDetail = returnedList.FirstOrDefault();

        return drinkDetail;
    }

    internal async Task<List<DrinkFromCategory>> GetDrinksByCategory(string? category)
    {
        var url = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={HttpUtility.UrlEncode(category)}";

        HttpResponseMessage response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        DrinksFromCategory result = JsonSerializer.Deserialize<DrinksFromCategory>(json);

        List<DrinkFromCategory> drinks = result.DrinksList;

        return drinks;
    }
}
