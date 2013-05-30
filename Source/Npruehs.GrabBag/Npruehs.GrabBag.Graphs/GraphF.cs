// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphF.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The graph f.
    /// </summary>
    [CLSCompliant(true)]
    public class GraphF : IWeightedGraph<int, float>
    {
        #region Fields

        /// <summary>
        /// The graph.
        /// </summary>
        private readonly Graph<IntVertex, float> graph;

        /// <summary>
        /// The vertices.
        /// </summary>
        private readonly IntVertex[] vertices;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphF"/> class.
        /// </summary>
        /// <param name="vertexCount">
        /// The vertex count.
        /// </param>
        public GraphF(int vertexCount)
        {
            this.vertices = new IntVertex[vertexCount];

            for (var i = 0; i < vertexCount; i++)
            {
                this.vertices[i] = new IntVertex(i);
            }

            this.graph = new Graph<IntVertex, float>(this.vertices);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the edge count.
        /// </summary>
        public int EdgeCount
        {
            get
            {
                return this.graph.EdgeCount;
            }
        }

        /// <summary>
        /// Gets the vertex count.
        /// </summary>
        public int VertexCount
        {
            get
            {
                return this.graph.VertexCount;
            }
        }

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
        /// <param name="edge">
        /// The edge.
        /// </param>
        public void AddDirectedEdge(int source, int target, float edge)
        {
            this.graph.AddDirectedEdge(this.vertices[source], this.vertices[target], edge);
        }

        /// <summary>
        /// The add directed edge.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        public void AddDirectedEdge(int source, int target)
        {
            this.graph.AddDirectedEdge(this.vertices[source], this.vertices[target], 1.0f);
        }

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
        public void AddEdge(int source, int target, float edge)
        {
            this.graph.AddEdge(this.vertices[source], this.vertices[target], edge);
        }

        /// <summary>
        /// The add edge.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        public void AddEdge(int source, int target)
        {
            this.graph.AddEdge(this.vertices[source], this.vertices[target], 1.0f);
        }

        /// <summary>
        /// The adjacent vertices.
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<int> AdjacentVertices(int vertex)
        {
            IEnumerable<IntVertex> adjacentVertices = this.graph.AdjacentVertices(this.vertices[vertex]);
            return adjacentVertices.Select(intVertex => intVertex.Index);
        }

        /// <summary>
        /// The degree.
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Degree(int vertex)
        {
            return this.graph.Degree(this.vertices[vertex]);
        }

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
        /// The <see cref="float"/>.
        /// </returns>
        public float GetEdge(int source, int target)
        {
            return this.graph.GetEdge(this.vertices[source], this.vertices[target]);
        }

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
        public bool HasEdge(int source, int target)
        {
            return this.graph.HasEdge(this.vertices[source], this.vertices[target]);
        }

        /// <summary>
        /// The incident edges.
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<float> IncidentEdges(int vertex)
        {
            return this.graph.IncidentEdges(this.vertices[vertex]);
        }

        #endregion

        /// <summary>
        /// The int vertex.
        /// </summary>
        private class IntVertex : IVertex
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="IntVertex"/> class.
            /// </summary>
            /// <param name="index">
            /// The index.
            /// </param>
            public IntVertex(int index)
            {
                this.Index = index;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the index.
            /// </summary>
            public int Index { get; private set; }

            #endregion
        }
    }
}