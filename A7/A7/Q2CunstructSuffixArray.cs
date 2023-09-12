using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        // rahe ghabli
        const int MOD = 1_000_000_000 + 9;
        const int P = 31;
        long[] H;
        long[] Pow;
        // --------------------------------------

        public char[] txt;
        public Dictionary<char, int> chars;
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public virtual long[] Solve(string text)
        {
            chars = new Dictionary<char, int>();
            chars['$'] = 0;
            chars['A'] = 1;
            chars['C'] = 2;
            chars['G'] = 3;
            chars['T'] = 4;

            txt = text.ToCharArray();
            return SuffixArray();

            // rahe 1
            // this.txt = txt;
            // H = BuildHash(txt);
            // Pow = Powers();
            // long[] suffixes = SuffixArray();
            // return suffixes;
        }

        public long[] SuffixArray() 
        {
            var suffixes = new long[txt.Length];
            CountSort(suffixes);

            var SortClass = new int[txt.Length];
            Class(suffixes, SortClass);

            for (int i = 1; i <= txt.Length; i *= 2) 
            {
                suffixes = DoubleSort(i, suffixes, SortClass);
                SortClass = UpdateClass(suffixes, SortClass, i);
            }

            return suffixes;
        }
        public void CountSort(long[] suffixes) 
        {
            var Count = new int[5];
            for (int i = 0; i < txt.Length; i++) 
                Count[chars[txt[i]]]++;
            
            for (int i = 1; i < 5; i++) 
                Count[i] += Count[i - 1];

            for (int i = txt.Length - 1; i >= 0; i--) 
                suffixes[--Count[chars[txt[i]]]] = i;
        }
        public long[] DoubleSort(int j, long[] suffixes, int[] SortClass) 
        {
            var UpdatedSuffixes = new long[txt.Length];
            var characters = new int[txt.Length];

            for (int i = 0; i < suffixes.Length; i++) 
                characters[SortClass[i]]++;
            
            for (int i = 1; i < characters.Length; i++) 
                characters[i] += characters[i - 1];
            
            for (int i = suffixes.Length - 1; i >= 0; i--) 
            {
                var s = (suffixes[i] - j + suffixes.Length) % suffixes.Length;
                UpdatedSuffixes[--characters[SortClass[s]]] = s;
            }
            return UpdatedSuffixes;
        }

        public void Class(long[] suffixes, int[] SortClass) 
        {
            int count = 0;
            SortClass[suffixes[0]] = count;
            for (int i = 1; i < suffixes.Length; i++) 
            {
                if (txt[suffixes[i]] == txt[suffixes[i - 1]])
                    SortClass[suffixes[i]] = count;
                else
                    SortClass[suffixes[i]] = ++count;
            }
        }

        public int[] UpdateClass(long[] suffixes, int[] SortClass, int j) 
        {
            var UpdatedSortClass = new int[suffixes.Length];
            int count = 0;
            UpdatedSortClass[suffixes[0]] = count;
            for (int i = 1; i < suffixes.Length; i++) 
            {
                var current = suffixes[i];
                var current_mid = (suffixes[i] + j) % suffixes.Length;
                var prev = suffixes[i - 1];
                var prev_mid = (suffixes[i - 1] + j) % suffixes.Length;
                if (SortClass[current]!= SortClass[prev] || SortClass[current_mid] != SortClass[prev_mid]) 
                    UpdatedSortClass[suffixes[i]] = ++count;
                else 
                    UpdatedSortClass[suffixes[i]] = count;
            }
            return UpdatedSortClass;
        }

        // -----------------------------------------------------------
        // rahe ghabli
        public long[] BuildHash(string txt)
        {
            var H = new long[txt.Length];
            long value = 0;
            long power = 1;
            for (int i = 0; i < txt.Length; i++)
            { 
                value = (value + (txt[i] - 'a' + 1) * power) % MOD;
                power = (power * P) % MOD;
                H[i] = value;
            }
            return H;
        }
        public long[] Powers()
        {
            var n = txt.Length;
            var pow = new long[n];
            long value = 1;
            for (int i = 1; i < n; i++)
            {
                value = (value*P)%MOD;
                pow[i] = value;
            }
            return pow;
        }
        // public long[] SuffixArray()
        // {
        //     var suffixes = new long[txt.Length];
        //     for (int i = 0; i < txt.Length; i++)
        //         suffixes[i] = i;
        //     Array.Sort(suffixes, Compare);
        //     return suffixes;
        // }
        private int Compare(long a, long b)
        {
            var i = (int)a;
            var j = (int)b;
            var min = txt.Length - Math.Max(i, j);
            if(txt[j] != txt[i])
                return txt[i].CompareTo(txt[j]);
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
            return txt[i + start].CompareTo(txt[j + start]);
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

    }
}
