using System.Collections.Generic;
using System;
using Priority_Queue;

namespace C2
{
    public class Vertex: FastPriorityQueueNode
    {
        public long index;
        public List<long> Neighbors;
        public List<long> Edges;
        public double Cost;
        public bool Visited;
        public Vertex(long i)
        => (Neighbors, Edges, Cost, Visited, index) 
        = (new List<long>(), new List<long>(), double.PositiveInfinity, false, i);
        // self.prev = None
    }
}