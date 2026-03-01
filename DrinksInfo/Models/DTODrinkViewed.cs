namespace DrinksInfo.Models;
internal class DTODrinkViewed
{
    public string DrinkName { get; set; }
    public int Counter { get; set; } = 0;

    public DTODrinkViewed()
    {

    }

    public DTODrinkViewed(string drinkname, int counter)
    {
        DrinkName = drinkname;
        Counter = counter;
    }
}
