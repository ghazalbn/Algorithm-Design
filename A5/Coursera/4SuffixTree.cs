using System;
using System.Collections.Generic;

namespace SuffixTree
{
    public class TreeNode 
    {
        public string Value;
        public int index;  
        public int ind;               
        public List<int> Children = new List<int>();
 
        public TreeNode() 
        {
            Value = "^";
            index = 2;
            ind = -1;
        }
 
        public TreeNode(int v, int i, string Value, params int[] children) 
        {
            this.Value = Value;
            Children.AddRange(children);
            index = v;
            ind = i;
        }
    }
    public class Tree 
    {
        public List<TreeNode> nodes = new List<TreeNode>();
 
        public Tree(string str, int v) 
        {
            nodes.Add(new TreeNode());
            AddText(str, v);
        }
        public void AddText(string text, int v)
        {
            for (int i = 0; i < text.Length; i++) 
            {
                AddSuffix(text.Substring(i), v, i);
            }
        }
 
        private void AddSuffix(string suffix, int v, int ind) 
        {
            int n = 0;
            int i, j; //j = past part in common
            for (i = 0; i < suffix.Length; i += j) {
                char b = suffix[i];
                int now = 0;
                int last;
                while (true) {
                    var children = nodes[n].Children;
                    if (now == children.Count) {
                        last = nodes.Count;
                        nodes.Add(new TreeNode(v, ind, suffix.Substring(i)));
                        if(n == 145)
                        {
                            
                        }
                        nodes[n].Children.Add(last);
                        nodes[n].index = nodes[n].index==v? nodes[n].index: 2;
                        return;
                    }
                    last = children[now];
                    if (nodes[last].Value[0] == b) 
                    {
                        nodes[last].index = nodes[last].index==2? 3:nodes[last].index==v? v: 2;
                        break;
                    }
                    now++;
                }

                var lastValue = nodes[last].Value;
                for (j = 0; j < lastValue.Length; j++) 
                {
                    if (suffix[i + j] != lastValue[j]) 
                    {
                        var newNode = last;
                        last = nodes.Count;
                        nodes.Add(new TreeNode(v, nodes[newNode].ind, lastValue.Substring(0, j), newNode));

                        nodes[last].index = nodes[newNode].index==v? v: 2;
                        nodes[newNode].index = nodes[newNode].index == 3? 2: nodes[newNode].index==v? v: 1 - v;

                        nodes[newNode].Value = lastValue.Substring(j); 
                        nodes[n].Children[now] = last;
                        nodes[n].index = nodes[n].index==v? v: 2;
                        break;
                    }

                }
                n = last; 
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var text = Console.ReadLine();
            var trie = new Tree(text, 0);
            PrintEdges(0, trie.nodes);
            Console.ReadKey();
        }

        private static void PrintEdges(int node, List<TreeNode> nodes)
        {
            var root = nodes[node];
            if(root.Value != "^")
                Console.WriteLine(root.Value);
            foreach (var child in root.Children)
            {
                PrintEdges(child, nodes);
            }
        }
    }
}
