using System;
using System.Collections.Generic;
using TestCommon;

namespace C1
{
    public class Q1GumballMachine : Processor
    {
        private Node[] Nodes {get; set;}
        public Q1GumballMachine(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long>)Solve);

        public long Solve(long x, long y)
        {
            GenerateNodes(Math.Max(2*y, x));
            return BFS(x, y);
        }
        private void GenerateNodes(long n)
        {
            Nodes = new Node[n + 1];
            for (int i = 1; i <= n; i++)
                Nodes[i] = new Node(i, 0);
            for (int i = 1; i <= n; i++)
            {
                if (i > 1)
                    Nodes[i].Neighbors.Add(Nodes[i - 1]);
                if (2*i <= n)
                    Nodes[i].Neighbors.Add(Nodes[2*i]);

            }
        }
        private long BFS(long x, long y)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Nodes[x]);
            while(queue.Count > 0)
            {
                Node current = queue.Dequeue();
                if (current.Value == y)
                    return current.Distance;
                current.Visited = true;
                foreach(var node in current.Neighbors)
                {
                    if (!node.Visited)
                    {
                        node.Distance = current.Distance + 1;
                        queue.Enqueue(node);   
                    }
                }
            }
            return - 1;
        }
    }
}
