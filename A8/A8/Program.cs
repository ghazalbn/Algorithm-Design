using System;

namespace A8
{
    class Program
    {
        static void Main(string[] args)
        {
            // var s = new Q1Evaquating("");
            // var edges = new long[1][]; 
            // edges[0] = new long[]{1, 2, 5};
            // s.Solve(2, 1, edges);

            // var s = new Q2Airlines("");
            // var edges = new long[7][]; 
            // // edges[0] = new long[1]{1};
            // edges[0] = new long[]{0, 1, 0, 1, 0, 1};
            // edges[1] = new long[]{1, 0, 1, 1, 0, 0};
            // edges[2] = new long[]{1, 1, 0, 0, 1, 0};
            // edges[3] = new long[]{1, 1, 0, 0, 0, 0};
            // edges[4] = new long[]{1, 1, 0, 0, 1, 1};
            // edges[5] = new long[]{0, 0, 1, 1, 0, 1};
            // edges[6] = new long[]{1, 0, 0, 0, 1, 0};

            // s.Solve(7, 6, edges);

            var matrix = new long[2][];
            matrix[0] = new long[1]{0};
            matrix[1] = new long[1]{1001};
            var s = new Q3Stocks("");
            s.Solve(2, 1, matrix);
        }
    }
}
