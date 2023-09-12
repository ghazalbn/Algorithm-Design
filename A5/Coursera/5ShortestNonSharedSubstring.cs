using System;
using System.Collections.Generic;
using System.Linq;

namespace NonSharedSubstring
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
            var text1 = Console.ReadLine();
            var text2 = Console.ReadLine();

            // text1: 0
            // text2: 1
            // text1 va text2: 2
            var tree = new Tree(text1 + '#', 0);
            tree.AddText(text2 + '$', 1);
            int minheight = int.MaxValue, height = 0, ind = -1;
            DFS(tree.nodes, 0, height, ref minheight, ref ind, "");
            
            System.Console.WriteLine(ind >= 0? text1.Substring(ind, minheight - 1): "");
            Console.ReadKey();
        }
        private static void DFS (List<TreeNode> nodes, int i, int height, ref int minheight, ref int ind, string str)
        {
            var node = nodes[i];
            var val = node.Value.Replace("#", "");
            if(node.index == 0 && val.Length != 0 && (height + 1 < minheight
            ) && node.Children.All(c => nodes[c].index == 0))
            {
                minheight = height + 1;
                ind = node.ind;
                return;
            }
            else if(node.index == 1) return;
            foreach (var child in node.Children)
            {
                DFS(nodes, child, height + val.Length, ref minheight, ref ind, str+val);
            }
        }
        // private static string BFS( List<Node> nodes, string text1, string text2)
        // {
        //     Dictionary<Node, string> values = new Dictionary<Node, string>();
        //     values[nodes[0]] = "";
        //     var q = new Queue<Node>();
        //     q.Enqueue(nodes[0]);
        //     while (q.Count != 0)
        //     {
        //         var node = q.Dequeue();
        //         // if (!text2.Contains(values[node]) && text1.Contains(values[node]))
        //         //     return values[node];
        //         var min = 0;
        //         Node m = null;
        //         foreach (var n in node.ch)
        //         {
        //             var child = nodes[n];
        //             var val = values[node] + child.Value;
        //             // var index = val.IndexOf("#");
        //             // if (index != -1)
        //             //     val = val.Remove(index, val.Length - index);
        //             values[child] = val;
        //             q.Enqueue(child);
        //             if (!text2.Contains(values[child]) && text1.Contains(values[child]))
        //             if (val.Length < min)
        //             {
        //                 min = val.Length;
        //                 m =child;
        //             }
        //         }
        //         if (m != null)
        //             return values[m];
        //     }
        //     return "";
        // }
    }
}
