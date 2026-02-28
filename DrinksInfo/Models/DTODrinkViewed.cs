namespace DrinksInfo.Models;
internal class DTODrinkViewed
{
    public string Drinkname { get; set; }
    public int Counter { get; set; }

    public DTODrinkViewed()
    {

    }

    public DTODrinkViewed(string drinkname)
    {
        Drinkname = drinkname;
        Counter = 0;
    }
}
