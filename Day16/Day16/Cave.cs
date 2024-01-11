namespace Day16;

public class Cave
{
    public List<Valve> valves;

    public Cave(string[] lines)
    {
        valves = new List<Valve>();
        foreach (var line in lines)
        {
            valves.Add(new Valve(line));
        }
        
        foreach (var valve in valves)
        {
            valve.AddNeighbour(valves);
        }
    }

    public int FindMaxReleasedPressure(int minutes)
    {
        var mcts = new MCTS(this);
        
        return mcts.DoMCTS(minutes);
    }
    
    public int FindMaxReleasedPressureWithElephant(int minutes)
    {
        minutes -= 4;
        var mcts = new MCTS2(this);
        
        return mcts.DoMCTS(minutes);
    }


}