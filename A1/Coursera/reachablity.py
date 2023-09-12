from collections import namedtuple  
      
# Declaring namedtuple()   
vertex = namedtuple('vertex',['visited','neighbors'])

def reach(adj, x, y):
    adj[x] = vertex(1, adj[x].neighbors)
    for n in adj[x].neighbors:
        if adj[n].visited == 0:
            reach(adj, n, y)

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex(0, []) for _ in range(n)]
    for i in range(m):
        a, b = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[b - 1].neighbors.append(a - 1)
    x, y = map(int, input().split())
    
    x, y = x - 1, y - 1
    b = 0
    reach(adj, x, y)
    print(adj[y].visited)
