using System.Security.Cryptography.X509Certificates;

namespace Day11;

public class Group
{
    public List<Monkey> monkeys;
    public int pgcd = 1;

    public Group(string[] lines)
    {
        monkeys = new List<Monkey>();
        for (var index = 0; index < lines.Length;)
        {
            var line = lines[index].Split(" ",StringSplitOptions.RemoveEmptyEntries);
            if(line.Length==0)
            {
                index++;
            }
            else if (line[0] == "Monkey")
            {
                AddMonkey(lines[index + 1], lines[index + 2], lines[index + 3], lines[index + 4], lines[index + 5]);
                index += 6;
            }
            else
            {
                throw new Exception("not read right");
            }
        }

        foreach (var monk in monkeys)
        {
            pgcd *= monk.divisor;
        }
    }

    public void DoRounds(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            for (int j = 0; j < monkeys.Count; j++)
            {
                monkeys[j].ThrowItems(this,pgcd);
            }
            Console.Write("");
        }
    }

    public long GetMonkeyActivity()
    {
        var activities = new List<int>();
        foreach (var monkey in monkeys)
        {
            activities.Add(monkey.inspect);
        }

        var twoBest=activities.OrderByDescending(v => v).Take(2).ToArray();
        return twoBest[0] * twoBest[1];
    }
    private void AddMonkey(string items, string operation, string test, string testTrue, string testFalse)
    {
        Monkey monkey = new Monkey();
        monkey.AddItems(items);
        monkey.AddOperation(operation);
        monkey.AddTest(test, testTrue, testFalse);
        monkeys.Add(monkey);
    }
}