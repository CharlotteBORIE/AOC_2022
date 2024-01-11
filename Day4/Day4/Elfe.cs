namespace Day4;

public class Elfe
{
    public int firstSection;
    public int lastSection;
    public int numberOfSections => lastSection - firstSection;

    public Elfe(string section)
    {
        var split = section.Split("-");
        if (split.Length != 2)
        {
            throw new Exception("not right length");
        }

        firstSection = Convert.ToInt32(split[0]);
        lastSection = Convert.ToInt32(split[1]);
    }

    public override string ToString()
    {
        return firstSection + "-" + lastSection;
    }
}