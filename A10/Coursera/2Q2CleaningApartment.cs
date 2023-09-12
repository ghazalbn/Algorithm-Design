using System;
using System.Collections.Generic;
using System.Linq;

namespace Q2CleaningApartment
{
    class Program
    {
        public static List<string> Clauses;
        static int K;
        static void Main(string[] args)
        {
            var query = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            var V = query[0];
            var E = query[1];

            var matrix = new long[E, 2];
            for (int i = 0; i < E; i++)
            {
                query = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                matrix[i, 0] = query[0];
                matrix[i, 1] = query[1];
            }

            K = V;

            int varCount = K*V;

            long[,] adj = BuildAdj(V, matrix);

            Clauses = new List<string>{""};
            var Vertex = Enumerable.Range(1, V);

            foreach(var v in Vertex)
            {
                var literals = Vertex.Select(k => Varnam(v, k)).ToList();
                ExactlyOneOF(literals);
                // AtLeastOneOF(literals);

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
                        var literals = new List<long>{Varnam(v1, k), Varnam(v2,k)};
                        AtMostOneOF(literals);

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
            Clauses[0] = clauseCount + " " + varCount;

            Console.WriteLine(string.Format("{0}", string.Join("\n", Clauses)));
            Console.ReadKey();

        }

        public static void ExactlyOneOF(List<long> literals)
        {
            Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
            AtMostOneOF(literals);
        }

        public static void AtLeastOneOF(List<long> literals)
        {
            Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
        }

        public static void AtMostOneOF(List<long> literals)
        {
            for (int i = 0; i < literals.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Clauses.Add("-" + literals[j] + " -" + literals[i] +  " 0");
                }
            }
        }

        public static long Varnam(long v, int k)
        {
            // return 10*v + k;
            return K * v + k - K;
        }

        public static bool Contains(long[,] array, int v1, int v2)
        {
            for (int i = 0; i < array.Length/2; i++)
            {
                if((array[i, 0] == v1 && array[i, 1] == v2) 
                    || array[i, 0] == v2 && array[i, 1] == v1)
                    return true;
            }
            return false;
        }

        private static long[, ] BuildAdj(int V, long[,] matrix)
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

    }
}
