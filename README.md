Nicks-Grab-Bag-of-Useful-Stuff-2
================================

This repository is my personal version of what Mike McShaffry calls a "Grab Bag
of Useful Stuff": It contains all code snippets that I've written in my life and
which are most likely to be useful in future projects again. All of this code is
more or less well documented and tested. I provide this code free of charge but
WITHOUT ANY WARRANTY - copy and use it for whatever you like, but don't bother
me if your system catches fire.

The Grab Bag is organized as collection of .NET libraries. All of these
libraries have been developed according to the best practices presented in
[Framework Design Guidelines](http://www.amazon.co.uk/Framework-Design-Guidelines-Conventions-Development/dp/0321545613/ref=sr_1_1?ie=UTF8&qid=1364667699&sr=8-1)
by Cwalina and Abrams and adhere to the
[C# Code Convetions](http://msdn.microsoft.com/en-us/library/vstudio/ff926074.aspx).
Thus, if you are familiar with .NET technology, you'll find it easy
to navigate the libraries and find what you're looking for. Either way, the
consistency of the library allows you to transfer your learnings in using my
Grab Bag from one set of features to another. The libraries are all
[CLS-compliant](http://msdn.microsoft.com/en-us/library/bhc3fa7f.aspx) and thus
can be used with any language of the .NET family.

For each data type, the Grab Bag features an interface, a low-level and a
high-level implementation. It is highly recommended to write code against the
interface, as implementation details might change during further development of
the respective library. In most cases you can just use the high-level
implementation and don't worry about the internals.

Sometimes though, especially
if you're writing performance-critial applications, you might wanna base your
code on the low-level implementation. A good example might be the usage of
priority queues. While you can use the high-level PriorityQueue class of the
Npruehs.GrabBag.PriorityQueues namespace, it is possible that the keys of your
heap items are natural numbers anyway, such as the ids of nodes in your A*
pathfinding graph. In this case, you can access the FibonacciHeap class in the
namespace instead and handle the storage of its items yourself - and put them in
an array, for example.

Code quality is ensured by a load of unit tests for all features. If you should
encounter a bug anyway, feel free to file a bug report at Github and/or provide
a unit test yourself for helping me fix the issue as soon as possible. Invalid
arguments or states are communicated by clear exception messages that help you
isolate the problem.

Special thanks go to [Alexander Graefenstein](http://www.ginie.eu),
[Denis Vaz Alves](http://www.xing.com/profile/Denis_VazAlves) and
[Christian Oeing](http://oeing.eu) for the permission to share the code we've
developed together.

Contents
--------

Below you can find an overview of the contents of this Grab Bag. For more
details please refer to the README files located in the respective namespace
folders.

### Priority Queues

Priority Queues (or heaps) consist of a set of items, each with a real-valued
key, and allow inserting new items, finding the minimum item and removing the
minimum item.

Fast heaps are incredibly helpful for speeding up pathfinding algorithms such as
Dijkstra and A*.


### Union-Find

Union-find structures provide two operations for manipulating disjoint sets:
Finding the set containing an element, and combining two sets into a new set.

These structures are useful in many contexts, including the computation of
minimum spanning trees.
