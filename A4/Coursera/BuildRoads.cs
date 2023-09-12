using System;
using System.Collections.Generic;

namespace BuildRoads
{
    class Program
    {
        public class Vertex
        {
            public double x;
            public double y;
            public long index;
            public Vertex Parent;
            public List<long> Neighbors;
            public List<double> Edges;
            public double Cost;
            public bool Visited;
            public Vertex(long i)
            {
                Neighbors = new List<long>();
                Edges = new List<double>();
                Cost = long.MaxValue;
                Visited = false; 
                index = i; 
            }
            // self.prev = None
        }
        public class PriorityQueue<T>
        {
            class Node
            {
                public double Priority { get; set; }
                public long Object { get; set; }
            }

            //object array
            List<Node> queue = new List<Node>();
            int heapSize = -1;
            bool _isMinPriorityQueue;
            public int Count { get { return queue.Count; } }

            /// <summary>
            /// If min queue or max queue
            /// </summary>
            /// <param name="isMinPriorityQueue"></param>
            public PriorityQueue(bool isMinPriorityQueue = false)
            {
                _isMinPriorityQueue = isMinPriorityQueue;
            }

            /// <summary>
            /// Enqueue the object with priority
            /// </summary>
            /// <param name="priority"></param>
            /// <param name="obj"></param>
            public void Enqueue(double priority, long obj)
            {
                Node node = new Node() { Priority = priority, Object = obj };
                queue.Add(node);
                heapSize++;
                //Maintaining heap
                if (_isMinPriorityQueue)
                    BuildHeapMin(heapSize);
                else
                    BuildHeapMax(heapSize);
            }
            /// <summary>
            /// Dequeue the object
            /// </summary>
            /// <returns></returns>
            public long Dequeue()
            {
                if (heapSize > -1)
                {
                    var returnVal = queue[0].Object;
                    queue[0] = queue[heapSize];
                    queue.RemoveAt(heapSize);
                    heapSize--;
                    //Maintaining lowest or highest at root based on min or max queue
                    if (_isMinPriorityQueue)
                        MinHeapify(0);
                    else
                        MaxHeapify(0);
                    return returnVal;
                }
                else
                    throw new Exception("Queue is empty");
            }
            /// <summary>
            /// Updating the priority of specific object
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="priority"></param>
            public void UpdatePriority(long obj, double priority)
            {
                // int i = Find(0,obj);
                int i = 0;
                for (; i <= heapSize; i++)
                {
                    Node node = queue[i];
                    if (node.Object == obj)
                    {
                        node.Priority = priority;
                        if (_isMinPriorityQueue)
                        {
                            BuildHeapMin(i);
                            MinHeapify(i);
                        }
                        else
                        {
                            BuildHeapMax(i);
                            MaxHeapify(i);
                        }
                        break;
                    }
                }
            }

                private int Find(int i, long obj)
                {
                    if(obj > queue[i].Object)
                        return Find(2*i + 2, obj);
                    if(obj < queue[i].Object)
                        return Find(2*i + 1, obj);
                    return i;
                }

                /// <summary>
                /// Searching an object
                /// </summary>
                /// <param name="obj"></param>
                /// <returns></returns>
                public bool IsInQueue(long obj)
            {
                foreach (Node node in queue)
                    if (object.ReferenceEquals(node.Object, obj))
                        return true;
                return false;
            }

            /// <summary>
            /// Maintain max heap
            /// </summary>
            /// <param name="i"></param>
            private void BuildHeapMax(int i)
            {
                while (i >= 0 && queue[(i - 1) / 2].Priority < queue[i].Priority)
                {
                    Swap(i, (i - 1) / 2);
                    i = (i - 1) / 2;
                }
            }
            /// <summary>
            /// Maintain min heap
            /// </summary>
            /// <param name="i"></param>
            private void BuildHeapMin(int i)
            {
                while (i >= 0 && queue[(i - 1) / 2].Priority > queue[i].Priority)
                {
                    Swap(i, (i - 1) / 2);
                    i = (i - 1) / 2;
                }
            }

            private void MaxHeapify(int i)
            {
                int left = ChildL(i);
                int right = ChildR(i);

                int heighst = i;

                if (left <= heapSize && queue[heighst].Priority < queue[left].Priority)
                    heighst = left;
                if (right <= heapSize && queue[heighst].Priority < queue[right].Priority)
                    heighst = right;

                if (heighst != i)
                {
                    Swap(heighst, i);
                    MaxHeapify(heighst);
                }
            }
            private void MinHeapify(int i)
            {
                int left = ChildL(i);
                int right = ChildR(i);

                int lowest = i;

                if (left <= heapSize && queue[lowest].Priority > queue[left].Priority)
                    lowest = left;
                if (right <= heapSize && queue[lowest].Priority > queue[right].Priority)
                    lowest = right;

                if (lowest != i)
                {
                    Swap(lowest, i);
                    MinHeapify(lowest);
                }
            }
            private void Swap(int i, int j)
            {
                var temp = queue[i];
                queue[i] = queue[j];
                queue[j] = temp;
            }
            private int ChildL(int i)
            {
                return i * 2 + 1;
            }
            private int ChildR(int i)
            {
                return i * 2 + 2;
            }
        }
        static void Main(string[] args)
        {
            var nodeCount = long.Parse(Console.ReadLine());
            var adj = new Vertex[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                adj[i]= new Vertex(i);
                var points = Array.ConvertAll( Console.ReadLine().Split(), long.Parse);
                adj[i].x = points[0];
                adj[i].y = points[1];
            }
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j< nodeCount; j++)
                {
                    if (i!= j)
                    {
                        double dd = Distance(adj[i], adj[j]);
                        adj[i].Neighbors.Add(j);
                        adj[i].Edges.Add(dd);
                    }
                }
            }
            var d = Prim(adj, 0);
            Console.WriteLine(d);
            Console.ReadKey();
        }
        private static double Prim(Vertex[] adj, long startNode)
        {
            double distance = 0;
            adj[startNode].Cost = 0;
            var dist = new PriorityQueue<Vertex>(true);
            for (int i = 0; i < adj.Length; i++)
                dist.Enqueue((adj[i].Cost), i);
            while (dist.Count > 0)
            {
                var node = dist.Dequeue();
                if(adj[node].Cost == double.PositiveInfinity)
                    break;
                adj[node].Visited = true;
                distance += adj[node].Cost;
                for (int i = 0; i < adj[node].Neighbors.Count; i++)
                {
                    var n = adj[node].Neighbors[i];
                    var jadid = adj[node].Edges[i];
                    if (adj[n].Cost > jadid && !adj[n].Visited)
                    {
                        adj[n].Cost = jadid;
                        dist.UpdatePriority(n, (adj[n].Cost));
                    }
                }
            }
            return Math.Round(distance, 6);
        }


        private Vertex[] Make(long nodeCount, long[][] points)
        {
            var adj = new Vertex[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                adj[i]= new Vertex(i);
                adj[i].x = points[i][0];
                adj[i].y = points[i][1];

            }
            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j< nodeCount; j++)
                {
                    if (i!= j)
                    {
                        double d = Distance(adj[i], adj[j]);
                        adj[i].Neighbors.Add(j);
                        adj[i].Edges.Add(d);
                    }
                }
            }
            return adj;
        }
        private static double Distance(Vertex n, Vertex endNode)
        {
            return Math.Sqrt(Math.Pow(n.x - endNode.x, 2) + Math.Pow(n.y - endNode.y, 2));
        }
    }
}
