using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skynet_revolution_episode_1
{
    class Test
    {
        public static void TestSkynetPath()
        {
            Program._nodes = new List<Node>()
            {
                new Node() { Id = 1, IsExit = false, ConnectedNode = new List<Node>() },
                new Node() { Id = 2, IsExit = true, ConnectedNode = new List<Node>() },
                new Node() { Id = 0, IsExit = false, ConnectedNode = new List<Node>() }
            };
            Program._nodes[0].ConnectedNode.Add(Program._nodes[1]);
            Program._nodes[2].ConnectedNode.Add(Program._nodes[1]);
            Program._nodes[1].ConnectedNode.Add(Program._nodes[0]);
            Program._nodes[1].ConnectedNode.Add(Program._nodes[2]);
            Program._countNodes = 3;
            Program._countLinks = 2;
            Program._countExits = 1;
            Program._skynetNodeIndex = 1;
            
            Program.DetermineNextSkynetNode();
        }
    }
}
