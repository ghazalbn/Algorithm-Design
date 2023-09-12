using System;
using System.Collections.Generic;

namespace Q2ReconstructStringFromBWT
{
    class Program
    {
        static char[] characters = new char[5]{'$', 'A', 'C', 'G', 'T'};
        static int[] LF;
        static Dictionary<char, int> count;
        static Dictionary<char, int> SmallerCount;
        static void Main(string[] args)
        {
            var bwt = Console.ReadLine();
            count = new Dictionary<char, int>();
            SmallerCount = new Dictionary<char, int>();
            LF = new int[bwt.Length];

            ComputeLF(bwt);

            var result = new char[bwt.Length];
            int j = 0;
            for (int i = 1; i < bwt.Length; i++)
            {
                result[bwt.Length - i] = bwt[j];
                j = LF[j];
            }
            Console.WriteLine(string.Join("", result).Remove(0, 1) + '$');
        }
        private static void ComputeLF(string bwt)
        {
            SmallerCount['$'] = 0;
            for (int i = 0; i < bwt.Length; i++)
            {
                foreach (var chr in characters)
                {
                    if(chr > bwt[i])
                    {
                        if(SmallerCount.ContainsKey(chr))
                            SmallerCount[chr]++;
                        else
                            SmallerCount[chr] = 1;
                    }
                }
            }

            for (int i = 0; i < bwt.Length; i++)
            {
                count[bwt[i]] = count.ContainsKey(bwt[i])? count[bwt[i]] + 1: 1;
                LF[i] = SmallerCount[bwt[i]] + count[bwt[i]] - 1;
            }
        }
    }
}
