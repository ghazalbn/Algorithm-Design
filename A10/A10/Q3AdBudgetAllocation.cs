using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class Q3AdBudgetAllocation : Processor
    {
        List<string> Clauses;
        public Q3AdBudgetAllocation(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            Clauses = new List<string>{""};
            var Variables = Enumerable.Range(1, (int)varCount);

            for (int j = 0; j < eqCount; j++)
            {
                var variables = new int[3]{0, 0, 0};
                var line = A[j];
                // binary 000 ta 111
                for (int i = 0; i < 8; i++)
                {
                    if(!IsSatisfied(variables, line, b[j]))   
                    {
                        var literals = new List<long>();
                        int c = 0;
                        foreach (int k in Variables)
                            if(line[k - 1] != 0)
                                literals.Add(variables[c++] == 1? -k:k);
                        AtLeastOneOF(literals);
                    }

                    variables[0] = 1 - variables[0];
                    if(i % 2 == 1)
                    {
                        variables[1] = 1 - variables[1];
                        if(varCount == 1) break;
                    }
                    if(i % 4 == 3)
                    {
                        variables[2] = 1 - variables[2];
                        if(varCount == 2) break;
                    }
                }
            }
            var clauseCount = Clauses.Count - 1;
            Clauses[0] = $"{clauseCount} {varCount}";

            return Clauses.ToArray();
        }

        private bool IsSatisfied(int[] variables, long[] line, long b)
        {
            long sum = 0, i = 0;
            foreach (var l in line)
            {
                if(l != 0)
                    sum += l * variables[i++];
            }
            return sum <= b;
        }


        private void XNor(List<long> literals)
        {
            Clauses.Add($"-{literals[0]} {literals[1]}");
            Clauses.Add($"-{literals[1]} {literals[0]}");
        }

        public void AtLeastOneOF(List<long> literals)
        {
            Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
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
    }
}
