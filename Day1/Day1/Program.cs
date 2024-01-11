using System.IO;
namespace Day1;

public static class Program
{
    public static void Main()
    {
        int counter = 0;
        //int index = 0;
        //int[] indexElfe = {0,0,0};
        int[] max = {0, 0, 0};

        // Read the file and display it line by line.
        foreach (string line in File.ReadLines(@"../../../Day1.txt"))
        {
            if (line == "\n" || line == "")
            {
                Console.WriteLine(String.Join(",",max));
                //index++;
                if (counter > max[2])
                {
                    if (counter > max[1])
                    {
                        if (counter > max[0])
                        {
                            max[2] = max[1];
                            max[1] = max[0];
                            max[0] = counter;
                            //indexElfe = index;
                        }
                        else
                        {
                            max[2] = max[1];
                            max[1] = counter;
                        }

                    }
                    else
                    {
                        max[2] = counter;
                    }

                    
                }
                counter = 0;
            }
            else
                {
                    counter += Int32.Parse(line);
                    //Console.WriteLine("There were {0} calories ({2}) for {1} elfe.", Int32.Parse(line),index,counter);
                }
            }

            Console.WriteLine("There were {0} calories ", max.Sum());

            Console.ReadLine();
    }
}