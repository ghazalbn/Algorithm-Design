import math


class vertex:
    def __init__(self):
        self.neighbors = []
        self.edjes = []
        self.cost = math.inf
        self.prev = None
        self.visited = False
def has_cycle(adj, s):
    adj[s].cost = 0
    if not (adj[s].neighbors):
        return 0
    for k in range(len(adj)):
        for node in range(len(adj)):
            for i in range(len(adj[node].neighbors)):
                n = adj[node].neighbors[i]
                # if not adj[n].visited:
                jadid = adj[node].cost + adj[node].edjes[i]
                if adj[n].cost > jadid:
                    adj[node].visited = True
                    if k >= len(adj) - 1:
                        return 1
                    adj[n].cost = jadid
                    adj[n].prev = node
    return 0

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex() for _ in range(n)]
    for i in range(m):
        a, b, w = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[a - 1].edjes.append(w)
    d = 0
    for s in range(len(adj)):
        if adj[s].visited == False:
            if has_cycle(adj, s):
                d = 1
                break
            
    print(d)