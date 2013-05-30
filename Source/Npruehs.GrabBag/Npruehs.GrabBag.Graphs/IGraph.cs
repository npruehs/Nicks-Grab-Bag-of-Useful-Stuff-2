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
    /// The Graph interface.
    /// </summary>
    /// <typeparam name="Vertex">
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IGraph<Vertex>
    {
        #region Public Properties

        /// <summary>
        /// Gets the edge count.
        /// </summary>
        int EdgeCount { get; }

        /// <summary>
        /// Gets the vertex count.
        /// </summary>
        int VertexCount { get; }

        #endregion

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
        void AddDirectedEdge(Vertex source, Vertex target);

        /// <summary>
        /// The add edge.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        void AddEdge(Vertex source, Vertex target);

        /// <summary>
        /// The adjacent vertices.
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<Vertex> AdjacentVertices(Vertex vertex);

        /// <summary>
        /// The degree.
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Degree(Vertex vertex);

        /// <summary>
        /// The has edge.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool HasEdge(Vertex source, Vertex target);

        #endregion
    }
}