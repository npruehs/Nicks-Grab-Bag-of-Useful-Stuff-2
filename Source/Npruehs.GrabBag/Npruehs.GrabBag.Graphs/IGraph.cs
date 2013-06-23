// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGraph.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// <para>
    /// Unweighted graph G = (V, E) where V denotes the set of
    /// vertices and E the set of edges between these vertices.
    /// </para>
    /// <para>
    /// Implementing classes can be used for representing either directed or
    /// undirected graphs (calling <see cref="AddEdge(TVertex,TVertex)"/> and
    /// <see cref="AddDirectedEdge(TVertex,TVertex)"/>, respectively).
    /// </para>
    /// <para>
    /// Whether multi-graphs and loops are supported depends on the actual
    /// implementation.
    /// </para>
    /// </summary>
    /// <typeparam name="TVertex">
    /// Type of the vertices of this graph.
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IGraph<TVertex>
    {
        #region Public Properties

        /// <summary>
        /// Number of edges between the vertices of this graph.
        /// </summary>
        int EdgeCount { get; }

        /// <summary>
        /// Number of vertices of this graph.
        /// </summary>
        int VertexCount { get; }

        /// <summary>
        /// Vertices of this graph.
        /// </summary>
        IList<TVertex> Vertices { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds a directed edge between two vertices in this graph.
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge.
        /// </param>
        void AddDirectedEdge(TVertex source, TVertex target);

        /// <summary>
        /// Adds an undirected edge between two vertices in this graph.
        /// </summary>
        /// <param name="source">
        /// First vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Second vertex of the edge.
        /// </param>
        void AddEdge(TVertex source, TVertex target);

        /// <summary>
        /// Gets the adjacent vertices of the specified vertex in this graph.
        /// </summary>
        /// <param name="vertex">
        /// Vertex to get the neighbors of.
        /// </param>
        /// <returns>
        /// Neighbors of the given vertex.
        /// </returns>
        ICollection<TVertex> AdjacentVertices(TVertex vertex);

        /// <summary>
        /// Returns the degree of the given vertex,
        /// the number of adjacent vertices.
        /// </summary>
        /// <param name="vertex">
        /// Vertex to get the degree of.
        /// </param>
        /// <returns>
        /// Degree of the vertex.
        /// </returns>
        int Degree(TVertex vertex);

        /// <summary>
        /// Checks if there is an edge between two vertices of this graph.
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge to check.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge to check.
        /// </param>
        /// <returns>
        /// True if there is an edge, and false otherwise.
        /// </returns>
        bool HasEdge(TVertex source, TVertex target);

        #endregion
    }
}