using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using TestCommon;
using static A4.Q1BuildingRoads;

namespace A4
{
    // public class SubSet
    // {
    //     public long Parent;
    //     public long Index;
    //     public long Rank;
    //     public SubSet(long p, long index)
    //     => (Parent, Index, Rank) = (p, index, 0);
    // }
    public class Edge: FastPriorityQueueNode
    {
        public Vertex Sourse;
        public Vertex Dest;
        public double Weight;
        public Edge(Vertex s, Vertex d, double w)
        => (Sourse, Dest, Weight) = (s, d, w);
    }
    public class Q2Clustering : Processor
    {
        // public List<SubSet> SubSets;
        // public List<Edge> edges;
        public FastPriorityQueue<Edge> edges;

        public Vertex[] adj;
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);

        public double Solve(long pointCount, long[][] points, long clusterCount)
        {
            Make(pointCount, points); 
            // SubSets = new List<SubSet>();
            var d = Kruskal(clusterCount);
            return d;
        }
        private double Kruskal(long clusterCount)
        {
            long count = adj.Length;
            // for (int i = 0; i < adj.Length; i++)
            //     MakeSet(i);
            // var X = new HashSet<>();
            // edges = edges.OrderBy(e => e.Weight).ToList();
            // for (; i<edges.Count; i++)
            while(edges.Count > 0)
            {
                var e = edges.Dequeue();
                if(Find(e.Sourse) != Find(e.Dest))
                {
                    if (count == clusterCount)
                        break;
                    Union(e.Sourse, e.Dest);
                    count--;
                }
            }
            return Math.Round(edges.Dequeue().Weight, 6);
        }

        private void Union(Vertex sourse, Vertex dest)
        {
            var sourse_Parent = Find(sourse);
            var dest_Parent = Find(dest);
            dest_Parent.Parent = sourse_Parent;
        }

        private Vertex Find(Vertex sourse)
        {
            if (sourse.index == sourse.Parent.index)
                    return sourse;
                return sourse.Parent = Find(sourse.Parent);
        }

        // private void MakeSet(long vertex)
        // {
        //     SubSets.Add(new SubSet(vertex, vertex));
        // }

        private void Make(long pointCount, long[][] points)
        {
            adj = new Vertex[pointCount];
            edges = new FastPriorityQueue<Edge>((int)(pointCount * pointCount));
            for (int i = 0; i < pointCount; i++)
            {
                adj[i] = new Vertex(i);
                adj[i].Parent = adj[i];
                (adj[i].x, adj[i].y) = (points[i][0], points[i][1]);
            }
            for (int i = 0; i < pointCount; i++)
            {
                for (int j = 0; j < pointCount; j++)
                {
                    if(i != j)
                    {
                        double d = Distance(adj[i], adj[j]);
                        edges.Enqueue(new Edge(adj[i], adj[j], d), (float)d);
                    }
                }
            }
        }
        private double Distance(Vertex n, Vertex endNode)
        {
            return Math.Sqrt(Math.Pow(n.x - endNode.x, 2) + Math.Pow(n.y - endNode.y, 2));
        }
    }
}
