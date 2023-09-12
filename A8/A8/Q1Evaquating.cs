using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        long[,] graph;
        long[,] rGraph;
        public Q1Evaquating(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            graph = new long[nodeCount, nodeCount];
            rGraph = new long[nodeCount, nodeCount]; 
            edges.ToList().ForEach(e => graph[e[0] - 1, e[1] - 1] += e[2]);
            return EdmondKarp(nodeCount, 0, nodeCount - 1);
        }

        public long EdmondKarp(long nodeCount, long s, long t) 
        {     
            rGraph = graph;
            var prev = new long[nodeCount]; 
    
            long max_flow = 0;
            while (bfs(nodeCount, s, t, prev)) 
            { 
                long u, flow = long.MaxValue; 
                for (long v = t; v != s; v = prev[v]) 
                { 
                    u = prev[v]; 
                    flow = Math.Min(flow, rGraph[u, v]); 
                } 
                for (long v = t; v != s; v = prev[v]) 
                { 
                    u = prev[v]; 
                    rGraph[u, v] -= flow; 
                    rGraph[v, u] += flow; 
                } 
                max_flow += flow; 
            } 

            return max_flow; 
        }  

        public bool bfs(long nodeCount, long s, long t, long[] prev) 
        { 
            bool[] visited = new bool[nodeCount]; 
            var queue = new Queue<long>(); 
            queue.Enqueue(s); 
            visited[s] = true; 
            prev[s] = -1; 

            while (queue.Count > 0) 
            { 
                long u = queue.Dequeue(); 
    
                for (long v = 0; v < nodeCount; v++) 
                { 
                    if (visited[v] == false && rGraph[u, v] > 0) 
                    { 
                        queue.Enqueue(v); 
                        visited[v] = true; 
                        prev[v] = u; 
                    } 
                } 
            } 
            return (visited[t] == true); 
        }

    }
}
