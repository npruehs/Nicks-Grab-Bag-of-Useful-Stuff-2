// --------------------------------------------------------------------------------
// <copyright file="PriorityQueueTests.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Tests
{
    using System;

    using Npruehs.GrabBag.PriorityQueues;

    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the priority queues.
    /// </summary>
    public class PriorityQueueTests
    {
        #region Fields

        /// <summary>
        /// First test item to add to the priority queue.
        /// </summary>
        private string itemA;

        /// <summary>
        /// Second test item to add to the priority queue.
        /// </summary>
        private string itemB;

        /// <summary>
        /// Key of the first test item to add to the priority queue.
        /// </summary>
        private double keyA;

        /// <summary>
        /// Key of the second test item to add to the priority queue.
        /// </summary>
        private double keyB;

        /// <summary>
        /// Priority queue to run all unit tests on.
        /// </summary>
        private IPriorityQueue<string> priorityQueue;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Tests adding two items to the priority queue, clearing it, and checks whether it's empty after.
        /// </summary>
        [Test]
        public void Clear()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            this.priorityQueue.Insert(this.itemB, this.keyB);
            this.priorityQueue.Clear();

            Assert.AreEqual(true, this.priorityQueue.Empty);
            Assert.AreEqual(0, this.priorityQueue.Size);
        }

        /// <summary>
        /// Tests adding two items, decreasing the key of the greater one, and retrieving it as minimum.
        /// </summary>
        [Test]
        public void DecreaseKey()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            this.priorityQueue.Insert(this.itemB, this.keyB);
            this.priorityQueue.DecreaseKey(this.itemB, 2.0);

            var min = this.priorityQueue.FindMin();

            Assert.AreEqual(this.itemB, min);
        }

        /// <summary>
        /// Tests increasing the key of an item.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DecreaseKeyNegative()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            this.priorityQueue.DecreaseKey(this.itemA, -2.0);
        }

        /// <summary>
        /// Tests adding two items, decreasing the key of the greater one, and retrieving it as minimum.
        /// </summary>
        [Test]
        public void DecreaseKeyTo()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            this.priorityQueue.Insert(this.itemB, this.keyB);
            this.priorityQueue.DecreaseKeyTo(this.itemB, 0.0);

            var min = this.priorityQueue.FindMin();

            Assert.AreEqual(this.itemB, min);
        }

        /// <summary>
        /// Tests inserting an item, deleting it after, and checks whether the priority queue is empty then.
        /// </summary>
        [Test]
        public void Delete()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            this.priorityQueue.Delete(this.itemA);

            Assert.AreEqual(true, this.priorityQueue.Empty);
            Assert.AreEqual(0, this.priorityQueue.Size);
        }

        /// <summary>
        /// Tests adding two items and deleting the minimum after.
        /// Asserts the smaller item has been deleted and the size of the
        /// priority queue has been reduced.
        /// </summary>
        [Test]
        public void DeleteMin()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            this.priorityQueue.Insert(this.itemB, this.keyB);

            var min = this.priorityQueue.DeleteMin();

            Assert.AreEqual(this.itemA, min);
            Assert.AreEqual(1, this.priorityQueue.Size);
        }

        /// <summary>
        /// Tests deleting the minimum of an empty heap.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteMinEmptyHeap()
        {
            this.priorityQueue.DeleteMin();
        }

        /// <summary>
        /// Tests whether priority queues are initially empty.
        /// </summary>
        [Test]
        public void Empty()
        {
            Assert.AreEqual(true, this.priorityQueue.Empty);
        }

        /// <summary>
        /// Tests adding two items and finding the minimum after.
        /// </summary>
        [Test]
        public void FindMin()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            this.priorityQueue.Insert(this.itemB, this.keyB);

            var min = this.priorityQueue.FindMin();

            Assert.AreEqual(this.itemA, min);
        }

        /// <summary>
        /// Tests retrieving the minimum of an empty heap.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FindMinEmptyHeap()
        {
            this.priorityQueue.FindMin();
        }

        /// <summary>
        /// Tests whether priority queues are not empty after having inserted an item.
        /// </summary>
        [Test]
        public void NotEmpty()
        {
            this.priorityQueue.Insert(this.itemA, this.keyA);
            Assert.AreEqual(false, this.priorityQueue.Empty);
        }

        /// <summary>
        /// Sets up the priority queue for all unit tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.priorityQueue = new PriorityQueue<string>();
            this.itemA = "a";
            this.itemB = "b";
            this.keyA = 1.0;
            this.keyB = 2.0;
        }

        /// <summary>
        /// Tests adding two items and checks the size in between.
        /// </summary>
        [Test]
        public void Size()
        {
            Assert.AreEqual(0, this.priorityQueue.Size);
            this.priorityQueue.Insert(this.itemA, this.keyA);
            Assert.AreEqual(1, this.priorityQueue.Size);
            this.priorityQueue.Insert(this.itemB, this.keyB);
            Assert.AreEqual(2, this.priorityQueue.Size);
        }

        #endregion
    }
}