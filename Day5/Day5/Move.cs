namespace Day5;

public class Move
{
    public int provenance;
    public int destination;
    public int number;
    
    public Move(string move)
    {
        var split = move.Split(" ");
        provenance =Int32.Parse( split[3])-1; 
        destination =Int32.Parse( split[5])-1;
        number =Int32.Parse( split[1]);
    }
}