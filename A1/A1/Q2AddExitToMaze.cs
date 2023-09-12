using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            var adj = Make(nodeCount, edges);
            int result = 0;
            for (long i = 0; i < nodeCount; i++)
                if (adj[i].Item1 == 0)
                {
                    result++;
                    // var stackSize = 100000;
                    // Thread thread = new Thread(new ThreadStart(() => Dfs(adj, i)), stackSize);
                    // thread.Start();
                    Dfs(adj, i);
                }
            return result;
        }
        // non recursive Dfs
        private void Dfs((long, List<long>)[] adj, long startNode)
        {
            var stack = new Stack<long>();
            stack.Push(startNode);
            while (stack.Count != 0)
            {
            var node = stack.Pop();
                adj[node].Item1 = 1;
                foreach (var n in adj[node].Item2)
                {
                    if (adj[n].Item1 == 0)
                        stack.Push(n);
                }
            }
            // adj[startNode].Item1 = 1;
            // foreach (var n in adj[startNode].Item2)
            // {
            //     if (adj[n].Item1 == 0)
                    // Dfs(adj, n);
            // }
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
