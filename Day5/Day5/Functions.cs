namespace Day5;

public static class Functions
{
    public static int GetValueChar( char commonLetter)
    {
        int ascii = (int) commonLetter;
        if (commonLetter >= 97 && commonLetter<=122)
        {
            return commonLetter - 96;
        }
        else if (commonLetter >= 65 && commonLetter <= 90)
        {
            return commonLetter - 64 + 26;
        }
        else
        {
            throw new Exception("not a letter");
        }
    }
}