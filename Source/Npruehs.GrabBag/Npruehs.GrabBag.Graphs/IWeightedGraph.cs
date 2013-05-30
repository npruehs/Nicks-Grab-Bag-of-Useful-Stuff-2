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
    /// The WeightedGraph interface.
    /// </summary>
    /// <typeparam name="Vertex">
    /// </typeparam>
    /// <typeparam name="Edge">
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IWeightedGraph<Vertex, Edge> : IGraph<Vertex>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add directed edge.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="edge">
        /// The edge.
        /// </param>
        void AddDirectedEdge(Vertex source, Vertex target, Edge edge);

        /// <summary>
        /// The add edge.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="edge">
        /// The edge.
        /// </param>
        void AddEdge(Vertex source, Vertex target, Edge edge);

        /// <summary>
        /// The get edge.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="Edge"/>.
        /// </returns>
        Edge GetEdge(Vertex source, Vertex target);

        /// <summary>
        /// The incident edges.
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<Edge> IncidentEdges(Vertex vertex);

        #endregion
    }
}