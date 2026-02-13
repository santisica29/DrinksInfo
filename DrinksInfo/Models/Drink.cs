using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DrinksInfo.Models;
internal class Drink
{
    public string IdDrink { get; set; }
    public string StrDrink { get; set; }
}

internal class Drinks
{
    [JsonProperty("drinks")]
    public List<Drink> DrinksList { get; set; }
}