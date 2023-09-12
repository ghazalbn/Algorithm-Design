using System;

namespace A10
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Q3AdBudgetAllocation("");
            var V = 3;
            var E = 2;
            var matrix = new long[3][];
            // for (int i = 0; i < 3; i++)
            // {
            //     matrix[i] = new long[2];   
            // }
            matrix[0] = new long[]{-1, -1};
            matrix[1] = new long[]{0, 1};
            matrix[2] = new long[]{1, 0};

            var b = new long[]{-1, 2, 2};
            var r = s.Solve(V, E, matrix, b);
            Console.WriteLine(matrix.ToString());
        }
    }
}
