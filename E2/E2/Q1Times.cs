using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace E2
{
    public class Q1Times : Processor
    {
        public Q1Times(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr)
            => E2Processors.Q1Processor(inStr, Solve);

        public string[] Solve(char[][] board, string[] words)
        {
            var trie = new Trie();
            trie.InsertRange(words.ToList());
            var indexes = trie.TrieBoardMatching(board);
            var ind = indexes.ToArray();
            Array.Sort(ind);
            return ind.ToArray();
        }
    }
}
