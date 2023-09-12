using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q5ShortestNonSharedSubstring : Processor
    {
        public Q5ShortestNonSharedSubstring(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(50);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, String>)Solve);

        public string Solve(string text1, string text2)
        {
            // text1: 0
            // text2: 1
            // text1 va text2: 2
            var tree = new Tree(text1 + '#', 0);
            tree.AddText(text2 + '$', 1);
            int minheight = int.MaxValue, height = 0, ind = -1;
            DFS(tree.nodes, 0, height, ref minheight, ref ind);
            return ind >= 0? text1.Substring(ind, minheight - 1): "";

            // rahe ghabli timeout midad
            // var tree = new Trie();
            // for (int i = 0; i < text1.Length; i++)
            // {
            //     tree.Insert(text1.Substring(0, i + 1));
            // }
            // return (BFS(tree.GetRoot(), text1, text2 + "$"));
        }

        private static void DFS (List<TreeNode> nodes, int i, int height, ref int minheight, ref int ind)
        {
            var node = nodes[i];
            var val = node.Value.Replace("#", "");
            if(node.index == 0 && val.Length != 0 && (height + 1 < minheight
            || (height + 1 == minheight && node.ind <ind)))
            {
                (minheight, ind) = (height + 1, node.ind);
                return;
            }
            else if(node.index == 1) return;
            for (int j = 0; j < node.Children.Count; j++)
            {
                var child = node.Children[j];
                DFS(nodes, child, height + val.Length, ref minheight, ref ind);
            }
        }
        private static string BFS( Node root, string text1, string text2)
        {
            Dictionary<Node, string> values = new Dictionary<Node, string>();
            values[root] = "";
            var q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                var node = q.Dequeue();
                if (!text2.Contains(values[node]) || !text1.Contains(values[node]))
                    return values[node];
                foreach (var child in node.Children)
                {
                    // var child = nodes[n];
                    values[child] = (values[node] + child.Value).Replace("$", "");
                    q.Enqueue(child);
                }
            }
            return "";
        }
    }
}
