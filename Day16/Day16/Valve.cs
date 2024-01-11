namespace Day16;

public class Valve
{
    public string name;
    public int valvePressure;
    public List<string> neighboursName;
    public List<Valve> neighbours;
    public bool explored;

    public Valve(string line)
    {
        var split = line.Split(new [] {',' , ' ' },StringSplitOptions.RemoveEmptyEntries);
        name = split[1];
        
        var pressure = split[4].Split(new [] {';' , '=' });
        valvePressure = Int32.Parse(pressure[1]);

        neighbours = new List<Valve>();
        
        neighboursName = new List<string>();
        for (int i = 9; i < split.Length; i++)
        {
            neighboursName.Add(split[i]);
        }

        explored = false;
    }

    public void AddNeighbour(List<Valve> caveValves)
    {
        foreach (var neighbourName in neighboursName)
        {
            foreach (var valve in caveValves)
            {
                if (valve.name == neighbourName)
                {
                    neighbours.Add(valve);
                    break;
                }
            }
            
        }
    }
}