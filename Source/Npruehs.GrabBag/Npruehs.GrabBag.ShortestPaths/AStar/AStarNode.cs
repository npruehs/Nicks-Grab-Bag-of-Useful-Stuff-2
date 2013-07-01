// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AStarNode.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Npruehs.GrabBag.ShortestPaths.AStar
{
    /// <summary>
    /// Node used for the A* algorithm.
    /// </summary>
    public abstract class AStarNode : IAStarNode
    {
        #region Public Properties

        /// <summary>
        /// Whether this node has already been discovered and added to the
        /// open list, or not.
        /// </summary>
        public bool Discovered { get; set; }

        /// <summary>
        /// F score of this node, computed by adding G and H.
        /// </summary>
        public int F { get; set; }

        /// <summary>
        /// G score of this node, telling the movement cost needed
        /// for travelling from the starting node to this one.
        /// </summary>
        public int G { get; set; }

        /// <summary>
        /// H score of this node, telling the estimated movement cost
        /// needed for travelling from this node to the finish.
        /// </summary>
        public int H { get; set; }

        /// <summary>
        /// Unique array index of this node in the graph.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Specifies whether this node is walkable or not.
        /// </summary>
        public bool IsWalkable { get; set; }

        /// <summary>
        /// Previous node on the path to the finish.
        /// </summary>
        public IAStarNode ParentNode { get; set; }

        /// <summary>
        /// Whether this node has already been visited and moved to the
        /// closed list, or not.
        /// </summary>
        public bool Visited { get; set; }

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
        public abstract int EstimateHeuristicMovementCost(IAStarNode target);

        #endregion
    }
}