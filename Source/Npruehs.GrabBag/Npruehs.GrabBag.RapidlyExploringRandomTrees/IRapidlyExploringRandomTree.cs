// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRapidlyExploringRandomTree.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.RapidlyExploringRandomTrees
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Randomized data structure designed for a broad class of path planning problems.
    /// </summary>
    /// <typeparam name="TConfiguration">
    /// Type of the elements of the configuration space the tree lives in.
    /// </typeparam>
    /// <typeparam name="TInput">
    /// Type of the actions that can affect a configuration.
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IRapidlyExploringRandomTree<TConfiguration, TInput>
    {
        #region Public Properties

        /// <summary>
        /// Edges of this tree.
        /// </summary>
        IList<IRapidlyExploringRandomTreeEdge<TConfiguration, TInput>> Edges { get; }

        /// <summary>
        /// Vertices of this tree.
        /// </summary>
        IList<TConfiguration> Vertices { get; }

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
        void GrowTree(IConfigurationSpace<TConfiguration, TInput> c, TConfiguration qInit, int k);

        #endregion
    }
}