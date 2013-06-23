Minimum Spanning Trees
======================

Let _G = (V, E)_ be a connected, undirected graph with _|V| = n_ vertices and
_|E| = m_ edges _(v, w)_ with non-negative edge weights _c(v, w)_. A _minimum
spanning tree_ of _G_ is a spanning tree of _G_ with minimum total edge weight.

The algorithm presented in "Fibonacci Heaps and Their Uses in Improved Network
Optimization Algorithms" by Michael L. Fredman and Robert Endre Tarjan in 1985
finds a minimum spanning tree of an input graph in _O(m ß(m,n))_, where

_ß(m,n) = min{ i | log(i) n &le; (m/n)}_

and _log(i) n_ is defined inductively by

* _log(0) n = n_
* _log(i + 1) n = log log(i) n_

Passing such a graph to the algorithm will return a graph with the same
vertices, and a subset of edges that make up a minimum spanning tree.

```csharp
var fredmanTarjan = new FredmanTarjan<IntVertex, FloatEdge>();
var solution = fredmanTarjan.FindSolution(graph);
```
