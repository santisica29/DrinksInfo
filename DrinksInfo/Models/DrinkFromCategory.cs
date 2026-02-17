using System.Text.Json.Serialization;

namespace DrinksInfo.Models;
internal class DrinkFromCategory
{
    [JsonPropertyName("idDrink")]
    public string Id { get; set; }

    [JsonPropertyName("strDrink")]
    public string Name { get; set; }

}

internal class DrinksFromCategory
{
    [JsonPropertyName("drinks")]
    public List<DrinkFromCategory> DrinksList { get; set; }
}