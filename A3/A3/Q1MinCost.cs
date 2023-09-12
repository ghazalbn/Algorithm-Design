using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            var adj = Make(nodeCount, edges); 
            var d =Dikstra(adj, startNode - 1, endNode - 1);
            return d<int.MinValue || d>int.MaxValue? -1: d;
        }
        private long Dikstra(Vertex[] adj, long startNode, long endNode)
        {
            adj[startNode].Cost = 0;
            var dist = new List<(double, long)>();
            for (int i = 0; i < adj.Length; i++)
                dist.Add((adj[i].Cost, i));
            while (dist.Count > 0)
            {
                (double c, long node) = dist.Min();
                dist.Remove((c, node));
                if (node == endNode)
                    return (long)c;
                for (int i = 0; i < adj[node].Neighbors.Count; i++)
                {
                    var n = adj[node].Neighbors[i];
                    var jadid = adj[node].Cost + adj[node].Edges[i];
                    if (adj[n].Cost > jadid)
                    {
                        dist.Remove((adj[n].Cost, n));
                        adj[n].Cost = jadid;
                        dist.Add((adj[n].Cost, n));
                    }
                }
            }
            return -1;
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
