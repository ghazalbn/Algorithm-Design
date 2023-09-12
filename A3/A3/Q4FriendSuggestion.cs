using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using Priority_Queue;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);

        public long[] Solve(long NodeCount, long EdgeCount, 
                              long[][] edges, long QueriesCount, 
                              long[][]Queries)
        {
            var dists = new long[QueriesCount];
            (var adj, var adj_R) = Make(NodeCount, edges);
            for(int i = 0; i < QueriesCount; i++)
            {
                // (var adj, var adj_R) = Make(NodeCount, edges);
                dists[i] = Bidirectional_Dijkstra(adj, adj_R, Queries[i][0] - 1, Queries[i][1] - 1); 
                if (i != QueriesCount - 1)
                {
                    adj.ToList().ForEach(v => v.Cost = double.PositiveInfinity);
                    adj_R.ToList().ForEach(v => v.Cost = double.PositiveInfinity);
                }
            } 
            return dists;
        }
        private long Bidirectional_Dijkstra(Vertex[] adj, Vertex[] adj_R, long s, long e)
        {
            var proc = new List<long>();
            var proc_R = new List<long>();
            adj[s].Cost = 0;
            adj_R[e].Cost = 0;
            var dist = new FastPriorityQueue<Vertex>(adj.Length);
            var dist_R = new FastPriorityQueue<Vertex>(adj_R.Length);
            // var dist_R = new List<(double, long)>();

            for (int i = 0; i < adj.Length; i++)
            {
                dist.Enqueue(adj[i], (float)adj[i].Cost);
                dist_R.Enqueue(adj_R[i], (float)adj_R[i].Cost);
            }
            while (dist.Count > 0 && dist_R.Count > 0)
            {
                var node = dist.Dequeue().index;
                Process(node, adj, dist, proc);
                if (proc_R.Contains(node))  
                    return ShortestPath(s, adj, proc, e, adj_R, proc_R);

                node = dist_R.Dequeue().index;
                Process(node, adj_R, dist_R, proc_R);
                if (proc.Contains(node))  
                    return ShortestPath(e, adj_R, proc_R, s, adj, proc);
            }
            return -1;
        }

        private long ShortestPath(long s, Vertex[] adj, List<long> proc, long e, Vertex[] adj_R, List<long> proc_R)
        {
            var distance = double.PositiveInfinity;
            HashSet<long> set = proc.Concat(proc_R).ToHashSet();
            foreach (var u in set)
                if (adj[u].Cost + adj_R[u].Cost < distance)
                    distance = adj[u].Cost + adj_R[u].Cost;

            return distance > int.MaxValue || distance < int.MinValue? -1 :(long)distance;
        }

        private void Process(long node, Vertex[] adj, FastPriorityQueue<Vertex> dist, List<long> proc)
        {
            for (int i = 0; i < adj[node].Neighbors.Count; i++)
            {
                var n = adj[node].Neighbors[i];
                var jadid = adj[node].Cost + adj[node].Edges[i];
                if (adj[n].Cost > jadid)
                {
                    adj[n].Cost = jadid;
                    dist.UpdatePriority(adj[n], (float)adj[n].Cost);
                }
            }
            proc.Add(node);
        }

        private (Vertex[], Vertex[]) Make(long NodeCount, long[][] edges)
        {
            var adj = new Vertex[NodeCount];
            var adj_R = new Vertex[NodeCount];
            for (int i = 0; i < NodeCount; i++)
            {
                adj[i]= new Vertex(i);
                adj_R[i]= new Vertex(i);
            }
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Neighbors.Add(edges[i][1] - 1);
                adj[edges[i][0] - 1].Edges.Add(edges[i][2]);

                adj_R[edges[i][1] - 1].Neighbors.Add(edges[i][0] - 1);
                adj_R[edges[i][1] - 1].Edges.Add(edges[i][2]);
            }
            return (adj, adj_R);
        }
    }
}
