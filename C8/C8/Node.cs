using System.Collections.Generic;

namespace C8
{
    public class Node
    {
        private int index;

        public Node(int i)
        {
            this.index = i;
            Neighbors = new List<Node>();

        }

        public bool Visited { get; set; }
        public long Components { get; set; } = -1;
        public List<Node> Neighbors { get; set; }
    }
}