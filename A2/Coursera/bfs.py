import queue
from collections import namedtuple

# Declaring namedtuple()   
vertex = namedtuple('vertex',['distance','neighbors'])

def distance(adj, s, t):
    q = queue.Queue(len(adj))
    q.put(s)
    while not q.empty():
        node = q.get()
        # adj[node] = vertex(True, adj[node].distance, adj[node].neighbors)
        for n in adj[node].neighbors:
            if not adj[n].distance:
                dist = adj[node].distance + 1
                adj[n] = vertex(dist, adj[n].neighbors)
                if n == t:
                    return adj[n].distance
                q.put(n)
    return -1

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex(0, []) for _ in range(n)]
    for i in range(m):
        a, b = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[b - 1].neighbors.append(a - 1)
    x, y = map(int, input().split())
    x, y = x - 1, y - 1
    adj[x] = vertex(0, adj[x].neighbors)
    print(distance(adj, x, y))
