// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FredmanTarjan.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.MinimumSpanningTrees.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Npruehs.GrabBag.Graphs;
    using Npruehs.GrabBag.PriorityQueues;
    using Npruehs.GrabBag.Util;

    /// <summary>
    /// <para>
    /// Implementation of the minimum spanning tree algorithm presented in
    ///     "Fibonacci Heaps and Their Uses in Improved Network Optimization Algorithms"
    ///     by Michael L. Fredman and Robert Endre Tarjan in 1985.
    /// </para>
    /// <para>
    /// Finds a minimum spanning tree of the input graph in O(m ß(m,n))
    ///     where ß(m,n) = min{ i | log(i) n &lt;= (m/n)}
    ///     and log(i) n is defined inductively by
    /// <list type="bullet">
    /// <item>
    /// <description>log(0) n = n</description>
    /// </item>
    /// <item>
    /// <description>log(i + 1) n = log log(i) n.</description>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// Assumes that the input graph is a connected undirected graph with
    ///     non-negative edge weights.
    /// </para>
    /// </summary>
    /// <typeparam name="TVertex">
    /// Type of the vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TEdge">
    /// Type of the edges of the graph.
    /// </typeparam>
    [CLSCompliant(true)]
    public class FredmanTarjan<TVertex, TEdge> : IMinimumSpanningTreeAlgorithm<TVertex, TEdge>
        where TVertex : IVertex where TEdge : IFredmanTarjanEdge, new()
    {
        #region Fields

        /// <summary>
        /// Maps vertex indices to their containing trees.
        /// </summary>
        private Dictionary<int, Tree> containingTree;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Computes and returns a minimum spanning tree of the passed weighted,
        /// undirected graph.
        /// </summary>
        /// <param name="graph">
        /// Graph to compute a minimum spanning tree of.
        /// </param>
        /// <returns>
        /// Minimum spanning tree of <paramref name="graph"/>.
        /// </returns>
        public IWeightedGraph<TVertex, TEdge> FindSolution(IWeightedGraph<TVertex, TEdge> graph)
        {
            // Check arguments.
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            // Prepare constructing the minimum spanning tree.
            var mst = new Graph<TVertex, TEdge>(graph.Vertices);

            // Initialize the forest to contain each of the n vertices as a one-vertex tree.
            var initialTrees = graph.Vertices.Select(vertex => new Tree(vertex)).ToList();

            // Get a list of all edges in the original graph.
            var initialEdges = new List<TEdge>();

            foreach (var vertex in graph.Vertices)
            {
                initialEdges.AddRange(graph.IncidentEdges(vertex));
            }

            // Compute minimum spanning tree.
            return this.Pass(mst, graph.EdgeCount, initialTrees, initialEdges);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the old tree containing the vertex with the specified index.
        /// </summary>
        /// <param name="v">
        /// Index of the vertex to get the old tree of.
        /// </param>
        /// <returns>
        /// Old tree containing the vertex with the specified index.
        /// </returns>
        private Tree GetTree(int v)
        {
            return this.containingTree[v];
        }

        /// <summary>
        /// Single pass of the algorithm. Begins with a forest of previously
        ///     grown trees, the <paramref name="oldTrees"/>, and connects there
        ///     old trees into new larger trees that become the old trees for
        ///     the next pass, until only one tree remains.
        /// </summary>
        /// <param name="mst">
        /// Current iteration of the minimum spanning tree.
        /// </param>
        /// <param name="m">
        /// Number of edges of the original graph.
        /// </param>
        /// <param name="oldTrees">
        /// Old trees for this pass.
        /// </param>
        /// <param name="oldEdges">
        /// Edges of the original graph that have not been discarded yet.
        /// </param>
        /// <returns>
        /// Minimum spanning tree of the original graph.
        /// </returns>
        private Graph<TVertex, TEdge> Pass(
            Graph<TVertex, TEdge> mst, int m, IReadOnlyList<Tree> oldTrees, List<TEdge> oldEdges)
        {
            // Check for termination.
            if (oldTrees.Count == 1)
            {
                return mst;
            }

            this.containingTree = new Dictionary<int, Tree>();

            for (var i = 0; i < oldTrees.Count; i++)
            {
                // Assign unique indices to old trees.
                var oldTree = oldTrees[i];

                oldTree.Index = i;

                // Remember trees containing vertices.
                foreach (var v in oldTree.Vertices)
                {
                    this.containingTree[v.Index] = oldTree;
                }
            }

            /*
             * Cleanup.
             * Discard every edge connecting two vertices in the same old tree
             * and all but a minimum-cost edge connecting each pair of old
             * trees.
            */

            /* 
             * First radix-sort pass: Sort the edges lexicographically on the
             * numbers of trees containing their sources.
             */
            var a = oldEdges.ToArray();
            var k = 32;

            ListUtils.RadixSort(a, 0, a.Length - 1, k, edge => this.GetTree(edge.Source).Index);

            /*
             * Second radix-sort pass: Sort edges whose sources are within
             * the same tree by the numbers of trees containing their targets.
             */
            var l = 0;
            var r = 0;
            var t = 0;

            while (r < a.Length)
            {
                l = r;
                t = this.GetTree(a[r].Source).Index;

                // Look for the first edge with its source in another tree.
                while (r < a.Length && this.GetTree(a[r].Source).Index == t)
                {
                    r++;
                }

                ListUtils.RadixSort(a, l, r - 1, k, edge => this.GetTree(edge.Target).Index);
            }

            // Scan the sorted edge list, saving only the appropriate edges.
            var newEdges = new List<TEdge>();

            var currentSource = this.GetTree(a[0].Source).Index;
            var currentTarget = this.GetTree(a[0].Source).Index;
            var currentWeight = a[0].Weight;
            var currentMinimumEdgeIndex = 0;

            // Iterate over all edges.
            for (var i = 1; i < a.Length; i++)
            {
                // Check if the current edge originates from the next tree.
                if (this.GetTree(a[i].Source).Index != currentSource)
                {
                    // If the minimum edge connects two trees, save it.
                    if (this.GetTree(a[currentMinimumEdgeIndex].Source).Index
                        != this.GetTree(a[currentMinimumEdgeIndex].Target).Index)
                    {
                        newEdges.Add(a[currentMinimumEdgeIndex]);
                    }

                    // Start looking for the minimum edge from the new source tree.
                    currentSource = this.GetTree(a[i].Source).Index;
                    currentTarget = this.GetTree(a[i].Target).Index;
                    currentWeight = a[i].Weight;
                    currentMinimumEdgeIndex = i;
                }
                else if (this.GetTree(a[i].Target).Index != currentTarget)
                {
                    // Current edge targets the next tree. If the minimum edge connects two trees, save it.
                    if (this.GetTree(a[currentMinimumEdgeIndex].Source).Index
                        != this.GetTree(a[currentMinimumEdgeIndex].Target).Index)
                    {
                        newEdges.Add(a[currentMinimumEdgeIndex]);
                    }

                    // Start looking for the minimum edge to the new target tree.
                    currentTarget = this.GetTree(a[i].Target).Index;
                    currentWeight = a[i].Weight;
                    currentMinimumEdgeIndex = i;
                }
                else
                {
                    // Check if the weight of the current edge is less than the one of the edges before.
                    if (a[i].Weight.CompareTo(currentWeight) < 0)
                    {
                        currentWeight = a[i].Weight;
                        currentMinimumEdgeIndex = i;
                    }
                }
            }

            // Process the last edge individually: Does it connect other trees?
            if ((this.GetTree(a[a.Length - 1].Source).Index != this.GetTree(a[a.Length - 2].Source).Index)
                || (this.GetTree(a[a.Length - 1].Target).Index != this.GetTree(a[a.Length - 2].Target).Index))
            {
                // If it connects two trees, save it.
                if (this.GetTree(a[a.Length - 1].Source).Index != this.GetTree(a[a.Length - 1].Target).Index)
                {
                    newEdges.Add(a[a.Length - 1]);
                }
            }

            /* 
             * Construct a list for each old tree T of the edges with one
             * endpoint in T (each edge will appear in two such lists).
             */
            foreach (var oldTree in oldTrees)
            {
                oldTree.Edges.Clear();
            }

            foreach (var e in newEdges)
            {
                /* 
                 * We only need to add every edge once, because we are modeling
                 * undirected graphs by adding every edge {v,w} twice:
                 * (v,w) and (w,v).
                 */
                this.GetTree(e.Source).Edges.Add(e);
            }

            // Get the number of trees before the pass.
            t = oldTrees.Count;
            k = (int)Math.Pow(2, 2 * m / t);

            // Give every old tree a key of INFINITY und unmark it.
            var unmarkedOldTrees = new LinkedList<Tree>();

            foreach (var oldTree in oldTrees)
            {
                oldTree.Key = float.PositiveInfinity;
                oldTree.Marked = false;

                unmarkedOldTrees.AddLast(oldTree);
            }

            // Create an empty heap.
            var fibonacciHeap = new FibonacciHeap<Tree>();
            var fibonacciHeapItems = new FibonacciHeapItem<Tree>[t];

            // Prepare a list holding the trees for the next pass.
            var newTrees = new List<Tree>();

            // Repeat tree-growing step until there are no unmarked old trees.
            while (unmarkedOldTrees.Count != 0)
            {
                // Grow a New Tree.

                // Select any unmarked old tree T0.
                var T0 = unmarkedOldTrees.First.Value;
                unmarkedOldTrees.RemoveFirst();

                while (T0.Marked && unmarkedOldTrees.Count != 0)
                {
                    T0 = unmarkedOldTrees.First.Value;
                    unmarkedOldTrees.RemoveFirst();
                }

                if (T0.Marked)
                {
                    continue;
                }

                newTrees.Add(T0);

                // Insert it as an item into the heap with a key of zero.
                fibonacciHeap.Insert(T0, 0);
                T0.Key = 0;

                do
                {
                    // Connect to Starting Tree.

                    // Delete an old T of minimum key from the heap.
                    var T = fibonacciHeap.DeleteMin().Item;

                    // Set key(T) = NegativeInfinity.
                    T.Key = float.NegativeInfinity;

                    // If T != T0, add e(T) to the forest.
                    if (T != T0)
                    {
                        mst.AddDirectedEdge(
                            mst[T.e.Source], 
                            mst[T.e.Target], 
                            new TEdge { Source = T.e.Source, Target = T.e.Target, Weight = T.e.Weight });

                        mst.AddDirectedEdge(
                            mst[T.e.Target], 
                            mst[T.e.Source], 
                            new TEdge { Source = T.e.Target, Target = T.e.Source, Weight = T.e.Weight });

                        // All vertices of T belong to T0 now.
                        T0.Vertices.AddRange(T.Vertices);
                    }

                    /*
                     * If T is marked, stop growing the current tree and
                     * finish the growth step as described below.
                     */
                    if (T.Marked)
                    {
                        break;
                    }

                    // Otherwise mark T.
                    T.Marked = true;

                    /*
                     *  For each edge (v,w) with v in T and 
                     *  c(v,w) < key(tree(w)): Set e(tree(w)) = (v,w).
                     */
                    foreach (var edge in T.Edges)
                    {
                        var treeW = this.GetTree(edge.Target);
                        var cvw = edge.Weight;

                        if (cvw.CompareTo(treeW.Key) >= 0)
                        {
                            continue;
                        }

                        treeW.e = edge;

                        /* 
                         * If key(tree(w)) = INFINITY, insert tree(w) in
                         * the heap with a redefined key of c(v,w).
                         */
                        if (float.IsInfinity(treeW.Key))
                        {
                            /*
                             * Check using Double.isInfinite is enough,
                             * because NegativeInfinity would never be
                             * greater than cVW.
                             */
                            treeW.Key = cvw;
                            fibonacciHeapItems[treeW.Index] = fibonacciHeap.Insert(treeW, cvw);
                        }
                        else
                        {
                            // Decrease the key of tree(w) to c(v,w).
                            treeW.Key = cvw;
                            fibonacciHeap.DecreaseKeyTo(fibonacciHeapItems[treeW.Index], cvw);
                        }
                    }
                }
                while (!fibonacciHeap.Empty && fibonacciHeap.Count <= k);

                // Finish growing step: Empty the heap...
                fibonacciHeap.Clear();

                // ...and set key(T) = PositiveInfinity for every old tree T with finite key.
                foreach (var oldTree in oldTrees.Where(oldTree => !float.IsInfinity(oldTree.Key)))
                {
                    oldTree.Key = float.PositiveInfinity;
                }
            }

            // Next pass.
            return this.Pass(mst, m, newTrees, newEdges);
        }

        #endregion

        /// <summary>
        /// Tree of the forest that finally will become the minimum spanning tree of the input graph.
        /// </summary>
        private class Tree
        {
            #region Fields

            /// <summary>
            /// Edge that will added to the forest as soon as this tree is removed from the heap.
            /// </summary>
            public TEdge e;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Constructs a new tree containing the vertex with the passed index only.
            /// The tree initially has no number, edges or key and is not marked.
            /// </summary>
            /// <param name="v">
            /// Initial vertex of the new tree.
            /// </param>
            public Tree(TVertex v)
            {
                this.Vertices = new List<TVertex>();
                this.Edges = new List<TEdge>();

                this.Vertices.Add(v);
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Edges with one endpoint in this tree.
            /// </summary>
            public List<TEdge> Edges { get; private set; }

            /// <summary>
            /// Unique index of this tree.
            /// </summary>
            public int Index { get; set; }

            /// <summary>
            /// Key of this tree in the heap.
            /// </summary>
            public float Key { get; set; }

            /// <summary>
            /// Whether this tree is marked, or not.
            /// </summary>
            public bool Marked { get; set; }

            /// <summary>
            /// Vertices of this tree.
            /// </summary>
            public List<TVertex> Vertices { get; private set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     Gets the unique index of this tree.
            /// </summary>
            /// <returns>Unique index of this tree.</returns>
            public override string ToString()
            {
                return this.Index.ToString(CultureInfo.InvariantCulture);
            }

            #endregion
        }
    }
}