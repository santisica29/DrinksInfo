using DrinksInfo.DTO;
using DrinksInfo.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using static Dapper.SqlMapper;

namespace DrinksInfo.Controllers;
internal class DrinkController
{
    internal static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    internal int Add(Drink drinkDetail)
    {
        using var connection = new SqlConnection(connectionString);

        var sql = @"INSERT INTO FavoriteDrinks (DrinkName, DrinkCategory, DrinkInstructions, IsAlcoholic, Ingredient1, Ingredient2, Ingredient3)
            VALUES (@DrinkName, @DrinkCategory, @DrinkInstructions, @IsAlcoholic, @Ingredient1, @Ingredient2, @Ingredient3);";

        return connection.Execute(sql, new
        {
            drinkDetail.DrinkName,
            drinkDetail.DrinkCategory,
            drinkDetail.DrinkInstructions,
            drinkDetail.IsAlcoholic,
            drinkDetail.Ingredient1,
            drinkDetail.Ingredient2,
            drinkDetail.Ingredient3,
        });
    }

    internal List<Drink> Get()
    {
        using var connection = new SqlConnection(connectionString);

        var sql = "SELECT * FROM FavoriteDrinks;";

        var list = connection.Query<Drink>(sql).ToList();

        return list ?? new List<Drink>();
    }
}
