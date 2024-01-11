namespace Day3;

public class Troupe 
{
    public int value;
    public ElfeGroup[] troupe;

    public Troupe(ReadFile main)
    {
        var lines = main.lines;
        int numberOfGroups = lines.Length / 3;
        troupe = new ElfeGroup[numberOfGroups];

        for (int index = 0; index < numberOfGroups; index++)
        {
            troupe[index] = new ElfeGroup(lines[index * 3],lines[index*3+1],lines[index*3+2]);
            value += Functions.GetValueChar(troupe[index].FindBadge());
        }
    }
}