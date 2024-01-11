namespace Day3;

public class Elfe
{
    public int value;
    public RuckSack[] RuckSacks;

    public Elfe(ReadFile main)
    {
        var read = main.lines;
        RuckSacks = new RuckSack[read.Length];
        for (var index = 0; index < read.Length; index++)
        {
            var line = read[index];
            RuckSacks[index] = new RuckSack(line);
            value += Functions.GetValueChar(RuckSacks[index].commonLetter);
        }
    }
    
    
   
}