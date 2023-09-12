using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        const int MOD = 1_000_000_000 + 9;
        const int P = 31;
        string text;
        long[] H;
        long[] Pow;
        // long[] SuffixArray;
        public Q4ConstructSuffixArray(string testDataName) 
        : base(testDataName) { }
        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public long[] Solve(string text)
        {
            this.text = text;

            H = BuildHash(text);
            Pow = Powers();

            var SuffixArray = Enumerable.Range(0, text.Length).ToArray();
            Array.Sort(SuffixArray, Compare);
            
            return SuffixArray.Select(i => (long)i).ToArray();

            // return SuffixArray.OrderBy(i => text.Substring(i), Compar).Select(i => (long)i).ToArray();

            // rahe aval
            // var A = new int[text.Length][];

            // for (int i = 0; i < text.Length; i++)
            // {
            //     A[i] = Enumerable.Range(i, text.Length - i).Concat(Enumerable.Range(0, i)).ToArray();
                
            // }
            // Array.Sort(A, Compare);
            // A = A.OrderBy(i => text[i[0]]).ToArray();
            // return A.Select(s => (long)s[0]).ToArray();
        }

        private int Compare(int x, int y)
        {
            // text.Substring(x).CompareTo(text.Substring(y));

            var i = (int)x;
            var j = (int)y;
            var min = text.Length - Math.Max(i, j);
            if(text[j] != text[i])
                return text[i].CompareTo(text[j]);
            if(HashEqual(i, j, min))
                return j.CompareTo(i);

            int start = 1, end = min;
            while(start + 1 < end)
            {
                int mid = (start + end)/2;
                if (HashEqual(i, j, mid))
                    start = mid;
                else
                    end = mid;
            }
            return text[i + start].CompareTo(text[j + start]);
        }
        
        public static long[] BuildHash(string text)
        {
            var H = new long[text.Length];
            long value = 0;
            long power = 1;
            for (int i = 0; i < text.Length; i++)
            { 
                value = (value + (text[i] - 'a' + 1) * power) % MOD;
                power = (power * P) % MOD;
                H[i] = value;
            }
            return H;
        }
        public long[] Powers()
        {
            var n = text.Length;
            var pow = new long[n];
            long value = 1;
            for (int i = 1; i < n; i++)
            {
                value = (value*P)%MOD;
                pow[i] = value;
            }
            return pow;
        }

        private bool HashEqual(int i, int j, int mid)
        {
            long hash1 = (H[i + mid - 1] - (i<=0?0:H[i - 1]) + MOD) % MOD;
            long hash2 = (H[j + mid - 1] - (j<=0?0:H[j - 1]) + MOD) % MOD;
            if(j > i)
                hash1 = (hash1*Pow[j - i])%MOD;
            else
                hash2 = (hash2*Pow[i - j])%MOD;
            return hash1 == hash2;
        }

        // private int Compare(int[] x, int[] y)
        // {
        //     return text.Substring(x[0]).CompareTo(text.Substring(y[0]));
        // }
    }
}
