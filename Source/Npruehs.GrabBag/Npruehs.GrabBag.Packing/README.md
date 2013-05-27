2D Packing
==========

We consider the problem of two-dimensional strip packing: The input is a set
of items, in this case rectangles. The goal is to pack all items into a strip
of unlimited height, minimizing the maximum height used, in such a way that
the sides of all items are aligned with the sides the strip. The quality of
strip packing algorithms is measured in terms of the
[optimal solution](http://en.wikipedia.org/wiki/Approximation_algorithms#Performance_guarantees).
Applications include cutting objects out of a strip of material minimizing the
waste.

In this implementation, items that have to be packed are represented by
rectangles. While the initial position of these rectangles doesn't 
matter for the packing algorithms, their size does.

```csharp
List<PackingItem> items = new List<PackingItem>();

items.Add(new PackingItem(0, 0.3f, 0.5f));
items.Add(new PackingItem(1, 0.5f, 0.4f));
items.Add(new PackingItem(2, 0.4f, 0.8f));
```

Given a strip width, each algorithm is able to find a solution for an
arbitrary list of items. Some of the algorithms, such as the Epstein/van Stee,
rotate items in order to approximate a better solution.

```csharp
FirstFitDecreasingHeight ffdh = new FirstFitDecreasingHeight();
float stripWidth = 1.0f;

ffdh.FindSolution(stripWidth, items);
```

The solution, in other words the positions of all packed items, can be
accessed by enumerating the algorithm instance.

```csharp
foreach (PackingItem item in ffdh)
{
    if (item.Rectangle.X + item.Rectangle.Width > stripWidth)
    {
        Assert.Fail("Item {0} exceeds strip width {1}.", item, stripWidth);
    }
}
```

The algorithms in this library belong to the class of level-oriented
packing algorithms. After having found a solution, you may enumerate the
solution levels as well, accessing their items and y-position.
