using DrinksInfo.Models;
using RestSharp;
using System.Reflection;
using System.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace DrinksInfo;
internal class DrinksService
{
    private static readonly HttpClient _client = new();
    internal async Task<List<Category>> GetCategories()
    {
        var url = "http://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";

        HttpResponseMessage response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        Categories? result = JsonSerializer.Deserialize<Categories>(json);

        List<Category> categories = result.CategoriesList;

        return categories ?? new List<Category>();
    }

    internal async Task<DrinkDetail?> GetDrink(string? drink)
    {
        var url = ($"http://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={drink}");

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

    internal List<Drink> GetDrinksByCategory(string? category)
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
        var response = client.ExecuteAsync(request);

        List<Drink> drinks = new();

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

            drinks = serialize.DrinksList;
            TableVisualisationEngine.ShowTable(drinks, "Categories Menu");
            return drinks;
        }

        return drinks;
    }
}
