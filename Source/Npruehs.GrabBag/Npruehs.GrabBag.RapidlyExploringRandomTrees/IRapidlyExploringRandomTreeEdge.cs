// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRapidlyExploringRandomTreeEdge.cs" company="Nick Pruehs">
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
    public interface IRapidlyExploringRandomTreeEdge<out TConfiguration, out TInput>
    {
        #region Public Properties

        /// <summary>
        /// Motion made to get from the source to the target vertex.
        /// </summary>
        TInput Input { get; }

        /// <summary>
        /// Vertex this edge starts at.
        /// </summary>
        TConfiguration Source { get; }

        /// <summary>
        /// Vertex this edge leads to.
        /// </summary>
        TConfiguration Target { get; }

        #endregion
    }
}