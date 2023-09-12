using System;
using System.Linq;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        double[,] matrix;
        long size;
        public Q1InferEnergyValues(string testDataMATRIX_SIZEame) : base(testDataMATRIX_SIZEame)
        {
            // ExcludeTestCaseRangeInclusive(1, 25);
            // ExcludeTestCaseRangeInclusive(27, 28);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

        public double[] Solve(long MATRIX_SIZE, double[,] mat)
        {
            this.matrix = mat;
            this.size = MATRIX_SIZE;
            
            Eliminate();

            double []x = new double[size];
            
            for(long i = size - 1; i >= 0; i--)
            {
                x[i] = matrix[i,size];
                for(long j = i + 1; j < size; j++)
                    x[i] -= matrix[i,j] * x[j];

                x[i] = x[i] / matrix[i,i];
                
            }
            return x.Select(n => n >= -0.25 && n <= 0? 0:
            Math.Abs(n % 1) <= 0.25 || Math.Abs(n % 1) >= 0.75? Math.Round(n): 
            n<=0? (long)n - 0.5: (long)n + 0.5).ToArray();
        }

        private void Eliminate()
        {
            for(int k = 0; k < size; k++)
            {
                int i_max = k;
                for(int i = k + 1; i < size; i++)
                {
                    if (Math.Abs(matrix[i, k]) > Math.Abs(matrix[i_max, k]))
                        i_max = i;
                }

                if (i_max != k)
                    // jabjayi satr
                    for(int j = 0; j <= size; j++)
                        (matrix[i_max, j], matrix[k, j]) = (matrix[k, j], matrix[i_max, j]);
                
                for(int i = k + 1; i < size; i++)
                {
                    double f = matrix[i, k] / matrix[k, k];
                    for(int j = k + 1; j <= size; j++)
                        matrix[i, j] -= matrix[k, j] * f;
                }
            }
        }
    }
}
