// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RapidlyExploringRandomTree.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.RapidlyExploringRandomTrees
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implementation of Rapidly-Exploring Random Trees introduced by
    /// Steven M. LaValle, a randomized data structure designed for a broad
    /// class of path planning problems.
    /// </summary>
    /// <typeparam name="TConfiguration">
    /// Type of the elements of the configuration space the tree lives in.
    /// </typeparam>
    /// <typeparam name="TInput">
    /// Type of the actions that can affect a configuration.
    /// </typeparam>
    public class RapidlyExploringRandomTree<TConfiguration, TInput> :
        IRapidlyExploringRandomTree<TConfiguration, TInput>
    {
        #region Fields

        /// <summary>
        /// Edges of this tree.
        /// </summary>
        private List<IRapidlyExploringRandomTreeEdge<TConfiguration, TInput>> edges;

        /// <summary>
        /// Vertices of this tree.
        /// </summary>
        private List<TConfiguration> vertices;

        #endregion

        #region Public Properties

        /// <summary>
        /// Edges of this tree.
        /// </summary>
        public IList<IRapidlyExploringRandomTreeEdge<TConfiguration, TInput>> Edges
        {
            get
            {
                return this.edges;
            }
        }

        /// <summary>
        /// Vertices of this tree.
        /// </summary>
        public IList<TConfiguration> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Grows a new rapidly-exploring random tree with k vertices around the
        /// given initial configuration in the given configuration space.
        /// </summary>
        /// <param name="c">
        /// Configuration space the new tree should live in.
        /// </param>
        /// <param name="qInit">
        /// Initial configuration to grow the tree around.
        /// </param>
        /// <param name="k">
        /// Number of vertices of the tree to generate.
        /// </param>
        public void GrowTree(IConfigurationSpace<TConfiguration, TInput> c, TConfiguration qInit, int k)
        {
            // Check passed paramters.
            if (c == null)
            {
                throw new ArgumentNullException("c");
            }

            if (qInit == null)
            {
                throw new ArgumentNullException("qInit");
            }

            if (k < 1)
            {
                throw new ArgumentOutOfRangeException("k", "k must be greater than or equal to 1.");
            }

            // Start at initial configuration.
            this.vertices = new List<TConfiguration> { qInit };
            this.edges = new List<IRapidlyExploringRandomTreeEdge<TConfiguration, TInput>>();

            for (var i = 0; i < k - 1; i++)
            {
                // Grab a random configuration within the space.
                var qRand = c.GetRandomState();

                // Grow the tree in direction of that configuration.
                this.Extend(c, qRand);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Extends this tree by making a motion from a vertex already in this
        /// tree in direction of q.
        /// </summary>
        /// <param name="c">
        /// Configuration space to make a motion in.
        /// </param>
        /// <param name="q">
        /// Configuration that specified the direction to extend to.
        /// </param>
        private void Extend(IConfigurationSpace<TConfiguration, TInput> c, TConfiguration q)
        {
            TConfiguration qNew;
            TInput uNew;

            // Find the nearest vertex already in the RRT to q.
            var qNear = this.NearestNeighbor(c, q);

            // If a valid motion can be made towards q...
            if (c.NewState(q, qNear, out qNew, out uNew))
            {
                // ... add a new vextex between q and qNear.
                this.vertices.Add(qNew);
                this.edges.Add(new RapidlyExploringRandomTreeEdge<TConfiguration, TInput>(qNear, qNew, uNew));
            }
        }

        /// <summary>
        /// Finds and returns the nearest vertex already in this RRT to q in O(n).
        /// </summary>
        /// <param name="c">
        /// Configuration space to look in.
        /// </param>
        /// <param name="q">
        /// Configuration to find the nearest vertex to.
        /// </param>
        /// <returns>
        /// Nearest vertex already in this RRT to q.
        /// </returns>
        private TConfiguration NearestNeighbor(IConfigurationSpace<TConfiguration, TInput> c, TConfiguration q)
        {
            // Start at the first vertex of this tree...
            var qNear = this.vertices[0];
            var distance = c.GetDistance(q, qNear);

            // ... and try to find another one that's closer.
            for (var i = 1; i < this.vertices.Count; i++)
            {
                var newDistance = c.GetDistance(q, this.vertices[i]);

                if (newDistance.CompareTo(distance) < 0)
                {
                    distance = newDistance;
                    qNear = this.vertices[i];
                }
            }

            return qNear;
        }

        #endregion
    }
}