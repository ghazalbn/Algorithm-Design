
using System;
using System.Collections.Generic;
using System.Linq;
namespace E2
{
    public class Trie
    {
        private readonly Node _root;
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
                int i = 0;
                currentNode = currentNode.FindChildNode(c.ToString(), ref i);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        public bool Search(string s)
        {
            int i = 0;
            var prefix = Prefix(s);
            return prefix.Depth == s.Length && prefix.FindChildNode("$", ref i) != null;
        }

        public Node SubPrefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                int i = 0;
                var end = currentNode.FindChildNode("$", ref i);
                if (end != null)
                    break;
                currentNode = currentNode.FindChildNode(c.ToString(), ref i);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }
        public bool PrefixTrieMatching(string text, ref int i)
        {
            var prefix = SubPrefix(text);
            return prefix.FindChildNode("$", ref i) != null;
        }

        public int[] TrieMatching(string text)
        {
            var indexes = new List<int>();
            var index = 0;
            while(index < text.Length)
            {
                int i = 0;
                if (PrefixTrieMatching(text.Substring(index), ref i))
                    indexes.Add(i);
                index++;
            }
            return indexes.ToArray();
        }

        public string[] TrieBoardMatching(char[][] board)
        {
            var indexes = new List<string>();
            var currentNode = _root;
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    var result = "";
                    int k = 0;
                    var node = currentNode.FindChildNode(board[i][j].ToString(), ref k);
                    if(node != null)
                        // result += board[i][j];
                    {
                        var Visited = new bool[board.Length, board[0].Length];
                        DFS(node, board, board[i][j], i, j, Visited, result, indexes);
                    }
                }
            }
            return indexes.ToArray();
        }

        private void DFS(Node node, char[][] board, char v, int i, int j, bool[,] visited, string result, List<string> indexes)
        {
            if(node == null)
                return;
            if(node.IsLeaf() && !indexes.Contains(result + node.Value))
                indexes.Add(result + node.Value);
                // indexes.Add(int.Parse(node.Children[0].Children[0].Value));

            result += v;
            visited[i, j] = true;
            foreach (var child in node.Children)
            {
                if(j < board[0].Length - 1 && !visited[i, j + 1] && child.Value == board[i][j + 1].ToString())
                    DFS(child, board, board[i][j + 1], i, j + 1, visited, result, indexes);
                if(i < board.Length - 1 && !visited[i + 1, j] && child.Value == board[i + 1][j].ToString())
                    DFS(child, board, board[i + 1][j], i + 1, j, visited, result, indexes);
                if(i > 0 && !visited[i - 1, j] && child.Value == board[i - 1][j].ToString())
                    DFS(child, board, board[i - 1][j], i - 1, j, visited, result, indexes);
                if(j > 0 && !visited[i, j - 1] && child.Value == board[i][j - 1].ToString())
                    DFS(child, board, board[i][j - 1], i, j - 1, visited, result, indexes);
            }
            visited[i, j] = false;

        }

        private void BFS(char[][] board, int i, int j, List<Node> adj)
        {
            var node = adj[i * board[0].Length + j];
            var q = new Queue<Node>();
            q.Enqueue(node);

            var indexes = new List<int>();

            var currentNode = _root;
            var result = currentNode;

            while (q.Count != 0)
            {
                node = q.Dequeue();

                int ii = 0;
                var end = currentNode.FindChildNode("$", ref ii);
                if (end != null)
                {
                    indexes.Add(i);
                    break;
                }
                currentNode = currentNode.FindChildNode(node.Value, ref ii);
                if (currentNode == null)
                    break;
                result = currentNode;
                foreach (var n in node.Children)
                {
                    if (!n.Visited)
                    {

                        q.Enqueue(n);
                    }
                }
            }
        }

        public void InsertRange(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
                Insert(items[i], i);
        }

        public void Insert(string s, int index)
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
            var node = new Node("$", current.Depth + 1, -1, current);
            current.Children.Add(node);
            node.Children.Add(new Node($"{index}", node.Depth + 1, -1, node));
        }

        public void Delete(string s)
        {
            if (Search(s))
            {
                int i = 0;
                var node = Prefix(s).FindChildNode("$", ref i);

                while (node.IsLeaf())
                {
                    var parent = node.Parent;
                    parent.DeleteChildNode(node.Value);
                    node = parent;
                } 
            }
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

    }
}