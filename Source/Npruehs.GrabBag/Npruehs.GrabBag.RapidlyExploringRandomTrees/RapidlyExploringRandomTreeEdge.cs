// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RapidlyExploringRandomTreeEdge.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.RapidlyExploringRandomTrees
{
    using System;

    /// <summary>
    /// Edge between two vertices in a rapidly-exploring random tree.
    /// </summary>
    /// <typeparam name="TConfiguration">
    /// Type of the elements of the configuration space the tree lives in.
    /// </typeparam>
    /// <typeparam name="TInput">
    /// Type of the actions that can affect a configuration.
    /// </typeparam>
    [CLSCompliant(true)]
    public class RapidlyExploringRandomTreeEdge<TConfiguration, TInput> : IRapidlyExploringRandomTreeEdge<TConfiguration, TInput>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new RRT edge between the two specified vertices.
        /// </summary>
        /// <param name="source">
        /// Vertex the edge starts at.
        /// </param>
        /// <param name="target">
        /// Vertex the edge leads to.
        /// </param>
        /// <param name="input">
        /// Motion made to get from the source to the target vertex.
        /// </param>
        public RapidlyExploringRandomTreeEdge(TConfiguration source, TConfiguration target, TInput input)
        {
            this.Source = source;
            this.Target = target;
            this.Input = input;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Motion made to get from the source to the target vertex.
        /// </summary>
        public TInput Input { get; private set; }

        /// <summary>
        /// Vertex this edge starts at.
        /// </summary>
        public TConfiguration Source { get; private set; }

        /// <summary>
        /// Vertex this edge leads to.
        /// </summary>
        public TConfiguration Target { get; private set; }

        #endregion
    }
}