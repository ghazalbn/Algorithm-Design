import queue
import math
from collections import namedtuple
import heapq


class vertex:
    def __init__(self):
        self.neighbors = []
        self.edjes = []
        self.cost = math.inf
        self.prev = None
        # self.visited = False
    def __eq__(self, value):
        return True

def distance(adj, s, t):
    dist = [(adj[i].cost, i) for i in range(len(adj))]
    while len(dist):
        heapq.heapify(dist)
        c, node = heapq.heappop(dist)
        # c, node = min(dist)
        # dist.remove((c, node))
        # adj[node].visited = True
        if node == t:
            return c
        if node == s and not len(adj[node].neighbors):
            return -1
        for i in range(len(adj[node].neighbors)):
            n = adj[node].neighbors[i]
            # if not adj[n].visited:
            jadid = adj[node].cost + adj[node].edjes[i]
            if adj[n].cost > jadid:
                dist.remove((adj[n].cost, n))
                adj[n].cost = jadid
                dist.append((adj[n].cost, n))
                adj[n].prev = node
    return -1

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex() for _ in range(n)]
    for i in range(m):
        a, b, w = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[a - 1].edjes.append(w)
    x, y = map(int, input().split())
    x, y = x - 1, y - 1
    adj[x].cost = 0
    d = distance(adj, x, y)
    print(-1 if adj[y].cost == math.inf else d)