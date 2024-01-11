namespace Day5;

public class CargoConfiguration
{
    public List<char>[] configurations = new List<char>[9];
    public List<Move> moves = new List<Move>();
    public CargoConfiguration(ReadFile read)
    {
        var lineIndex = 0;
        for (int i = 0; i < 9; i++)
        {
            configurations[i] = new List<char>();
        }
        while (read.lines[lineIndex].Length != 0)
        {
            var index = 1;
            for (int i = 0; i < 9; i++)
            {
                var cara = read.lines[lineIndex][index];
                //Console.WriteLine(cara);
                if (cara >= 65 && cara <= 90)
                {
                    configurations[i].Add(cara);
                }
                index += 4 ;
            }
            lineIndex++;
        }
        
        lineIndex++;
        
        while (lineIndex < read.lines.Length)
        {
            moves.Add(new Move(read.lines[lineIndex]));
            lineIndex++;
        }
    }

    public void DoMoves()
    {
        foreach (var move in moves)
        {
            DoMove(move);
        }
    }
    
    public void DoNewMoves()
    {
        foreach (var move in moves)
        {
            DoNewMove(move);
        }
    }

    public void DoMove(Move move)
    {
        for (int i = 0; i < move.number; i++)
        {
            configurations[move.destination].Insert(0,configurations[move.provenance].First());
            configurations[move.provenance].RemoveAt(0);
        }
        GetFirst();
        Console.WriteLine(" ");
    }
    
    public void DoNewMove(Move move)
    {
        for (int i = 1; i <= move.number; i++)
        {
            configurations[move.destination].Insert(0,configurations[move.provenance][move.number-i]);
        }
        configurations[move.provenance].RemoveRange(0,move.number);
        GetFirst();
        Console.WriteLine(" ");
    }

    public void GetFirst()
    {
        foreach (var list in configurations)
        {
            if(list.Count!=0)
                Console.Write(list.First());
            else
                Console.Write(" ");
            
        }
    }
}