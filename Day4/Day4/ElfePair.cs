namespace Day4;

public class ElfePair
{
    public Elfe firstElfe;
    public Elfe secondElfe;

    public ElfePair()
    {
        
    }

    public ElfePair(string line)
    {
        var split = line.Split(",");
        if (split.Length != 2)
        {
            throw new Exception("there isn't two elves");
        }
        firstElfe = new Elfe(split[0]);
        secondElfe = new Elfe(split[1]);
    }

    public bool GetEncapsuledPairs()
    {
        int test = firstElfe.numberOfSections - secondElfe.numberOfSections;
        if (test == 0)
        {
            if (firstElfe.firstSection == secondElfe.firstSection)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (test > 0)
        {
            if (firstElfe.firstSection <= secondElfe.firstSection
                &&firstElfe.lastSection >= secondElfe.lastSection
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (firstElfe.firstSection >= secondElfe.firstSection
                && firstElfe.lastSection <= secondElfe.lastSection
               )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
    }
    
    public bool GetOverlappedPairs()
    {
        if (firstElfe.firstSection >= secondElfe.firstSection
            && firstElfe.firstSection<=secondElfe.lastSection)
        {
            return true;
        }
        else if (firstElfe.firstSection <= secondElfe.firstSection
                 && firstElfe.lastSection>=secondElfe.firstSection)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}