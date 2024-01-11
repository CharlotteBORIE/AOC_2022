using System.Numerics;

namespace Day14;

public struct RockPath
{
    public bool[,] grid;
    public List<List<Vector2>> rockPaths;
    public Vector2 startSand;

    public RockPath(string[] lines)
    {
        float height = -1;
        float width = -1;
        float heightMin = 0;
        float widthMin = 1000000f;
        rockPaths = new List<List<Vector2>>();
        foreach (var line in lines)
        {
            List<Vector2> path = new List<Vector2>();
            var split = line.Split(" -> ");
            foreach (var coordinateString in split)
            {
                var splitCoord = coordinateString.Split(",");
                if (splitCoord.Length != 2)
                {
                    throw new Exception("not coordinates");
                }
                else
                {

                    var x = float.Parse(splitCoord[0]);
                    var y = float.Parse(splitCoord[1]);
                    var position = new Vector2(x,y);
                    path.Add(position);

                    if (x > width)
                    {
                        width = x;
                    }
                    if (y > height)
                    {
                        height = y;
                    }
                    
                    if (x < widthMin)
                    {
                        widthMin = x;
                    }
                    if (y < heightMin)
                    {
                        heightMin = y;
                    }
                }
            }
            rockPaths.Add(path);
        }
        
        height -= heightMin-3;

        widthMin = 500 - height;
        width = height * 2+1;
        
        startSand = new Vector2(500-widthMin+1, -heightMin);
        grid = new bool[(int)width,(int)height];
        AdjustPaths(heightMin,widthMin-1);

        for (int i = 0; i < width; i++)
        {
            grid[i, (int)height-1] = true;
        }

        CreatePaths();
        PrintCave();
    }

    private void AdjustPaths(float heightMin, float widthMin)
    {
        for (var index = 0; index < rockPaths.Count; index++)
        {
            rockPaths[index] = rockPaths[index].Select(vec => new Vector2(vec.X - widthMin, vec.Y - heightMin)).ToList();
        }
    }

    private void CreatePaths()
    {
        foreach (var path in rockPaths)
        {
            Vector2 firstPoint;
            Vector2 secondPoint;
            for (int i = 0; i < path.Count-1; i++)
            {
                firstPoint = path[i];
                secondPoint = path[i+1];
                CompleteLine(firstPoint, secondPoint);
            }
        }
    }

    private void CompleteLine(Vector2 firstPoint, Vector2 secondPoint)
    {
        if (firstPoint.X == secondPoint.X)
        {
            int max =(int) Math.Max(firstPoint.Y, secondPoint.Y);
            int min =(int) Math.Min(firstPoint.Y, secondPoint.Y);
            for (int i = min; i <=max; i++)
            {
                grid[(int) firstPoint.X, i] = true;
            }
            return;
        }
        else if (firstPoint.Y == secondPoint.Y)
        {
            int max =(int) Math.Max(firstPoint.X, secondPoint.X);
            int min =(int) Math.Min(firstPoint.X, secondPoint.X);
            for (int i = min; i <=max; i++)
            {
                grid[i,(int) firstPoint.Y] = true;
            }
            return;
        }

        throw new Exception(" not a line");
    }

    public void PrintCave()
    {
        for (var index1 = 0; index1 < grid.GetLength(1); index1++)
        {
            for (var index0 = 0; index0 < grid.GetLength(0); index0++)
            {
                var space = grid[index0, index1];
                if (space)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("");
        }
    }

    public int DropSand()
    {
        int count = 0;
        while (!DropOneGrainOfSand())
        {
            count++;
            
        }
        Console.WriteLine();
        PrintCave();
        return count;
    }

    private bool DropOneGrainOfSand()
    {
        var grain = new GrainOfSand(startSand);
        while (!grain.isResting)
        {
            grain.MoveGrainOnce(grid);
        }

        grid[(int)grain.position.X,(int) grain.position.Y] = true;

        return grain.isAtBottom;
    }
    
    public int DropSandUntilBlocked()
    {
        int count = 0;
        while (!DropOneGrainOfSandIfNotBlocked())
        {
            count++;
            
        }
        Console.WriteLine();
        PrintCave();
        return count;
    }
    
    private bool DropOneGrainOfSandIfNotBlocked()
    {
        var grain = new GrainOfSand(startSand);
        while (!grain.isResting)
        {
            grain.MoveGrainOnce(grid);
        }

        grid[(int)grain.position.X,(int) grain.position.Y] = true;

        return grain.position.Equals(startSand);
    }
}