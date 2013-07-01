// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAStarNode.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Npruehs.GrabBag.ShortestPaths.AStar
{
    using Npruehs.GrabBag.Graphs;

    /// <summary>
    /// Node used for the A* algorithm.
    /// </summary>
    public interface IAStarNode : IVertex
    {
        #region Public Properties

        /// <summary>
        /// Whether this node has already been discovered and added to the
        /// open list, or not.
        /// </summary>
        bool Discovered { get; set; }

        /// <summary>
        /// F score of this node, computed by adding G and H.
        /// </summary>
        int F { get; set; }

        /// <summary>
        /// G score of this node, telling the movement cost needed
        /// for travelling from the starting node to this one.
        /// </summary>
        int G { get; set; }

        /// <summary>
        /// H score of this node, telling the estimated movement cost
        /// needed for travelling from this node to the finish.
        /// </summary>
        int H { get; set; }

        /// <summary>
        /// Specifies whether this node is walkable or not.
        /// </summary>
        bool IsWalkable { get; }

        /// <summary>
        /// Previous node on the path to the finish.
        /// </summary>
        IAStarNode ParentNode { get; set; }

        /// <summary>
        /// Whether this node has already been visited and moved to the
        /// closed list, or not.
        /// </summary>
        bool Visited { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Returns the estimated, heuristic movement cost needed to get
        /// from this node to the specified other one.
        /// </summary>
        /// <param name="target">
        /// Target node.
        /// </param>
        /// <returns>
        /// Estimated movement cost.
        /// </returns>
        int EstimateHeuristicMovementCost(IAStarNode target);

        #endregion
    }
}