using System;
using System.Collections.Generic;
using System.Linq;

namespace Q1FrequencyAssignment
{
    class Program
    {
        public static List<string> Clauses;

        static void Main(string[] args)
        {
            var query = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            var V = query[0];
            var E = query[1];

            int K = 3;
            // int clauseCount = 4*V + 2*K*E; //agar hamash exatcly one of bud
            int clauseCount = 4*V + K*E;    //bekhatere at most one of
            int varCount = K*V;

            Clauses = new List<string>{clauseCount + " " + varCount};
            var Digits = Enumerable.Range(1, K);
            var Vertex = Enumerable.Range(1, V);

            foreach(var v in Vertex)
            {
                var literals = Digits.Select(k => Varnam(v, k)).ToList();
                ExactlyOneOF(literals);
            }
            for (int i=0; i < E; i++)
            {
                query = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                var v1 = query[0];
                var v2 = query[1];

                foreach (var k in Digits)
                {
                    var literals = new List<long>{Varnam(v1, k), Varnam(v2,k)};
                    AtMostOneOF(literals);
                }
            }
            Console.WriteLine(string.Format("{0}", string.Join("\n", Clauses)));
            Console.ReadKey();
        }

        public static void ExactlyOneOF(List<long> literals)
        {
            Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
            AtMostOneOF(literals);
        }

        public static void AtMostOneOF(List<long> literals)
        {
            for (int i = 0; i < literals.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {

                    Clauses.Add("-" + literals[j] + " -" + literals[i] + " 0");
                }
            }
        }

        public static long Varnam(long v, int k)
        {
            // return 10*v + k;
            return 3 * v + k - 3;
        }

    }
}
