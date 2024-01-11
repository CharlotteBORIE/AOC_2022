using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;

namespace Day18;

public class Droplets
{
    public List<Vector3> dropletPositioins;

    public int maxHeight;
    public int maxWidth;
    public int maxDepth;

    public bool[,,] spaceMap;

    public Droplets(string[] lines)
    {
        dropletPositioins = new List<Vector3>(lines.Length);
        foreach (var line in lines)
        {
            var split = line.Split(',');
            dropletPositioins.Add(new Vector3(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
        }
    }

    public int AppearingSides()
    {
        int count = 0;
        foreach (var drop in dropletPositioins)
        {
            count += 6 - FindNeighbours(drop);
        }

        return count;
    }

    public int FindNeighbours(Vector3 droplet)
    {
        int count = 0;

        int X = (int) droplet.X;
        int Y = (int) droplet.Y;
        int Z = (int) droplet.Z;

        int otherX;
        int otherY;
        int otherZ;

        foreach (var other in dropletPositioins)
        {
            otherX = (int) other.X;
            otherY = (int) other.Y;
            otherZ = (int) other.Z;

            if (!other.Equals(droplet))
            {
                if ((otherX == X && otherY == Y && (otherZ == Z + 1 || otherZ == Z - 1))
                    || (otherX == X && otherZ == Z && (otherY == Y + 1 || otherY == Y - 1))
                    || (otherZ == Z && otherY == Y && (otherX == X + 1 || otherX == X - 1)))
                {
                    count++;
                }
            }
        }

        if (count > 6)
        {
            throw new Exception("too many neighbours");
        }

        return count;
    }

    public int AppearingSidesExterior()
    {
        AirBubblesMap();
        for (var index0 = 0; index0 < spaceMap.GetLength(0); index0++)
        for (var index1 = 0; index1 < spaceMap.GetLength(1); index1++)
        for (var index2 = 0; index2 < spaceMap.GetLength(2); index2++)
        {
            var space = spaceMap[index0, index1, index2];
            if (!space)
            {
                //Console.Write($"{index0 - 1},{index1-1},{index2-1}  ");
            }
        }

        return AppearingSidesWithAirBubbles();
    }
    
    public int AppearingSidesWithAirBubbles()
    {
        int count = 0;
        foreach (var drop in dropletPositioins)
        {
            var list1 = FindNeighboursAirBubbleList(drop);
            var list2 = FindNeighboursList(drop);
            var neigbours = FindNeighbours(drop) + FindNeighboursAirBubble(drop);
            if (neigbours > 6)
            {
                throw new Exception("too many sides");
            }
            count += 6 - FindNeighbours(drop) - FindNeighboursAirBubble(drop);
        }

        return count;
    }

    public int FindNeighboursAirBubble(Vector3 droplet)
    {
        int count = 0;

        foreach (var other in ListNeighbours(droplet))
        {
            if (!spaceMap[(int) other.X+1, (int) other.Y+1, (int) other.Z+1])
            {
                count++;
            }
        }

        if (count > 6)
        {
            throw new Exception("too many neighbours");
        }

        return count;
    }
    
    public List<Vector3> FindNeighboursAirBubbleList(Vector3 droplet)
    {
        List<Vector3> list = new List<Vector3>();

        foreach (var other in ListNeighbours(droplet))
        {
            if (!spaceMap[(int) other.X+1, (int) other.Y+1, (int) other.Z+1])
            {
                list.Add(other);
            }
        }

        if (list.Count > 6)
        {
            throw new Exception("too many neighbours");
        }

        return list;
    }
    
    public List<Vector3> FindNeighboursList(Vector3 droplet)
    {
        List < Vector3 > list= new List<Vector3>();

        int X = (int) droplet.X;
        int Y = (int) droplet.Y;
        int Z = (int) droplet.Z;

        int otherX;
        int otherY;
        int otherZ;

        foreach (var other in dropletPositioins)
        {
            otherX = (int) other.X;
            otherY = (int) other.Y;
            otherZ = (int) other.Z;

            if (!other.Equals(droplet))
            {
                if ((otherX == X && otherY == Y && (otherZ == Z + 1 || otherZ == Z - 1))
                    || (otherX == X && otherZ == Z && (otherY == Y + 1 || otherY == Y - 1))
                    || (otherZ == Z && otherY == Y && (otherX == X + 1 || otherX == X - 1)))
                {
                    list.Add(other);
                }
            }
        }

        if (list.Count > 6)
        {
            throw new Exception("too many neighbours");
        }

        return list;
    }

    private void AirBubblesMap()
    {
        maxHeight = (int) dropletPositioins.Select(position => position.X).Max() + 3;
        maxWidth = (int) dropletPositioins.Select(position => position.Y).Max() + 3;
        maxDepth = (int) dropletPositioins.Select(position => position.Z).Max() + 3;

        spaceMap = new bool[maxHeight, maxWidth, maxDepth];
        foreach (var dropVector in dropletPositioins)
        {
            spaceMap[(int) dropVector.X + 1, (int) dropVector.Y + 1, (int) dropVector.Z + 1] = true;
        }

        var edges = new List<Vector3>();
        var edge = new Vector3(0, 0, 0);
        edges.Add(edge);
        int i = 0;
        while (edges.Count != 0)
        {
            i++;
            edge = edges.First();
            edges.Remove(edge);
            if (!spaceMap[(int) edge.X+1, (int) edge.Y+1, (int) edge.Z+1])
            {
                spaceMap[(int) edge.X+1, (int) edge.Y+1, (int) edge.Z+1] = true;
                edges.AddRange(ListNeighbours(edge));
            }
        }
    }

    private List<Vector3> ListNeighbours(Vector3 position)
    {
        var vectorList = new List<Vector3>();
        if (position.X > -1)
        {
            vectorList.Add(position with {X = position.X - 1});
        }

        if (position.X < maxHeight-2)
        {
            vectorList.Add(position with {X = position.X + 1});
        }

        if (position.Y > -1)
        {
            vectorList.Add(position with {Y = position.Y - 1});
        }

        if (position.Y < maxWidth-2)
        {
            vectorList.Add(position with {Y = position.Y + 1});
        }

        if (position.Z > -1)
        {
            vectorList.Add(position with {Z = position.Z - 1});
        }

        if (position.Z < maxDepth-2)
        {
            vectorList.Add(position with {Z = position.Z + 1});
        }


        return vectorList;
    }
}