using System.Numerics;

namespace Day15;

public struct Sensor
{
    public Vector2 position;
    public int distance;

    public Sensor(string line)
    {
        var split = line.Split(" ");
        var Xstring = split[2].Split('=',',');
        var X = Int32.Parse(Xstring[1]);
        
        var Ystring = split[3].Split('=',':');
        var Y = Int32.Parse(Ystring[1]);

        position = new Vector2(X, Y);
        
        var XstringBeacon = split[8].Split('=',',');
        var XBeacon = Int32.Parse(XstringBeacon[1]);
        
        var YstringBeacon = split[9].Split('=',',');
        var YBeacon = Int32.Parse(YstringBeacon[1]);

        distance = Math.Abs(X - XBeacon) + Math.Abs(Y - YBeacon);
    }

    public (int,int) CoveringAtRow(int row)
    {
        int width = (distance - Math.Abs(row - (int) position.Y));
        if (width > 0)
        {
            int start = (int) position.X - width;
            int end = (int) position.X + width;

            return (start, end);
        }

        return (0, 0);
    }
}