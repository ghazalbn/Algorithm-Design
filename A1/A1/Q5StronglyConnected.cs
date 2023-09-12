using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            (var adj, var reverse_adj) = Make(nodeCount, edges);
            int result = 0;
            var order = TopoSort(reverse_adj);
            foreach(var i in order)
                if (adj[i].Item1 == 0)
                {
                    List<long>  tmp = new List<long>();
                    Dfs(adj, i, tmp);
                    result ++;
                }
            return result;
        }

        private long[] TopoSort((long, List<long>)[] adj)
        {
            var order = new List<long>();
            for (long i = 0; i < adj.Length; i++)
                if (adj[i].Item1 == 0)
                    Dfs(adj, i, order);
            order.Reverse();
            return order.ToArray();
        }

        private void Dfs((long, List<long>)[] adj, long startNode, List<long> order)
        {
            adj[startNode].Item1 = 1;
            foreach (var n in adj[startNode].Item2)
            {
                if (adj[n].Item1 == 0)
                    Dfs(adj, n, order);
            }
            order.Add(startNode);
        }
        private ((long, List<long>)[], (long, List<long>)[]) Make(long nodeCount, long[][] edges)
        {
            var adj = new (long, List<long>)[nodeCount];
            var reverse_adj = new (long, List<long>)[nodeCount];

            for (int i = 0; i < nodeCount; i++)
            {
                adj[i].Item2 = new List<long>();
                reverse_adj[i].Item2 = new List<long>();
            }
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Item2.Add(edges[i][1] - 1);
                reverse_adj[edges[i][1] - 1].Item2.Add(edges[i][0] - 1);
            }
            return (adj, reverse_adj);

        }
    }
}
