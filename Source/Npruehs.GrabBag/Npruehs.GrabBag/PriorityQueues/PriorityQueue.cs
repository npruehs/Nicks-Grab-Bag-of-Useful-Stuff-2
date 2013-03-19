// --------------------------------------------------------------------------------
// <copyright file="PriorityQueue.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.PriorityQueues
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// <para>
    /// Wraps an implementation of a Fibonacci heap (abbreviated F-heap) by
    /// Michael L. Fredman and Robert Endre Tarjan, which represents a
    /// very fast priority queue, by mapping
    /// elements to Fibonacci heap items with a dictionary. Use
    /// <see cref="FibonacciHeap{T}"/> for handling the items yourself.
    /// </para>
    /// <para>
    /// Provides insertion, finding the minimum, melding and decreasing keys in
    /// constant amortized time, and deleting from an n-item heap in O(log n)
    /// amortized time.
    /// </para>
    /// </summary>
    /// <typeparam name="T">
    /// Type of the items held by this priority queue.
    /// </typeparam>
    [CLSCompliant(true)]
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        #region Fields

        /// <summary>
        /// Fibonacci heap handling all heap operations
        /// </summary>
        private readonly FibonacciHeap<T> heap = new FibonacciHeap<T>();

        /// <summary>
        /// Items of this priority queue.
        /// </summary>
        private readonly Dictionary<T, FibonacciHeapItem<T>> items = new Dictionary<T, FibonacciHeapItem<T>>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Whether this priority queue is empty, or not.
        /// </summary>
        public bool Empty
        {
            get
            {
                return this.heap.Empty;
            }
        }

        /// <summary>
        /// Number of elements of this priority queue.
        /// </summary>
        public int Size
        {
            get
            {
                return this.heap.Size;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Clears this priority queue, removing all items.
        /// </summary>
        public void Clear()
        {
            this.heap.Clear();
        }

        /// <summary>
        /// Decreases the key of the specified item in this priority queue by subtracting
        /// the passed non-negative real number <c>delta</c>.
        /// </summary>
        /// <param name="item">
        /// Item to decrease the key of.
        /// </param>
        /// <param name="delta">
        /// Non-negative real number to be subtracted from the item's key.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="delta"/> is negative.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// This priority queue is empty.
        /// </exception>
        public void DecreaseKey(T item, double delta)
        {
            var heapItem = this.GetHeapItem(item);
            this.heap.DecreaseKey(heapItem, delta);
        }

        /// <summary>
        /// Decreases the key of the specified item in this priority queue to the passed
        /// non-negative real number.
        /// </summary>
        /// <param name="item">
        /// Item to decrease the key of.
        /// </param>
        /// <param name="newKey">
        /// New item key.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The resulting key would be greater than the current one.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// This priority queue is empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="item"/> is not an item of this priority queue.
        /// </exception>
        public void DecreaseKeyTo(T item, double newKey)
        {
            var heapItem = this.GetHeapItem(item);
            this.heap.DecreaseKeyTo(heapItem, newKey);
        }

        /// <summary>
        /// Deletes the specified item from this priority queue.
        /// </summary>
        /// <param name="item">
        /// Item to delete.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// This priority queue is empty.
        /// </exception>
        public void Delete(T item)
        {
            var heapItem = this.GetHeapItem(item);
            this.heap.Delete(heapItem);
        }

        /// <summary>
        /// Deletes the item with the minimum key in this priority queue and returns it.
        /// </summary>
        /// <returns>Item with the minimum key in this priority queue.</returns>
        /// <exception cref="InvalidOperationException">
        /// This priority queue is empty.
        /// </exception>
        public T DeleteMin()
        {
            return this.heap.DeleteMin().Item;
        }

        /// <summary>
        /// Returns the item with the minimum key in this priority queue.
        /// </summary>
        /// <returns>Item with the minimum key in this priority queue.</returns>
        /// <exception cref="InvalidOperationException">
        /// This priority queue is empty.
        /// </exception>
        public T FindMin()
        {
            return this.heap.FindMin().Item;
        }

        /// <summary>
        /// Inserts the passed item with the specified key into this priority queue.
        /// </summary>
        /// <param name="item">
        /// Item to insert.
        /// </param>
        /// <param name="key">
        /// Key of the item to insert.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="item"/> has already been added to this priority queue.
        /// </exception>
        public void Insert(T item, double key)
        {
            var heapItem = this.heap.Insert(item, key);

            if (!this.items.ContainsKey(item))
            {
                this.items.Add(item, heapItem);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format(
                        "This priority queue already contains the item {0}. The dictionary mapping items to priority queue items doesn't allow adding the same item more than once. Use the FibonacciHeap class for working on the Fibonacci heap items yourself.", 
                        item));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the Fibonacci heap item the passed item is mapped to.
        /// </summary>
        /// <param name="item">
        /// Item to get the Fibonacci heap item of.
        /// </param>
        /// <returns>
        /// Fibonacci heap item the passed item is mapped to.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="item"/> is not in this priority queue.
        /// </exception>
        private FibonacciHeapItem<T> GetHeapItem(T item)
        {
            FibonacciHeapItem<T> heapItem;

            this.items.TryGetValue(item, out heapItem);

            if (heapItem == null)
            {
                throw new InvalidOperationException(
                    string.Format("Unknown item: {0}. Use Insert for adding new elements to this priority queue.", item));
            }

            return heapItem;
        }

        #endregion
    }
}