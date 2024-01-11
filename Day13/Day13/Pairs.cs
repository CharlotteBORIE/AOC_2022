namespace Day13;

public struct Pairs
{
    public Packet left;
    public Packet right;

    public Pairs(string left, string right)
    {
        this.left = new Packet(left);
        this.right = new Packet(right);
    }

    public bool ValidPair()
    {
        Console.WriteLine(left);
        Console.WriteLine(right);
        var test = left.Inferior(right);
        Console.WriteLine(test);
        return test;
    }
}