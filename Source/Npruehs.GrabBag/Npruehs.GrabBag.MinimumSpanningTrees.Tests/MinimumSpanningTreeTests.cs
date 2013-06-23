// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinimumSpanningTreeTests.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.MinimumSpanningTrees.Tests
{
    using System.Diagnostics;
    using System.Linq;

    using Npruehs.GrabBag.Graphs;
    using Npruehs.GrabBag.MinimumSpanningTrees.Algorithms;

    using NUnit.Framework;

    /// <summary>
    ///  Unit tests for the minimum spanning tree algorithms.
    /// </summary>
    [TestFixture]
    public class MinimumSpanningTreeTests
    {
        #region Fields

        /// <summary>
        /// Test graph to run unit tests on.
        /// </summary>
        private Graph<IntVertex, FloatEdge> graph;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Tests the Fredman/Tarjan minimum spanning tree algorithm.
        /// </summary>
        [Test]
        public void TestFredmanTarjan()
        {
            this.BuildExampleGraph();
            var fredmanTarjan = new FredmanTarjan<IntVertex, FloatEdge>();
            var solution = fredmanTarjan.FindSolution(this.graph);

            PrintGraph(solution);

            // Verify solution.
            Assert.IsFalse(solution.HasEdge(this.graph[0], this.graph[1]));
            Assert.IsTrue(solution.HasEdge(this.graph[0], this.graph[2]));
            Assert.IsFalse(solution.HasEdge(this.graph[0], this.graph[10]));

            Assert.IsTrue(solution.HasEdge(this.graph[1], this.graph[2]));
            Assert.IsTrue(solution.HasEdge(this.graph[1], this.graph[6]));
            Assert.IsFalse(solution.HasEdge(this.graph[1], this.graph[7]));
            Assert.IsTrue(solution.HasEdge(this.graph[1], this.graph[9]));
            Assert.IsTrue(solution.HasEdge(this.graph[1], this.graph[10]));

            Assert.IsFalse(solution.HasEdge(this.graph[3], this.graph[4]));
            Assert.IsTrue(solution.HasEdge(this.graph[3], this.graph[6]));

            Assert.IsTrue(solution.HasEdge(this.graph[4], this.graph[5]));
            Assert.IsTrue(solution.HasEdge(this.graph[4], this.graph[6]));

            Assert.IsFalse(solution.HasEdge(this.graph[5], this.graph[6]));

            Assert.IsFalse(solution.HasEdge(this.graph[6], this.graph[7]));

            Assert.IsTrue(solution.HasEdge(this.graph[7], this.graph[8]));

            Assert.IsTrue(solution.HasEdge(this.graph[8], this.graph[9]));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the edges of the passed graph to the console.
        /// </summary>
        /// <param name="graph">
        /// Graph to print.
        /// </param>
        private static void PrintGraph(IWeightedGraph<IntVertex, FloatEdge> graph)
        {
            foreach (var edge in
                graph.Edges.Where(edge => edge.Source < edge.Target))
            {
                Debug.WriteLine("{0} ---> {1}: {2}", edge.Source, edge.Target, edge.Weight);
            }
        }

        /// <summary>
        /// Adds an undirected edge between the two specified vertices to the test graph.
        /// </summary>
        /// <param name="source">
        /// Index of the edge source vertex.
        /// </param>
        /// <param name="target">
        /// Index of the edge target vertex.
        /// </param>
        /// <param name="weight">
        /// Weight of the new edge.
        /// </param>
        private void AddEdge(int source, int target, int weight)
        {
            var edge = new FloatEdge { Source = source, Target = target, Weight = weight };
            this.graph.AddDirectedEdge(this.graph.Vertices[source], this.graph.Vertices[target], edge);

            edge = new FloatEdge { Source = target, Target = source, Weight = weight };
            this.graph.AddDirectedEdge(this.graph.Vertices[target], this.graph.Vertices[source], edge);
        }

        /// <summary>
        /// Constructs an example graph to run unit tests on.
        /// </summary>
        private void BuildExampleGraph()
        {
            var vertices = new IntVertex[11];

            for (var i = 0; i < 11; i++)
            {
                vertices[i] = new IntVertex(i);
            }

            this.graph = new Graph<IntVertex, FloatEdge>(vertices);

            this.AddEdge(0, 1, 78);
            this.AddEdge(0, 2, 21);
            this.AddEdge(0, 10, 5001);

            this.AddEdge(1, 2, 3);
            this.AddEdge(1, 6, 89);
            this.AddEdge(1, 7, 829);
            this.AddEdge(1, 9, 5);
            this.AddEdge(1, 10, 290);

            this.AddEdge(3, 4, 157);
            this.AddEdge(3, 6, 41);

            this.AddEdge(4, 5, 1688);
            this.AddEdge(4, 6, 7);

            this.AddEdge(5, 6, 3622);

            this.AddEdge(6, 7, 36025);

            this.AddEdge(7, 8, 11);

            this.AddEdge(8, 9, 19);
        }

        #endregion
    }
}