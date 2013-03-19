// --------------------------------------------------------------------------------
// <copyright file="IPriorityQueue.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.PriorityQueues
{
    using System;

    /// <summary>
    /// Priority queue which allows inserting items with real
    /// keys, decreasing their keys, and finding or removing the minimum item.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the items held by this priority queue.
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IPriorityQueue<T>
    {
        #region Public Properties

        /// <summary>
        /// Whether this priority queue is empty, or not.
        /// </summary>
        bool Empty { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Decreases the key of the specified item in this priority queue to the passed non-negative real number.
        /// </summary>
        /// <param name="item">
        /// Item to decrease the key of.
        /// </param>
        /// <param name="newKey">
        /// New key of the item.
        /// </param>
        void DecreaseKeyTo(T item, double newKey);

        /// <summary>
        /// Deletes the item with the minimum key in this priority queue and returns it.
        /// </summary>
        /// <returns>Item with the minimum key in this priority queue.</returns>
        T DeleteMin();

        /// <summary>
        /// Returns the item with the minimum key in this priority queue.
        /// </summary>
        /// <returns>Item with the minimum key in this priority queue.</returns>
        T FindMin();

        /// <summary>
        /// Inserts the passed item with the specified key into this priority queue.
        /// </summary>
        /// <param name="item">
        /// Item to insert.
        /// </param>
        /// <param name="key">
        /// Key of the item to insert.
        /// </param>
        void Insert(T item, double key);

        #endregion
    }
}