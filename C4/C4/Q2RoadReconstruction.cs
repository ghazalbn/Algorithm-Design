using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using TestCommon;

namespace C4
{
        public class Edge: FastPriorityQueueNode
        {
            public Vertex Sourse;
            public Vertex Dest;
            public double Weight;
            public Edge(Vertex s, Vertex d, double w)
            => (Sourse, Dest, Weight) = (s, d, w);
        }
    public class Q2RoadReconstruction : Processor
    {
        public List<Edge> edges;

        public Vertex[] adj;
        public Edge obj;
        public Q2RoadReconstruction(string testDataName) : base(testDataName)
        {
            // this.ExcludeTestCaseRangeInclusive(4, 12);

        }

        public override Action<string, string> Verifier => RoadReconstructionVerifier.Verify;

        public override string Process(string inStr) {
            long count;
            long[][] data;
            TestTools.ParseGraph(inStr, out count, out data);
            return string.Join("\n", Solve(count, data).Select(edge => string.Join(" ", edge)));
        }

        // returns n different edges in the form of {u, v, weight}
        public long[][] Solve(long n, long[][] distance)
        {
            Make(n, distance); 
            var result = Kruskal();
            AddNeighbors(result);
            for (int i = 0; i < edges.Count; i++)
            {
                var s = edges[i].Sourse.index;
                var d = edges[i].Dest.index;
                if(!result.Any(e => (e[0] == s + 1 && e[1] == d + 1 && e[2] == edges[i].Weight)))
                {
                    if(BFS(s, d) > distance[s][d])
                    {
                        result.Add(new long[]{s + 1, d + 1, distance[s][d]});
                        break;
                    }
                    else
                    {
                        for (long p = 0; p < n; p++)
                        {
                            adj[p].Cost = double.PositiveInfinity;
                        }
                    }
                }
            }
            if(result.Count < n)
                result.Add(new long[]{obj.Sourse.index + 1, obj.Dest.index + 1, (long)obj.Weight});
            return result.ToArray();
        }

        private double BFS(long startNode, long endNode)
        {
            var dist = new FastPriorityQueue<Vertex>(adj.Length);
            adj[startNode].Cost = 0;
            for (int i = 0; i < adj.Length; i++)
                dist.Enqueue(adj[i], (float)adj[i].Cost);
            while (dist.Count > 0)
            {
                var node = dist.Dequeue().index;
                if (node == endNode)
                    return adj[node].Cost;

                for (int i = 0; i < adj[node].Neighbors.Count; i++)
                {
                    var n = adj[node].Neighbors[i];
                    var d = adj[node].Cost + adj[node].Edges[i];
                    if (adj[n].Cost > d)
                    {
                        adj[n].Cost = d;

                        dist.UpdatePriority(adj[n], (float)adj[n].Cost);
                    }
                }
            }
            return double.PositiveInfinity;
        }
        private List<long[]> Kruskal()
        {
            var result = new List<long[]>();
            obj = null;
            int i = 0;
            while(result.Count < adj.Length - 1 || obj == null)
            {
                var e = edges[i++];
                if(Find(e.Sourse) != Find(e.Dest))
                {
                    result.Add(new long[]{e.Sourse.index + 1, e.Dest.index + 1, (long)e.Weight});
                    Union(e.Sourse, e.Dest);
                }
                else if (obj == null)
                        obj = e;
            }
            return result;
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


        private void Make(long pointCount, long[][] points)
        {
            adj = new Vertex[pointCount];
            edges = new List<Edge>();
            for (int i = 0; i < pointCount; i++)
            {
                adj[i] = new Vertex(i);
                adj[i].Parent = adj[i];
            }
            for (int i = 0; i < pointCount; i++)
            {
                for (int j = i + 1; j < pointCount; j++)
                {
                        double d = points[i][j];
                        edges.Add(new Edge(adj[i], adj[j], d));
                }
            }
            edges = edges.OrderBy(e => e.Weight).ToList();
        }
        private void AddNeighbors(List<long[]> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                var r = result[i];
                adj[r[0] - 1].Neighbors.Add(r[1] - 1);
                adj[r[0] - 1].Edges.Add(r[2]);
                adj[r[1] - 1].Neighbors.Add(r[0] - 1);
                adj[r[1] - 1].Edges.Add(r[2]);
            }
        }
    }
  
}
