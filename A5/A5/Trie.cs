
using System;
using System.Collections.Generic;
namespace A5
{
    public class Trie
    {
        private Node _root;
        public long nodeCount;
        public long Depth;

        public Trie()
        {
            _root = new Node("^", 0, 0, null);
            Depth = 0;
            nodeCount = 0;
        }
        public Node GetRoot()
        => _root;

        public Node Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                currentNode = currentNode.FindChildNode(c.ToString());
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        public bool Search(string s)
        {
            var prefix = Prefix(s);
            return prefix.Depth == s.Length && prefix.FindChildNode("$") != null;
        }

        public Node SubPrefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                var end = currentNode.FindChildNode("$");
                if (end != null)
                    break;
                currentNode = currentNode.FindChildNode(c.ToString());
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }
        public bool PrefixTrieMatching(string text)
        {
            var prefix = SubPrefix(text);
            return prefix.FindChildNode("$") != null;
        }

        public long[] TrieMatching(string text)
        {
            var indexes = new List<long>();
            var index = 0;
            while(index < text.Length)
            {
                if (PrefixTrieMatching(text.Substring(index)))
                    indexes.Add(index);
                index++;
            }
            return indexes.ToArray();
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
                var newNode = new Node(s[i].ToString(), current.Depth + 1, ++nodeCount , current);
                current.Children.Add(newNode);
                current = newNode;
            }
            Depth = current.Depth + 1;
            current.Children.Add(new Node("$", current.Depth + 1, -1, current));
        }

        public void BuildSuffixTree(Node node)
        {
            int c = node.Children.Count;
            var n = node;
            if (n.Children.Count == 1)
            {
                var child = n.Children[0];
                child.Value = n.Value + child.Value;
                child.Depth--;
                child.Parent = n.Parent;
                var index = n.Parent.Children.IndexOf(n);
                n.Parent.Children[index] = child;
                BuildSuffixTree(child);
            }
            else
                for (int i = 0; i < c ;i++)
                    BuildSuffixTree(n.Children[i]);
        }

        public void Delete(string s)
        {
            if (Search(s))
            {
                var node = Prefix(s).FindChildNode("$");

                while (node.IsLeaf())
                {
                    var parent = node.Parent;
                    parent.DeleteChildNode(node.Value);
                    node = parent;
                } 
            }
        }
    }
}