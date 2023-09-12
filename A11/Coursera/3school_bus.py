# python3
import math
import itertools

infinity = 99999999999

class node:
    def __init__(self, n):
        self.parent = [None for i in range (int(math.pow(2, n)))]


def getpath(nodes, par, path):
    if par == None:
        return
    i, p = par[0], par[1]
    path.insert(0, i + 1)
    getpath(nodes, nodes[i].parent[p], path)



def TSP(nodes, graph):
    n = len(graph)
    total_set = range(n)
    total_length = int(math.pow(2, n))
    C = []
    for i in range(total_length):
        C.append([infinity for j in range(n)])

    C[1][0] = 0
    for s in range(2, n + 1):
        comb = itertools.combinations(total_set, s)
        for S in comb:
            if (0 in S):
                k = sum([1 << i for i in S])
                for i in S:
                    if i != 0:
                        for j in S:
                            if j != i:
                                new = C[k ^ (1 << i)][j] + graph[j][i]
                                if new < C[k][i]:
                                    C[k][i] = new
                                    nodes[i].parent[k] = (j, k ^ (1 << i))

    l = [C[total_length - 1][i] + graph[i][0] for i in range(n)]
    min_i = 0
    min_p = total_length - 1
    min_weight = l[0]
    for i in range(n):
        if l[i] < min_weight:
            min_weight = l[i]
            min_i = i
    
    if min_weight < infinity:
        print(min_weight)
        path = []
        getpath(nodes, (min_i, min_p), path)
        print(*path)
    else:
        print(-1)



n, m = map(int, input().split())
nodes = [node(n) for i in range(n)]
graph = []
for i in range(n):
    graph.append([infinity for j in range(n)])

for i in range(m):
    u, v, weight = map(int, input().split())
    graph[u - 1][v - 1] = weight
    graph[v - 1][u - 1] = weight

TSP(nodes, graph)
