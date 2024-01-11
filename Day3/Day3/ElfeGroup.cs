namespace Day3;

public class ElfeGroup
{
    public RuckSack[] group= new RuckSack[3];

    public char badgeletter;

    public ElfeGroup(string first,string second,string third)
    {
        group[0] = new RuckSack(first);
        group[1] = new RuckSack(second);
        group[2] = new RuckSack(third);
    }

    public char FindBadge()
    {
        var first = group[1].fullSac.Intersect(group[2].fullSac);
        badgeletter = first.Intersect(group[0].fullSac).First();
        return badgeletter;
    }

}