using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace C2
{
    public class Q1RoadsInHackerLand : Processor
    {
        public Q1RoadsInHackerLand(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] first = lines[0].TrimEnd().Split(' ');

            long n = long.Parse(first[0]);
            long m = long.Parse(first[1]);

            long[][] roads = lines.Skip(1).Select(line => line.Split(' ').Select(num => long.Parse(num)).ToArray()).ToArray();
            return Solve(n, roads);
        }

        public string Solve(long n, long[][] roads)
        {
            return Solve2((int) n, roads.Select(x => x.Select(num => (int)num).ToArray()).ToArray());
        }

        class Dsu
        {
            private readonly int[] parent;

            public Dsu(int n)
            {
                parent = Enumerable.Range(0, n).ToArray();
            }

            public int FindSet(int v)
            {
                if (v == parent[v])
                    return v;
                return parent[v] = FindSet(parent[v]);
            }

            public void UnionSets(int a, int b)
            {
                a = FindSet(a);
                b = FindSet(b);
                if (a != b)
                    parent[b] = a;
            }
        }
        private static T[] Init<T>(int size) where T : new() { var ret = new T[size]; for (int i = 0; i < size; i++) ret[i] = new T(); return ret; }

        public string Solve2(int n, int[][] roads)
        {

            var ed = roads;
            int m = roads.Length;
            Array.Sort(ed, (e1, e2) => e1[2].CompareTo(e2[2]));
            var dsu = new Dsu(n);
            var g = Init<List<Tuple<int, int>>>(n);
            for (int i = 0; i < m; i++)
            {
                ed[i][0]--;
                ed[i][1]--;
                if (dsu.FindSet(ed[i][0]) != dsu.FindSet(ed[i][1]))
                {
                    g[ed[i][0]].Add(Tuple.Create(ed[i][1], ed[i][2]));
                    g[ed[i][1]].Add(Tuple.Create(ed[i][0], ed[i][2]));
                    dsu.UnionSets(ed[i][0], ed[i][1]);
                }
            }

            var stack = new Stack<Tuple<int, int>>();
            var stack2 = new Stack<Tuple<int, int>>();
            stack.Push(Tuple.Create(0, -1));
            while (stack.Count > 0)
            {
                var t = stack.Pop();
                foreach (var e in g[t.Item1])
                    if (e.Item1 != t.Item2)
                        stack.Push(Tuple.Create(e.Item1, t.Item1));
                stack2.Push(t);
            }

            var cnt = new int[n];
            while (stack2.Count > 0)
            {
                var t = stack2.Pop();
                cnt[t.Item1] = 1;
                foreach (var e in g[t.Item1])
                    if (e.Item1 != t.Item2)
                        cnt[t.Item1] += cnt[e.Item1];
            }

            var stack3 = new Stack<Tuple<int, int, int>>();
            var ans = Enumerable.Repeat(0L, m).ToList();
            stack3.Push(Tuple.Create(0, -1, 0));
            while (stack3.Count > 0)
            {
                var t = stack3.Pop();
                foreach (var e in g[t.Item1])
                    if (e.Item1 != t.Item2)
                    {
                        ans[e.Item2] = 1L * cnt[e.Item1] * (cnt[t.Item1] - cnt[e.Item1] + t.Item3);
                        stack3.Push(Tuple.Create(e.Item1, t.Item1, t.Item3 + cnt[t.Item1] - cnt[e.Item1]));
                    }
            }

            for (int i = 0; i < ans.Count; i++)
            {
                long ex = ans[i] / 2;
                ans[i] %= 2;
                if (ex > 0)
                {
                    if (i == ans.Count - 1)
                        ans.Add(0);
                    ans[i + 1] += ex;
                }
            }
            while (ans[ans.Count - 1] == 0)
                ans.RemoveAt(ans.Count - 1);
            ans.Reverse();

            return string.Concat(ans);
        }
    }
}
