using Dapper;
using Microsoft.Data.SqlClient;
using System.Configuration;


namespace DrinksInfo.Services;
internal class DatabaseManager
{
    internal static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    internal static string serverConnection = ConfigurationManager.ConnectionStrings["ServerConnection"].ConnectionString;
    public void Initialize()
    {
        CreateDatabase();
        CreateTable();
    }

    internal void CreateDatabase()
    {
        using var connection = new SqlConnection(serverConnection);

        var sql = @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Drinks')
            BEGIN
                CREATE DATABASE Drinks
            END;";

        connection.Execute(sql);
    }

    private void CreateTable()
    {
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var sql = @"IF OBJECT_ID('dbo.FavoriteDrinks', 'U') IS NULL
            BEGIN
                CREATE TABLE FavoriteDrinks (
                    DrinkId INT IDENTITY NOT NULL,
                    DrinkName VARCHAR(25) NOT NULL,
                    DrinkCategory VARCHAR(255),
                    DrinkInstructions VARCHAR(255),
                    IsAlcoholic VARCHAR (25),
                    Ingredient1 VARCHAR(255),
                    Ingredient2 VARCHAR(255),
                    Ingredient3 VARCHAR(255),

                    CONSTRAINT PK_FavDrink PRIMARY KEY (DrinkId),
                );
            END";

        connection.Execute(sql);
    }
}
