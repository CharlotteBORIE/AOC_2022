using System.Numerics;

namespace Day12;

public struct SearchTree2
{
    public static int lengthMin;
    public static int[,] grid;
    public static int height;
    public static int width;
    public static bool[,] visited;

    public SearchTree2(Vector2 start,Vector2 end,int[,] grid)
    {
        lengthMin = grid.Length;
        SearchTree2.grid = grid;
        height = grid.GetUpperBound(0);
        width = grid.GetUpperBound(1);
        visited = new bool[height + 1, width + 1];
        
        var path = new List<Node>();
        var first = new Node(start,end,0);
        path.Add(first);
        var pathByStep = new List<Node>();
        var pathA = new List<Node>();
        
        //find a
        while (path.Count != 0)
        {
            pathByStep.AddRange(path);
            path.Clear();
            while (pathByStep.Count != 0)
            {
                var nodeTested = pathByStep.Last();
                pathByStep.RemoveAll(v => v.position.Equals(nodeTested.position));
                visited[(int) nodeTested.position.X, (int) nodeTested.position.Y] = true;

                if (nodeTested.terminal)
                {
                    lengthMin = Math.Min(lengthMin, nodeTested.distance);
                    Console.WriteLine(lengthMin);
                    return;
                }
                else
                {
                    var adding = nodeTested.GetValidNeighbhoursAtA();
                    path.AddRange(adding);
                    pathA.AddRange(adding);
                }
            }
        }


        path = pathA;
        while (path.Count != 0)
        {
            pathByStep.AddRange(path);
            path.Clear();
            while (pathByStep.Count != 0)
            {
                var nodeTested = pathByStep.Last();
                pathByStep.RemoveAll(v=>v.position.Equals(nodeTested.position));
                visited[(int) nodeTested.position.X, (int) nodeTested.position.Y] = true;

                if (nodeTested.terminal)
                {
                    lengthMin = Math.Min(lengthMin, nodeTested.distance);
                    Console.WriteLine(lengthMin);
                    return;
                }
                else
                {
                    var adding = nodeTested.GetValidNeighbhours();
                    path.AddRange(adding);
                }
            }
        }
    }
    
    public struct Node
    {
        public Vector2 position;
        public bool terminal;
        public int value;
        public Vector2 end;
        public int distance;

        public Node(Vector2 pos, Vector2 end,int distance)
        {
            position = pos;
            terminal = position.Equals(end);
            value = grid[(int) position.X, (int) position.Y];
            this.end = end;
            if (value == 0)
            {
                this.distance = 0;
            }
            else
            {
                this.distance = distance;
            }
        }

        public List<Node> GetValidNeighbhours()
        {
            var list = new List<Node>();

            List<Node> posNeighboursList = GetNeighboursList();
            for (var index = 0; index < posNeighboursList.Count; index++)
            {
                var node = posNeighboursList[index];
                if (value + 1 >= node.value)
                {
                    list.Add(node);
                }
            }

            return list;
        }

        private List<Node> GetNeighboursList()
        {
            var list = new List<Node>();

            var vectorList = new List<Vector2>();
            vectorList.Add( position with {X = position.X + 1});
            vectorList.Add(position with {X = position.X - 1});
            vectorList.Add(position with {Y = position.Y - 1});
            vectorList.Add( position with {Y = position.Y + 1});

            foreach (var vec in vectorList)
            {
                if (IsValid(vec))
                {
                    list.Add(new Node(vec,end,distance+1));
                }
            }
            return list;
        }

        private bool IsValid(Vector2 vec)
        {
            return vec.X >= 0
                   && vec.X <= height
                   && vec.Y >= 0
                   && vec.Y <= width
                   && !visited[(int) vec.X,(int) vec.Y];
        }
        
        public List<Node> GetValidNeighbhoursAtA()
        {
            var list = new List<Node>();

            List<Node> posNeighboursList = GetNeighboursList();
            for (var index = 0; index < posNeighboursList.Count; index++)
            {
                var node = posNeighboursList[index];
                if (value==0 && value + 1 >= node.value)
                {
                    list.Add(node);
                }
            }

            return list;
        }
    }
}