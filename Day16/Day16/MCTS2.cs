namespace Day16;

public class MCTS2
{
    public Cave cave;

    public MCTS2(Cave cave)
    {
        this.cave = cave;
    }

    public int DoMCTS(int minutesLeft)
    {
        int scoreUpperLimit = minutesLeft * cave.valves.Select(info => info.valvePressure).Sum();
        Console.WriteLine(scoreUpperLimit);
        Random random = new Random(41);
        var history = new List<(Node, Edge)>(15);
        List<Edge> unexplored = new List<Edge>(100);
        int maxPressure = 0;
        int pressureReleased = 0;


        Node root = new Node(cave.valves[0],
            cave.valves[0],
            cave.valves[0],
            cave.valves[0],
            minutesLeft,
            false,
            false,
            false,
            false);

        for (var i = 0; i < 500000000; i++)
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

            if (i % 100000 == 0)
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
                    constante *= 0.7f;
                    if (constante < 0)
                    {
                        Console.WriteLine(" negative constante " + node.N);
                    }
                    else
                    {
                        float edgeScore = edge.average_score
                                          + (float) Math.Sqrt(2f
                                                              * Math.Log(node.N)
                                                              / (float) edge.n
                                               * Math.Min(0.25f, constante)
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
                pressureReleased += DoAction(node, bestEdge);
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

                    pressureReleased += DoAction(node, chosenRandomUnexploredEdge);
                }
            }
            else
            {
                Console.Write(" ahhh");
            }

            //Random
            pressureReleased += RollOut(node, history.Last().Item2, random);
            //Console.WriteLine();

            if (pressureReleased > maxPressure)
            {
                maxPressure = pressureReleased;
            }

            float dist = (float) pressureReleased / scoreUpperLimit;

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
        //Console.Write(node.valve.name + "/");
        //Console.Write(node.valveE.name + ":");
        int count = 0;

        if (node.openingValve)
        {
            node.valve.explored = true;
        }
        if (node.lastValveOpened)
        {
            //Console.Write(node.valve.valvePressure * node.minutesLeft + " (me) ");
            count += node.valve.valvePressure * node.minutesLeft;
        }

        if (node.openingValveE)
        {
            node.valveE.explored = true;
        }
        if (node.lastValveOpenedE)
        {
            //Console.Write(node.valveE.valvePressure * node.minutesLeft + " (ele) ");
            count += node.valveE.valvePressure * node.minutesLeft;
        }

        return count;
    }

    private int RollOut(Node node, Edge edge, Random random)
    {
        int cyclesLeft = edge.child.minutesLeft;

        var nextValve = edge.child.valve;
        var nextValveE = edge.child.valveE;

        bool opening = edge.child.openingValve;
        bool openingE = edge.child.openingValveE;

        int count = 0;
        while (cyclesLeft > 0)
        {
            //Console.Write(nextValve.name + "/");
            //Console.Write(nextValveE.name + ":");
            if (opening && openingE)
            {
                cyclesLeft--;
                cyclesLeft--;
                nextValve.explored = true;
                nextValveE.explored = true;
                count += (nextValve.valvePressure + nextValveE.valvePressure) * cyclesLeft;

                //Console.Write(nextValve.valvePressure * cyclesLeft + " (me) ");
                //Console.Write(nextValveE.valvePressure * cyclesLeft + " (elep) ");

                
                nextValve = nextValve.neighbours[random.Next(nextValve.neighbours.Count)];
                nextValveE = nextValveE.neighbours[random.Next(nextValveE.neighbours.Count)];
                opening = !nextValve.explored && nextValve.valvePressure > 0 && random.Next(2) == 1;
                openingE = !nextValveE.explored && nextValveE.valvePressure > 0 && nextValve!=nextValveE && random.Next(2) == 1;
            }
            else if (opening && !openingE)
            {
                cyclesLeft--;
                nextValve.explored = true;
                count += nextValve.valvePressure * (cyclesLeft-1);


                //Console.Write(nextValve.valvePressure * (cyclesLeft-1) + " (me) ");
                //Console.Write(" 0 ");

                nextValveE = nextValveE.neighbours[random.Next(nextValveE.neighbours.Count)];
                openingE = !nextValveE.explored && nextValveE.valvePressure > 0 && nextValve!=nextValveE && random.Next(2) == 1;
                opening = false;
            }
            else if (!opening && openingE)
            {
                cyclesLeft--;
                nextValveE.explored = true;
                count += nextValveE.valvePressure *( cyclesLeft-1);
                //Console.Write(" 0 ");
                //Console.Write(nextValveE.valvePressure * (cyclesLeft -1)+ " (ele) ");

                nextValve = nextValve.neighbours[random.Next(nextValve.neighbours.Count)];
                opening = !nextValve.explored && nextValve.valvePressure > 0 && nextValve!=nextValveE && random.Next(2) == 1;
                openingE = false;
            }
            else
            {
                cyclesLeft--;
                cyclesLeft--;
                nextValve = nextValve.neighbours[random.Next(nextValve.neighbours.Count)];
                nextValveE = nextValveE.neighbours[random.Next(nextValveE.neighbours.Count)];
                opening = !nextValve.explored && nextValve.valvePressure > 0 && random.Next(2) == 1;
                openingE = !nextValveE.explored && nextValveE.valvePressure > 0 && nextValve!=nextValveE && random.Next(2) == 1;
            }
        }
        //Console.WriteLine();

        return count;
    }

