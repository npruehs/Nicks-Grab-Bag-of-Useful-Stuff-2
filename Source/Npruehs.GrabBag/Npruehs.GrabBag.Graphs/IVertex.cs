// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVertex.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;

    /// <summary>
    /// The Vertex interface.
    /// </summary>
    [CLSCompliant(true)]
    public interface IVertex
    {
        #region Public Properties

        /// <summary>
        /// Gets the index.
        /// </summary>
        int Index { get; }

        #endregion
    }
}