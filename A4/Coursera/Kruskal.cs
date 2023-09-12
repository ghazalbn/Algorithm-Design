using System;
using System.Collections.Generic;
using System.Linq;

namespace Kruskal
{
    class Program
    {
        public class Vertex
    {
        public double x;
        public double y;
        public long index;
        public Vertex Parent;
        public List<long> Neighbors;
        public List<double> Edges;
        public double Cost;
        public bool Visited;
        public Vertex(long i)
        {
            Neighbors = new List<long>();
            Edges = new List<double>();
            Cost = long.MaxValue;
            Visited = false; 
            index = i; 
        }
    }
        public class Edge
        {
            public Vertex Sourse;
            public Vertex Dest;
            public double Weight;
            public Edge(Vertex s, Vertex d, double w)
            {
                Sourse = s;
                Dest = d;
                Weight = w;
            }
        }
        public static List<Edge> edges;
        public static Vertex[] adj;
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            adj = new Vertex[n];
            for (int i = 0; i < n; i++)
            {
                var point = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
                adj[i]= new Vertex(i);
                adj[i].x = point[0];
                adj[i].y = point[1];
                adj[i].Parent = adj[i];
            }

            edges = new List<Edge>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(i != j)
                    {
                        double di = Distance(adj[i], adj[j]);
                        edges.Add(new Edge(adj[i], adj[j], di));
                    }
                }
            }
            long clusterCount = long.Parse(Console.ReadLine());
            var d = Kruskal(clusterCount);
            System.Console.WriteLine(Math.Round(d, 6));
        }
        private static double Kruskal(long clusterCount)
        {
            long count = adj.Length;
            edges = edges.OrderBy(e => e.Weight).ToList();
            int i = 0;
            for (; i<edges.Count; i++)
            {
                if(Find(edges[i].Sourse) != Find(edges[i].Dest))
                {
                    if (count == clusterCount)
                        break;
                    Union(edges[i].Sourse, edges[i].Dest);
                    count--;
                }
            }
            return Math.Round(edges[i].Weight, 6);
        }

        private static void Union(Vertex sourse, Vertex dest)
        {
            var sourse_Parent = Find(sourse);
            var dest_Parent = Find(dest);
            dest_Parent.Parent = sourse_Parent;
        }

        private static Vertex Find(Vertex sourse)
        {
            if (sourse.index == sourse.Parent.index)
                    return sourse;
                return sourse.Parent = Find(sourse.Parent);
        }
        private static double Distance(Vertex n, Vertex endNode)
        {
            return Math.Sqrt(Math.Pow(n.x - endNode.x, 2) + Math.Pow(n.y - endNode.y, 2));
        }
    }
}
