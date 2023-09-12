using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3MatchingAgainCompressedString
{
    class Program
    {
        static char[] characters = new char[5]{'$', 'A', 'C', 'G', 'T'};
        // int[] LF;
        static Dictionary<char, int> SmallerCount;
        static Dictionary<char, int[]> Count;
        static char[] lastcolumn;
    
        static void Main(string[] args)
        {
            SmallerCount = new Dictionary<char, int>();
            Count = new Dictionary<char, int[]>();
            lastcolumn = Console.ReadLine().ToCharArray();
            var n = long.Parse(Console.ReadLine());
            var Patterns = Console.ReadLine().Split();
            PreCompute();
            
            // var result = new long[n];
            for (int i = 0; i < n; i++)
            {
                System.Console.Write(CountPattern(Patterns[i].ToList()) + " ");
            }
        }
        private static long CountPattern(List<char> Pattern)
        {
            var symbol = Pattern.Last();
            Pattern.RemoveAt(Pattern.Count-1);
            var top = SmallerCount[symbol];
            var bottom = SmallerCount[symbol] + Count[symbol][lastcolumn.Length] - 1; 
            while (top <= bottom)
            {
                if(Pattern.Count != 0)
                {
                    symbol = Pattern.Last();
                    Pattern.RemoveAt(Pattern.Count-1);
                    // if (lastcolumn.Substring(top, bottom - top + 1).Contains(symbol))
                    // {
                        top = SmallerCount[symbol] + Count[symbol][top - 1];
                        bottom = SmallerCount[symbol] + Count[symbol][bottom] - 1;
                    // }
                    // else
                    //     return 0;
                }
                else
                    return bottom - top + 1;
            }
            return 0;

        }

        private static void PreCompute()
        {
            characters.ToList().ForEach(c => Count[c] = new int[lastcolumn.Length + 1]);

            SmallerCount['$'] = 0;
            for (int i = 0; i < lastcolumn.Length; i++)
            {
               foreach (var chr in characters)
                {
                    if(chr > lastcolumn[i])
                    {
                        if(SmallerCount.ContainsKey(chr))
                            SmallerCount[chr]++;
                        else
                            SmallerCount[chr] = 1;
                    }
                }
                if(i != 0)
                    characters.ToList().ForEach(c => Count[c][i] = Count[c][i - 1]);
                Count[lastcolumn[i]][i]++;

            }
            characters.ToList().ForEach(c => Count[c][lastcolumn.Length] = Count[c][lastcolumn.Length - 1]);

        }
    }
}
