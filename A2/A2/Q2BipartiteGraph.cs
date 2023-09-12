using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long NodeCount, long[][] edges)
        {
            var adj = Make(NodeCount, edges); 
            for (int i = 0; i < NodeCount; i++)
                if (adj[i].Item1 == 2 && BFS(adj, i) == 0)
                    return 0;
            return 1;
        }
        private long BFS((long, List<long>)[] adj, int start)
        {
            adj[start].Item1 = 0;
            var q = new Queue<long>();
            q.Enqueue(start);
            while (q.Count != 0)
            {
                var node = q.Dequeue();
                foreach (var n in adj[node].Item2)
                {
                    if (adj[n].Item1 != 2)
                    {
                        if (adj[n].Item1 == adj[node].Item1)
                            return 0;
                    }
                    else
                    {
                        adj[n].Item1 = 1 - adj[node].Item1;
                        q.Enqueue(n);
                    }
                }
            }
            return 1;
        }

        private (long, List<long>)[] Make(long nodeCount, long[][] edges)
        {
            var adj = new (long, List<long>)[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adj[i] = (2, new List<long>());
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Item2.Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Item2.Add(edges[i][0] - 1);
            }
            return adj;
        }
    }
}
