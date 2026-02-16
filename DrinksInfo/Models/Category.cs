using System.Text.Json.Serialization;

namespace DrinksInfo.Models;
internal class Category
{
    [JsonPropertyName("strCategory")]
    public string CategoryName {  get; set; }
}

internal class Categories
{
    [JsonPropertyName("drinks")]
    public List<Category> CategoriesList { get; set; }
}
