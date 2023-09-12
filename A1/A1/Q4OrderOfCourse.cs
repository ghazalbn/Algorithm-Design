using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long[][] edges)
        {
            var adj = Make(nodeCount, edges);
            var order = new List<long>();
            for (long i = 0; i < nodeCount; i++)
                if (adj[i].Item1 == 0)
                    Dfs(adj, i, order);
            order.Reverse();
            return order.Select(A => A+1).ToArray();
        }

        private void Dfs((long, List<long>)[] adj, long startNode, List<long> order)
        {
            adj[startNode].Item1 = 1;
            foreach (var n in adj[startNode].Item2)
            {
                if (adj[n].Item1 == 0)
                    Dfs(adj, n, order);
            }
            order.Add(startNode);
        }

        private (long, List<long>)[] Make(long nodeCount, long[][] edges)
        {
            var adj = new (long, List<long>)[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adj[i].Item2 = new List<long>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Item2.Add(edges[i][1] - 1);
            }
            return adj;
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
