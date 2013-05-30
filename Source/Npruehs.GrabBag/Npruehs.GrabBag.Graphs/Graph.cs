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
    /// <para>
    /// Implementation of a graph G = (V, E) where V denotes the set of
    /// vertices and E the set of edges between these vertices.
    /// </para>
    /// <para>
    /// The edges E of the graph are stored as adjacency list, making this
    /// implementation fast at enumerating vertex neighbors, but slow at
    /// accessing specific edges for dense graphs (which have many edges
    /// between each pair of vertices).
    /// </para>
    /// <para>
    /// This implementation can be used for representing either directed or
    /// undirected graphs (calling <see cref="AddEdge(Vertex,Vertex)"/> and
    /// <see cref="AddDirectedEdge(Vertex,Vertex)"/>, respectively).
    /// </para>
    /// <para>
    /// The weight of the edges between vertices depends on the specified edge
    /// type. Calling <see cref="AddEdge(Vertex,Vertex)"/> without specifying
    /// an edge creates a default edge between both vertices, usually resulting
    /// in an unweighted graph.
    /// </para>
    /// <para>
    /// // TODO Check multigraph and loops.
    /// This implementation allows adding multiple edges between two vertices,
    /// thus being feasible for modeling multi-graphs. Also, it allows creating
    /// loops, edges whose source and target vertex are identical.
    /// </para>
    /// </summary>
    /// <typeparam name="Vertex">
    /// Type of the vertices of this graph.
    /// </typeparam>
    /// <typeparam name="Edge">
    /// Type of the edges of this graph.
    /// </typeparam>
    [CLSCompliant(true)]
    public class Graph<Vertex, Edge> : IWeightedGraph<Vertex, Edge>
        where Vertex : IVertex
    {
        #region Fields

        /// <summary>
        /// Neighbors of all vertices of this graph.
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
        /// Creates a new graph without edges.
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

            // Initially there are no edges.
            this.edges = new List<Edge>[this.vertexCount];
            this.adjacencyList = new List<Vertex>[this.vertexCount];

            for (int i = 0; i < this.vertexCount; i++)
            {
                this.edges[i] = new List<Edge>();
                this.adjacencyList[i] = new List<Vertex>();
            }

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
        /// Adds the specified directed edge between two vertices in this graph in O(1).
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
        public void AddDirectedEdge(Vertex source, Vertex target, Edge edge)
        {
            this.edges[source.Index].Add(edge);
            this.adjacencyList[source.Index].Add(target);

            this.edgeCount++;
        }

        /// <summary>
        /// Adds the default directed edge between two vertices in this graph in O(1).
        /// </summary>
        /// <param name="source">
        /// Source vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Target vertex of the edge.
        /// </param>
        public void AddDirectedEdge(Vertex source, Vertex target)
        {
            this.AddDirectedEdge(source, target, default(Edge));
        }

        /// <summary>
        /// Adds the default undirected edge between two vertices in this graph in O(1).
        /// </summary>
        /// <param name="source">
        /// First vertex of the edge.
        /// </param>
        /// <param name="target">
        /// Second vertex of the edge.
        /// </param>
        public void AddEdge(Vertex source, Vertex target)
        {
            this.AddEdge(source, target, default(Edge));
        }

        /// <summary>
        /// Adds the specified undirected edge between two vertices in this graph in O(1).
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
        public void AddEdge(Vertex source, Vertex target, Edge edge)
        {
            this.AddDirectedEdge(source, target, edge);
            this.AddDirectedEdge(target, source, edge);
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
        public IEnumerable<Vertex> AdjacentVertices(Vertex vertex)
        {
            return this.adjacencyList[vertex.Index];
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
        public int Degree(Vertex vertex)
        {
            return this.edges[vertex.Index].Count;
        }

        /// <summary>
        /// Gets the first edge between the specified vertices
        /// in O(n), where n is the number of adjacent vertices of the first one.
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
        public Edge GetEdge(Vertex source, Vertex target)
        {
            // Look at the list of neighbors of the first vertex and get
            // the list index of the second vertex there.
            int listIndex = this.adjacencyList[source.Index].IndexOf(target);

            // If there is no edge between the specified vertices, return default edge.
            if (listIndex < 0)
            {
                return default(Edge);
            }

            // Return the edge at the same index in the list of edges.
            return this.edges[source.Index][listIndex];
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
        public bool HasEdge(Vertex source, Vertex target)
        {
            return this.adjacencyList[source.Index].Contains(target);
        }

        /// <summary>
        /// Gets all edges that are incident to the specified vertex.
        /// </summary>
        /// <param name="vertex">
        /// Vertex to get the incident edges of.
        /// </param>
        /// <returns>
        /// Incident edges of the specified vertex.
        /// </returns>
        public IEnumerable<Edge> IncidentEdges(Vertex vertex)
        {
            return this.edges[vertex.Index];
        }

        #endregion
    }
}