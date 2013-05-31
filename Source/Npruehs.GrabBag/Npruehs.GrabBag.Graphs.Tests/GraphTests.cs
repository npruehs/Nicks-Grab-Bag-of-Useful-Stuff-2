// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphTests.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs.Tests
{
    using System;

    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the graphs.
    /// </summary>
    [TestFixture]
    public class GraphTests
    {
        #region Fields

        /// <summary>
        /// Test graph to run unit tests on.
        /// </summary>
        private GraphI graph;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Sets up the test graph to run unit tests on.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.graph = new GraphI(10);
        }

        /// <summary>
        /// Tests adding a directed, unweighted edge to a graph.
        /// </summary>
        [Test]
        public void TestAddDirectedUnweightedEdge()
        {
            var oldEdgeCount = this.graph.EdgeCount;
            this.graph.AddDirectedEdge(1, 2);

            Assert.AreEqual(oldEdgeCount + 1, this.graph.EdgeCount);

            Assert.True(this.graph.HasEdge(1, 2));
            Assert.False(this.graph.HasEdge(2, 1));
        }

        /// <summary>
        /// Tests adding a directed, weighted edge to a graph.
        /// </summary>
        [Test]
        public void TestAddDirectedWeightedEdge()
        {
            var oldEdgeCount = this.graph.EdgeCount;
            this.graph.AddDirectedEdge(1, 2, 40);

            Assert.AreEqual(oldEdgeCount + 1, this.graph.EdgeCount);
            Assert.AreEqual(40, this.graph.GetEdge(1, 2));
        }

        /// <summary>
        /// Tests adding an undirected, unweighted edge to a graph.
        /// </summary>
        [Test]
        public void TestAddUndirectedUnweightedEdge()
        {
            var oldEdgeCount = this.graph.EdgeCount;
            this.graph.AddEdge(1, 2);

            Assert.AreEqual(oldEdgeCount + 1, this.graph.EdgeCount);

            Assert.True(this.graph.HasEdge(1, 2));
            Assert.True(this.graph.HasEdge(2, 1));
        }

        /// <summary>
        /// Tests adding an undirected, weighted edge to a graph.
        /// </summary>
        [Test]
        public void TestAddUndirectedWeightedEdge()
        {
            var oldEdgeCount = this.graph.EdgeCount;
            this.graph.AddEdge(1, 2, 40);

            Assert.AreEqual(oldEdgeCount + 1, this.graph.EdgeCount);

            Assert.AreEqual(40, this.graph.GetEdge(1, 2));
            Assert.AreEqual(40, this.graph.GetEdge(2, 1));
        }

        /// <summary>
        /// Tests accessing the neighbors of a graph.
        /// </summary>
        [Test]
        public void TestAdjacentVertices()
        {
            this.graph.AddDirectedEdge(1, 2);
            this.graph.AddDirectedEdge(2, 3);
            this.graph.AddEdge(2, 4);
            this.graph.AddEdge(5, 2);

            var neighbors = this.graph.AdjacentVertices(2);

            Assert.False(neighbors.Contains(1));
            Assert.True(neighbors.Contains(3));
            Assert.True(neighbors.Contains(4));
            Assert.True(neighbors.Contains(5));
        }

        /// <summary>
        /// Tests constructing a graph with vertices with duplicate indices.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructGraphWithDuplicateIndexVertex()
        {
            var vertices = new[] { new IntVertex(1), new IntVertex(1) };
#pragma warning disable 168
            var g = new Graph<IntVertex, int>(vertices);
#pragma warning restore 168
        }

        /// <summary>
        /// Tests constructing a graph with a vertex whose index is too big.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructGraphWithExceedingIndexVertex()
        {
            var vertices = new[] { new IntVertex(42) };
#pragma warning disable 168
            var g = new Graph<IntVertex, int>(vertices);
#pragma warning restore 168
        }

        /// <summary>
        /// Tests constructing a graph with a vertex with negative index.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructGraphWithNegativeIndexVertex()
        {
            var vertices = new[] { new IntVertex(-1) };
#pragma warning disable 168
            var g = new Graph<IntVertex, int>(vertices);
#pragma warning restore 168
        }

        /// <summary>
        /// Tests constructing a graph without vertices.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructGraphWithNullVertices()
        {
#pragma warning disable 168
            var g = new Graph<IntVertex, int>(null);
#pragma warning restore 168
        }

        /// <summary>
        /// Tests getting the degree of a graph index.
        /// </summary>
        [Test]
        public void TestDegree()
        {
            this.graph.AddDirectedEdge(1, 2);
            this.graph.AddDirectedEdge(2, 3);
            this.graph.AddEdge(2, 4);
            this.graph.AddEdge(5, 2);

            Assert.AreEqual(3, this.graph.Degree(2));
        }

        /// <summary>
        /// Tests accessing all incident edges of a graph vertex.
        /// </summary>
        [Test]
        public void TestIncidentEdges()
        {
            this.graph.AddDirectedEdge(1, 2, 10);
            this.graph.AddDirectedEdge(2, 3, 30);
            this.graph.AddEdge(2, 4, 40);
            this.graph.AddEdge(5, 2, 50);

            var edgeWeights = this.graph.IncidentEdges(2);

            Assert.False(edgeWeights.Contains(10));
            Assert.True(edgeWeights.Contains(30));
            Assert.True(edgeWeights.Contains(40));
            Assert.True(edgeWeights.Contains(50));
        }

        /// <summary>
        /// Tests getting the number of vertices of a graph.
        /// </summary>
        [Test]
        public void TestVertexCount()
        {
            var vertices = new[] { new IntVertex(0), new IntVertex(1), new IntVertex(2) };
            var g = new Graph<IntVertex, int>(vertices);

            Assert.AreEqual(3, g.VertexCount);
        }

        #endregion
    }
}