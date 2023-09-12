using System;
using System.Collections.Generic;
using System.Linq;

namespace _8_1
{
    class Program
    {
        static long[,] graph;
        static long[,] rGraph;
        static void Main(string[] args)
        {
            var line = Array.ConvertAll(Console.ReadLine().Split(), long.Parse); 
            var nodeCount = line[0];
            var edgeCount = line[1];
            graph = new long[nodeCount, nodeCount];
            rGraph = new long[nodeCount, nodeCount]; 
            var edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
                edges[i] = Array.ConvertAll(Console.ReadLine().Split(), long.Parse); 
            edges.ToList().ForEach(e => graph[e[0] - 1, e[1] - 1] += e[2]);
            Console.WriteLine(EdmondKarp(nodeCount, 0, nodeCount - 1));
        }

        public static long EdmondKarp(long nodeCount, long s, long t) 
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

        public static bool bfs(long nodeCount, long s, long t, long[] prev) 
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
