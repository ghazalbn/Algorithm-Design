using System;
using System.Linq;

namespace _8_3
{
    class Program
    {
        static long[][] graph;
        static void Main(string[] args)
        {
            int result = 0;
            var line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            var stockCount = line[0];
            var pointCount = line[1];
            var matrix = new long[stockCount][];
            for (int i = 0; i < stockCount; i++)
                matrix[i] = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            graph = new long[stockCount][];
            graph = graph.Select(l => new long[stockCount]).ToArray();

            var prev = new long[stockCount];
            prev = prev.Select(p => (long)-1).ToArray();

            BuildGraph(stockCount, pointCount, matrix);

            for (int u = 0; u < stockCount; u++)
            {
                bool[] visited = new bool[stockCount] ;
                result += DFS(u, visited, prev);
            }
            Console.WriteLine(string.Join(" ", result));
        }

        private static void BuildGraph(long stockCount, long pointCount, long[][] matrix)
        {
            for (int i = 0; i < stockCount; i++)
                for (int j = 0; j < stockCount; j++)
                {
                    graph[i][j] = 1;
                    for (int k = 0; k < pointCount; k++)
                        if(matrix[i][k] <= matrix[j][k]) 
                        {
                            graph[i][j] = 0;
                            break;
                        }  
                }
        }

        public static int DFS(long u, bool[] visited, long[] result)
        {

            for (int v = 0; v < graph.Length; v++)
            {
                if (graph[v][u] == 1 && !visited[v])
                {
                    visited[v] = true;
                    if (result[v] < 0 || DFS(result[v], visited, result) == 0)
                    {
                        result[v] = u;
                        return 0;
                    }
                }
            }
            return 1;
        }
    }
}
