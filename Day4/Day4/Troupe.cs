namespace Day4;

public class Troupe
{
    public ElfePair[] troupe;

    public Troupe(ReadFile read)
    {
        var lines = read.lines;
        troupe = new ElfePair[lines.Length];
        for (var index = 0; index < lines.Length; index++)
        {
            troupe[index] = new ElfePair(lines[index]);
        }
    }

    public int GetCollosionNumber()
    {
        int count = 0;
        foreach (var pair in troupe)
        {
            if (pair.GetEncapsuledPairs())
            {
                count++;
            }
            
        }
        return count;
    }
    
    public int GetOverLapNumber()
    {
        int count = 0;
        foreach (var pair in troupe)
        {
            if (pair.GetOverlappedPairs())
            {
                count++;
            }
            
        }
        return count;
    }
}