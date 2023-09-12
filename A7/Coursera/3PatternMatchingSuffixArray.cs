using System;
using System.Collections.Generic;

namespace _7Q3PatternMatchingSuffixArray
{
    public static class FindPattern
    {
        public static Dictionary<char, int> chars;
        public static List<long> Find(char[] text, char[] pattern, List<long> indexes)
        {
            chars = new Dictionary<char, int>();
            chars['$'] = 0;
            chars['A'] = 1;
            chars['C'] = 2;
            chars['G'] = 3;
            chars['T'] = 4;

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
                    // if(!indexes.Contains(index))
                        indexes.Add(index);
                    index += (index + m < n)? m - (int)H[chars[text[index + m]]] : 1;
        
                }
        
                else
                    index += Math.Max(1, i - (int)H[chars[text[index + i]]]);
                
            }
            return indexes;
        }

        public static long[] Hash(char[] text, char[] pattern)
        {
            var H = new long[5];
            for (int i = 0; i < pattern.Length; i++)
                H[chars[pattern[i]]] = i;
            return H;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var text = Console.ReadLine().ToCharArray();
            var n = long.Parse(Console.ReadLine());
            var patterns = Console.ReadLine().Split();
            var indexes = new List<long>();
            foreach (var pattern in patterns)
                FindPattern.Find(text, pattern.ToCharArray(), indexes);
            System.Console.WriteLine(indexes.Count > 0? string.Join(" ", indexes): "-1");
        }
    }
}
