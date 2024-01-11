namespace Day16;

public class MCTS
{
    public Cave cave;

    public MCTS(Cave cave)
    {
        this.cave = cave;
    }

    public int DoMCTS(int minutesLeft)
    {
        int scoreUpperLimit = 30 * cave.valves.Select(info => info.valvePressure).Sum();
        Random random = new Random(42);
        var history = new List<(Node, Edge)>(15);
        List<Edge> unexplored = new List<Edge>(100);
        int maxPressure = 0;
        int pressureReleased=0;
        

       
        Node root = new Node(cave.valves[0],cave.valves[0], minutesLeft,false);

        for ( var i =0; i <10000000; i++)
        {
            history.Clear();
            foreach (var val in cave.valves)
            {
                val.explored = false;
            }
            var node = root;
            pressureReleased = 0;
            
            if (node.terminal)
            {
                Console.WriteLine("break");
                break;
            }

            if (i % 100000==0)
            {
                Console.WriteLine(maxPressure);
            }

            //Select

            while (!node.terminal && node.pathsPossible != null && node.pathsPossible.All(e => e.n > 0))
            {
                var bestEdge = new Edge();
                var bestEdgeScore = float.MinValue;

                foreach (var edge in node.pathsPossible)
                {
                    float constante = (float) Math.Sqrt(2f * Math.Log(node.N) / (float) edge.n);

                    //constante += edge.average_score_squared - edge.average_score * edge.average_score;
                    //constante *= 0.7f;
                    if (constante < 0)
                    {
                        Console.WriteLine(" negative constante "+node.N);
                    }
                    else
                    {
                        float edgeScore = edge.average_score
                                          + (float) Math.Sqrt(100f*Math.Log(node.N)
                                                              / (float) edge.n
                                                             // * Math.Min(0.25f, constante)
                                          );

                        //Console.WriteLine(" score " + edgeScore + " " + constante + " edge " + edge.actionID);
                        if (edge.child.terminal)
                        {
                            edgeScore = float.MinValue;
                        }

                        if (edgeScore > bestEdgeScore)
                        {
                            bestEdge = edge;
                            bestEdgeScore = edgeScore;
                        }
                    }
                }

                history.Add((node, bestEdge));
                pressureReleased+= DoAction(node, bestEdge);
                node = bestEdge.child;
            }

            //Expand

            if (!node.terminal)
            {
                if (node.pathsPossible == null)
                {
                    node.pathsPossible = new List<Edge>();
                    node.ExploreEdge();
                }

                unexplored.Clear();

                foreach (var edge in node.pathsPossible)
                {
                    if (edge.n == 0)
                    {
                        unexplored.Add(edge);
                    }
                }

                if (unexplored.Count == 0)
                {
                    Console.WriteLine(node.valve.name + " problème");
                }
                else
                {
                    var chosenRandomUnexploredEdge = unexplored[random.Next(unexplored.Count)];
                    history.Add((node, chosenRandomUnexploredEdge));

                    pressureReleased+= DoAction(node, chosenRandomUnexploredEdge);
                }
            }
            else
            {
                Console.Write(" ahhh");
            }

            //Random
            pressureReleased+= RollOut(node,history.Last().Item2,random);

            if (pressureReleased > maxPressure)
            {
                maxPressure = pressureReleased;
            }

            float dist = (float)pressureReleased / scoreUpperLimit;

            //Retrograde
            
            history.Reverse();
            foreach (var (node1, edge1) in history)
            {
                if (!node1.terminal && node1.pathsPossible.All(e => e.child.terminal))
                {
                    node1.terminal = true;
                }

                node1.N += 1;
                edge1.n += 1;
                edge1.average_score = (edge1.average_score * (edge1.n - 1) + dist) / edge1.n;
                edge1.average_score_squared = (edge1.average_score_squared * (edge1.n - 1) + dist * dist) / edge1.n;
            }
        }

        return maxPressure;
    }

    private int DoAction(Node node, Edge edge)
    {
       // Console.Write(node.valve.name + ":");
        if (node.openingValve)
        {
            node.valve.explored = true;
            //Console.Write(node.valve.valvePressure * node.minutesLeft+" ");
            return node.valve.valvePressure * node.minutesLeft;
        }
        return 0;
    }

    private int RollOut(Node node, Edge edge, Random random)
    {
        int cyclesLeft = edge.child.minutesLeft;
        var nextValve = edge.child.valve;
        int count = 0;
        while (cyclesLeft > 0)
        {
            //Console.Write(nextValve.name + ":");
            if (!nextValve.explored && random.Next(2)==1)
            {
                cyclesLeft--;
                nextValve.explored = true;
                count+=nextValve.valvePressure * cyclesLeft;
                //Console.Write(nextValve.valvePressure * cyclesLeft+" ");
            }
            cyclesLeft--;
            nextValve = nextValve.neighbours[random.Next(nextValve.neighbours.Count)];
            
        }
        //Console.WriteLine();

        return count;
    }

    public class Node
    {
        public Valve parent;
        public Valve valve;
        public int minutesLeft;
        public List<Edge> pathsPossible;
        public int N;
        public bool terminal;
        public bool openingValve;

        public Node(Valve valve,Valve parent, int minutes,bool openingValve)
        {
            this.parent = parent;
            this.valve = valve;
            this.openingValve = openingValve;
            minutesLeft = minutes;
            N = 0;
            if (minutes < 2)
            {
                terminal = true;
            }
            else
            {
                terminal = false;
            }
        }

        public void ExploreEdge()
        {
            foreach (var val in valve.neighbours)
            {
                if (!openingValve || val != parent || valve.neighbours.Count==1)
                {
                    var node = new Node(val, valve, minutesLeft - 1, false);
                    pathsPossible.Add(new Edge(node));

                    if (val.valvePressure > 0 && !val.explored)
                    {
                        var node2 = new Node(val, valve, minutesLeft - 2, true);
                        pathsPossible.Add(new Edge(node2));
                    }
                }
            }
        }
    }

    public class Edge
    {
        public int n;
        public float average_score;
        public Node child;
        public float average_score_squared;

        public Edge(Node child)
        {
            this.child = child;
        }

        public Edge()
        {
        }
    }
}