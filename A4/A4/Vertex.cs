using System.Collections.Generic;
using System;
using Priority_Queue;

namespace A4
{
    public class Vertex: FastPriorityQueueNode
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
        => (Neighbors, Edges, Cost, Visited, index) 
        = (new List<long>(), new List<double>(), double.PositiveInfinity, false, i);
        // self.prev = None
    }
}