import queue
from collections import namedtuple

# Declaring namedtuple()   
vertex = namedtuple('vertex',['color','neighbors'])

def is_bipartite(adj, start):
    q = queue.Queue(len(adj))
    q.put(start)
    adj[start] = (vertex(0, adj[start].neighbors))
    while not q.empty():
        node = q.get()
        for n in adj[node].neighbors:
            if adj[n].color != 2:
                if adj[n].color == adj[node].color:
                    return 0
            else:
                c = 1 - adj[node].color
                adj[n] = vertex(c, adj[n].neighbors)
                q.put(n)
    return 1

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex(2, []) for _ in range(n)]
    for i in range(m):
        a, b = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[b - 1].neighbors.append(a - 1)
    b = 1
    for i in range(m):
        if adj[i].color == 2 and not is_bipartite(adj, i):
            b = 0
            break
    print(b)
