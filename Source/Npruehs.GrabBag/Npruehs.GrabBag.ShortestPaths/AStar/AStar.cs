// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AStar.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Npruehs.GrabBag.ShortestPaths.AStar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Npruehs.GrabBag.Graphs;
    using Npruehs.GrabBag.PriorityQueues;

    /// <summary>
    /// Calculates the most efficient path between two specified nodes using
    /// the A* algorithm.
    /// </summary>
    public static class AStar
    {
        /// <summary>
        /// Computes the most efficient path in the specified graph between the
        /// specified nodes using the A* algorithm.
        /// </summary>
        /// <param name="graph">
        /// Graph to find the shortest path in.
        /// </param>
        /// <param name="start">
        /// Starting node of the path to find.
        /// </param>
        /// <param name="finish">
        /// Finish node of the path to find.
        /// </param>
        /// <typeparam name="T">
        /// Type of the nodes of the graph to find the path in.
        /// </typeparam>
        /// <returns>
        /// List of nodes representing the shortest path, if there is one,
        /// and null otherwise.
        /// </returns>
        public static List<T> FindPath<T>(IWeightedGraph<T, int> graph, T start, T finish)
            where T : IAStarNode
        {
            IList<T> visitedNodes;
            return FindPath(graph, start, finish, out visitedNodes);
        }

        #region Public Methods and Operators

        /// <summary>
        /// Computes the most efficient path in the specified graph between the
        /// specified nodes using the A* algorithm.
        /// </summary>
        /// <param name="graph">
        /// Graph to find the shortest path in.
        /// </param>
        /// <param name="start">
        /// Starting node of the path to find.
        /// </param>
        /// <param name="finish">
        /// Finish node of the path to find.
        /// </param>
        /// <param name="visitedNodes">
        /// Nodes that have been visited finding the path.
        /// </param>
        /// <typeparam name="T">
        /// Type of the nodes of the graph to find the path in.
        /// </typeparam>
        /// <returns>
        /// List of nodes representing the shortest path, if there is one,
        /// and null otherwise.
        /// </returns>
        public static List<T> FindPath<T>(IWeightedGraph<T, int> graph, T start, T finish, out IList<T> visitedNodes) where T : IAStarNode
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            if (start == null)
            {
                throw new ArgumentNullException("start");
            }

            if (finish == null)
            {
                throw new ArgumentNullException("finish");
            }

            // Initialize variables to check for the algorithm to terminate.
            var algorithmComplete = false;
            var algorithmAborted = false;

            // Initialize list to choose the next node of the path from.
            FibonacciHeap<T> openList = new FibonacciHeap<T>();
            FibonacciHeapItem<T>[] fibHeapItems = new FibonacciHeapItem<T>[graph.VertexCount];

            visitedNodes = new List<T>();

            // Initialize queue to hold the nodes along the path to the finish.
            Queue<T> closedList = new Queue<T>();

            // Add starting node to open list.
            fibHeapItems[start.Index] = openList.Insert(start, 0);
            visitedNodes.Add(start);
            start.Discovered = true;

            // A* Pathfinding Algorithm.
            while (!algorithmAborted)
            {
                // Get the node with the lowest F score in the open list.
                var currentNode = openList.DeleteMin().Item;

                // Drop that node from the open list and add it to the closed list.
                closedList.Enqueue(currentNode);
                currentNode.Visited = true;

                // We're done if the target node is added to the closed list.
                if (currentNode.Equals(finish))
                {
                    algorithmComplete = true;
                    break;
                }

                // Otherwise, get all adjacent nodes.
                var neighbors = graph.AdjacentVertices(currentNode);

                // Add all nodes that aren't already on the open or closed list to the open list.
                foreach (var node in neighbors.Where(node => !node.Visited))
                {
                    if (!node.Discovered)
                    {
                        // Parent node is the previous node on the path to the finish.
                        node.ParentNode = currentNode;

                        // The G score of the node is calculated by adding the G score
                        // of the parent node and the movement cost of the path between
                        // the node and the current node.
                        // In other words: The G score of the node is the total cost of the
                        // path between the starting node and this one.
                        node.G = node.ParentNode.G + graph.GetEdge(node, (T)node.ParentNode);

                        // The H score of the node is calculated by heuristically
                        // estimating the movement cost from the node to the finish.
                        // In other words: The H score of the node is the total remaining
                        // cost of the path between this node and the finish.
                        node.H = node.EstimateHeuristicMovementCost(finish);

                        // The F score of the node is calculated by adding the G and H scores.
                        // In other words: The F score is an indicator that tells whether this
                        // node should be crossed on the path to the finish, or not.
                        node.F = node.G + node.H;

                        // Add to open list.
                        fibHeapItems[node.Index] = openList.Insert(node, node.F);
                        visitedNodes.Add(node);
                        node.Discovered = true;
                    }
                    else
                    {
                        // Node is already in open list!
                        // Check if the new path to this node is a better one.
                        if ((currentNode.G + graph.GetEdge(node, currentNode)) < node.G)
                        {
                            // G cost of new path is lower!
                            // Change parent node to current node.
                            node.ParentNode = currentNode;

                            // Recalculate F and G costs.
                            node.G = node.ParentNode.G + graph.GetEdge(node, (T)node.ParentNode);
                            node.F = node.G + node.H;

                            openList.DecreaseKeyTo(fibHeapItems[node.Index], node.F);
                        }
                    }
                }

                // We've failed to find a path if the open list is empty.
                if (openList.Empty)
                {
                    algorithmAborted = true;
                }
            }

            // Return the path to the finish, if there is one.
            if (!algorithmComplete)
            {
                return null;
            }

            // Generate path through parent pointers using a stack.
            List<T> path = new List<T>();
            var astarnode = finish;
            while (!astarnode.Equals(start))
            {
                path.Add(astarnode);
                astarnode = (T)astarnode.ParentNode;
            }

            path.Add(astarnode);
            path.Reverse();

            return path;
        }

        #endregion
    }
}