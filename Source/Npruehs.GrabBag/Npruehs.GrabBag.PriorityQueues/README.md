Priority Queues
===============

Priority Queues (or heaps) consist of a set of items, each with a real-valued
key, and provide the following operations:

* _Clear()_ clears the priority queue, removing all items.
* _Insert(item, key)_ inserts the passed item with the specified key into the
priority queue.
* _FindMin()_ returns the item with the minimum key in the priority queue.
* _DeleteMin()_ deletes and returns the item with the minimum key in the
priority queue.
* _DecreaseKey(item, delta)_ decreases the key of the specified item in the
priority queue by subtracting the passed non-negative real number _delta_.
* _DecreaseKeyTo(item, newKey)_ decreases the key of the specified item in the
priority queue to the passed non-negative real number.
* _Delete(item)_ deletes the specified item from the priority queue.

This implementation is based on _Fibonacci Heaps and Their Uses in Improved
Network Optimization Algorithms_ by _Michael L. Fredman_ and _Robert Endre
Tarjan_. Given a heap with _n_ items, deleting an arbitrary item is done in
_O(log n)_ amortized time. All other heap operations are performed in _O(1)_
amortized time. 

The implementation implements the `IEnumerable<T>` interface, enabling you to
iterate through heaps with `foreach`.

Fast heaps are incredibly helpful for speeding up pathfinding algorithms such as
Dijkstra and A*.


