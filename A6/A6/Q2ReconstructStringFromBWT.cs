using System;
using TestCommon;
using System.Linq;
using System.Collections.Generic;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        char[] characters = new char[5]{'$', 'A', 'C', 'G', 'T'};
        int[] LF;
        Dictionary<char, int> count;
        Dictionary<char, int> SmallerCount;
        public Q2ReconstructStringFromBWT(string testDataName) 
        : base(testDataName) 
        {
            // ExcludeTestCaseRangeInclusive(31, 40);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public string Solve(string bwt)
        {
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
            return string.Join("", result).Remove(0, 1) + '$';

            // ba string javab nadad bayad char array mishod:)))
            // var result = "";
            // int i = 0;
            // while (bwt[i] != '$')
            // {
            //     result = bwt[i] + result;
            //     i = LF[i];
            // }


            // rahe aval
            // string[] A = new string[bwt.Length];
            // for (int i = 0; i < bwt.Length; i++)
            // {
            //     int j = 0;
            //     A = A.Select(s => bwt[j++] + s).OrderBy(s => s[0]).ToArray();
            // }
            // // return A[0].Replace("$", "") + '$';
            // return A.Where(s => s.EndsWith('$')).ElementAt(0);
        }

        private void ComputeLF(string bwt)
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
                count[bwt[i]] = count.GetValueOrDefault(bwt[i], 0) + 1;
                LF[i] = SmallerCount[bwt[i]] + count[bwt[i]] - 1;
            }

            // rahe 3 
            // if(bwt[i] == '$')
            // {
            //     SmallerCount['A'] = SmallerCount.GetValueOrDefault('A', 0) + 1;
            //     SmallerCount['C'] = SmallerCount.GetValueOrDefault('C', 0) + 1;
            //     SmallerCount['G'] = SmallerCount.GetValueOrDefault('G', 0) + 1;
            //     SmallerCount['T'] = SmallerCount.GetValueOrDefault('T', 0) + 1;
            // }
            // else if(bwt[i] == 'A')
            // {
            //     SmallerCount['C'] = SmallerCount.GetValueOrDefault('C', 0) + 1;
            //     SmallerCount['G'] = SmallerCount.GetValueOrDefault('G', 0) + 1;
            //     SmallerCount['T'] = SmallerCount.GetValueOrDefault('T', 0) + 1;
            // }
            // else if(bwt[i] == 'C')
            // {
            //     SmallerCount['G'] = SmallerCount.GetValueOrDefault('G', 0) + 1;
            //     SmallerCount['T'] = SmallerCount.GetValueOrDefault('T', 0) + 1;
            // }
            // else if(bwt[i] == 'G')
            // {
            //     SmallerCount['T'] = SmallerCount.GetValueOrDefault('T', 0) + 1;
            // }

            // rahe dovom
            // SmallerCount['$'] = 0;
            // SmallerCount['A'] = 1;
            // SmallerCount['C'] = bwt.Count(c => c == 'A') + 1;
            // SmallerCount['G'] = bwt.Count(c => c == 'C') + SmallerCount['C'];
            // SmallerCount['T'] = bwt.Count(c => c == 'G') + SmallerCount['G'];

            // rahe aval
            // var bwtsorted = String.Concat(bwt.OrderBy(c => c));
            // SmallerCount[bwtsorted[0]] = 0;
            // int j = 1;
            // for (int i = 1; i < bwt.Length; i++)
            // {
            //     if(bwtsorted[i] != bwtsorted[i - 1])
            //     {
            //         SmallerCount[bwtsorted[i]] =  SmallerCount[bwtsorted[i - 1]] + j;
            //         j = 1;
            //     }
            //     else j++;
            // }
        }
    }
}
