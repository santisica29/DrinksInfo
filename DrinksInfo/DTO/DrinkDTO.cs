using System.Text.Json.Serialization;

namespace DrinksInfo.DTO;
internal class DrinkDTO
{
    public string Id { get; set; }
    public string? Name { get; set; }

    public string? Category { get; set; }

    public string? IsAlcoholic { get; set; }

    public string? Instructions { get; set; }

    public string? Ingredient1 { get; set; }

    public string? Ingredient2 { get; set; }

    public string? Ingredient3 { get; set; }
}
