// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMinimumSpanningTreeAlgorithm.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.MinimumSpanningTrees
{
    using System;

    using Npruehs.GrabBag.Graphs;

    /// <summary>
    /// Algorithm for computing minimum spanning trees: Let G be a connected,
    /// undirected graph with n vertices and m edges (v, w) with non-negative
    /// edge weights c(v, w). A minimum spanning tree of G is a spanning tree
    /// of G of minimum total edge weight.
    /// </summary>
    /// <typeparam name="TVertex">
    /// Type of the graph vertices.
    /// </typeparam>
    /// <typeparam name="TEdge">
    /// Type of the graph edges.
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IMinimumSpanningTreeAlgorithm<TVertex, TEdge>
    {
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
        IWeightedGraph<TVertex, TEdge> FindSolution(IWeightedGraph<TVertex, TEdge> graph);

        #endregion
    }
}