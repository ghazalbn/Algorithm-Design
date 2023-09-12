using System;
using System.Linq;
using TestCommon;

namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {
        double[][] AddedMAtrix;
        long[][] IsBase;
        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

        public string Solve(int c, int v, double[,] matrix1)
        {
            int M = matrix1.GetLength(0) - 1;
            int N = matrix1.GetLength(1) - 1;

            AddedMAtrix = new double[M + 1][];
            IsBase = new long[M + 1][];

            BuildMAtrix(matrix1 , N, M);

            while(M>0)
            {
                // kuchiktarin zarip tu objective function mishe vorudi
                int k = 0, p = 1;
                while (p < M+N+1)
                {
                    if(AddedMAtrix[0][p] < 0 && 
                    (k == 0 || AddedMAtrix[0][p] < AddedMAtrix[0][k]) &&
                    AddedMAtrix.Skip(1).Where(l => l[M+N+1] == 0).All(l => l[p] == 0))
                        k = p;
                    p++;
                }

                // age hame >= 0 budan yani optimal peyda shode
                if(k == 0)
                {
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
                    if(!IsValid(result.Select(r => r-r%0.0000000001).ToArray(), matrix1))
                        return "No Solution";
                    return $"Bounded Solution\n{string.Join(' ', result.Select(n => n % 1 <= 0.25 || n % 1 >= 0.75? Math.Round(n): (long)n + 0.5))}";
                }

                // mintest
                double mintest = double.PositiveInfinity; 
                int i_mintest = 0;
                for(int i = 1; i <= M; i++)
                {
                    var ratio = AddedMAtrix[i][N+M+1]/AddedMAtrix[i][k];
                    if ( AddedMAtrix[i][N+M+1]>0 && AddedMAtrix[i][k] > 0 && ratio< mintest)
                    {
                        mintest = ratio;
                        i_mintest = i;
                    }
                }

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
            }

            return null;
        }

        private bool IsValid(double[] result, double[,] matrix1)
        {
            for (int i = 0; i < matrix1.GetLength(0) - 1; i++)
            {
                double sum = 0;
                for (int j = 0; j < result.Length; j++)
                    sum += matrix1[i, j]*result[j];
                if(sum > matrix1[i, result.Length])
                    return false;
            }
            return true;
        }

        private void BuildMAtrix(double[,] matrix1, int n, int m)
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
                        AddedMAtrix[i][j] = matrix1[i - 1, j - 1];
                    else
                        AddedMAtrix[i][j] = -matrix1[m, j - 1];
                }
                if(i != 0)
                {
                    // moteghayere komaki
                    AddedMAtrix[i][i + n] = 1;
                    // b[i]
                    AddedMAtrix[i][n + m + 1] = matrix1[i - 1, n];
                }
            }
        }
    }
}
