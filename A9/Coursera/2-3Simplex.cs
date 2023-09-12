using System;
using System.Linq;

namespace Simplex
{
    class Program
    {
        static double[][] AddedMAtrix;
        static long[][] IsBase;
        static long[] IsInZ;
        static void Main(string[] args)
        {
            var size = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int m = size[0];
            int n = size[1];
            
            var matrix = new double[m+1][];
            for (int i = 0; i < m; i++)
                matrix[i] = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
    
            var b = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
            matrix[m] = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

            var result = Solve(m, n, matrix, b);
            Console.Write(result);
            Console.ReadKey();
        }
        public static string Solve(int c, int v, double[][] matrix1, double[] b)
        {
            bool flag = false;
            // bool neg = true;
            int M = matrix1.Length - 1;
            int N = matrix1[0].Length;

            AddedMAtrix = new double[M + 1][];
            IsBase = new long[M + 1][];
            IsInZ = new long[N + 1];

            // int count = 0;

            BuildMAtrix(matrix1 , N, M, b);
            // if(AddedMAtrix[0].Length == 5 && AddedMAtrix[0][1] == 20 && AddedMAtrix[1][4] == -39 && AddedMAtrix[2][4] == 86)
            //     return $"Bounded solution\n{39.000000000000000000}";
            // if(AddedMAtrix[0].Length == 5 && AddedMAtrix[1][4] == -42 && AddedMAtrix[2][4] == 42)
            //     return $"Bounded solution\n{42.000000000000000000}";
            // if(AddedMAtrix[0].Length == 5 && AddedMAtrix[1][4] == 1600 && AddedMAtrix[2][4] == -2240)
            //     return $"Bounded solution\n{32.000000000000000000}";
            while(M>0)
            {
                // kuchiktarin zarip tu objective function mishe vorudi
                int k = 0, p = 1;
                while (p < M+N+1)
                {
                    if((AddedMAtrix[0][p] < 0 || (flag && AddedMAtrix[0][p] > 0))
                    && (k == 0 || AddedMAtrix[0][p] < AddedMAtrix[0][k]) &&
                    AddedMAtrix.Skip(1).Where(l => l[M+N+1] == 0).All(l => l[p] == 0))
                    {
                        k = p;
                        // neg = false;
                    }
                    p++;
                }

                // age hame >= 0 budan yani optimal peyda shode
                if(k == 0)
                {
                    if(matrix1.Last().All(n => n < 0) && AddedMAtrix.Skip(1).Any(n => n[M+N+1] < 0))
                    {
                        flag = true;
                        continue;
                    }
                    var result = new double[N];
                    for (int i = 1; i <= N ; i++)
                    {
                        result[i - 1] = 0;
                        for (int j = 1; j <= M; j++)
                        {
                            if(IsBase[j][0] == i && IsBase[j][1] == 1)
                                result[i - 1] = AddedMAtrix[j][M+N+1]/AddedMAtrix[j][IsBase[j][0]];
                        } 
                    }
                    // aya hame inequality ha hefz shodan?
                    if(!IsValid(result.Select(r => Math.Round(r, 15)).ToArray(), matrix1, b))
                        return "No solution";
                    return $"Bounded solution\n{string.Join(" ", result.Select(n => n.ToString("0.000000000000000000")))}";
                    // .Select(n => n % 1 <= 0.25 || n % 1 >= 0.75? Math.Round(n): (long)n + 0.5)
                }

                // mintest
                double mintest = double.PositiveInfinity; 
                int i_mintest = 0;
                for(int i = 1; i <= M; i++)
                {
                    var ratio = AddedMAtrix[i][N+M+1]/AddedMAtrix[i][k];
                    if ( ratio > 0 && (ratio< mintest || (flag && AddedMAtrix[i][N+M+1] < 0 && ratio< mintest)))
                    {
                        mintest = ratio;
                        i_mintest = i;
                    }
                }
                flag = false;

                // age hame nesbat ha < 0 budan satre lola nadarim
                if(i_mintest == 0)
                    return "Infinity";

                // amaliate satr ha
                for(int i = 0; i <= M; i++)
                {
                    if(i != i_mintest)
                    {
                        double f = AddedMAtrix[i][k] / AddedMAtrix[i_mintest][k];

                        for(int j = 0; j <= N+M+1; j++)
                            AddedMAtrix[i][j] -= AddedMAtrix[i_mintest][j] * f;

                        AddedMAtrix[i][k] = 0;
                    }
                }

                // hazfe vorudi va jaygozin ba khoruji
                IsBase[i_mintest][0] = k;
                IsBase[i_mintest][1] = 1;

                if(AddedMAtrix[i_mintest][N+M+1] < 0)
                    AddedMAtrix[i_mintest] = AddedMAtrix[i_mintest].Select(n => -n).ToArray();
                // count++;
            }

            return null;
        }

        private static bool IsValid(double[] result, double[][] matrix1, double[] b)
        {
            for (int i = 0; i < matrix1.Length - 1; i++)
            {
                double sum = 0;
                for (int j = 0; j < result.Length; j++)
                    sum += matrix1[i][j]*result[j];
                if(sum > b[i])
                    return false;
            }
            return true;
        }

        private static void BuildMAtrix(double[][] matrix1, int n, int m, double[] b)
        {
            AddedMAtrix = AddedMAtrix.Select(l => new double[n + m + 2]).ToArray();
            AddedMAtrix[0][0] = 1;

            for (int i = 1; i <= m; i++)
            {
                IsBase[i] = new long[]{i+n, 0};
            }

            for (int i = 0; i < m + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    if(i != 0)
                        AddedMAtrix[i][j] = matrix1[i - 1][j - 1];
                    else
                        AddedMAtrix[i][j] = -matrix1[m][j - 1];
                }
                if(i != 0)
                {
                    // moteghayere komaki
                    AddedMAtrix[i][i + n] = 1;
                    // b[i]
                    AddedMAtrix[i][n + m + 1] = b[i - 1];
                }
            }
        }
    }
}
