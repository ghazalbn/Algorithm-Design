using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        
        public long Solve(long NodeCount, long[][] edges, 
                          long StartNode,  long EndNode)
        {
            var adj = Make(NodeCount, edges); 
            return BFS(adj, StartNode - 1, EndNode - 1);
        }
        private long BFS((long, long, List<long>)[] adj, long startNode, long endNode)
        {
            adj[startNode].Item1 = 1;
            var q = new Queue<long>();
            q.Enqueue(startNode);
            while (q.Count != 0)
            {
                var node = q.Dequeue();
                foreach (var n in adj[node].Item3)
                {
                    if (adj[n].Item1 == 0)
                    {
                        var dist = adj[node].Item2 + 1;
                        adj[n].Item1 = 1;
                        adj[n].Item2 = dist;
                        if (n == endNode)
                            return adj[n].Item2;
                        q.Enqueue(n);
                    }
                }
            }
            return -1;
        }

        private (long, long, List<long>)[] Make(long nodeCount, long[][] edges)
        {
            var adj = new (long, long, List<long>)[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adj[i].Item3 = new List<long>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Item3.Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Item3.Add(edges[i][0] - 1);
            }
            return adj;
        }
    }
}
