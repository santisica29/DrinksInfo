using System.Text.Json.Serialization;

namespace DrinksInfo.Models;
internal class Drink
{
    [JsonPropertyName("idDrink")]
    public string IdDrink { get; set; }

    [JsonPropertyName("strDrink")]
    public string StrDrink { get; set; }
}

internal class Drinks
{
    [JsonPropertyName("drinks")]
    public List<Drink> DrinksList { get; set; }
}