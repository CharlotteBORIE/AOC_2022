namespace Day10;

public struct Processsor
{
    public int cycle=0;
    public int valueX = 1;
    public List<int> scores;
    public List<int> value;
    public int margin = 1;

    public bool[] CRT = new bool[40 * 6+1];

    public int[] levels = {20, 60, 100, 140, 180, 220};

    public Processsor(string[] read)
    {
        scores= new List<int>();
        value = new List<int>();
        foreach (var line in read)
        {
            DoInstruction(line);
        }
    }

    public void DoInstruction(string line)
    {
        var split = line.Split(" ");
        switch (split[0])
        {
            case "noop":
                cycle++;
                UpdateScore();
                break;
            case "addx":
                cycle++;
                UpdateScore();
                cycle++;
                UpdateScore();
                valueX += Int32.Parse(split[1]);
                break;
            default:
                break;
        }
    }

    public int GetCycleScore()
    {
        Console.WriteLine(cycle+"  "+valueX);
        return cycle * valueX;
    }

    public void UpdateScore()
    {
        CRT[cycle-1] = ((cycle-1) % 40) <= (valueX + margin) && ((cycle-1)%40) >= valueX - margin;  
        if (levels.Contains(cycle))
        {
            scores.Add(GetCycleScore());
        }
    }

    public int GetSumLevels()
    {
        return scores.Sum();
    }

    public void ShowCRT()
    {
        for (var index0 = 0; index0 < CRT.GetLength(0); index0++)
        {
            if (CRT[index0])
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }

            if (index0 % 40 == 39)
            {
                Console.WriteLine("");
            }
        }
    }
}