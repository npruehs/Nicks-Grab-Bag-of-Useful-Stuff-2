Union-Find
==========

Union-find structures provide two operations for manipulating disjoint sets:

* _Find(x)_ finds the unique set containing the element x.
* _Union(A, B)_ combines the sets A and B into a new set.

This implementation is based on _Efficiency of a Good But Not Linear Set Union
Algorithm_ by _Robert Endre Tarjan_. Given a union-find universe with _n_
elements, each in a singleton set, a sequence of _m > n finds_ and _n-1_
intermixed _unions_ has a worst-case running time of _O(ma(m, n))_, where
_a(m, n)_ is related to a functional inverse of Ackermann's function and is very
slow-growing.

As union-find universes consist of disjoint sets, it is prohibited to add the
same element more than once. In other words, you're unable to add two objects
`o1` and `o2` such that `o1.Equals(o2)`.

Additionally, you're not allowed to add `null` elements to the structure.

The implementation implements the `IEnumerable<T>` interface, enabling you to
iterate through the structure with `foreach`.

Union-find structures are useful in many contexts, including the computation of
minimum spanning trees.

Usage
-----
Let's assume we want to store strings in a union-find structure. Setting up a
new union-find structure is as easy as

```csharp
IUnionFind<string> unionFind = new UnionFind<string>();
```

Now, let's add two strings as singleton sets to the universe:

```csharp
var itemA = "a";
var itemB = "b";

unionFind.MakeSet(itemA);
unionFind.MakeSet(itemB);
```

Merging the two sets we just created is done with _union_. Note that the
method requires you to pass the canonical elements of the sets:

```csharp
unionFind.Union(itemA, itemB);
```

The canonical element of a set can be accessed by calling _find_. Note that
the question "Are v and w in the same set?" can be reduced to
`find(v) == find(w)`.

```csharp
var findA = unionFind.Find(itemA);
var findB = unionFind.Find(itemB);

Assert.AreEqual(findA, findB);
```