    public class Node
    {
        public int minutesLeft;
        public int N;
        public bool terminal;
        public List<Edge> pathsPossible;

        public Valve parent;
        public Valve valve;
        public bool openingValve;
        public bool lastValveOpened;

        public Valve parentE;
        public Valve valveE;
        public bool openingValveE;
        public bool lastValveOpenedE;

        public Node(Valve valve,
            Valve parent,
            Valve elephant,
            Valve elephantParent,
            int minutes,
            bool openingValve,
            bool elephantOpening,
            bool LastopenValve,
            bool LastelephantOpen)
        {
            this.parent = parent;
            this.valve = valve;
            this.openingValve = openingValve;
            lastValveOpened = LastopenValve;

            this.parentE = elephantParent;
            this.valveE = elephant;
            this.openingValveE = elephantOpening;
            lastValveOpenedE = LastelephantOpen;


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
            if (openingValveE && openingValve)
            {
                var node = new Node(valve,
                    parent,
                    valveE,
                    parentE,
                    minutesLeft - 1,
                    false,
                    false,
                    openingValve,
                    openingValveE);
                pathsPossible.Add(new Edge(node));

            }
            else if (!openingValveE && !openingValve)
            {
                foreach (var val in valve.neighbours)
                {
                    if (!(lastValveOpened && val == parent) || valve.neighbours.Count == 1)
                    {
                        foreach (var valE in valveE.neighbours)
                        {
                            if (!(lastValveOpenedE && valE == parentE) || valveE.neighbours.Count == 1)
                            {
                                var node = new Node(val,
                                    valve,
                                    valE,
                                    valveE,
                                    minutesLeft - 1,
                                    false,
                                    false,
                                    openingValve,
                                    openingValveE);
                                pathsPossible.Add(new Edge(node));

                                if (valE.valvePressure > 0 && !valE.explored)
                                {
                                    var node2 = new Node(val,
                                        valve,
                                        valE,
                                        valveE,
                                        minutesLeft - 1,
                                        false,
                                        true,
                                        openingValve,
                                        openingValveE);
                                    pathsPossible.Add(new Edge(node2));
                                }

                                if (val.valvePressure > 0 && !val.explored)
                                {
                                    var node3 = new Node(val,
                                        valve,
                                        valE,
                                        valveE,
                                        minutesLeft - 1,
                                        true,
                                        false,
                                        openingValve,
                                        openingValveE);
                                    pathsPossible.Add(new Edge(node3));

                                    if (valE.valvePressure > 0 && !valE.explored && val != valE)
                                    {
                                        var node4 = new Node(val,
                                            valve,
                                            valE,
                                            valveE,
                                            minutesLeft - 2,
                                            true,
                                            true,
                                            openingValve,
                                            openingValveE);
                                        pathsPossible.Add(new Edge(node4));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (openingValve)
            {
                foreach (var valE in valveE.neighbours)
                {
                    if (!(lastValveOpenedE && valE == parentE) || valveE.neighbours.Count == 1)
                    {
                        var node = new Node(valve,
                            parent,
                            valE,
                            valveE,
                            minutesLeft - 1,
                            false,
                            false,
                            openingValve,
                            openingValveE);
                        pathsPossible.Add(new Edge(node));

                        if (valE.valvePressure > 0 && !valE.explored && valE != valve)
                        {
                            var node2 = new Node(valve,
                                parent,
                                valE,
                                valveE,
                                minutesLeft - 1,
                                false,
                                true,
                                openingValve,
                                openingValveE);
                            pathsPossible.Add(new Edge(node2));
                        }
                    }
                }
                //only elphant can do something else
            }
            else
            {
                foreach (var val in valve.neighbours)
                {
                    if (!(lastValveOpened && val == parent) || valve.neighbours.Count == 1)
                    {
                        var node = new Node(val,
                            valve,
                            valveE,
                            parentE,
                            minutesLeft - 1,
                            false,
                            false,
                            openingValve,
                            openingValveE);
                        pathsPossible.Add(new Edge(node));

                        if (val.valvePressure > 0 && !val.explored && val != valveE)
                        {
                            var node2 = new Node(val,
                                valve,
                                valveE,
                                parentE,
                                minutesLeft - 1,
                                true,
                                false,
                                openingValve,
                                openingValveE);
                            pathsPossible.Add(new Edge(node2));
                        }
                    }
                }
                //only I can do something else
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