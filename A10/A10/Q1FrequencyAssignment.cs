using System;
using TestCommon;
using System.Linq;
using System.Collections.Generic;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {
        int K = 3;
        List<string> Clauses;
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) 
        { 
            // ExcludeTestCaseRangeInclusive(2, 32);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            // int clauseCount = 4*V + 2*K*E; //agar hamash exatcly one of bud
            int clauseCount = 4*V + K*E;    //4 bekhatere (2 az 3) + 1
            int varCount = K*V;

            Clauses = new List<string>{$"{clauseCount} {varCount}"};
            var Digits = Enumerable.Range(1, K);
            var Vertex = Enumerable.Range(1, V);

            foreach(var v in Vertex)
            {
                var literals = Digits.Select(k => Varnam(v, k)).ToList();
                ExactlyOneOF(literals);
            }
            for (int i=0; i < E; i++)
            {
                (var v1, var v2) = (matrix[i, 0], matrix[i, 1]);
                foreach (var k in Digits)
                {
                    var literals = new List<long>{Varnam(v1, k), Varnam(v2,k)};
                    AtMostOneOF(literals);
                }
            }
            return Clauses.ToArray();
        }

        public void ExactlyOneOF(List<long> literals)
        {
            Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
            AtMostOneOF(literals);
        }

        public void AtMostOneOF(List<long> literals)
        {
            for (int i = 0; i < literals.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Clauses.Add($"-{literals[j]} -{literals[i]} 0");
                }
            }
        }

        public long Varnam(long v, int k)
        {
            // return 10*v + k;
            return K * v + k - K;
        }
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }
}
