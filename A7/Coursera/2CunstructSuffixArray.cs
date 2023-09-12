using System;
using System.Collections.Generic;

namespace _7Q2CunstructSuffixArray
{
    class Program
    {
        static char[] text;
        static Dictionary<char, int> chars;
        static void Main(string[] args)
        {
            chars = new Dictionary<char, int>();
            chars['$'] = 0;
            chars['A'] = 1;
            chars['C'] = 2;
            chars['G'] = 3;
            chars['T'] = 4;

            text = Console.ReadLine().ToCharArray();
            long[] suffixes = SuffixArray();
            System.Console.WriteLine(string.Join(" ", suffixes));
        }
        public static long[] SuffixArray() 
        {
            var suffixes = new long[text.Length];
            CountSort(suffixes);

            var SortClass = new int[text.Length];
            Class(suffixes, SortClass);

            for (int i = 1; i <= text.Length; i *= 2) 
            {
                suffixes = DoubleSort(i, suffixes, SortClass);
                SortClass = UpdateClass(suffixes, SortClass, i);
            }

            return suffixes;
        }
        
        // public static int GetNum(char x) 
        // {
        //     switch (x) 
        //     {
        //         case '$':
        //             return 0;
        //         case 'A':
        //             return 1;
        //         case 'C':
        //             return 2;
        //         case 'G':
        //             return 3;
        //         case 'T':
        //             return 4;
        //     }
        //     return -1;
        // }

        public static void CountSort(long[] suffixes) 
        {
            var Count = new int[5];
            for (int i = 0; i < text.Length; i++) 
                Count[chars[text[i]]]++;
            
            for (int i = 1; i < 5; i++) 
                Count[i] += Count[i - 1];

            for (int i = text.Length - 1; i >= 0; i--) 
                suffixes[--Count[chars[text[i]]]] = i;
        }
        public static long[] DoubleSort(int j, long[] suffixes, int[] SortClass) 
        {
            var UpdatedSuffixes = new long[text.Length];
            var characters = new int[text.Length];

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

        public static void Class(long[] suffixes, int[] SortClass) 
        {
            int count = 0;
            SortClass[suffixes[0]] = count;
            for (int i = 1; i < suffixes.Length; i++) 
            {
                if (text[suffixes[i]] == text[suffixes[i - 1]])
                    SortClass[suffixes[i]] = count;
                else
                    SortClass[suffixes[i]] = ++count;
            }
        }

        public static int[] UpdateClass(long[] suffixes, int[] SortClass, int j) 
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

    }
}
