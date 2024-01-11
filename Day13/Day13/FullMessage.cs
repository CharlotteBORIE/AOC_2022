namespace Day13;

public struct FullMessage
{
    public List<Pairs> PairsList;

    public FullMessage(string[] lines)
    {
        PairsList = new List<Pairs>(lines.Length / 3);
        for (var index = 0; index < lines.Length;)
        {
            var lineLeft = lines[index];
            var lineRight = lines[index+1];
            var pair = new Pairs(lineLeft, lineRight);
            PairsList.Add(pair);
            index += 3;
            if (lineLeft.Length <= 0 || lineRight.Length <= 1)
            {
                throw new Exception("not read right");
            }

        }
    }
    
    public int CountValid()
    {
        return  PairsList.Select((pair, index) => (pair, index))
            .Where(pair => pair.pair.ValidPair())
            .Select(pair=>(pair.index+1)).Sum();
    }
}