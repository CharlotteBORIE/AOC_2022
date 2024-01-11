using System.Numerics;

namespace Day12;

public struct Path
{
    public int[,] grid;
    public Vector2 start;
    public Vector2 end;
    public List<List<Vector2>> paths;

    public Path(string[] lines)
    {
        paths = new List<List<Vector2>>();
        start = default;
        end = default;
        
        grid = new int[lines.Length, lines[0].Length];
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var index = 0; index < line.Length; index++)
            {
                var letter = line[index];
                switch (letter)
                {
                    case 'S':
                        start = new Vector2(i, index);
                        grid[i,index]=0;
                        break;
                    case 'E':
                         end=new Vector2(i, index);
                         grid[i,index]=25;
                        break;
                    default:
                        grid[i,index]=letter-97;
                        break;
                }
            }
        }

        var tree =new SearchTree2(start, end, grid);
    }
    
}