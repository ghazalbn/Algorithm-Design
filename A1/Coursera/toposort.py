from collections import namedtuple  
      
# Declaring namedtuple()   
vertex = namedtuple('vertex',['visited','neighbors'])

def dfs(adj, x, order):
    adj[x] = vertex(1, adj[x].neighbors)
    for n in adj[x].neighbors:
        if adj[n].visited == 0:
            dfs(adj, n, order)
    order.append(x)

def toposort(adj):
    order = []
    for i in range(len(adj)):
        if adj[i].visited == 0:
            dfs(adj, i, order)
    order.reverse()
    return order

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex(0, []) for _ in range(n)]
    for i in range(m):
        a, b = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
    order = toposort(adj)
    for x in order:
        print(x + 1, end=' ')

