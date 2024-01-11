using System.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Day7;

public class ReadFile
{
    public string[] lines;
    public Directory Home;
    public int computerSize = 70000000;
    public ReadFile(string path)
    {
        lines = System.IO.File.ReadAllLines(path);
        
        Home = new Directory("Home");
        var currentDirectory = Home;

        foreach (var line in lines)
        {
            //Console.WriteLine(line+"   "+currentDirectory.name);
            var infos = line.Split(" ");
            switch (infos[0])
            {
                case "$":
                    switch (infos[1])
                    {
                        case "ls":
                            break;
                        case "cd":
                            switch (infos[2])
                            {
                                case "/":
                                    currentDirectory = Home;
                                    break;
                                case "..":
                                    currentDirectory = currentDirectory.parent;
                                    break;
                                default:
                                    currentDirectory = currentDirectory.FindChild(infos[2]);
                                    break;
                            }

                            break;
                        default :
                            throw new Exception("Wrong command");
                    }

                    break;
                case "dir":
                    currentDirectory.AddDirectory(infos[1]);
                    break;
                default :
                    currentDirectory.AddFile(Int32.Parse(infos[0]),infos[1]);
                    break;
            }
        }

        Console.WriteLine(Home);
        // Console.WriteLine(Home.FindHeaviest(3));
        Console.WriteLine(Home.FindUnder(100000));

        int freeSize = computerSize - Home.FullSize;
        int spaceToFind = 30000000 - freeSize;
        Console.WriteLine(spaceToFind);

        Console.WriteLine(Home.FindSmallestAbove(spaceToFind));
    }
    
    
}