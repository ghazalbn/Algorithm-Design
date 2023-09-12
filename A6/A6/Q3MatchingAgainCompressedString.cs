using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        char[] characters = new char[5]{'$', 'A', 'C', 'G', 'T'};
        // int[] LF;
        Dictionary<char, int> SmallerCount;
        Dictionary<char, int[]> Count;
        string lastcolumn;
        public Q3MatchingAgainCompressedString(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        /// <summary>
        /// Implement BetterBWMatching algorithm
        /// </summary>
        /// <param name="text"> A string lastcolumn(Text) </param>
        /// <param name="n"> Number of patterns </param>
        /// <param name="patterns"> Collection of n strings Patterns </param>
        /// <returns> A list of integers, where the i-th integer corresponds
        /// to the number of substring matches of the i-th member of Patterns
        /// in Text. </returns>
        public long[] Solve(string text, long n, String[] patterns)
        {
            SmallerCount = new Dictionary<char, int>();
            Count = new Dictionary<char, int[]>();
            // LF = new int[lastcolumn.Length];
            lastcolumn = text;
            PreCompute();
            
            var result = new long[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = CountPattern(patterns[i]);
            }
            return result;
        }

        private long CountPattern(string Pattern)
        {
            var symbol = Pattern.Last();
            Pattern = Pattern.Remove(Pattern.Length - 1);
            var top = SmallerCount[symbol];
            var bottom = SmallerCount[symbol] + Count[symbol][lastcolumn.Length] - 1; 
            while (top <= bottom)
            {
                if(Pattern != "")
                {
                    symbol = Pattern.Last();
                    Pattern = Pattern.Remove(Pattern.Length - 1);
                    if (lastcolumn.Substring(top, bottom - top + 1).Contains(symbol))
                    {
                        top = SmallerCount[symbol] + Count[symbol][top - 1];
                        bottom = SmallerCount[symbol] + Count[symbol][bottom] - 1;
                    }
                    else
                        return 0;
                }
                else
                    return bottom - top + 1;
            }
            return 0;

        }

        private void PreCompute()
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
