using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        long[][] graph;
        long[][] rGraph;
        public Q2Airlines(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);

        public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            graph = new long[flightCount+crewCount+2][];
            graph = graph.Select(l => new long[flightCount+crewCount+2]).ToArray();

            BuildGraph(flightCount, crewCount, info);
            var result = EdmondKarp(flightCount, flightCount+crewCount+2, flightCount+crewCount, flightCount+crewCount+1);
            return result;
        }

        private void BuildGraph(long N, long M, long[][] info)
        {
            // source to v
            for (long i = 0; i < N; i++)
            {
                graph[M+N][i] = 1;
            }
            // u to sink
            for (long i = N; i < N+M; i++)
            {
                graph[i][N+M+1] = 1;
            }
            // v to u
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    graph[i][j+N] = info[i][j];
                }
            }
        }
        public long[] EdmondKarp(long N, long nodeCount, long s, long t) 
        { 
            rGraph = graph;
            long[] result = new long[N], 
            prev = new long[nodeCount]; 
    
            long max_flow = 0;

            while (BFS(nodeCount, s, t, prev)) 
            { 
                long u, flow = long.MaxValue; 
                for (long v = t; v != s; v = prev[v]) 
                { 
                    u = prev[v]; 
                    flow = Math.Min(flow, rGraph[u][v]); 
                } 
                for (long v = t; v != s; v = prev[v]) 
                { 
                    u = prev[v]; 
                    if(u < N && flow == 1)
                        result[u] = v;
                    rGraph[u][v] -= flow; 
                    rGraph[v][u] += flow; 
                } 
                max_flow += flow; 
            } 

            return result.Select(u => u<N?-1:u - N+1).ToArray(); 
        } 

        public bool BFS(long nodeCount, long s, long t, long[] prev) 
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
                    if (visited[v] == false && rGraph[u][v] > 0) 
                    { 
                        queue.Enqueue(v); 
                        visited[v] = true; 
                        prev[v] = u; 
                    } 
                } 
            } 
            return visited[t] == true; 
        }
    }
}
