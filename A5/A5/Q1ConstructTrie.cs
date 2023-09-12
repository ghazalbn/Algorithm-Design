using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public string[] Solve(long n, string[] patterns)
        {
           var trie = new Trie();
            trie.InsertRange(patterns.ToList());
            return PrintTrie(trie.GetRoot());
        }
        private string[] PrintTrie(Node root)
        {
            var Edges = new List<string>();
            var q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                var node = q.Dequeue();
                foreach (var n in node.Children)
                {
                    if (n.Value != "$")
                    {
                        Edges.Add($"{node.Number}->{n.Number}:{n.Value}");
                        q.Enqueue(n);
                    }
                }
            }
            return Edges.ToArray();
        }
    }
}
