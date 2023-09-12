using System;
using System.Collections.Generic;
using System.Linq;

namespace Q3AdBudgetAllocation
{
    class Program
    {
        static List<List<long>> Clauses;
        static void Main(string[] args)
        {
            var query = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            var eqCount = query[0];
            var varCount = query[1];

            var matrix = new long[eqCount][];
            for (int i = 0; i < eqCount; i++)
                matrix[i] = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            long[] b = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            Solve(eqCount, varCount, matrix, b);
            // Console.WriteLine(string.Join("\n", Clauses));
            Console.ReadKey();
        }
        public static void Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            Clauses = new List<List<long>>();
            // var Variables = Enumerable.Range(1, (int)varCount);

            for (int j = 0; j < eqCount; j++)
            {
                var line = A[j];
                int c = 0;
                var NonZero = line.Select(a => new long[]{c, line[c++]}).Where(a => a[1] != 0).ToArray();
                var count = NonZero.Length;
                for (int i = 0; i < Math.Pow(2, count); i++)
                {
                    c = 0;
                    var current = NonZero.Where(a => ((long)((i/Math.Pow(2, c++))%2) == 1)).ToArray();
                    long sum = current.Sum(a => a[1]);
                    if(sum > b[j])
                    {
                        var literals = current.Select(a => -(a[0]+1))
                        .Concat(NonZero.Where(a => !current.Contains(a)).Select(a => a[0]+1)).ToList();
                        // AtLeastOneOF(literals);
                        Clauses.Add(literals);
                    }
                }
            }
            
            if(0 == Clauses.Count)
            {
                Clauses.Add(new List<long>{1, -1});
                varCount = 1;
            }
            Console.WriteLine(Clauses.Count + " " + varCount);
            foreach(var clause in Clauses)
            {
                clause.Add(0);
                System.Console.WriteLine(string.Join(" ", clause));
            }
        }

        private static bool IsSatisfied(int j, long[] line, long b)
        {
            long sum = 0, i = 0;
            foreach (var l in line)
            {
                if(l != 0 && (int)((j/Math.Pow(2, i++))%2) == 1)
                    sum += l;
            }
            return sum <= b;
        }
        // public static void AtLeastOneOF(List<long> literals)
        // {
        //     Clauses.Add(string.Format("{0} 0", string.Join(" ", literals)));
        // }
    }
}
