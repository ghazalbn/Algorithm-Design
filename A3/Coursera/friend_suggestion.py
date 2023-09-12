import math
import heapq
from queue import PriorityQueue

class Vertex:

    def __init__(self, i):
        (self.neighbors, self.edges, self.cost, self.index) = ([], [], math.inf, i)
    def __lt__(self, other):
        return self.cost < other.cost
    def  __gt__(self, other):
        return self.cost > other.cost

def bidirectional_dijkstra(adj, adj_R, s, e):
    if e == s:
        return 0
    proc = []
    proc_R = []
    adj[s].cost = 0
    adj_R[e].cost = 0
    dist = [adj[i] for i in range(len(adj))]
    dist_R = [adj_R[i] for i in range(len(adj))]

    while (dist and dist_R):
        heapq.heapify(dist)
        node = heapq.heappop(dist).index
        process(node, adj, dist, proc)
        if (proc_R.__contains__(node)):
            return shortestPath(s, adj, proc, e, adj_R, proc_R)

        heapq.heapify(dist_R)
        node = heapq.heappop(dist_R).index
        process(node, adj_R, dist_R, proc_R)
        if (proc.__contains__(node)):
            return shortestPath(e, adj_R, proc_R, s, adj, proc)
    
    return -1


def shortestPath(s, adj, proc, e, adj_R, proc_R):

    distance = math.inf
    proc.extend(proc_R)
    for u in proc:
        if (adj[u].cost + adj_R[u].cost < distance):
            distance = adj[u].cost + adj_R[u].cost

    return distance


def process(node, adj, dist, proc):

    for i in range(len(adj[node].neighbors)):  
        n = adj[node].neighbors[i]
        jadid = adj[node].cost + adj[node].edges[i]
        if (adj[n].cost > jadid):   
            # dist.remove((adj[n].cost, n))
            adj[n].cost = jadid
            # dist.append((adj[n].cost, n))
        
    
    proc.append(node)


if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [Vertex(_) for _ in range(n)]
    adj_R = [Vertex(_) for _ in range(n)]

    for i in range(m):
        a, b, w = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[a - 1].edges.append(w)
        adj_R[b - 1].neighbors.append(a - 1)
        adj_R[b - 1].edges.append(w)
    queriesCount = int(input())
    # queries = [(int, int) for _ in range(queriesCount)]
    for i in range(queriesCount):
        s, e = map(int, input().split())
        d = bidirectional_dijkstra(adj, adj_R, s - 1, e - 1)
        print(-1 if d == math.inf else d)
        if i != queriesCount - 1:
            for j in range(n):
                adj[j].cost = math.inf
                adj_R[j].cost = math.inf