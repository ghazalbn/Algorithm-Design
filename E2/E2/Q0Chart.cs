using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace E2
{
    public class Q0Chart : Processor
    {
        int I, J, K;
        List<string> Clauses;
        public Q0Chart(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            E2Processors.Q0Processor(inStr, Solve);

        public string[] Solve(int professorsCount,
                              int classCount,
                              int timeCount,
                              long[,,] professorsCanTeach,
                              long[,,] classCanBeOccupied)
        {
            (I, J, K) = (professorsCount, classCount, timeCount);
            int varCount = I*J*K;
            Clauses = new List<string>{""};

            var Profs = Enumerable.Range(1, I);
            var Classes = Enumerable.Range(1, J);
            var Time = Enumerable.Range(1, K);

            
            foreach (var i in Profs)
            {
                foreach (var j in Classes)
                {
                    foreach (var k in Time)
                    {
                        // sharte 1 va 2
                        if(professorsCanTeach[i - 1, j - 1, k - 1] == -1 || classCanBeOccupied[i - 1, j - 1, k - 1] == -1)
                            Clauses.Add($"-{Varnam(i, j, k)} 0");
                    }
                }
            }

            // sharte 3
            foreach (var i in Profs)
            {
                foreach (var k in Time)
                {
                    var literals = Classes.Select(j => Varnam(i, j, k)).ToList();
                    AtMostOneOF(literals);
                }
            }

            // sharte 4
            foreach (var j in Classes)
            {
                foreach (var k in Time)
                {
                    var literals = Profs.Select(i => Varnam(i, j, k)).ToList();
                    AtMostOneOF(literals);
                }
            }

            // sharte 5
            foreach (var i in Profs)
            {
                var literals = new List<long>();
                foreach (var j in Classes)
                {
                    literals.AddRange(Time.Select(k => Varnam(i, j, k)).ToList());
                }
                AtLeastOneOF(literals);
            }

            // sharte 6
            foreach (var j in Classes)
            {
                var literals = new List<long>();
                foreach (var i in Profs)
                {
                    literals.AddRange(Time.Select(k => Varnam(i, j, k)).ToList());
                }
                AtLeastOneOF(literals);
            }

            var clauseCount = Clauses.Count - 1;
            Clauses[0] = $"{clauseCount} {varCount}";
            return Clauses.ToArray();
        }


        public void ExactlyOneOF(List<long> literals)
        {
            Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
            AtMostOneOF(literals);
        }

        public void AtLeastOneOF(List<long> literals)
        {
            Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
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

        public long Varnam(int i, int j, int k)
        {
            // return 100*i + 10*j + k;
            return (i - 1) * J + (j - 1) * K + k;
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }

}