using System;
using System.Linq;
using Priority_Queue;
using TestCommon;

namespace E1
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
        public FastPriorityQueue<Edge> edges;

        public Vertex[] adj;
        public Q2RoadReconstruction(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(4, 12);

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
            // System.Console.WriteLine(1);
            var d = Kruskal();
            // System.Console.WriteLine(1);
            return d;
        }
        private long[][] Kruskal()
        {
            var result = new long[adj.Length][];
            long i = 0;
            // for(; i < adj.Length - 1; i++)
            // {
            //     var e = edges.Dequeue();
            //     if(Find(e.Sourse) != Find(e.Dest))
            //     {
            //         // result[i] = new long[3];
            //         result[i] = new long[]{e.Sourse.index + 1, e.Dest.index + 1, (long)e.Weight};
            //         // System.Console.WriteLine(e.Weight);
            //         // System.Console.WriteLine(e.Sourse.index);
            //         // if (i++ >= adj.Length)
            //         //     break;
            //         // if (count == clusterCount)
            //         //     break;
            //         Union(e.Sourse, e.Dest);
            //     }
            // }
            while(i < adj.Length && edges.Count > 0)
            {
                var e = edges.Dequeue();
                if(Find(e.Sourse) != Find(e.Dest) || i == adj.Length - 1)
                {
                    result[i++] = new long[]{e.Sourse.index + 1, e.Dest.index + 1, (long)e.Weight};
                    Union(e.Sourse, e.Dest);
                }
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
            edges = new FastPriorityQueue<Edge>((int)(pointCount * pointCount));
            for (int i = 0; i < pointCount; i++)
            {
                adj[i] = new Vertex(i);
                adj[i].Parent = adj[i];
                // (adj[i].x, adj[i].y) = (points[i][0], points[i][1]);
            }
            for (int i = 0; i < pointCount; i++)
            {
                for (int j = i + 1; j < pointCount; j++)
                {
                        double d = points[i][j];
                        edges.Enqueue(new Edge(adj[i], adj[j], d), (float)d);
                }
            }
        }
    }
  
}
