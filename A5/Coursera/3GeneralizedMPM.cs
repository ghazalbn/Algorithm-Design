using System;
using System.Collections.Generic;

namespace TrieMatching
{
    public class Node
    {
        public char Value { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }
        public int Depth { get; set; }
        public long Number { get; set; }
        public bool Visited {get; set;}

        public Node(char value, int depth, long number, Node parent)
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
            return Children.Count == 0;
        }

        public Node FindChildNode(char c)
        {
            foreach (var child in Children)
                if (child.Value == c)
                    return child;

            return null;
        }
    }
    public class Trie
    {
        private readonly Node _root;
        public long nodeCount;
        public long Depth;

        public Trie()
        {
            _root = new Node('^', 0, 0, null);
            Depth = 0;
            nodeCount = 0;
        }
        public Node GetRoot()
        { return _root;}

        public Node Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }
        public Node SubPrefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                var end = currentNode.FindChildNode('$');
                if (end != null)
                    break;
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        public bool Search(string s)
        {
            var prefix = Prefix(s);
            return prefix.Depth == s.Length && prefix.FindChildNode('$') != null;
        }

        public bool PrefixTrieMatching(string text)
        {
            var prefix = SubPrefix(text);
            return prefix.FindChildNode('$') != null;
        }

        public void TrieMatching(string text)
        {
            var index = 0;
            while(index < text.Length)
            {
                if (PrefixTrieMatching(text.Substring(index)))
                    Console.Write(index + " ");
                index++;
            }
        }

        public void InsertRange(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
                Insert(items[i]);
        }

        public void Insert(string s)
        {
            var commonPrefix = Prefix(s);
            var current = commonPrefix;

            for (var i = current.Depth; i < s.Length; i++)
            {
                var newNode = new Node(s[i], current.Depth + 1, ++nodeCount , current);
                current.Children.Add(newNode);
                current = newNode;
            }
            Depth = current.Depth + 1;
            current.Children.Add(new Node('$', current.Depth + 1, -1, current));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var text = Console.ReadLine();
            var n = long.Parse(Console.ReadLine());
            var trie = new Trie();
            for (int i = 0; i < n; i++)
            {
                trie.Insert(Console.ReadLine());
            }
            trie.TrieMatching(text);

            Console.ReadKey();
        }
    }
}
