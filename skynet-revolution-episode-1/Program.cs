using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skynet_revolution_episode_1
{
    public struct Node
    {
        public int Id;
        public bool IsExit;
        public List<int> ConnectedNode;
    }

    static class Program
    {
        public static List<Node> _nodes;
        public static List<Node> _skynetPath;
        public static int _countNodes;
        public static int _countLinks;
        public static int _countExits;
        public static int _skynetNodeIndex;
        
        static void Main(string[] args)
        {
            Init();
            //Test.TestSkynetPath();
            LoadData();
            Process();
        }
        public static void Init()
        {
            _nodes = new List<Node>();
            _skynetPath = new List<Node>();
        }
        public static void LoadData()
        {
            Node ntmp;
            string[] inputs;
            _nodes.Clear();

            inputs = Console.ReadLine().Split(' ');
            _countNodes = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            _countLinks = int.Parse(inputs[1]); // the number of links
            _countExits = int.Parse(inputs[2]); // the number of exit gateways

            Console.Error.WriteLine(string.Format("COUNT : Nodes[{0}] Links[{1}] Exit[{2}]", _countNodes, _countLinks, _countExits));
            for (int i = 0; i < _countLinks; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                Console.Error.WriteLine(string.Format("N1 : {0} N2 : {1}", N1, N2));

                if (_nodes.Where(n => n.Id.Equals(N1)).Count() == 0) _nodes.Add(new Node() { Id = N1, ConnectedNode = new List<int>() });
                if (_nodes.Where(n => n.Id.Equals(N2)).Count() == 0) _nodes.Add(new Node() { Id = N2, ConnectedNode = new List<int>() });
                
                ntmp = _nodes.Where(n => n.Id.Equals(N1)).First();
                ntmp.ConnectedNode.Add(N2);
                ntmp = _nodes.Where(n => n.Id.Equals(N2)).First();
                ntmp.ConnectedNode.Add(N1);

            }
            for (int i = 0; i < _countExits; i++)
            {
                int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
                ntmp = _nodes.Where(n => n.Id.Equals(EI)).ToList()[0];
                ntmp.IsExit = true;
                _nodes.Remove(_nodes.Where(n => n.Id.Equals(EI)).First());
                _nodes.Add(ntmp);
            }

            DisplayNodes();
        }
        public static void Process()
        {
            // game loop
            while (true)
            {
                _skynetNodeIndex = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

                Console.Error.WriteLine(string.Format("Skynet position : {0}", _skynetNodeIndex));
                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                
                DetermineNextSkynetNode();
                //StrategySimpleExitCut();
                StrategyCutNextLink();
            }
        }
        
        public static void DisplayNodes()
        {
            DisplayLine(" ALL NODES ");
            string listNodes;
            foreach (Node node in _nodes)
            {
                listNodes = string.Empty;
                foreach (int nCo in node.ConnectedNode)
                {
                    listNodes += nCo + " ";
                }
                listNodes = listNodes.TrimEnd();
                Console.Error.WriteLine(string.Format(" --> Id : {0} Exit : {1} Connections : [{2}]", node.Id, node.IsExit, listNodes));
            }
            DisplayLine();
        }
        public static void DisplaySSkynetPath()
        {
            DisplayLine("  SKYNET   ");
            string listNodes;
            foreach (Node node in _skynetPath)
            {
                listNodes = string.Empty;
                foreach (int nCo in node.ConnectedNode)
                {
                    listNodes += nCo + " ";
                }
                listNodes = listNodes.TrimEnd();
                Console.Error.WriteLine(string.Format(" --> Id : {0} Exit : {1} Connections : [{2}]", node.Id, node.IsExit, listNodes));
            }
            DisplayLine();
        }
        public static void DisplayLine(string text = null)
        {
            if (string.IsNullOrEmpty(text)) { Console.Error.WriteLine("---------------------------------------------------------------"); }
            else { Console.Error.WriteLine("----------------------------"+ text +"------------------------"); }
        }
        
        public static void DetermineNextSkynetNode()
        {
            int jumps = 0;
            _skynetPath.Clear();
            ReachExit(_nodes.Where(n => n.Id.Equals(_skynetNodeIndex)).First(), ref jumps, ref _skynetPath);
            Console.Error.WriteLine("Exit distance : " + jumps);
            DisplaySSkynetPath();
        }
        public static bool ReachExit(Node node, ref int jumpCount, ref List<Node> shortestPath)
        {
            Node tmpNode;
            List<Node> currentNodePath;
            int minDist;
            int reachDistance = int.MaxValue;
            bool findExit = false;

            foreach (int nodeItem in node.ConnectedNode)
            {
                tmpNode = _nodes.Where(n => n.Id.Equals(nodeItem)).First();
                minDist = jumpCount;
                if (tmpNode.IsExit)
                {
                    jumpCount++;
                    shortestPath.Add(node);
                    shortestPath.Add(tmpNode);
                    return true;
                }
                else if (shortestPath.Contains(tmpNode)) { continue; }
                else
                {
                    minDist++;
                    currentNodePath = new List<Node>(shortestPath);
                    currentNodePath.Add(node);

                    if (ReachExit(tmpNode, ref minDist, ref currentNodePath))
                    {
                        if (minDist > reachDistance)
                        {
                            Console.Error.WriteLine("Recurcive found dist : " + minDist);
                            reachDistance = minDist;
                            shortestPath = currentNodePath;
                            jumpCount = minDist;
                            findExit = true;
                        }
                    }
                }
            }
            return findExit;
        }
        public static Node[] GetLinkedNodeRandom()
        {
            Node[] retNode = new Node[2];
            foreach (Node n in _nodes)
            {
                if (n.ConnectedNode.Count > 0)
                {
                    retNode[0] = n;
                    retNode[1] = _nodes.Where(no => no.Id == n.ConnectedNode[0]).First();
                    break;
                }
            }
            return retNode;
        }
        
        public static void StrategySimpleExitCut()
        {
            List<Node> exitNodes = _nodes.Where(n => n.IsExit).ToList();
            if (exitNodes.Count > 0)
            {
                Console.WriteLine(string.Format("{0} {1}", exitNodes[0].Id, exitNodes[0].ConnectedNode[0]));
                exitNodes[0].ConnectedNode.Remove(exitNodes[0].ConnectedNode[0]);
            }
            else
            {
                Console.WriteLine("0 1");
            }
        }
        public static void StrategyCutNextLink()
        {
            Node tmpNode;
            if (_skynetPath.Count > 1)
            {
                Console.WriteLine(string.Format("{0} {1}", _skynetPath[0].Id, _skynetPath[1].Id));

                tmpNode = _nodes.Where(n => n.Id.Equals(_skynetPath[0].Id)).First();
                tmpNode.ConnectedNode.Remove(_skynetPath[1].Id);
                tmpNode = _nodes.Where(n => n.Id.Equals(_skynetPath[1].Id)).First();
                tmpNode.ConnectedNode.Remove(_skynetPath[0].Id);
            }
            else
            {
                Console.Error.WriteLine("404 : " + _skynetPath.Count);
                Node[] nodes = GetLinkedNodeRandom();
                Console.WriteLine(nodes[0].Id + " " + nodes[1].Id);

                tmpNode = _nodes.Where(n => n.Id.Equals(nodes[0].Id)).First();
                tmpNode.ConnectedNode.Remove(nodes[1].Id);
                tmpNode = _nodes.Where(n => n.Id.Equals(nodes[1].Id)).First();
                tmpNode.ConnectedNode.Remove(nodes[0].Id);
            }
        }
    }
}
