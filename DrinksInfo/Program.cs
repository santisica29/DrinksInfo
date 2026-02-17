namespace DrinksInfo;

internal class Program
{
    static async Task Main(string[] args)
    {
        UserInput userInput = new();
        await userInput.GetCategoriesInput();
    }
}
