// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Graph.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The graph.
    /// </summary>
    /// <typeparam name="Vertex">
    /// </typeparam>
    /// <typeparam name="Edge">
    /// </typeparam>
    [CLSCompliant(true)]
    public class Graph<Vertex, Edge> : IWeightedGraph<Vertex, Edge>
        where Vertex : IVertex
    {
        #region Fields

        /// <summary>
        /// The adjacency list.
        /// </summary>
        private readonly List<Vertex>[] adjacencyList;

        /// <summary>
        /// Edges between the vertices of this graph.
        /// </summary>
        private readonly List<Edge>[] edges;

        /// <summary>
        /// Number of vertices of this graph.
        /// </summary>
        private readonly int vertexCount;

        /// <summary>
        /// Vertices of this graph.
        /// </summary>
        private readonly List<Vertex> vertices;

        /// <summary>
        /// Number of edges between the vertices of this graph.
        /// </summary>
        private int edgeCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Default constructor for this implementation of
        /// directed graphs.
        /// Initially there are no edges.
        /// Vertex indices are assumed to be unique and in [0 .. vertices.Count - 1].
        /// </summary>
        /// <param name="vertices">
        /// Vertices of the new graph.
        /// </param>
        public Graph(IEnumerable<Vertex> vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException("vertices");
            }

            // Set vertices.
            this.vertices = vertices.ToList();
            this.vertexCount = this.vertices.Count;
            
            // Check vertex indices.
            var vertexIndices = new HashSet<int>();

            foreach (Vertex vertex in this.vertices)
            {
                if (vertex.Index < 0 || vertex.Index >= this.vertices.Count)
                {
                    throw new ArgumentException(
                        string.Format(
                            "For a set of {0} vertices, vertex indices have to be between 0 and {1}",
                            this.vertexCount,
                            this.vertexCount - 1));
                }

                if (vertexIndices.Contains(vertex.Index))
                {
                    throw new ArgumentException(
                        string.Format("Duplicate vertex index: {0}. Vertex indices have to be unique.", vertex.Index));
                }

                vertexIndices.Add(vertex.Index);
            }

            // Construct new arrays to hold the lists for the edges and
            // edge weights between the vertices of this graph.
            this.edges = new List<Edge>[this.vertexCount];
            this.adjacencyList = new List<Vertex>[this.vertexCount];

            for (int i = 0; i < this.vertexCount; i++)
            {
                // Construct new lists for the edges and
                // edge weights between the vertices of this graph.
                this.edges[i] = new List<Edge>();
                this.adjacencyList[i] = new List<Vertex>();
            }

            // Initially there are no edges.
            this.edgeCount = 0;
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
                return this.edgeCount;
            }
        }

        /// <summary>
        /// Edges between the vertices of this graph.
        /// </summary>
        public List<Edge>[] Edges
        {
            get
            {
                return this.edges;
            }
        }

        /// <summary>
        /// Number of vertices of this graph.
        /// </summary>
        public int VertexCount
        {
            get
            {
                return this.vertexCount;
            }
        }

        /// <summary>
        /// Vertices of this graph.
        /// </summary>
        public IEnumerable<Vertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds one edge between two vertices in this graph in O(1),
        /// weighted with the given value.
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
        public void AddDirectedEdge(Vertex source, Vertex target, Edge edge)
        {
            this.edges[source.Index].Add(edge);
            this.adjacencyList[source.Index].Add(target);

            this.edgeCount++;
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
        public void AddDirectedEdge(Vertex source, Vertex target)
        {
            this.AddDirectedEdge(source, target, default(Edge));
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
        public void AddEdge(Vertex source, Vertex target)
        {
            this.AddEdge(source, target, default(Edge));
        }

        /// <summary>
        /// Adds two edges between two vertices in this graph in O(1),
        /// weighted with the given value.
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
        public void AddEdge(Vertex source, Vertex target, Edge edge)
        {
            this.AddDirectedEdge(source, target, edge);
            this.AddDirectedEdge(target, source, edge);
        }

        /// <summary>
        /// Returns a list containing the adjacent
        /// vertices of a given vertex in this graph in O(1).
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// List with the neighbors of the given vertex.
        /// </returns>
        public IEnumerable<Vertex> AdjacentVertices(Vertex vertex)
        {
            return this.adjacencyList[vertex.Index];
        }

        /// <summary>
        /// Returns the degree of the given vertex,
        /// in other words the number of adjacent vertices, in O(1).
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// Degree of the vertex.
        /// </returns>
        public int Degree(Vertex vertex)
        {
            return this.edges[vertex.Index].Count;
        }

        /// <summary>
        /// Gets the weight of the edge between the specified vertices in O(n),
        /// where n is the number of adjacent vertices of the first one.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// Edge weight of the edge between the two vertices, if there is one,
        /// and -1 otherwise.
        /// </returns>
        public Edge GetEdge(Vertex source, Vertex target)
        {
            // Look at the list of neighbors of the first node and get
            // the list index of the second node there.
            int listIndex = this.adjacencyList[source.Index].IndexOf(target);

            // If there is no edge between the specified vertices, return -1.
            if (listIndex < 0)
            {
                return default(Edge);
            }

            // Return the weight at the same index in the list of weights.
            return this.edges[source.Index][listIndex];
        }

        /// <summary>
        /// Checks if there is an edge between two vertices of this
        /// graph in O(n), where n is the number of adjacent vertices
        /// of the first one.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// True if there is an edge, and false otherwise.
        /// </returns>
        public bool HasEdge(Vertex source, Vertex target)
        {
            return this.adjacencyList[source.Index].Contains(target);
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
        public IEnumerable<Edge> IncidentEdges(Vertex vertex)
        {
            return this.edges[vertex.Index];
        }

        #endregion
    }
}