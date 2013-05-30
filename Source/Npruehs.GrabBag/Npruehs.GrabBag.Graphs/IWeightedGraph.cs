// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWeightedGraph.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// <para>
    /// Weighted graph G = (V, E) where V denotes the set of
    /// vertices and E the set of edges between these vertices.
    /// </para>
    /// <para>
    /// Implementing classes can be used for representing either directed or
    /// undirected graphs (calling <see cref="AddEdge(Vertex,Vertex)"/> and
    /// <see cref="AddDirectedEdge(Vertex,Vertex)"/>, respectively).
    /// </para>
    /// <para>
    /// Whether multi-graphs and loops are supported depends on the actual
    /// implementation.
    /// </para>
    /// </summary>
    /// <typeparam name="Vertex">
    /// Type of the vertices of this graph.
    /// </typeparam>
    /// <typeparam name="Edge">
    /// Type of the edges of this graph.
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IWeightedGraph<Vertex, Edge> : IGraph<Vertex>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Adds the specified directed edge between two vertices in this graph.
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge.
        /// </param>
        /// <param name="edge">
        /// Edge to add.
        /// </param>
        void AddDirectedEdge(Vertex source, Vertex target, Edge edge);

        /// <summary>
        /// Adds the specified undirected edge between two vertices in this graph.
        /// </summary>
        /// <param name="source">
        /// First vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Second vertex of the edge.
        /// </param>
        /// <param name="edge">
        /// Edge to add.
        /// </param>
        void AddEdge(Vertex source, Vertex target, Edge edge);

        /// <summary>
        /// Gets the first edge between the specified vertices.
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge to get.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge to get.
        /// </param>
        /// <returns>
        /// First edge between the two vertices, if there is one,
        /// and default edge otherwise.
        /// </returns>
        Edge GetEdge(Vertex source, Vertex target);

        /// <summary>
        /// Gets all edges that are incident to the specified vertex.
        /// </summary>
        /// <param name="vertex">
        /// Vertex to get the incident edges of.
        /// </param>
        /// <returns>
        /// Incident edges of the specified vertex.
        /// </returns>
        IEnumerable<Edge> IncidentEdges(Vertex vertex);

        #endregion
    }
}