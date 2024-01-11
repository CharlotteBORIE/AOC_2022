namespace Day3;

public class RuckSack
{
    public char commonLetter;
    public string fullSac;
    public string firstSac;
    public string secondSac;

    public RuckSack(string line)
    {
        fullSac = line;
        int middle = line.Length / 2;
        firstSac = line.Substring(0, middle);
        secondSac = line.Substring(middle, middle);

        FindCommonLetter();
    }

    public void FindCommonLetter()
    {
        commonLetter = firstSac.Intersect(secondSac).First();
    }

}