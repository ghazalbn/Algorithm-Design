using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace C8
{
    public class Q1SuperPalindromes : Processor
    {
        public Q1SuperPalindromes(string testDataName) : base(testDataName)
        {
            // this.ExcludeTestCaseRangeInclusive(5, 6);
            // this.ExcludeTestCaseRangeInclusive(8, 17);
        }

        public override string Process(string inStr)
        {
            var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var num = lines[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            long n = long.Parse(num[0]);
            long m = long.Parse(num[1]);
            string s = lines[1];
            return Solve(n, m, s).ToString();
        }

        

        public long Solve(long n, long m, string s)
        {
            long result = 0;
            var letters = new List<char>[n];
            Graph G = new Graph(n);
            for (int i = 0; i < n; i++)
            {
                letters[i] = new List<char>();
            }
            G.BuildEdges(m);
            long components = 1;
            for (int i = 0; i < n; i++)
            {
                if(!G.Nodes[i].Visited)
                {
                    G.DFS(G.Nodes[i], components);
                    components++;
                }
                letters[G.Nodes[i].Components].Add(s[i]);
            }
            for (int i = 0; i < components; i++)
            {
                var cletters = letters[i];
                result += cletters.Count - GetMax(cletters);
            }
            return result;
        }

        public long GetMax(List<char> s)
        {
            long result = 0;
            var count = new long[28];
            foreach (var c in s)
            {
                count[c - 'a']++;
                if (count[c - 'a'] > result)
                    result = count[c - 'a'];
            }
            return result;
        }
        public long TMP(long nn, long mm, string s)
        {
            int count = 0;
            int n = (int)nn;
            int m =(int)mm;
            StringBuilder sb = new StringBuilder(s);
 
            for(int i = 0; i < n; i++)
            {
                if(i< n/2)
                    if(sb[i] != sb[n - i - 1])
                    {
                        if(i>= m)
                        {
                            if(s[i] != s[i - m])
                                count++;
                            if (s[n - 1 - i] != s[i - m])
                                count++;
                            sb[i] = sb[i - m];
                            sb[n - 1 - i] = sb[i - m];
                        }
                        else
                        {
                            count++;
                            sb[n - 1 - i] = sb[i];
                        }
                    }
                if( i>= m && sb[i] != sb[i - m])
                {
                    count++;
                    sb[i] = sb[i - m];
                    sb[n - 1 - i] = sb[i - m];
                }
            }
            sb.Clear();
            return count;
        }
    }
}
