using System;
using System.Collections.Generic;

namespace A7
{
    public static class FindPattern
    {
        public static List<long> Find( string text, string pattern, List<long> indexes)
        {
            var H = Hash(text, pattern);

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
                    if(!indexes.Contains(index))
                        indexes.Add(index);
                    index += (index + m < n)? m - (int)H[text[index + m]] : 1;
        
                }
        
                else
                    index += Math.Max(1, i - (int)H[text[index + i]]);
                
            }
            return indexes;
        }

        public static long[] Hash(string text, string pattern)
        {
            var H = new long[90];
            for (int i = 0; i < pattern.Length; i++)
                H[(int) pattern[i]] = i;
            return H;
        }
    }
}