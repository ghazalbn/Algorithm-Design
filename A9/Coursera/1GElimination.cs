using System;
using System.Linq;

namespace GElimination
{
    class Program
    {
        static double[][] matrix;
        static long size;
        static void Main(string[] args)
        {
            size = long.Parse(Console.ReadLine());
            matrix = new double[size][];
            for (int i = 0; i < size; i++)
                matrix[i] = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

            var result = Solve();
            Console.Write(string.Join(" ", result.Select(n => n.ToString("0.000000"))));
            Console.ReadKey();
        }
        public static double[] Solve()
        {   
            Eliminate();

            double[] result = new double[size];
            
            for(long i = size - 1; i >= 0; i--)
            {
                result[i] = matrix[i][size];
                for(long j = i + 1; j < size; j++)
                    result[i] -= matrix[i][j] * result[j];

                result[i] = result[i] / matrix[i][i];
                
            }
            // return result.Select(n => n >= -0.25 && n <= 0? 0:
            // Math.Abs(n % 1) <= 0.25 || Math.Abs(n % 1) >= 0.75? Math.Round(n): 
            // n<=0? (long)n - 0.5: (long)n + 0.5).ToArray();
            return result;
        }

        static private void Eliminate()
        {
            for(int k = 0; k < size; k++)
            {
                int i_max = k;
                for(int i = k + 1; i < size; i++)
                {
                    if (Math.Abs(matrix[i][k]) > Math.Abs(matrix[i_max][k]))
                        i_max = i;
                }

                if (i_max != k)
                    // jabjayi satr
                    for(int j = 0; j <= size; j++)
                    {
                        double temp = matrix[i_max][j];
                        matrix[i_max][j] = matrix[k][j];
                        matrix[k][j] = temp;
                    }
                
                for(int i = k + 1; i < size; i++)
                {
                    double f = matrix[i][k] / matrix[k][k];
                    for(int j = k + 1; j <= size; j++)
                        matrix[i][j] -= matrix[k][j] * f;
                }
            }
        }
    }
}
