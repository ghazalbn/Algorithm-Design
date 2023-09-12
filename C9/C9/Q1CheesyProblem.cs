using System;
using System.Linq;
using TestCommon;

namespace C9
{
    public class Q1CheesyProblem : Processor
    {
        public Q1CheesyProblem(string testDataName) : base(testDataName)
        {
            // ExcludeTestCaseRangeInclusive(1, 7);
        }

        public override string Process(string inStr)
        {
            var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var first = lines[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(d => long.Parse(d)).ToArray();
            long n = first[0];
            long m = first[1];
            long[] P = lines[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(d => long.Parse(d)).ToArray();
            long[] C = lines[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(d => long.Parse(d)).ToArray();
            return Solve(n, m, P, C).ToString();
        }
        
        public long Solve(long n, long m, long[] p, long[] c)
        {
            var bn = new BN(n, m, p, c);
            return bn.EdmondKarp(); 
        }
    }
}
