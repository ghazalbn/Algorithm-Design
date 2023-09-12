using System;
using System.Collections.Generic;

namespace C8
{
    public class Graph
    {        public Graph(long n)
        {
            this.n = n;
            Nodes = new Node[n];
            for (int i = 0; i < n; i++)
            {
                Nodes[i] = new Node(i);
            }
        }

        public long n { get; set; }
        public Node[] Nodes { get; set; }

        internal void BuildEdges(long m)
        {
            for (int i = 0; i < n; i++)
                Nodes[i].Neighbors.Add(Nodes[n - 1 - i]);
            for (int i = 0; i < n - m; i++)
                Nodes[i].Neighbors.Add(Nodes[i + m]);
        }
        public void DFS(Node node, long components)
        {
            node.Visited = true;
            node.Components = components;
            foreach (Node v in node.Neighbors)
            {
                if(!v.Visited)
                    DFS(v, components);
            }
        }
    }
}