using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.GraphLogic;
using QuickGraph.Graphviz;
using System.Collections;



namespace QuickGraph
{
    public class Program
    {


        //Lot of the concepts and code comes from http://msdn.microsoft.com/en-us/library/aa289152(VS.71).aspx Thanks Dr.Thomas H. Cormen
       

        static void Main(string[] args)
        {
            /* Set Up     */
            Pathfinding pathFinding = new Pathfinding();
            int Node = 50;
            int Width=5;
            int Height=5;
            int TransmissionRange = 1;
            Random RandomNumber = new Random();

            /*Set Up ends*/
            for(int i=0; i<=Node;i++)
            {
                int XCoordinate = RandomNumber.Next(0, Width);
                int YCoordinate = RandomNumber.Next(0, Height);
                pathFinding.Graph.AddNode(i.ToString(), null, XCoordinate, YCoordinate, TransmissionRange: TransmissionRange);

            }
            
            /*Logic missing*/ 
            /*
                1. Add all nodes to queue
             *  2. Iterate througt the first all nodes
             *  3. check all nodes in the quue (determine if they are within range)
                4. if within range take the node (this) 
                   Add that node to the list but before you do make sure that the other node doesnt have a reference for our node.
             *     That way the parent node will have a reference to the child node (1st layer)
             *     Add the node pathfind.graph.AddUnderirectedEdge(1,2,calculateenergy)
             *     The second node will have a reference to its neighbhors second layer. 
             
             */
            Queue<Node> MyQueue = new Queue<Node>(); 
            foreach (Node n in pathFinding.Graph.Nodes)
            {
                MyQueue.Enqueue(n); 
                
            }

            foreach (Node x in pathFinding.Graph.Nodes)
            {

                foreach (var DataPoints in MyQueue)
                {
                    //Obviously a node will be connected to itself. So we will add a exception to skip that
                    if (x.Key != DataPoints.Key)
                    {

                        bool Test = isConnected(
                                     XInitial: x.XCoordinate,
                                     YInitial: x.YCoordinate,
                                     XFinal: DataPoints.XCoordinate,
                                     YFinal: DataPoints.YCoordinate,
                                     TransmissionRange: x.TransmissionRange
                                   );
                        if (Test == true)//then it is connected
                        {     
                            //Calculate the energy function
                            int Energy = 1;//chance this to double 
                            pathFinding.Graph.AddUndirectedEdge(x.Key,DataPoints.Key,Energy);
                      
                            
                           //  pathFinding.Graph.AddUndirectedEdge(x.Key, DataPoints.Key, Energy);
                        }
                    }
                }

                
                MyQueue.Dequeue();//remove an item from the queue FIFO
            }
  



                       
             Node start=null;
             Node end=null;
            int si = 0;
            foreach(Node mynode in pathFinding.Graph.Nodes)
            {
                if(si!=4)
                start = pathFinding.Graph.Nodes[mynode.Key];
                if(si==4)
                end = pathFinding.Graph.Nodes[mynode.Key];
                si = si + 1;
            }
            //start = pathFinding.Graph.Nodes["0"];
            //end = pathFinding.Graph.Nodes["2"];

            //here the bfs

            Pathfinding.BreadthFirstSearch(start, end);

            foreach (Node n in pathFinding.Graph.Nodes)
                n.Data = null;
            //
            Console.WriteLine("");
            Console.WriteLine("dfs");
            //here the dfs
            Pathfinding.DepthFirstSearch(start, end);
            foreach (Node n in pathFinding.Graph.Nodes)
                n.Data = null;


            Console.WriteLine("\n\nShortest path");

            Pathfinding.ShortestPath(start, end);

            pathFinding.Graph.Clear();

       

                Console.Read();
            }

        public class Pathfinding
        {
            private static Graph graph = new Graph();

            private static Hashtable dist = new Hashtable();
            private static Hashtable route = new Hashtable();


            public static void BreadthFirstSearch(Node start, Node end)
            {
                Queue<Node> queue = new Queue<Node>();

                queue.Enqueue(start);

                while (queue.Count != 0)
                {
                    Node u = queue.Dequeue();

                    // Check if node is the end
                    if (u == end)
                    {
                        Console.Write("Path found.");

                        break;
                    }
                    else
                    {
                        u.Data = "Visited";

                        // Expands u's neighbors in the queue
                        foreach (EdgeToNeighbor edge in u.Neighbors)
                        {
                            if (edge.Neighbor.Data == null)
                            {
                                edge.Neighbor.Data = "Visited";

                                if (edge.Neighbor != end)
                                {
                                    edge.Neighbor.PathParent = u;

                                    PrintPath(edge.Neighbor);
                                }
                                else
                                {
                                    edge.Neighbor.PathParent = u;

                                    PrintPath(edge.Neighbor);

                                    return;
                                }

                                Console.WriteLine();
                            }


                            queue.Enqueue(edge.Neighbor);
                        }
                    }
                }
            }

            /// <summary>
            /// Performs a Depth First search
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            public static void DepthFirstSearch(Node start, Node end)
            {
                Stack<Node> stack = new Stack<Node>();

                stack.Push(start);

                while (stack.Count != 0)
                {
                    Node u = stack.Pop();

                    // Check if node is the end
                    if (u == end)
                    {
                        Console.WriteLine("Path found");

                        break;
                    }
                    else
                    {
                        u.Data = "Visited";

                        // Store n's neighbors in the stack
                        foreach (EdgeToNeighbor edge in u.Neighbors)
                        {
                            if (edge.Neighbor.Data == null)
                            {
                                edge.Neighbor.Data = "Visited";

                                if (edge.Neighbor != end)
                                {
                                    edge.Neighbor.PathParent = u;

                                    PrintPath(edge.Neighbor);
                                }
                                else
                                {
                                    edge.Neighbor.PathParent = u;

                                    PrintPath(edge.Neighbor);

                                    return;
                                }

                                Console.WriteLine();

                                stack.Push(edge.Neighbor);
                            }
                            /* shows the repeated nodes
                            else
                            {
                              Console.Write(edge.Neighbor.Key);
                            } */
                        }
                    }
                }
            }


