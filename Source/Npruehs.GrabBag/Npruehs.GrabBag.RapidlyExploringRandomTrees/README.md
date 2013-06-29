Rapidly-Exploring Random Trees
==============================

_Rapidly-Exploring Random Trees_ are a randomized data structure designed by
Steven M. LaValle for a broad class of path planning problems.

Each tree lives in a configuration space that allows making motions from one
configuration towards another, and has a distance function imposed on. Given
a configuration space and an initial configuration, the tree can grow around
that configuration until it is unable to make any further motions.

This implementation features a 2-dimensional Euclidean configuration space as
an example. In this space, both configurations and motions are represented by
two-dimensional vectors.

```csharp
// Construct a new bounded 2-dimensional Euclidean configuration space.
var configurationSpaceSize = 300;
var stepSize = 5;

var configurationSpace = new ConfigurationSpace2I
	(configurationSpaceSize, configurationSpaceSize, stepSize);

// Start at the center of the configuration space.
var qInit = new Vector2I
	(configurationSpaceSize / 2, configurationSpaceSize / 2);

// Grow a new RRT.
var configurationCount = 50;

var tree = new RapidlyExploringRandomTree<Vector2I, Vector2I>();
tree.GrowTree(configurationSpace, qInit, configurationCount);
```

After having grown the tree, you can easily access its vertices and edges.

```csharp
foreach (var edge in tree.Edges)
{
    // Do something.
}
```
