using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q4SuffixTree : Processor
    {
        public Q4SuffixTree(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String[]>)Solve);

        public string[] Solve(string text)
        {
            var tree = new Tree(text, 0);

            var edges = new List<string>();
            PrintEdges(0, tree.nodes, edges);
            return edges.ToArray();
        }

        private void PrintEdges(int node, List<TreeNode> nodes, List<string> edges)
        {
            var root = nodes[node];
            if(root.Value != "^")
                edges.Add(root.Value);
            foreach (var child in root.Children)
                PrintEdges(child, nodes, edges);
        }
    }
}
