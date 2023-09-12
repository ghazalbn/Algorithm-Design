import math


class vertex:
    def __init__(self):
        self.neighbors = []
        self.edjes = []
        self.cost = math.inf
        self.prev = None
        self.visited = False
def bellmanford(adj, s):
    adj[s].cost = 0
    for k in range(2 * len(adj)):
        for node in range(len(adj)):
            for i in range(len(adj[node].neighbors)):
                n = adj[node].neighbors[i]
                # if not adj[n].visited:
                jadid = adj[node].cost + adj[node].edjes[i]
                if adj[n].cost > jadid:
                    adj[node].visited = True
                    adj[n].cost = -math.inf if k >= len(adj) - 1 else jadid
                    adj[n].prev = node
    return 0

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex() for _ in range(n)]
    for i in range(m):
        a, b, w = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[a - 1].edjes.append(w)
    s = int(input()) - 1
    bellmanford(adj, s)
    for x in range(n):
        if adj[x].cost == math.inf:
            print('*')
        elif adj[x].cost == -math.inf:
            print('-')
        else:
            print(adj[x].cost)
