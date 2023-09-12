using System;
using System.Collections.Generic;
using System.Threading;

namespace SuffixTreeFromSuffixArray
{
    public class Node
    {
        public Node parent;
        public Dictionary<char, Node> children;
        public int depth;
        public bool visited;
        public int start, end;
        public Node(Node node, int depth, int start, int end)
        {
            this.parent = node;
            this.children = new Dictionary<char, Node>();
            this.depth = depth;
            this.start = start;
            this.end = end;
            this.visited = false;
        }

        // internal Node GetChild(char c, string text)
        // {
        //     foreach (var child in children)
        //         if(text[child.start] == c)
        //             return child;
        //     return null;
        // }
    }
    public class SuffixTree
    {
        public char[] text;
        public char[] characters;
        public int[] SuffixArray, LCP;
        public Node root;
        public SuffixTree(char[] text, int[] suffixarray, int[] LCP)
        {
            this.text = text;
            this.characters = new char[]{'$', 'A', 'C', 'G', 'T'};
            this.SuffixArray = suffixarray;
            this.LCP = LCP;
            this.root = new Node(null, 0, -1, -1);
        }

        public Node BreakEdge(Node node, int mid_start, int offset)
        {
            var mid_char = text[mid_start];
            var left_char = text[mid_start + offset];
            var mid = new Node(node, node.depth + offset, mid_start, mid_start + offset);
            mid.children[left_char] = node.children[mid_char];
            node.children[mid_char].parent = mid;
            node.children[mid_char].start += offset;
            node.children[mid_char] = mid;
            return mid;
        }
        public Node NewLeaf(Node node, int suffix)
        {
            var leaf = new Node(node, text.Length - suffix, suffix + node.depth, text.Length);
            node.children[text[leaf.start]] = leaf;
            return leaf;
        }

        public void Build()
        {
            var LCP_prev = 0;
            var node = root;
            for(int i = 0; i < text.Length; i++)
            {
                int suffix = SuffixArray[i];
                while (node.depth > LCP_prev)
                    node = node.parent;
                if (node.depth == LCP_prev)
                    node = NewLeaf(node, suffix);
                else
                {
                    int mid_start = SuffixArray[i - 1] + node.depth;
                    var offset = LCP_prev - node.depth;
                    var mid = BreakEdge(node, mid_start, offset);
                    node = NewLeaf(mid, suffix);
                }
                if (i < text.Length - 1)
                    LCP_prev = LCP[i];
            }
        }

        public void PrintEdges(Node node)
        {
            // node.visited = true;
            if (node.depth != 0)
                System.Console.WriteLine(node.start + " " + node.end);
            for(int i = 0; i<5; i++)
            {
                var child = node.children.ContainsKey(characters[i])? node.children[characters[i]]: null;
                if (child != null && !child.visited)
                    PrintEdges(child);
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var text = Console.ReadLine().ToCharArray();
            var SuffixArray = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            var LCP = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            Console.WriteLine(text);
            var tree = new SuffixTree(text, SuffixArray, LCP);
            tree.Build();
            Thread T = new Thread(() => tree.PrintEdges(tree.root), 9060000);
            T.Start();
            
            // Console.ReadKey();
        }
    }
}
