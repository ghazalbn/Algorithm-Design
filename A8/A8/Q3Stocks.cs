using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A8
{
    public class Q3Stocks : Processor
    {
        long[][] graph;
        public Q3Stocks(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long stockCount, long pointCount, long[][] matrix)
        {
            int result = 0;

            graph = new long[stockCount][];
            graph = graph.Select(l => new long[stockCount]).ToArray();

            var prev = new long[stockCount];
            Array.Fill(prev, -1);

            BuildGraph(stockCount, pointCount, matrix);

            for (int u = 0; u < stockCount; u++)
            {
                bool[] visited = new bool[stockCount] ;
                result += DFS(u, visited, prev);
            }
            return result;
        }

        private void BuildGraph(long stockCount, long pointCount, long[][] matrix)
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

        public int DFS(long u, bool[] visited, long[] result)
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
