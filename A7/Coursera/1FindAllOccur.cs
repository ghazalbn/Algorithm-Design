using System;
using System.Collections.Generic;
using System.Linq;

namespace _7Q1FindAllOccur
{
    // public static class FindPattern
    // {
    //     public static List<long> Find(char[] text, char[] pattern, List<long> indexes)
    //     {
    //         var H = Hash(text, pattern);

    //         int n = text.Length;
    //         int m = pattern.Length;

    //         if(m > n)
    //             return new List<long>(){-1};
        
    //         int index = 0;
    //         while(index <= (n - m))
    //         {
    //             int i = m - 1;
    //             while(i >= 0 && pattern[i] == text[index + i])
    //                 i--;
        
    //             if (i < 0)
    //             {
    //                 if(!indexes.Contains(index))
    //                     indexes.Add(index);
    //                 index += (index + m < n)? m - (int)H[text[index + m]] : 1;
        
    //             }
        
    //             else
    //                 index += Math.Max(1, i - (int)H[text[index + i]]);
                
    //         }
    //         return indexes;
    //     }

    //     public static long[] Hash(char[] text, char[] pattern)
    //     {
    //         var H = new long[90];
    //         for (int i = 0; i < pattern.Length; i++)
    //             H[(int) pattern[i]] = i;
    //         return H;
    //     }
    // }
    class Program
    {
        static void Main(string[] args)
        {
            var pattern = Console.ReadLine().ToCharArray();
            var text = Console.ReadLine().ToCharArray();

            Char[] patternstr = pattern.Concat(new char[]{'$'}.Concat(text)).ToArray();
            var nums = Prefix(patternstr);
            for (int i = pattern.Length + 1; i < nums.Length; i++) 
            {
                if (nums[i] == pattern.Length)
                    Console.Write((i - 2 * pattern.Length) + " ");
            }
            
            // System.Console.WriteLine
            // (string.Join(" ", FindPattern.Find(text, pattern, new List<long>())));
        }
        public static int[] Prefix(char[] patternstr) 
        {
            var nums = new int[patternstr.Length];
            int border = 0;
            for (int i = 1; i < patternstr.Length; i++) 
            {
                while (border > 0 && patternstr[i] != patternstr[border]) 
                    border = nums[border - 1];
                if (patternstr[i] == patternstr[border]) 
                {
                    border++;
                    nums[i] = border;
                }
                if (border == 0)
                    nums[i] = 0;
            }
            return nums;
        }
    }
}
