
using DrinksInfo.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Web;

namespace DrinksInfo;
internal class DrinksService
{
    internal void GetCategories()
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest("list.php?c=list");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

            List<Category> returnedList = serialize.CategoriesList;

            TableVisualisationEngine.ShowTable(returnedList, "Categories Menu");
        }
    }

    internal void GetDrinksByCategory(string? category)
    {
        var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;

            var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

            List<Drink> returnedList = serialize.DrinksList;

            TableVisualisationEngine.ShowTable(returnedList, "Categories Menu");
        }
    }
}
