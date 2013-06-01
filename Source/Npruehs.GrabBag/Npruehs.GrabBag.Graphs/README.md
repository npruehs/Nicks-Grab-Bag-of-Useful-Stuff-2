Graphs
======

Graphs are pairs G = (V, E) where V denotes some set of vertices and E some set
of edges between these vertices. They provide the following operations:

* _AddEdge(source, target)_ adds an undirected edge between two vertices in the
graph.
* _AddDirectedEdge(source, target)_ adds a directed edge between two vertices in
the graph.
* _HasEdge(source, target)_ checks if there is an edge between two vertices of
the graph.
* _Degree(vertex)_ gets the number of adjacent vertices of the specified vertex.
* _AdjacentVertices(vertex)_ gets the adjacent vertices of the specified vertex
in the graph.

In weighted graphs, you may specify arbitrary additional information for the
edges you add:

* _AddEdge(source, target, edge)_ adds the specified undirected edge between
two vertices in the graph.
* _AddDirectedEdge(source, target, edge)_ adds the specified directed edge
between two vertices in the graph.
* _GetEdge(source, target)_ gets the first edge between the specified
vertices.
* _IncidentEdges(vertex)_ gets all edges that are incident to the specified
vertex.

This implementation stores the set of edges of the graph as adjacency list,
making it fast at enumerating vertex neighbors, but slows at accessing
specific edges for dense graphs (which have many edges between each pair
of vertices. It allows adding multiple edges between two vertices,
thus being feasible for modeling multi-graphs. Also, it allows creating
loops, edges whose source and target vertex are identical.

Usage
-----

In most cases, using the `GraphI` and `GraphF` classes will be sufficient.
Both assign unique indices to graph vertices, and they use integer and
real edge weights, respectively.

```csharp
GraphI graph = new GraphI(10);
graph.AddEdge(1, 2, 40);
```

Each graph implementation features methods for accessing its edges:

```csharp
Assert.True(graph.HasEdge(1, 2));
Assert.AreEqual(40, graph.GetEdge(1, 2));
```

Enumerating all neighbors of a graph vertex is easy:

```csharp
var neighbors = this.graph.AdjacentVertices(2);
```

Additionally, weighted graphs allow you to access all incident edges of a
vertex.

```csharp
var edgeWeights = this.graph.IncidentEdges(2);
```

In some cases, you might need to define your own vertex and/or edge types.
Graph vertices need a unique index at least, as defined by the `IVertex`
interface. For graph edges, you can specify any type of your choice.

```csharp
public class AStarNode : IVertex
{
    public int Index { get; set; }

    public AStarNode Parent { get; set; }

    public int F { get; set; }
    public int G { get; set; }
    public int H { get; set; }
}

// Create list of A* nodes...

Graph<AStarNode, int> graph = new Graph<AStarNode, int>(nodes);
```
