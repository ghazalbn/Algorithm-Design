using System;
using System.Linq;
using TestCommon;

namespace C5
{
    public class Q1LazyTypist : Processor
    {
        public Q1LazyTypist(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return Solve(long.Parse(lines[0]), lines.Skip(1).ToArray()).ToString();
        }

        public long Solve(long n, string[] words)
        {
            var trie = new Trie();
            for (int i = 0; i < n; i++)
            {
                trie.Insert(words[i]);
            }
            long d = 0;
            Dfs(trie.GetRoot(), ref d);
            d -= words.Max(w => w.Length) + n;
            return d;
        }
        private static void Dfs(Node startNode, ref long counter)
        {
            startNode.Visited = true;
            foreach (var n in startNode.Children)
            {
                if (!n.Visited)
                {
                    counter += 1; 
                    Dfs(n, ref counter);
                    counter += 1; 
                }

            }
        }
    }
}
