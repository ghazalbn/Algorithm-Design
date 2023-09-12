# python3
import sys
import threading

sys.setrecursionlimit(10**7)
threading.stack_size(2**25)

n, m = map(int, input().split())
clauses = [ list(map(int, input().split())) for i in range(m) ]

class node:
    def __init__(self):
        self.visited = False
        self.c_num = -1
        self.value = -1

# This solution tries all possible 2^n variable assignments.
# It is too slow to pass the problem.
# Implement a more efficient algorithm here.
def build_graph():
    graph = [[] for i in range(2*n + 1)]
    graph_r = [[] for i in range(2*n + 1)]
    adj = [node() for i in range(2*n + 1)]
    for clause in clauses:
        x, y = clause[0], clause[1]
        graph[(x + n) if x>0 else -x].append(y if y>0 else (n - y))
        graph[(y + n) if y>0 else -y].append(x if x>0 else (n - x))
        graph_r[x if x>0 else (n - x)].append((y + n) if y>0 else -y)
        graph_r[y if y>0 else (n - y)].append((x + n) if x>0 else -x)

    return graph, graph_r, adj


def dfs(graph, adj, x, order, num):
    adj[x].visited = True
    adj[x].c_num = num
    for n in graph[x]:
        if not adj[n].visited:
            dfs(graph, adj, n, order, num)
    order.append(x)

def toposort(graph_r, adj):
    order = []
    for i in range(len(graph_r)):
        if not adj[i].visited:
            dfs(graph_r, adj, i, order, 0)
    order.reverse()
    return order

def strongly_connected_components(graph, graph_r, adj):
    # result = 0
    order = toposort(graph_r, adj)
    components = []
    adj = [node() for i in range(2*n + 1)]
    num = 0
    for i in order:
        if not adj[i].visited:
            component = []
            dfs(graph, adj, i, component, num)
            num += 1
            # result += 1
            components.append(component)
    return components, adj


def isSatisfiable():
    graph, graph_r, adj = build_graph()
    components, adj = strongly_connected_components(graph, graph_r, adj)

    for i in range(1, n + 1):
        if adj[i].c_num == adj[i + n].c_num:
            return None
    
    for component in components:
        for v in component:
            if adj[v].value == -1 and v!= 0:
                adj[v].value = 1
                adj[(v - n) if v>n else (v + n)].value = 0
    return adj
    
    

def main():
    result = isSatisfiable()
    if result is None:
        print("UNSATISFIABLE")
    else:
        print("SATISFIABLE");
        print(" ".join(str(i if result[i].value else -i) for i in range(1, n + 1)))


threading.Thread(target=main).start()
