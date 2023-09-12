using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney : Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);

        public string[] Solve(long nodeCount, long[][] edges, long startNode)
        {
            var dists = new string[nodeCount];
            var adj = Make(nodeCount, edges);
            Bellmanford(adj, startNode - 1);
            for(long x = 0; x <nodeCount; x++)
            {
                if (double.IsPositiveInfinity(adj[x].Cost))
                    dists[x] = "*";
                else if (double.IsNegativeInfinity(adj[x].Cost))
                    dists[x] = "-";
                else
                    dists[x] = $"{adj[x].Cost}";
            }
            return dists;
        }
        private bool Bellmanford(Vertex[] adj, long s)
        {
            adj[s].Cost = 0;
            for (int k = 0; k < 2 * adj.Length; k++)
            {
                for (int node = 0; node < adj.Length; node++)
                {
                    for (int i = 0; i < adj[node].Neighbors.Count; i++)
                    {
                        var n = adj[node].Neighbors[i];
                        var jadid = adj[node].Cost + adj[node].Edges[i];
                        if (adj[n].Cost > jadid)
                        {
                            adj[node].Visited = true;
                            adj[n].Cost = k >= adj.Length - 1? double.NegativeInfinity: jadid;
                        }
                    }
                }
            }
            return false;
        }

        private Vertex[] Make(long nodeCount, long[][] edges)
        {
            var adj = new Vertex[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adj[i]= new Vertex(i);
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Neighbors.Add(edges[i][1] - 1);
                adj[edges[i][0] - 1].Edges.Add(edges[i][2]);
            }
            return adj;
        }
    }
}
