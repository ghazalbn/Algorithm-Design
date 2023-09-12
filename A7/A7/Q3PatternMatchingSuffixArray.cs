using System;
// using FindPattern;
using TestCommon;
using System.Collections.Generic;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, long n, string[] patterns)
        {
            var indexes = new List<long>();
            foreach (var pattern in patterns)
                FindPattern.Find(text, pattern, indexes);
            return indexes.Count > 0? indexes.ToArray(): new long[]{-1};
        }
    }
}
