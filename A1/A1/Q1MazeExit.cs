using System;
using System.Collections.Generic;
using TestCommon;
using System.Linq;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            var adj = Make(nodeCount, edges); 
            return Dfs(adj, StartNode - 1, EndNode - 1);
            // return adj[EndNode].Item1;
        }

        private long Dfs((long, List<long>)[] adj, long startNode, long endNode)
        {
            if (startNode == endNode)
                return 1;
            adj[startNode].Item1 = 1;
            foreach (var n in adj[startNode].Item2)
            {
                if (adj[n].Item1 == 0)
                    if (Dfs(adj, n, endNode) == 1)
                        return 1;  
            }
            return 0;
        }

        private (long, List<long>)[] Make(long nodeCount, long[][] edges)
        {
            var adj = new (long, List<long>)[nodeCount];
            for (int i = 0; i < nodeCount; i++)
                adj[i].Item2 = new List<long>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                adj[edges[i][0] - 1].Item2.Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Item2.Add(edges[i][0] - 1);
            }
            return adj;
        }
    }
}
