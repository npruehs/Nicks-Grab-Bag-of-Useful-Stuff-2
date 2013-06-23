// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMinimumSpanningTreeEdge.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.MinimumSpanningTrees
{
    using System;

    /// <summary>
    ///     Edge with a <see cref="float"/> weight.
    /// </summary>
    [CLSCompliant(true)]
    public interface IMinimumSpanningTreeEdge
    {
        #region Public Properties

        /// <summary>
        ///     Index of the source vertex of this edge.
        /// </summary>
        int Source { get; set; }

        /// <summary>
        ///     Index of the target vertex of this edge.
        /// </summary>
        int Target { get; set; }

        /// <summary>
        ///     Weight of this edge.
        /// </summary>
        float Weight { get; set; }

        #endregion
    }
}