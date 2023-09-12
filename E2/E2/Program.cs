using System;

namespace E2
{
    class Program
    {
        static void Main(string[] args)
        {



            var s = new Q1Times("");
            // long[, ,] p = new long[,,]
            // {
            //     {
            //         {1, 1, 1}, 
            //         {1 ,1 ,-1},
            //         {1, 1, 1}
            //     },
            //     { 
            //         {1, 1, 1},
            //         {1, 1, -1}, 
            //         {1, 1, 1}
            //     },
            //     {
            //         {1, 1, 1}, 
            //         {1, 1, 1},
            //         {1, 1, 1} 
            //     }
            // };
            // long[, ,] c = new long[,,]
            // {
            //     {
            //         {1, -1, 1}, 
            //         {1 ,-1 ,1},
            //         {1, 1, -1}
            //     },
            //     { 
            //         {1, 1, 1},
            //         {1, -1, 1}, 
            //         {1, 1, 1}
            //     },
            //     {
            //         {1, -1, 1}, 
            //         {1, -1, -1},
            //         {1, 1, 1} 
            //     }
            // };

            // s.Solve(3, 3, 3, p, c);
            char[][] board = new char[4][];
            // for (int i = 0; i < 3; i++)
            // {
                board[0] = new char[]{'s', 'q', 'q', 'g'};
                board[1] = new char[]{'a', 'z', 'q', 'a'};

                board[2] = new char[]{'j', 'h', 'v', 'i'};
                board[3] = new char[]{'j', 'u', 'w', 'l'};

            // }
            s.Solve(board, new string[]{"sqzh", "oaq", "aqz", "iqjg", "hvwu", "hu"});
        }
    }
}
