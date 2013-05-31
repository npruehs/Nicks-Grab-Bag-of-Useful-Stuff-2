// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphI.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// <para>
    /// Implementation of a graph G = (V, E) where V denotes the set of
    /// vertices and E the set of edges with integer edge weights between these vertices.
    /// </para>
    /// <para>
    /// The edges E of the graph are stored as adjacency list, making this
    /// implementation fast at enumerating vertex neighbors, but slow at
    /// accessing specific edges for dense graphs (which have many edges
    /// between each pair of vertices).
    /// </para>
    /// <para>
    /// This implementation can be used for representing either directed or
    /// undirected graphs (calling <see cref="AddEdge(int, int)" /> and
    /// <see cref="AddDirectedEdge(int, int)" />, respectively).
    /// </para>
    /// <para>
    /// // TODO Check multigraph and loops.
    /// This implementation allows adding multiple edges between two vertices,
    /// thus being feasible for modeling multi-graphs. Also, it allows creating
    /// loops, edges whose source and target vertex are identical.
    /// </para>
    /// </summary>
    [CLSCompliant(true)]
    public class GraphI : IWeightedGraph<int, int>
    {
        #region Fields

        /// <summary>
        /// Graph implementation method calls are delegated to.
        /// </summary>
        private readonly Graph<IntVertex, int> graph;

        /// <summary>
        /// Vertices of this graph.
        /// </summary>
        private readonly IntVertex[] vertices;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Creates a new graph without edges.
        /// </summary>
        /// <param name="vertexCount">
        /// Number of vertices of the new graph.
        /// </param>
        public GraphI(int vertexCount)
        {
            this.vertices = new IntVertex[vertexCount];

            for (var i = 0; i < vertexCount; i++)
            {
                this.vertices[i] = new IntVertex(i);
            }

            this.graph = new Graph<IntVertex, int>(this.vertices);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Number of edges between the vertices of this graph.
        /// </summary>
        public int EdgeCount
        {
            get
            {
                return this.graph.EdgeCount;
            }
        }

        /// <summary>
        /// Number of vertices of this graph.
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
        /// Adds a directed edge with the specified weight between two vertices in this graph in O(1).
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge.
        /// </param>
        /// <param name="edgeWeight">
        /// Weight of the edge to add.
        /// </param>
        public void AddDirectedEdge(int source, int target, int edgeWeight)
        {
            this.graph.AddDirectedEdge(this.vertices[source], this.vertices[target], edgeWeight);
        }

        /// <summary>
        /// Adds a directed edge with weight 1 between two vertices in this graph in O(1).
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge.
        /// </param>
        public void AddDirectedEdge(int source, int target)
        {
            this.graph.AddDirectedEdge(this.vertices[source], this.vertices[target], 1);
        }

        /// <summary>
        /// Adds an undirected edge with the specified weight between two vertices in this graph in O(1).
        /// </summary>
        /// <param name="source">
        /// First vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Second vertex of the edge.
        /// </param>
        /// <param name="edgeWeight">
        /// Weight of the edge to add.
        /// </param>
        public void AddEdge(int source, int target, int edgeWeight)
        {
            this.graph.AddEdge(this.vertices[source], this.vertices[target], edgeWeight);
        }

        /// <summary>
        /// Adds an undirected edge with weight 1 between two vertices in this graph in O(1).
        /// </summary>
        /// <param name="source">
        /// First vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Second vertex of the edge.
        /// </param>
        public void AddEdge(int source, int target)
        {
            this.graph.AddEdge(this.vertices[source], this.vertices[target], 1);
        }

        /// <summary>
        /// Gets the adjacent vertices of the specified vertex in this graph in O(1).
        /// </summary>
        /// <param name="vertex">
        /// Vertex to get the neighbors of.
        /// </param>
        /// <returns>
        /// Neighbors of the given vertex.
        /// </returns>
        public ICollection<int> AdjacentVertices(int vertex)
        {
            IEnumerable<IntVertex> adjacentVertices = this.graph.AdjacentVertices(this.vertices[vertex]);
            return adjacentVertices.Select(intVertex => intVertex.Index).ToList();
        }

        /// <summary>
        /// Returns the degree of the given vertex,
        /// the number of adjacent vertices, in O(1).
        /// </summary>
        /// <param name="vertex">
        /// Vertex to get the degree of.
        /// </param>
        /// <returns>
        /// Degree of the vertex.
        /// </returns>
        public int Degree(int vertex)
        {
            return this.graph.Degree(this.vertices[vertex]);
        }

        /// <summary>
        /// Gets the weight of the first edge between the specified vertices
        /// in O(n), where n is the number of adjacent vertices of the first one.
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge to get the weight of.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge to get the weight of.
        /// </param>
        /// <returns>
        /// Weight of the first edge between the two vertices, if there is one,
        /// and 0 otherwise.
        /// </returns>
        public int GetEdge(int source, int target)
        {
            return this.graph.GetEdge(this.vertices[source], this.vertices[target]);
        }

        /// <summary>
        /// Checks if there is an edge between two vertices of this
        /// graph in O(n), where n is the number of adjacent vertices
        /// of the first one.
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
        public bool HasEdge(int source, int target)
        {
            return this.graph.HasEdge(this.vertices[source], this.vertices[target]);
        }

        /// <summary>
        /// Gets the weights of all edges that are incident to the specified vertex.
        /// </summary>
        /// <param name="vertex">
        /// Vertex to get the weights of the incident edges of.
        /// </param>
        /// <returns>
        /// Weights of the incident edges of the specified vertex.
        /// </returns>
        public ICollection<int> IncidentEdges(int vertex)
        {
            return this.graph.IncidentEdges(this.vertices[vertex]);
        }

        #endregion
    }
}