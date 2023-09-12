using System;
using System.Collections.Generic;

namespace C9
{
    public class BN
    {
        public long n, m;
        public long[] p, c;
        private bool[] visited;
        private long[] parent;

        public BN(long n, long m, long[] p, long[] c)
        {
            this.n = n;
            this.m = m;
            this.p = p;
            this.c = c;
        }

        public long EdmondKarp()
        {
            var network = BuildNetwork();
            while (true)
            {
                Queue<long> nodes = new Queue<long>();
                nodes.Enqueue(n);
                Array.Fill(visited, false);
                Array.Fill(parent, 0);
                parent[n] = -1;

                while (nodes.Count > 0)
                {
                    var front = nodes.Dequeue();
                    visited[front] = true;
                    // node is source
                    if (front == n)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (!visited[i] && (network.Capacity[front, i] - network.Flow[front, i] > 0))
                            {
                                nodes.Enqueue(i);
                                parent[i] = front;
                                visited[i] = true;
                            }
                        }
                    } else if (0 <= front && front < n)
                    {
                        for (int i = (int) front + 1; i < n; i++)
                        {
                            if (!visited[i] && (network.Capacity[front, i] - network.Flow[front, i] > 0))
                            {
                                nodes.Enqueue(i);
                                parent[i] = front;
                                visited[i] = true;
                            }
                        }
                        if (!visited[n + 1] && (network.Capacity[front, n + 1] - network.Flow[front, n + 1] > 0))
                        {
                            nodes.Enqueue(n + 1);
                            parent[n + 1] = front;
                            visited[n + 1] = true;
                        }
                    } else {
                        // node is sink  
                        List<long> path = new List<long>();
                        long currentNode = n + 1;

                        long maxFlowToAdd = long.MaxValue;

                        while (parent[currentNode] != -1)
                        {
                            path.Add(currentNode);
                            // Capacity[u, v], Flow[u, v]
                            long passingFlow = network.Capacity[parent[currentNode], currentNode] - network.Flow[parent[currentNode], currentNode];
                            maxFlowToAdd = Math.Min(maxFlowToAdd, passingFlow);
                            currentNode = parent[currentNode];
                        }
                        path.Add(n);
                        path.Reverse();
                        network.AddFlowToPath(path, maxFlowToAdd);
                        break;
                    }
                }
                // if sink is not visited
                if (!visited[n + 1])
                {
                    break;
                }
            }

            long maxFlow = 0;
            for (int i = 0; i < n; i++)
            {
                maxFlow += network.Flow[i, n + 1];
            }
            return maxFlow;
        }

        public Network BuildNetwork()
        {
            visited = new bool[n + 2];
            parent = new long[n + 2];
            long[,] capacity = new long[n + 2, n + 2];
            // inter-city capacities
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    capacity[i, j] = m;
                }
            }
            // source-to-city capacities
            for (int i = 0; i < n; i++)
            {
                capacity[n, i] = p[i];
            }
            // city-to-destination capacities
            for (int i = 0; i < n; i++)
            {
                capacity[i, n + 1] = c[i];
            }

            Network net = new Network
            {
                Flow = new long[n + 2, n + 2],
                Capacity = capacity
            };
            return net;
        }        

    }
}