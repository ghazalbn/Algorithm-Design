using System;
using System.Linq;

namespace Q4ConstructSuffixArray
{
    class Program
    {
        static int MOD = 1000000009;
        static int P = 31;
        static string text;
        static long[] H;
        static long[] Pow;
        static void Main(string[] args)
        {
            text = Console.ReadLine();

            H = BuildHash(text);
            Pow = Powers();

            var SuffixArray = Enumerable.Range(0, text.Length).ToArray();
            Array.Sort(SuffixArray, Compare);
            
            Console.WriteLine(string.Join(" ", SuffixArray.Select(i => (long)i)));
        }
        private static int Compare(int x, int y)
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
        public static long[] Powers()
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

        private static bool HashEqual(int i, int j, int mid)
        {
            long hash1 = (H[i + mid - 1] - (i<=0?0:H[i - 1]) + MOD) % MOD;
            long hash2 = (H[j + mid - 1] - (j<=0?0:H[j - 1]) + MOD) % MOD;
            if(j > i)
                hash1 = (hash1*Pow[j - i])%MOD;
            else
                hash2 = (hash2*Pow[i - j])%MOD;
            return hash1 == hash2;
        }
    }
}