            static void InitDistRouteTables(string start)
            {
                // set the initial distance and route for each city in the pathFinding.Graph
                foreach (Node n in graph.Nodes)
                {
                    dist.Add(n.Key, Int32.MaxValue);
                    route.Add(n.Key, null);
                }

                // set the initial distance of start to 0
                dist[start] = 0;
            }


            private static void Relax(Node uCity, Node vCity, int cost)
            {
                int distTouCity = (int)dist[uCity.Key];
                int distTovCity = (int)dist[vCity.Key];

                if (distTovCity > distTouCity + cost)
                {
                    // update distance and route
                    dist[vCity.Key] = distTouCity + cost;
                    route[vCity.Key] = uCity;
                }
            }

            /// <summary>
            /// Retrieves the Node from the passed-in NodeList that has the <i>smallest</i> value in the distance table.
            /// </summary>
            /// <remarks>This method of grabbing the smallest Node gives Dijkstra's Algorithm a running time of
            /// O(<i>n</i><sup>2</sup>), where <i>n</i> is the number of nodes in the pathFinding.Graph.  A better approach is to
            /// use a <i>priority queue</i> data structure to store the nodes, rather than an array.  Using a priority queue
            /// will improve Dijkstra's running time to O(E lg <i>n</i>), where E is the number of edges.  This approach is
            /// preferred for sparse pathFinding.Graphs.  For more information on this, consult the README included in the download.</remarks>
            private static Node GetMin(NodeList nodes)
            {
                // find the node in nodes with the smallest distance value
                int minDist = Int32.MaxValue;
                Node minNode = null;
                foreach (Node n in nodes)
                {
                    if (((int)dist[n.Key]) <= minDist)
                    {
                        minDist = (int)dist[n.Key];
                        minNode = n;
                    }
                }

                return minNode;
            }

            /// <summary>
            /// Dijkstra's Algorithm to find the shortest path.
            /// </summary>
            static public void ShortestPath(Node start, Node end)
            {
                if (start == end)
                {
                    Console.WriteLine("There's no shortest path: start and end  are equal.");

                    return;
                }

                InitDistRouteTables(start.Key);		// initialize the route & distance tables

                NodeList nodes = graph.Nodes;	// nodes == Q

                /**** START DIJKSTRA ****/
                while (nodes.Count > 0)
                {
                    Node u = GetMin(nodes);		// get the minimum node
                    nodes.Remove(u);			// remove it from the set Q

                    // iterate through all of u's neighbors
                    foreach (EdgeToNeighbor edge in u.Neighbors)
                        Relax(u, edge.Neighbor, edge.Cost);		// relax each edge
                }	// repeat until Q is empty
                /**** END DIJKSTRA ****/

                // Display results
                string results = "The shortest path from " + start.Key + " to " + end.Key + " is " + dist[end.Key].ToString() + " joules goes as follows: ";

                Stack traceBackSteps = new Stack();

                Node current = new Node(end.Key, null);

                Node prev = null;

                do
                {
                    prev = current;
                    current = (Node)route[current.Key];

                    string temp = current.Key + " to " + prev.Key + "\r\n";
                    traceBackSteps.Push(temp);
                } while (current.Key != start.Key);

                StringBuilder sb = new StringBuilder(30 * traceBackSteps.Count);
                while (traceBackSteps.Count > 0)
                    sb.Append((string)traceBackSteps.Pop());

                Console.WriteLine(results + "\r\n\r\n" + sb.ToString());

                dist.Clear();
                route.Clear();
            }

            /// <summary>
            /// Prints the graph's edges
            /// </summary>
            static public void ShowEdges()
            {
                foreach (Node node in graph.Nodes)
                    foreach (EdgeToNeighbor edge in node.Neighbors)
                        Console.WriteLine("{0} <-> {1} - {2} miles", node.Key, edge.Neighbor.Key, edge.Cost);
            }

            /// <summary>
            /// Prints the full path for each search iteration
            /// </summary>
            /// <param name="node"></param>
            static public void PrintPath(Node node)
            {
                if (node.PathParent != null)
                    PrintPath(node.PathParent);

                Console.Write("{0} ", node.Key);
            }

            public virtual Graph Graph
            {
                get
                {
                    return graph;
                }
                set
                {
                    graph = value;
                }
            }

        }

        public static int AmountOfNodesToGenerate(int Width, int Height)
        {
            int PointsToGenerate = (int)Math.Round(Math.Abs(Width * Height * .5));
            //I would like to always generate more than 1 point. 
            if (PointsToGenerate < 1)
            {
                PointsToGenerate = 2;
            }
            return PointsToGenerate;


        }
        public static bool isConnected(int XInitial,int YInitial, int XFinal,int YFinal, int TransmissionRange)
        {
            double DistanceBetweenNodes = Math.Sqrt(Math.Pow(XFinal - XInitial, 2) + Math.Pow(YFinal - YInitial, 2));
            if (DistanceBetweenNodes <= TransmissionRange)
            {
                return true;//connected
            }
            else
                return false;//not connected 
        }
        }
    }
