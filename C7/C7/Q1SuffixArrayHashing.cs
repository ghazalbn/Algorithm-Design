using System;
using TestCommon;
using System.Linq;

namespace C7
{
    public class Q1SuffixArrayHashing : Processor
    {
        const int MOD = 1_000_000_000 + 9;
        const int P = 31;
        string text;
        long[] H;
        long[] Pow;

        public Q1SuffixArrayHashing(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => TestTools.Process(inStr, (Func<String, long[]>)Solve);

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

        public long[] Solve(string t)
        {
            this.text = t + '$';
            H = BuildHash(text);
            Pow = Powers();
            long[] suffixes = SuffixArray();
            return suffixes;
        }

        public long[] SuffixArray()
        {
            var suffixes = new long[text.Length];
            for (int i = 0; i < text.Length; i++)
                suffixes[i] = i;
            Array.Sort(suffixes, Compare);
            return suffixes;
        }
        private int Compare(long a, long b)
        {
            var i = (int)a;
            var j = (int)b;
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

        private int Partition(long[] arr, int start, int end)
        {
            long temp;
            long p = arr[end];
            int i = start - 1;
        
            for (int j = start; j <= end - 1; j++)
            {
                if (arr[j] <= p)
                {
                    i++;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        
            temp = arr[i + 1];
            arr[i + 1] = arr[end];
            arr[end] = temp;
            return i + 1;
        }

        public static long Hash(int i, string text) 
        {
            var s = text.Substring(0, i + 1);
            double value = 0;
            double power = 1;
            foreach (char c in s) {
                value = (value + (c - 'a' + 1) * power) % MOD;
                power = (power * P) % MOD;
            }
            return (long)value;
        }
    }
}
