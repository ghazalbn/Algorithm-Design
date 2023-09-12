using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class Q2CleaningApartment : Processor
    {
        int K;
        List<string> Clauses;

        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public String[] Solve(int V, int E, long[,] matrix)
        {
            K = V;
            int varCount = K*V;

            Clauses = new List<string>{""};
            var Vertex = Enumerable.Range(1, V);

            long[,] adj = BuildAdj(V, matrix);

            foreach(var v in Vertex)
            {
                // Each node v must appear in the path
                var literals = Vertex.Select(k => Varnam(v, k)).ToList();
                // ExactlyOneOF(literals);
                AtLeastOneOF(literals);

                // Every position k on the path must be occupied
                literals = Vertex.Select(k => Varnam(k, v)).ToList();
                // ExactlyOneOF(literals);
                AtLeastOneOF(literals);
            }
            foreach (var v1 in Vertex)
            {
                for (int v2 = v1 + 1; v2 <= V; v2++)
                {
                    foreach (var k in Vertex)
                    {
                        // No two nodes v1 and v2 occupy the same position in the path
                        var literals = new List<long>{Varnam(v1, k), Varnam(v2,k)};
                        AtMostOneOF(literals);
                        
                        // Nonadjacent nodes v1 and v2 cannot be adjacent in the path
                        // if (k != V && !Contains(matrix, (v1, v2).ToTuple()))
                        if (k != V && adj[v1,v2] == 0)

                        {
                            literals = new List<long>{Varnam(v1, k), Varnam(v2,k + 1)};
                            AtMostOneOF(literals);

                            literals = new List<long>{Varnam(v2, k), Varnam(v1,k + 1)};
                            AtMostOneOF(literals);
                        }
                    }
                }
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

        public long Varnam(long v, int k)
        {
            // return 10*v + k;
            return K * v + k - K;
        }

        private long[, ] BuildAdj(int V, long[,] matrix)
        {
            var adj = new long[V + 1, V + 1];
            for (int i = 0; i < matrix.Length/2; i++)
            {
                var min = Math.Min(matrix[i, 0], matrix[i, 1]);
                var max = Math.Max(matrix[i, 0], matrix[i, 1]);

                adj[min, max] = 1; 
            }
            return adj;
        }

        public static bool Contains(long[,] array, Tuple<int, int> value)
        {
            for (int i = 0; i < array.Length/2; i++)
            {
                if((array[i, 0] == value.Item1 && array[i, 1] == value.Item2) 
                    || array[i, 0] == value.Item2 && array[i, 1] == value.Item1)
                    return true;
            }
            return false;
        }

    }
}
