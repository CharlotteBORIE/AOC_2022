using System.Numerics;

namespace Day12;

public struct SearchTree
{
    public static int lengthMin;

    public SearchTree(Vector2 start,Vector2 end,int[,] grid)
    {
        lengthMin = 10000000;
        var first = new Node(start, grid,end);
        
        Console.WriteLine(lengthMin);
    }
    
    public struct Node
    {
        public Vector2 position;
        public List<Node> child;

        
        public Node(Vector2 pos,int[,] grid,Vector2 end) : this()
        {
            position = pos;
            child = new List<Node>();
            GetNextPositions(position,grid,end,new List<Node>{this});
            
        }
        public Node(Vector2 pos,int[,] grid,Vector2 end, List<Node> alreadyPast) : this()
        {
            position = pos;
            child = new List<Node>();
            if (alreadyPast.Count < lengthMin)
            {
                if (pos.Equals(end))
                {
                    if (alreadyPast.Count < lengthMin)
                    {
                        foreach (var vect in alreadyPast)
                        {
                            //Console.Write(vect.position.X + ":" + vect.position.Y + "  ");
                        }

                        lengthMin = alreadyPast.Count;
                    }
                }
                else
                {
                    var copy = alreadyPast.ToList();
                    copy.Add(this);
                    GetNextPositions(position, grid, end, copy);
                }
            }
        }

        private void GetNextPositions(Vector2 position,int[,] grid,Vector2 end,List<Node> alreadyPast)
        {
            int value = grid[(int)position.X, (int)position.Y];

            // right
            var newPos = position with {X = position.X - 1};
            bool alreadyDone = false;
            if (position.X > 0 && value + 1 >= grid[(int) position.X - 1, (int) position.Y] )
            {
                TestHeightAndAdd(grid, end, alreadyPast, newPos, alreadyDone);
            }
        
            // Left
            newPos = position with {X = position.X + 1};
            alreadyDone = false;
            if (position.X < grid.GetUpperBound(0) && value + 1 >= grid[(int) position.X + 1, (int) position.Y])
            {
                TestHeightAndAdd(grid, end, alreadyPast, newPos, alreadyDone);
            }
        
            // Up
            newPos = position with {Y = position.Y - 1};
            alreadyDone = false;
            if (position.Y > 0  && value + 1 >= grid[(int) position.X , (int) position.Y-1] )
            {
                TestHeightAndAdd(grid, end, alreadyPast, newPos, alreadyDone);
            }
        
            // Down
            newPos = position with {Y = position.Y + 1};
            alreadyDone = false;
            if (position.Y < grid.GetUpperBound(1) && value + 1 >= grid[(int) position.X , (int) position.Y+1])
            {
                TestHeightAndAdd(grid, end, alreadyPast, newPos, alreadyDone);
            }
        }

        private void TestHeightAndAdd(int[,] grid, Vector2 end, List<Node> alreadyPast, Vector2 newPos, bool alreadyDone)
        {
            foreach (var node in alreadyPast)
            {
                if (newPos.Equals(node.position))
                {
                    alreadyDone = true;
                    break;
                }
            }

            if (!alreadyDone)
            {
                var node2 = new Node(newPos, grid, end, alreadyPast);
                child.Add(node2);
            }
        }
    }

    
}