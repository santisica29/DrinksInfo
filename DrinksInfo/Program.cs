using DrinksInfo.Services;
using DrinksInfo.UI;

namespace DrinksInfo;

internal class Program
{
    static async Task Main(string[] args)
    {
        UserInput userInput = new();
        DatabaseManager databaseManager = new();

        databaseManager.Initialize();
        await userInput.MainMenu();
    }
}
