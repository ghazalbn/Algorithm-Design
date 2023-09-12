using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;
using Priority_Queue;

namespace C2
{
    public class Q1CaptureTheFlag : Processor
    {
        public Q1CaptureTheFlag(string testDataName) : base(testDataName) 
        {
            ExcludeTestCaseRangeInclusive(1, 17);
        }

        public override string Process(string inStr)
        {
            // return TestTools.Process(inStr, (Func<long, long, List<List<long>>, List<List<long>>, long>) Solve);
            return null;
        }

        public long Solve(long n, long k, List<List<long>> flags, List<List<long>> roads)
        {
            var adj = Make(n, roads); 
            var d =Dikstra(adj, 0, k);
            return d<int.MinValue || d>int.MaxValue? -1: d;
        }
        private long Dikstra(Vertex[] adj, long startNode, long k)
        {
            adj[startNode].Cost = 0;
            var dist = new FastPriorityQueue<Vertex>(adj.Length);
            for (int i = 0; i < adj.Length; i++)
                dist.Enqueue(adj[i], (float)adj[i].Cost);
            while (dist.Count > 0)
            {
                var node = dist.Dequeue();
                if (k <= 0)
                    return (long)node.Cost;
                for (int i = 0; i < node.Neighbors.Count; i++)
                {
                    var n = node.Neighbors[i];
                    var jadid = node.Cost + node.Edges[i];
                    if (adj[n].Cost > jadid)
                    {
                        adj[n].Cost = jadid;
                        dist.UpdatePriority(node, (float)node.Cost);
                    }
                }
            }
            return -1;
        }

        private Vertex[] Make(long NodeCount, List<List<long>> edges)
        {
            var adj = new Vertex[NodeCount];
            for (int i = 0; i < NodeCount; i++)
            {
                adj[i]= new Vertex(i);
            }
            for (int i = 0; i < edges.Count; i++)
            {
                adj[edges[i][0] - 1].Neighbors.Add(edges[i][1] - 1);
                adj[edges[i][0] - 1].Edges.Add(edges[i][2]);
            }
            return adj;
        }
    }
}
