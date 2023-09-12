using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace C6
{
    public class Q1BoyerMoore : Processor
    {
        public Q1BoyerMoore(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, string pattern)
        {
            return FindPattern(text, pattern).ToArray();
        }
        
        static List<long> FindPattern( string text, string pattern)
        {
            var badchar = badCharHeuristic(text, pattern);

            var indexes = new List<long>();
            int n = text.Length;
            int m = pattern.Length;

            if(m > n)
                return new List<long>(){-1};
        
            int index = 0;
            while(index <= (n - m))
            {
                int i = m - 1;
                while(i >= 0 && pattern[i] == text[index + i])
                    i--;
        
                if (i < 0)
                {
                    // Console.WriteLine(index + " ");
                    indexes.Add(index);
                    index += (index + m < n)? m - (int)badchar[text[index + m]] : 1;
        
                }
        
                else
                    index += Math.Max(1, i - (int)badchar[text[index + i]]);
                
            }
            return indexes;
        }

        static long[] badCharHeuristic( string text, string pattern)
        {
            var badchar = new long[90];
            for (int i = 0; i < pattern.Length; i++)
                badchar[(int) pattern[i]] = i;
            // System.Console.WriteLine((int)'Z');
            return badchar;
        }
    }
}
