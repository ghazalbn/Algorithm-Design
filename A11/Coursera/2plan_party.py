#uses python3

import sys
import threading

# This code is used to avoid stack overflow issues
sys.setrecursionlimit(10**6) # max depth of recursion
threading.stack_size(2**26)  # new thread will get stack of such size
infinity = 999999999


class Vertex:
    def __init__(self, weight):
        self.weight = weight
        self.children = []


def ReadTree():
    size = int(input())
    tree = [Vertex(w) for w in map(int, input().split())]
    for i in range(1, size):
        a, b = list(map(int, input().split()))
        tree[a - 1].children.append(b - 1)
        tree[b - 1].children.append(a - 1)
    return tree


def dfs(tree, vertex, parent, D):
    if D[vertex] == infinity:
        if vertex != 0 and len(tree[vertex].children) == 1:
                D[vertex] = tree[vertex].weight
        else:
            m1 = tree[vertex].weight
            for u in tree[vertex].children:
                if u == parent:
                    continue
                for w in tree[u].children:
                    if w == vertex:
                        continue
                    m1 = m1 + dfs(tree, w, u, D)
            m0 = 0
            for u in tree[vertex].children:
                if u == parent:
                    continue
                m0 = m0 + dfs(tree, u, vertex, D)
            D[vertex] = max(m0, m1)
    return D[vertex]

    # This is a template function for processing a tree using depth-first search.
    # Write your code here.
    # You may need to add more parameters to this function for child processing.


def MaxWeightIndependentTreeSubset(tree):
    size = len(tree)
    D = [infinity for i in range(size)]
    if size == 0:
        return 0
    # You must decide what to return.
    return dfs(tree, 0, -1, D)


def main():
    tree = ReadTree();
    weight = MaxWeightIndependentTreeSubset(tree);
    print(weight)


# This is to avoid stack overflow issues
threading.Thread(target=main).start()
