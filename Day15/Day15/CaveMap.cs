using System.Numerics;

namespace Day15;

public struct CaveMap
{
    public List<Sensor> sensors;

    public CaveMap(string[] lines)
    {
        sensors = new List<Sensor>();
        foreach (var line in lines)
        {
            sensors.Add(new Sensor(line));
        }
    }

    public int CountRangeNotAllowed(int row)
    {
        var positions = FindRangeNotAllowed(row);

        return positions.Select(pair=>pair.Item2-pair.Item1).Sum();
    }

    private List<(int, int)> FindRangeNotAllowed(int row)
    {
        List<(int, int)> positions = new List<(int, int)>();
        foreach (var sensor in sensors)
        {
            var temp = sensor.CoveringAtRow(row);
            var newStart = temp.Item1;
            var newEnd = temp.Item2;
            if (newStart < newEnd)
            {
                if (positions.Count == 0)
                {
                    positions.Add((newStart, newEnd));
                    continue;
                }

                GetIntervals(ref positions, newStart, newEnd);
            }
        }

        return positions;
    }

    private static void GetIntervals(ref List<(int, int)> positions, int newStart, int newEnd)
    {
        var positionCount = positions.Count;
        bool added = false;
        for (var index = 0; index < positionCount; index++)
        {
            var intervalle = positions[index];
            var intervalleStart = intervalle.Item1;
            var intervalleEnd = intervalle.Item2;
            if (intervalleStart < newStart && intervalleEnd >= newStart)
            {
                added = true;
                if (intervalleEnd <= newEnd)
                {
                    positions[index] = (intervalleStart, newEnd);
                }
                else
                {
                    break;
                }
            }
            else if (intervalleStart > newStart && intervalleStart <= newEnd)
            {
                added = true;
                if (intervalleEnd >= newEnd)
                {
                    positions[index] = (newStart, intervalleEnd);
                }
                else
                {
                    positions[index] = (newStart, newEnd);
                }
            }
            else if (intervalleStart == newStart )
            {
                added = true;
                if (intervalleEnd < newEnd)
                {
                    positions[index] = (intervalleStart, newEnd);
                }
                else
                {
                    break;
                }
            }
        }

        if (!added)
        {
            positions.Add((newStart, newEnd));
        }

        if (positions.Count > 1)
        {
            positions = MergeList(positions);
        }
    }

    private static List<(int,int)> MergeList(List<(int, int)> positions)
    {
        var ordered=positions.OrderBy(x => x.Item1).ToList();
        var index = 0;
        while( index < ordered.Count-1)
        {
            if (ordered[index].Item2 >= ordered[index+1].Item1-1)
            {
                ordered[index] = (ordered[index].Item1, ordered[index+1].Item2);
                ordered.RemoveAt(index+1);
            }
            else
            {
                index++;
            }
        }

        return ordered;
    }

    public (int,int) FindTuningFrequency(int max)
    {
        var posList = FindBeaconPosition(max);
        var pos=posList.First();
        return (4 *(int) pos.X , (int)pos.Y);
    }

    private List<Vector2> FindBeaconPosition(int max)
    {
        var list = new List<Vector2>();
        for (int i = 0; i <= max; i++)
        {
            var notAllowed = FindRangeNotAllowed(i);
            int positionX;
            if (notAllowed.Count != 1 ||  notAllowed[0].Item2 < max)
            {
                positionX = notAllowed[0].Item2 + 1;
                list.Add( new Vector2( positionX,i));
            }
            else if (notAllowed[0].Item1 > 0)
            {
                positionX = 0;
                list.Add(new Vector2(positionX, i));
            }
        }

        return list;
    }
}