// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDijkstraNode.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Npruehs.GrabBag.ShortestPaths.Dijkstra
{
    using Npruehs.GrabBag.Graphs;

    /// <summary>
    /// Node used for the Dijkstra algorithm.
    /// </summary>
    public interface IDijkstraNode : IVertex
    {
        #region Public Properties

        /// <summary>
        ///     Predecessor of this node on the way to the start.
        /// </summary>
        IDijkstraNode Predecessor { get; set; }

        #endregion
    }
}