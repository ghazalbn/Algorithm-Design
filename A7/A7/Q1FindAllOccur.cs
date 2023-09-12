using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        public virtual long[] Solve(string text, string pattern)
        {
            var r = new List<long>();
            var patt = pattern.ToCharArray();
            var txt = text.ToCharArray();

            Char[] patternstr = patt.Concat(new char[]{'$'}.Concat(txt)).ToArray();
            var nums = Prefix(patternstr);
            for (int i = patt.Length + 1; i < nums.Length; i++) 
            {
                if (nums[i] == patt.Length)
                    r.Add(i - 2 * patt.Length);
            }
            return r.Count > 0? r.ToArray(): new long[]{-1};

            // rahe 3
            // return FindPattern.Find(text, pattern, new List<long>()).ToArray();

            // // rahe 2
            // Regex rgx = new Regex($@"^.+{pattern}$");
            // var r = rgx.Matches(text).Select(m => (long)m.Index).ToArray();

            // var trie = new Trie();
            // trie.Insert(pattern);
            // var indexes = trie.TrieMatching(text);
            
            // var tree = new Tree(text + '$');
            // var root = tree.nodes[0];
            // List<long> indexes = new List<long>();
            // tree.PatternMatching(root, pattern, indexes);

            
            // return indexes.Length > 0? indexes.ToArray(): new long[]{-1};
        }
        public static int[] Prefix(char[] patternstr) 
        {
            var nums = new int[patternstr.Length];
            int border = 0;
            for (int i = 1; i < patternstr.Length; i++) 
            {
                while (border > 0 && patternstr[i] != patternstr[border]) 
                    border = nums[border - 1];
                if (patternstr[i] == patternstr[border]) 
                {
                    border++;
                    nums[i] = border;
                }
                if (border == 0)
                    nums[i] = 0;
            }
            return nums;
        }

        // private void PrintEdges(int node, List<TreeNode> nodes, List<string> edges)
        // {
        //     var root = nodes[node];
        //     if(root.Value != "^")
        //         edges.Add(root.Value);
        //     foreach (var child in root.Children)
        //         PrintEdges(child, nodes, edges);
        // }
    }
}
