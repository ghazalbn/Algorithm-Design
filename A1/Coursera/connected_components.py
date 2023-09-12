from collections import namedtuple  
      
# Declaring namedtuple()   
vertex = namedtuple('vertex',['visited','neighbors'])

def reach(adj, x):
    adj[x] = vertex(1, adj[x].neighbors)
    for n in adj[x].neighbors:
        if adj[n].visited == 0:
            reach(adj, n)

def number_of_components(adj):
    result = 0
    for i in range(len(adj)):
        if adj[i].visited == 0:
            reach(adj, i)
            result += 1
    return result

if __name__ == '__main__':
    n, m = map(int, input().split())
    adj = [vertex(0, []) for _ in range(n)]
    for i in range(m):
        a, b = map(int, input().split())
        adj[a - 1].neighbors.append(b - 1)
        adj[b - 1].neighbors.append(a - 1)

    print(number_of_components(adj))

