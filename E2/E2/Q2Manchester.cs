using System;
using TestCommon;

namespace E2
{
    public class Q2Manchester : Processor
    {
        public Q2Manchester(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr)
            => E2Processors.Q2Processor(inStr, Solve);

        /**
         * W[i] = Number of wins i-th team currently has.
         * R[i] = Total number of remaining games for i-th team.
         * G[i][j] = Number of upcoming games between teams i and j.
         * 
         * Should return whether the team can get first place.
         */
        public bool Solve(long[] W, long[] R, long[][] G)
        {
            int n = W.Length;
            var bn = new BN(n, W, R, G);
            return bn.EdmondKarp() == 2;
        }
    }
}
