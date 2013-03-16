// --------------------------------------------------------------------------------
// <copyright file="UnionFindTests.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Tests
{
    using System;

    using Npruehs.GrabBag.UnionFind;

    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the union-find structure.
    /// </summary>
    [TestFixture]
    public class UnionFindTests
    {
        #region Fields

        /// <summary>
        /// First test item to add to the union-find structure.
        /// </summary>
        private string itemA;

        /// <summary>
        /// Second test item to add to the union-find structure.
        /// </summary>
        private string itemB;

        /// <summary>
        /// Union-find structure to run all unit tests on.
        /// </summary>
        private IUnionFind<string> unionFind;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Tests finding an item that has never been added to the union-find structure.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FindUnknownItem()
        {
            this.unionFind.Find(this.itemA);
        }

        /// <summary>
        /// Tests adding an item to the union-find structure twice.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MakeSetDuplicateItem()
        {
            this.unionFind.MakeSet(this.itemA);
            this.unionFind.MakeSet(this.itemA);
        }

        /// <summary>
        /// Sets up the union-find structure for all unit tests.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.unionFind = new UnionFind<string>();
            this.itemA = "a";
            this.itemB = "b";
        }

        /// <summary>
        /// Tests adding two items to the union-find structure, uniting their
        /// sets and checking their canonical elements for equality.
        /// </summary>
        [Test]
        public void UnionFind()
        {
            this.unionFind.MakeSet(this.itemA);
            this.unionFind.MakeSet(this.itemB);

            this.unionFind.Union(this.itemA, this.itemB);

            var findA = this.unionFind.Find(this.itemA);
            var findB = this.unionFind.Find(this.itemB);

            Assert.AreEqual(findA, findB);
        }

        /// <summary>
        /// Tries to calling union in the union-find structure with an unknown first item.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnionUnknownFirstItem()
        {
            this.unionFind.MakeSet(this.itemB);
            this.unionFind.Union(this.itemA, this.itemB);
        }

        /// <summary>
        /// Tries to calling union in the union-find structure with an unknown second item.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnionUnknownSecondItem()
        {
            this.unionFind.MakeSet(this.itemA);
            this.unionFind.Union(this.itemA, this.itemB);
        }

        #endregion
    }
}