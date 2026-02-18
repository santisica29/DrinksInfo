namespace DrinksInfo.Validation;
internal class Validator
{
    internal static bool IsIdValid(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        foreach (char c in input)
        {
            if (!char.IsDigit(c)) return false;
        }

        return true;
    }

    internal static bool IsStringValid(string? input)
    {
        if (string.IsNullOrEmpty(input)) 
            return false;

        foreach (char c in input)
        {
            if (!char.IsLetter(c) && c != '/' && c != ' ')
                return false;
        }

        return true;
    }
}
