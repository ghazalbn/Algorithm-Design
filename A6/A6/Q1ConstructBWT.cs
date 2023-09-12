using System;
using TestCommon;
using System.Linq;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public string Solve(string text)
        {
            string[] A = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
                A[i] = text.Substring(i) + text.Substring(0, i);
            Array.Sort(A);
            A = A.Select(s => s.Last().ToString()).ToArray();
            return string.Format("{0}", string.Join("", A));
        }
    }
}
