
namespace DrinksInfo;
internal class Validator
{
    internal static bool IsStringValid(string? input)
    {
        if (String.IsNullOrEmpty(input)) 
            return false;

        foreach (char c in input)
        {
            if (!Char.IsLetter(c) && c != '/' && c != ' ')
                return false;
        }

        return true;
    }
}
