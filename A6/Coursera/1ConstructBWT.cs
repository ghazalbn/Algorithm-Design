using System;
using System.Linq;

namespace Q1ConstructBWT
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = Console.ReadLine();
            string[] A = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
                A[i] = text.Substring(i) + text.Substring(0, i);
            Array.Sort(A);
            A = A.Select(s => s.Last().ToString()).ToArray();
            System.Console.WriteLine(string.Format("{0}", string.Join("", A)));
        }
    }
}
