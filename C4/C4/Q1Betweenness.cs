using System;
using System.Collections.Generic;
using TestCommon;
using System.Linq;

namespace C4
{
    public class Q1Betweenness : Processor
    {
        public Q1Betweenness(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);


        public long[] Solve(long NodeCount, long[][] edges)
        {
            var Result = new long[NodeCount];
            var adj = Make(NodeCount, edges); 
            for(long v = 0; v < NodeCount; v++)
            {
                if(v != 0)
                    for (long i = 0; i < NodeCount; i++)
                    {
                        (adj[i].Item1, adj[i].Item2) = (0, 0);
                    }
                BFS(adj, v, Result);
            }
            return Result;
        }

        private void Path((long, long, long, List<long>)[] adj, long v, long u, long[] Result)
        {
            var uu = u;
            while (true)
            {
                if(uu == v)
                    break;
                uu = adj[uu].Item3;
                if(uu != v)
                    Result[uu]++;
            }
        }

        private long BFS((long, long, long, List<long>)[] adj, long startNode, long[] Result)
        {
            adj[startNode].Item1 = 1;
            var q = new Queue<long>();
            q.Enqueue(startNode);
            while (q.Count != 0)
            {
                var node = q.Dequeue();
                Path(adj, startNode, node, Result);
                adj[node].Item4 = adj[node].Item4.OrderByDescending(v => v).ToList();

                foreach (var n in adj[node].Item4)
                {
                    if (adj[n].Item1 == 0)
                    {
                        var dist = adj[node].Item2 + 1;
                        adj[n].Item1 = 1;
                        adj[n].Item2 = dist;
                        adj[n].Item3 = node;

                        q.Enqueue(n);
                    }
                }
            }
            return -1;
        }

        private (long, long, long, List<long>)[] Make(long nodeCount, long[][] edges)
        {
            var adj = new (long, long, long, List<long>)[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                adj[i].Item4 = new List<long>();
                adj[i].Item3 = -1;
            }
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Item4.Add(edges[i][1] - 1);
            }
            return adj;
        }
    }
}
