using System.Text.Json.Serialization;

namespace DrinksInfo.Models;
internal class Drink
{
    [JsonPropertyName("strDrink")]
    public string? Name { get; set; }

    [JsonPropertyName("strCategory")]
    public string? Category { get; set; }

    [JsonPropertyName("strAlcoholic")]
    public string? IsAlcoholic { get; set; }

    [JsonPropertyName("strInstructions")]
    public string? Instructions { get; set; }

    [JsonPropertyName("strIngredient1")]
    public string? Ingredient1 { get; set; }

    [JsonPropertyName("strIngredient2")]
    public string? Ingredient2 { get; set; }

    [JsonPropertyName("strIngredient3")]
    public string? Ingredient3 { get; set; }

}

internal class DrinkDetail
{
    [JsonPropertyName("drinks")]
    public List<Drink> DrinkDetailList { get; set; }
}
