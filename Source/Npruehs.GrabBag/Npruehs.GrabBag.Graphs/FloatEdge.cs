// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatEdge.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;

    /// <summary>
    ///     Edge with a <see cref="float"/> weight.
    /// </summary>
    [CLSCompliant(true)]
    public class FloatEdge
    {
        #region Public Properties

        /// <summary>
        ///     Index of the source vertex of this edge.
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        ///     Index of the target vertex of this edge.
        /// </summary>
        public int Target { get; set; }

        /// <summary>
        ///     Weight of this edge.
        /// </summary>
        public float Weight { get; set; }

        #endregion
    }
}