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

    internal int AddViewDrink(string drinkName)
    {
        using var connection = new SqlConnection(connectionString);

        var sql = @"IF EXISTS (SELECT * FROM ViewedDrinks WHERE DrinkName = @drinkName
        BEGIN 
            UPDATE ViewedDrinks
            SET Counter += 1
            WHERE DrinkName = @drinkName
        END
        ELSE
        BEGIN
            INSERT INTO ViewedDrinks(DrinkName) VALUES (@DrinkName)
        END;";

        return connection.Execute(sql, new DTODrinkViewed(drinkName));

//IF EXISTS(SELECT 1 FROM YourTable WHERE YourCondition)
//BEGIN
//    -- Code to execute if the record exists
//    SELECT 'Record exists' AS Status;
//        END
//        ELSE
//        BEGIN
//            -- Code to execute if the record does not exist
//    SELECT 'Record does not exist' AS Status;
//        END

    }

    internal List<DTODrinkViewed>? GetViewedDrinks()
    {
        using var connection = new SqlConnection(connectionString);

        var sql = "SELECT * FROM ViewedDrinks;";

        return connection.Query<DTODrinkViewed>(sql).ToList();
    }
}
