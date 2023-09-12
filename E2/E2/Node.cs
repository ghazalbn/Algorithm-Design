using System.Collections.Generic;
namespace E2
{
    public class Node
    {
        public string Value { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }
        public int Depth { get; set; }
        public long Number { get; set; }
        public bool Visited {get; set;}

        public Node(string value, int depth, long number, Node parent)
        {
            Value = value;
            Children = new List<Node>();
            Depth = depth;
            Parent = parent;
            Visited = false;
            Number = number;
        }

        public bool IsLeaf()
        {
            int i = 0;
            return FindChildNode("$", ref i) != null;
        }

        public Node FindChildNode(string c, ref int i)
        {
            foreach (var child in Children)
                if (child.Value == c)
                {
                    if (c == "$")
                        i = int.Parse(child.Children[0].Value);
                    return child;
                }

            return null;
        }

        public void DeleteChildNode(string c)
        {
            for (var i = 0; i < Children.Count; i++)
                if (Children[i].Value == c)
                    Children.RemoveAt(i);
        }
    }
}