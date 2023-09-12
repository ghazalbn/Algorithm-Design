using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Priority_Queue;
using TestCommon;

namespace A4
{
    public class Q1BuildingRoads : Processor
    {
        public Q1BuildingRoads(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], double>)Solve);

        public double Solve(long pointCount, long[][] points)
        {
            var adj = Make(pointCount, points); 
            var d = Prim(adj, 0);
            return d;
        }
        private double Prim(Vertex[] adj, long startNode)
        {
            double distance = 0;
            adj[startNode].Cost = 0;
            var dist = new FastPriorityQueue<Vertex>(adj.Length);
            for (int i = 0; i < adj.Length; i++)
                dist.Enqueue(adj[i], (float)(adj[i].Cost));
            while (dist.Count > 0)
            {
                var node = dist.Dequeue().index;
                if(adj[node].Cost == double.PositiveInfinity)
                    break;
                adj[node].Visited = true;
                distance += adj[node].Cost;
                for (int i = 0; i < adj[node].Neighbors.Count; i++)
                {
                    var n = adj[node].Neighbors[i];
                    var jadid = adj[node].Edges[i];
                    if (adj[n].Cost > jadid && !adj[n].Visited)
                    {
                        adj[n].Cost = jadid;
                        dist.UpdatePriority(adj[n], (float)(adj[n].Cost));
                    }
                }
            }
            return Math.Round(distance, 6);
        }


        private Vertex[] Make(long nodeCount, long[][] points)
        {
            var adj = new Vertex[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                adj[i]= new Vertex(i);
                (adj[i].x, adj[i].y) = (points[i][0], points[i][1]);
            }
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j< nodeCount; j++)
                {
                    if (i!= j)
                    {
                        double d = Distance(adj[i], adj[j]);
                        adj[i].Neighbors.Add(j);
                        adj[i].Edges.Add(d);
                    }
                }
            }
            return adj;
        }
        private double Distance(Vertex n, Vertex endNode)
        {
            return Math.Sqrt(Math.Pow(n.x - endNode.x, 2) + Math.Pow(n.y - endNode.y, 2));
        }
    }
}
