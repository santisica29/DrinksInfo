using System.Text.Json.Serialization;

namespace DrinksInfo.Models;
internal class DrinkDetail
{
    [JsonPropertyName("strDrink")]
    public string? Name { get; set; }

    [JsonPropertyName("strCategory")]
    public string? Category { get; set; }

    [JsonPropertyName("strAcloholic")]
    public string? StrAlcoholic { get; set; }
    public string? StrInstructions { get; set; }
    public string? StrInstructionsES { get; set; }
    public string? StrDrinkThumb { get; set; }
    public string? StrImageSource { get; set; }
}

internal class DrinkDetailObject
{
    [JsonPropertyName("drinks")]
    public List<DrinkDetail> DrinkDetailList { get; set; }
}
