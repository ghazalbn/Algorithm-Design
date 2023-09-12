using System;

namespace C8
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "qjbbfv";
            long n = 6, m = 2;
            var s = new Q1SuperPalindromes("");
            long answer = s.Solve(n, m, text);
        }
    }
}
