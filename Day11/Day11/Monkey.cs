using Microsoft.VisualBasic.CompilerServices;

namespace Day11;

public class Monkey
{

    public List<Int64> items ;
    public Func<Int64, Int64> operation;
    public Func<Int64, int> test;
    public int inspect;
    public int divisor = 1;

    public void AddTest(string s, string testTrue, string testFalse)
    {
        var split = s.Split( ' ' ,StringSplitOptions.RemoveEmptyEntries);
        if (split[0] != "Test:")
        {
            throw new Exception(" not operation");
        }
        divisor = Int32.Parse(split.Last());
        
        split = testTrue.Split( ' ' );
        var resultTrue = Int32.Parse(split.Last());

        split = testFalse.Split(' ');
        var resultFalse = Int32.Parse(split.Last());

        test = CreateTest(divisor, resultTrue, resultFalse);
    }

    private Func<Int64, int> CreateTest(int divisor, int resultTrue, int resultFalse)
    {
        return value => value % divisor == 0 ? resultTrue : resultFalse;
    }

    public void AddOperation(string s)
    {
        var split = s.Split( ' ',StringSplitOptions.RemoveEmptyEntries);
        if (split[0] != "Operation:")
        {
            throw new Exception(" not operation");
        }

        var firstoperand = split[3];
        var operatorString = split[4];
        var secondoperand = split[5];

        operation = CreateOperation(firstoperand, operatorString, secondoperand);
    }

    private Func<Int64, Int64> CreateOperation(string firstoperand, string operatorString, string secondoperand)
    {
        if (secondoperand == "old")
        {
            switch (operatorString)
            {
                case "*":
                    return old => old * old;
                case "+":
                    return old => old + old;
                default:
                    throw new Exception("not right");
            }
        }
        else
        {
            switch (operatorString)
            {
                case "*":
                    return old => old * Int32.Parse(secondoperand);
                case "+":
                    return old => old + Int32.Parse(secondoperand);
                default:
                    throw new Exception("not right");
            }
        }
    }

    public void AddItems(string s)
    {
        items = new List<Int64>();
        inspect = 0;
        var split = s.Split(new Char [] {',' , ' ' },StringSplitOptions.RemoveEmptyEntries);
        if (split[0] != "Starting")
        {
            throw new Exception(" not item list");
        }

        for (int i = 2; i < split.Length; i++)
        {
            items.Add(Int32.Parse(split[i]));
        }

    }
    
    public void AddItem(Int64 item)
    {
        items.Add(item);
    }

    public void ThrowItems(Group group)
    {
        foreach (var item in items)
        {
            var count = operation(item)/3;
            inspect++;
            var monkey = test(count);
            group.monkeys[monkey].AddItem(count);
        }
        items.Clear();
    }
    
    public void ThrowItems(Group group,int pgcd)
    {
        foreach (var item in items)
        {
            var count = operation(item) % pgcd;
            if (count < 0)
            {
                Console.WriteLine(" aaa ");
            }
            inspect++;
            var monkey = test(count);
            group.monkeys[monkey].AddItem(count);
        }
        items.Clear();
    }
}