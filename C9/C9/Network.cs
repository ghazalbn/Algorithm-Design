using System.Collections.Generic;

namespace C9
{
    public class Network
        {
            public long[,] Flow { get; set; }
            public long[,] Capacity { get; set; }

            public void AddFlowToPath(List<long> path, long flowToAdd)
            {
                for (int i = 1; i < path.Count; i++)
                {
                    Flow[path[i - 1], path[i]] += flowToAdd;
                }
            }
        }
}