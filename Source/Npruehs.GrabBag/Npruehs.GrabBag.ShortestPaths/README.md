Shortest Paths
==============

Let _G = (V,E)_ be an undirected, connected graph with _|V| = n_ vertices and
_|E| = m_ edges. We call _w: V × V → N_ the positive edge weight function of
_G_ and define _w(u,v) = ∞_ if _(u,v)_ is not in E. The task is to find the
distance _d(v) = dist(v,s)_ from a distinguished source vertex _s ∈ V_ to
all other vertices _v ∈ V_. This classic problem in algorithmic graph theory
is called the single-source shortest paths problem.

Most algorithms for solving this problem are based on the one proposed by
Dijkstra in 1959. 

```csharp
IDijkstraNode[] nodes = new IDijkstraNode[32];
// Fill array.
Graph<IDijkstraNode, int> graph = new Graph<IDijkstraNode, int>(nodes);
// Add edges.
int[] distances = Dijkstra.FindPaths(graph, nodes[0]);
```

The distances from the source vertex to all vertices are returned by the
_FindPaths_ method, while the predecessors are stored in the nodes themselves.

Distances are assumed to be integers. If you're requiring floating point
numbers as distances, it is highly recommended to convert them to integers
in order to increase algorithm performance anyway (e.g. use 10 and 14 instead
of 1 and sqrt(2)).

The A* algorithm uses _heuristics_ in order to find an approximation of the
shortest path between two vertices in a graph, instead of the shortest paths
from a source vertex to all others.

```csharp
List<IAStarNode> path = AStar.FindPath(graph, nodes[0], nodes[31]);
```

The heuristic is specified by implementing the corresponding interface method:

```csharp
int EstimateHeuristicMovementCost(IAStarNode target);
```

That algorithm returns the path as list of nodes instead of any distances.