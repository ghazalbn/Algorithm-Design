using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            var adj = Make(nodeCount, edges); 
            int b = 0;
            for(long i = 0; i<nodeCount; i++)
                if (HasCycle(adj, i))
                    b = 1;
            return b;
        }

        private bool HasCycle((long, List<long>)[] adj, long x)
        {
            adj[x].Item1 = 1;
            foreach(var n in adj[x].Item2)
            {
                if (adj[n].Item1 == 1)
                    return true;
                else if (adj[n].Item1 == 0)
                    if (HasCycle(adj, n))
                        return true;
            }
            adj[x].Item1 = 2;
            return false;
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
    }
}