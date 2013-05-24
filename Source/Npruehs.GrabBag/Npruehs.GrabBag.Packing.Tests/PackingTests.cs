// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackingTests.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Packing.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Npruehs.GrabBag.Packing.Algorithms;

    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the 2D packing algorithms.
    /// </summary>
    public class PackingTests
    {
        #region Fields

        /// <summary>
        /// Test problem instance for the packing algorithms.
        /// </summary>
        private List<PackingItem> testInstance;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Sets up the test problem instance for the packing algorithms.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.testInstance = new List<PackingItem>
                                    {
                                        new PackingItem(0, 0.3f, 0.5f), 
                                        new PackingItem(1, 0.5f, 0.3f), 
                                        new PackingItem(2, 0.4f, 0.4f), 
                                        new PackingItem(3, 0.7f, 0.7f), 
                                        new PackingItem(4, 0.2f, 0.2f), 
                                        new PackingItem(5, 0.1f, 0.7f), 
                                        new PackingItem(6, 0.9f, 0.2f), 
                                        new PackingItem(7, 0.4f, 0.6f), 
                                        new PackingItem(8, 0.2f, 0.8f), 
                                        new PackingItem(9, 0.7f, 0.3f)
                                    };
        }

        /// <summary>
        /// Tests adding an item to a packing level, increasing its width.
        /// </summary>
        [Test]
        public void TestAddItemToLevel()
        {
            var level = new PackingLevel();
            var item = this.testInstance[0];

            level.AddItem(item);

            Assert.True(level.Items.Contains(item));
            Assert.AreEqual(item.Rectangle.Width, level.Width);
        }

        /// <summary>
        /// Tests the Epstein/van Stee packing algorithm.
        /// </summary>
        [Test]
        public void TestEpsteinVanStee()
        {
            var epsteinVanStee = new EpsteinVanStee();
            epsteinVanStee.FindSolution(EpsteinVanStee.AssumedStripWidth, this.testInstance);
            this.CheckSolution(EpsteinVanStee.AssumedStripWidth, epsteinVanStee);
        }

        /// <summary>
        /// Tests checking the assumed strip width of the Epstein/van Stee packing algorithm.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEpsteinVanSteeAssumedStripWidth()
        {
            var epsteinVanStee = new EpsteinVanStee();
            epsteinVanStee.FindSolution(1.2f, this.testInstance);
        }

        /// <summary>
        /// Tests checking the maximum item height of the Epstein/van Stee packing algorithm.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEpsteinVanSteeMaximumItemHeight()
        {
            var epsteinVanStee = new EpsteinVanStee();
            epsteinVanStee.FindSolution(
                EpsteinVanStee.AssumedStripWidth, new List<PackingItem> { new PackingItem(0, 0.3f, 1.5f) });
        }

        /// <summary>
        /// Tests checking the maximum item width of the Epstein/van Stee packing algorithm.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEpsteinVanSteeMaximumItemWidth()
        {
            var epsteinVanStee = new EpsteinVanStee();
            epsteinVanStee.FindSolution(
                EpsteinVanStee.AssumedStripWidth, new List<PackingItem> { new PackingItem(0, 1.3f, 0.5f) });
        }

        /// <summary>
        /// Tests packing items with the First Fit Decreasing Height packing algorithm with a maximum strip height.
        /// </summary>
        [Test]
        public void TestFfdhWithMaximumStripHeight()
        {
            var ffdh = new FirstFitDecreasingHeight();
            const float MaxStripHeight = 1.2f;

            float value = ffdh.FindSolution(1.0f, this.testInstance, MaxStripHeight);

            Assert.True(value <= MaxStripHeight);
        }

        /// <summary>
        /// Tests the First Fit Decreasing Height packing algorithm.
        /// </summary>
        [Test]
        public void TestFirstFitDecreasingHeight()
        {
            var ffdh = new FirstFitDecreasingHeight();
            ffdh.FindSolution(1.0f, this.testInstance);
            this.CheckSolution(1.0f, ffdh);
        }

        /// <summary>
        /// Tests rotating an item, interchanging its width and height.
        /// </summary>
        [Test]
        public void TestRotateItem()
        {
            PackingItem item = this.testInstance[0];

            float oldWidth = item.Rectangle.Width;
            float oldHeight = item.Rectangle.Height;

            item.Rotate();

            Assert.AreEqual(item.Rectangle.Width, oldHeight);
            Assert.AreEqual(item.Rectangle.Height, oldWidth);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fails if any two different items in the specified solution overlap.
        /// </summary>
        /// <param name="result">
        /// Packing algorithm solution to check.
        /// </param>
        private void CheckItemsOverlap(PackingAlgorithm result)
        {
            foreach (PackingItem first in result)
            {
                foreach (PackingItem second in result)
                {
                    if (first != second && first.Rectangle.Intersects(second.Rectangle))
                    {
                        Assert.Fail("Items {0} and {1} overlap.", first, second);
                    }
                }
            }
        }

        /// <summary>
        /// Fails if any two different items in the specified solution overlap,
        /// or if any item in the specified solution exceeds the strip width,
        /// and writes the solution to the debug console.
        /// </summary>
        /// <param name="stripWidth">
        /// Strip width to check.
        /// </param>
        /// <param name="result">
        /// Packing algorithm solution to check.
        /// </param>
        private void CheckSolution(float stripWidth, PackingAlgorithm result)
        {
            this.PrintSolution(result);

            this.CheckStripWidthExceeded(stripWidth, result);
            this.CheckItemsOverlap(result);
        }

        /// <summary>
        /// Fails if any item in the specified solution exceeds the strip width.
        /// </summary>
        /// <param name="stripWidth">
        /// Strip width to check.
        /// </param>
        /// <param name="result">
        /// Packing algorithm solution to check.
        /// </param>
        private void CheckStripWidthExceeded(float stripWidth, IEnumerable<PackingItem> result)
        {
            foreach (PackingItem item in result)
            {
                if (item.Rectangle.X + item.Rectangle.Width > stripWidth)
                {
                    Assert.Fail("Item {0} exceeds strip width {1}.", item, stripWidth);
                }
            }
        }

        /// <summary>
        /// Writes the specified solution to the debug console.
        /// </summary>
        /// <param name="result">
        /// Solution to show.
        /// </param>
        private void PrintSolution(IEnumerable<PackingItem> result)
        {
            foreach (PackingItem item in result)
            {
                Debug.WriteLine(item.ToString());
            }
        }

        #endregion
    }
}