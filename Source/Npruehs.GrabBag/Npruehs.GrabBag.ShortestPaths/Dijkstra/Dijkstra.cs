// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dijkstra.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Npruehs.GrabBag.ShortestPaths.Dijkstra
{
    using System;
    using System.Collections.Generic;

    using Npruehs.GrabBag.Graphs;
    using Npruehs.GrabBag.PriorityQueues;

    /// <summary>
    /// Implementation of the single-source shortest paths algorithm by Dijkstra.
    /// </summary>
    public static class Dijkstra
    {
        #region Public Methods and Operators

        /// <summary>
        /// Computes the paths from the specified source vertex to all others
        /// in the passed graph.
        /// </summary>
        /// <typeparam name="TVertex">Type of the vertices of the graph.</typeparam>
        /// <param name="graph">Graph to find the paths in.</param>
        /// <param name="source">Source vertex to find all paths from.</param>
        /// <returns>Distances from the source vertex to all others in the graph.</returns>
        public static int[] FindPaths<TVertex>(IWeightedGraph<TVertex, int> graph, TVertex source)
            where TVertex : IDijkstraNode
        {
            IList<TVertex> visitedVertices;
            return FindPaths(graph, source, out visitedVertices);
        }

        /// <summary>
        /// Computes the paths from the specified source vertex to all others
        /// in the passed graph.
        /// </summary>
        /// <typeparam name="TVertex">Type of the vertices of the graph.</typeparam>
        /// <param name="graph">Graph to find the paths in.</param>
        /// <param name="source">Source vertex to find all paths from.</param>
        /// <param name="visitedVertices">Order the graph vertices have been visited in.</param>
        /// <returns>Distances from the source vertex to all others in the graph.</returns>
        public static int[] FindPaths<TVertex>(IWeightedGraph<TVertex, int> graph, TVertex source, out IList<TVertex> visitedVertices)
            where TVertex : IDijkstraNode
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            // Initialize data structures.
            int[] colors = new int[graph.VertexCount];
            int[] distances = new int[graph.VertexCount];

            const int White = 0;
            const int Gray = 1;
            const int Black = 2;

            for (var v = 0; v < graph.VertexCount; v++)
            {
                colors[v] = White;
                distances[v] = -1;
            }

            var q = new FibonacciHeap<TVertex>();
            var items = new FibonacciHeapItem<TVertex>[graph.VertexCount];

            visitedVertices = new List<TVertex>();

            // Mark start vertex as visited.
            colors[source.Index] = Gray;
            items[source.Index] = q.Insert(source, 0);
            visitedVertices.Add(source);

            // Extend distance tree until border is empty.
            while (!q.Empty)
            {
                // Get next vertex for the distance tree and set its distance.
                var item = q.DeleteMin();
                var d = (int)item.Key;
                var v = item.Item;
                distances[v.Index] = d;

                // Mark that vertex as visited.
                colors[v.Index] = Black;

                // Update border and border approximation.
                var a = graph.AdjacentVertices(v);
                var da = graph.IncidentEdges(v);

                for (var i = 0; i < a.Count; i++)
                {
                    // Get next neighbor.
                    var w = a[i];

                    // Update approximation.
                    var dw = d + da[i];

                    // Update entry if necessary.
                    if (colors[w.Index] != White && (colors[w.Index] != Gray || !(items[w.Index].Key > dw)))
                    {
                        continue;
                    }

                    colors[w.Index] = Gray;
                    w.Predecessor = v;

                    if (items[w.Index] == null)
                    {
                        items[w.Index] = q.Insert(w, dw);
                        visitedVertices.Add(w);
                    }
                    else
                    {
                        q.DecreaseKeyTo(items[w.Index], dw);
                    }
                }
            }

            return distances;
        }

        #endregion
    }
}