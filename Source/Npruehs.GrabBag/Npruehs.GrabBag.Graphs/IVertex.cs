// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVertex.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    using System;

    /// <summary>
    /// Interface for graph vertices.
    /// </summary>
    [CLSCompliant(true)]
    public interface IVertex
    {
        #region Public Properties

        /// <summary>
        /// Index of this vertex.
        /// </summary>
        int Index { get; }

        #endregion
    }
}