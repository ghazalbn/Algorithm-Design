from collections import namedtuple  
      
# Declaring namedtuple()   
vertex = namedtuple('vertex',['visited','neighbors'])

def has_cycle(adj, x):
    adj[x] = vertex(1, adj[x].neighbors)
    for n in adj[x].neighbors:
        if adj[n].visited == 1:
            return 1
        elif adj[n].visited == 0:
            if has_cycle(adj, n):
                return 1
    adj[x] = vertex(2, adj[x].neighbors)
    return 0
        

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex(0, []) for _ in range(n)]
    for i in range(m):
        a, b = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
    b = 0
    for i in range(n):
        if has_cycle(adj, i):
            b = 1
    print(b)
