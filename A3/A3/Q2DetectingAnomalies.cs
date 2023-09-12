using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long[] dist;

        public long Solve(long nodeCount, long[][] edges)
        {
            var adj = Make(nodeCount, edges); 
            for (int i = 0; i < nodeCount; i++)
                if (!adj[i].Visited)
                    if(Has_Cycle(adj, i))
                        return 1;
            return 0;
        }
        private bool Has_Cycle(Vertex[] adj, long s)
        {
            adj[s].Cost = 0;
            if (adj[s].Neighbors.Count == 0)
                return false;
            for (int k = 0; k < adj.Length; k++)
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
                            if (k >= adj.Length - 1)
                                return true;
                            adj[n].Cost = jadid;
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
