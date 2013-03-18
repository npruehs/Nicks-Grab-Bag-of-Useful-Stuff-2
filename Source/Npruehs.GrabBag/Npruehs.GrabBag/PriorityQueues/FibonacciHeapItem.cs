// --------------------------------------------------------------------------------
// <copyright file="FibonacciHeapItem.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.PriorityQueues
{
    /// <summary>
    /// Container for an item that can be inserted into a Fibonacci heap.
    /// Provides a pointer to its heap position and a key for comparing it to
    /// other heap items for order.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the items held by the Fibonacci heap.
    /// </typeparam>
    public class FibonacciHeapItem<T>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new container for the passed item that can be inserted
        /// into a Fibonacci heap with the specified key for comparing it to
        /// other heap items for order.
        /// </summary>
        /// <param name="item">
        /// Item held by the new container.
        /// </param>
        /// <param name="key">
        /// Key of the item which is used for comparing it to other
        /// heap items for order.
        /// </param>
        internal FibonacciHeapItem(T item, double key)
        {
            this.Item = item;
            this.Key = key;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Item held by this container.
        /// </summary>
        public T Item { get; internal set; }

        /// <summary>
        /// Key of the item which is used for comparing it to other heap
        /// items for order.
        /// </summary>
        public double Key { get; internal set; }

        #endregion

        #region Properties

        /// <summary>
        /// Heap node which contains the item.
        /// </summary>
        internal FibonacciHeap<T>.TreeNode ContainingNode { get; set; }

        #endregion
    }
}