// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DijkstraNode.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Npruehs.GrabBag.ShortestPaths.Dijkstra
{
    /// <summary>
    /// Node used for the Dijkstra algorithm.
    /// </summary>
    public class DijkstraNode : IDijkstraNode
    {
        #region Public Properties

        /// <summary>
        /// Index of this vertex.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     Predecessor of this node on the way to the start.
        /// </summary>
        public IDijkstraNode Predecessor { get; set; }

        #endregion
    }
}