using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using GeoCoordinatePortable;
using Priority_Queue;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName) { }

        public static readonly char[] IgnoreChars = new char[] { '\n', '\r', ' ' };
        public static readonly char[] NewLineChars = new char[] { '\n', '\r' };
        private static double[][] ReadTree(IEnumerable<string> lines)
        {
            return lines.Select(line => 
                line.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(n => double.Parse(n)).ToArray()
                            ).ToArray();
        }
        public override string Process(string inStr)
        {
            return Process(inStr, (Func<long, long, double[][], double[][], long,
                                    long[][], double[]>)Solve);
        }
        public static string Process(string inStr, Func<long, long, double[][]
                                  ,double[][], long, long[][], double[]> processor)
        {
           var lines = inStr.Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
           long[] count = lines.First().Split(IgnoreChars,
                                              StringSplitOptions.RemoveEmptyEntries)
                                        .Select(n => long.Parse(n))
                                        .ToArray();
            double[][] points = ReadTree(lines.Skip(1).Take((int)count[0]));
            double[][] edges = ReadTree(lines.Skip(1 + (int)count[0]).Take((int)count[1]));
            long queryCount = long.Parse(lines.Skip(1 + (int)count[0] + (int)count[1]) 
                                         .Take(1).FirstOrDefault());
            long[][] queries = ReadTree(lines.Skip(2 + (int)count[0] + (int)count[1]))
                                        .Select(x => x.Select(z => (long)z).ToArray())
                                        .ToArray();

            return string.Join("\n", processor(count[0], count[1], points, edges,
                                queryCount, queries));
        }
        public double[] Solve(long nodeCount,
                            long edgeCount,
                            double[][] points,
                            double[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            var dists = new double[queriesCount];
            var adj = Make(nodeCount, edges, points);
            for(int i = 0; i < queriesCount; i++)
            {
                dists[i] = Dijkstra(adj, queries[i][0] - 1, queries[i][1] - 1); 
                if (i != queriesCount - 1)
                {
                    adj.ToList().ForEach(v => v.Cost = double.PositiveInfinity);
                }
            } 
            return dists;
        }
        
        private double Dijkstra(Vertex[] adj, long startNode, long endNode)
        {
            if(startNode == endNode)
                return 0;
            adj[startNode].Cost = 0;
            var dist = new FastPriorityQueue<Vertex>(adj.Length);
            for (int i = 0; i < adj.Length; i++)
                dist.Enqueue(adj[i], (float)(adj[i].Cost + Pi(adj[i], adj[endNode])));
            while (dist.Count > 0)
            {
                var node = dist.Dequeue().index;
                if(adj[node].Cost == double.PositiveInfinity)
                    break;
                if (node == endNode)
                    return adj[node].Cost;
                for (int i = 0; i < adj[node].Neighbors.Count; i++)
                {
                    var n = adj[node].Neighbors[i];
                    var jadid = adj[node].Cost + adj[node].Edges[i];
                    if (adj[n].Cost > jadid)
                    {
                        adj[n].Cost = jadid;
                        dist.UpdatePriority(adj[n], (float)(adj[n].Cost + Pi(adj[n], adj[endNode])));
                    }
                }
            }
            return -1;
        }

        private double Pi(Vertex n, Vertex endNode)
        {
            return Math.Sqrt(Math.Pow(n.x - endNode.x, 2) + Math.Pow(n.y - endNode.y, 2));
        }

        private Vertex[] Make(long nodeCount, double[][] edges, double[][] points)
        {
            var adj = new Vertex[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                adj[i]= new Vertex(i);
                (adj[i].x, adj[i].y) = (points[i][0], points[i][1]);
            }
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[(long)edges[i][0] - 1].Neighbors.Add((long)edges[i][1] - 1);
                adj[(long)edges[i][0] - 1].Edges.Add(edges[i][2]);
            }
            return adj;
        }
    }
}