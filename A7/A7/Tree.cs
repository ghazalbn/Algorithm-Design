
using System;
using System.Collections.Generic;
namespace A7
{
    public class TreeNode {
        public string Value;                     // a Valuestring of the input string
        public List<int> Children = new List<int>(); // vector of Childrenild nodes
 
        public TreeNode() {
            Value = "^";
        }
 
        public TreeNode(string Value, params int[] children) {
            this.Value = Value;
            Children.AddRange(children);
        }
    }
    public class Tree {
        public readonly List<TreeNode> nodes = new List<TreeNode>();
 
        public Tree(string str) {
            nodes.Add(new TreeNode());
            for (int i = 0; i < str.Length; i++) {
                AddSuffix(str.Substring(i), i);
            }
        }
 
        private void AddSuffix(string suf, int index) {
            int n = 0;
            int i = 0;
            while (i < suf.Length) {
                char b = suf[i];
                int x2 = 0;
                int n2;
                while (true) {
                    var children = nodes[n].Children;
                    if (x2 == children.Count) {
                        // no matching child, remainder of suf becomes new node
                        n2 = nodes.Count;
                        nodes.Add(new TreeNode(suf.Substring(i)));
                        nodes[n].Children.Add(n2);
                        // if(nodes[n2].Value == "$")
                        // {
                            nodes.Add(new TreeNode($"{index}"));
                            nodes[n2].Children.Add(n2 + 1);
                        // }
                        return;
                    }
                    n2 = children[x2];
                    if (nodes[n2].Value[0] == b) {
                        break;
                    }
                    x2++;
                }
                // find prefix of remaining suffix in common with child
                var Value2 = nodes[n2].Value;
                int j = 0;
                while (j < Value2.Length) {
                    if (suf[i + j] != Value2[j]) {
                        // split n2
                        var n3 = n2;
                        // new node for the part in common
                        n2 = nodes.Count;
                        nodes.Add(new TreeNode(Value2.Substring(0, j), n3));
                        nodes[n3].Value = Value2.Substring(j); // old node loses the part in common
                        nodes[n].Children[x2] = n2;
                        break; // continue down the tree
                    }
                    j++;
                }
                i += j; // advance past part in common
                n = n2; // continue down the tree
            }
        }

        public void PatternMatching(TreeNode node, string pattern, List<long> indexes)
        {

            foreach (var i in node.Children)
            {
                var n = nodes[i];
                if(n.Value.StartsWith(pattern))
                    FindIndexes(n, indexes);
                else if(pattern.StartsWith(n.Value))
                    PatternMatching(n, pattern.Substring(n.Value.Length), indexes);
            }
        }

        private void FindIndexes(TreeNode n, List<long> indexes)
        {
            foreach (var i in n.Children)
            {
                var child = nodes[i];
                if(int.TryParse(child.Value, out int s))
                    indexes.Add(s);
                FindIndexes(child, indexes);
            }
        }
    }
}