# DrinksInfo

Console App Project to learn how to consume and API with C# and save data from the API with SQL Server

## Requirements

- Use the cocktail API to retrieve information about drinks

- You were hired by restaurant to create a solution for their drinks menu.

- Their drinks menu is provided by an external company. All the data about the drinks is in the companies database, accessible through an API.

- Your job is to create a system that allows the restaurant employee to pull data from any drink in the database.

- All you need is to create an user-friendly way to present the data to the users.

- When the users open the application, they should be presented with the Drinks Category Menu and invited to choose a category. Then they'll have the chance to choose a drink and see information about it.

- When the users visualise the drink detail, there shouldn't be any properties with empty values.

- You should handle errors so that if the API is down, the application doesn't crash.

## Features

- SQL Server database connection
	- The program uses a SQL Server db connection to store your favorite drinks
	- If no database exists, or the correct table does not exist they will be created on program start.

- Console based UI where users choose to see the drinks from the API or their favorite drinks saved.

- Uses Spectre.Console to show the data in a nicer way using it's table feature.

## Challenges

- using HTTP instead of RestSharp as the tutorial does.

## Resources

- [Cocktail API website](https://www.thecocktaildb.com/api.php)
- [HTTP request tutorial](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient)
- [Project idea from The C# Academy](https://www.thecsharpacademy.com/project/15/drinks)
