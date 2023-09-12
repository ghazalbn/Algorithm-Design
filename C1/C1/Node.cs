using System.Collections.Generic;

namespace C1
{
    public class Node
    {
        public long Value {get; set;}
        public long Distance {get; set;}
        public bool Visited {get; set;}
        public List<Node> Neighbors {get; set;}
        public Node(long value, long distance)
        {
            (Value, Distance, Visited) = (value, distance, false);
            Neighbors = new List<Node>();
        }
    }
}