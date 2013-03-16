// --------------------------------------------------------------------------------
// <copyright file="IUnionFind.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.UnionFind
{
    /// <summary>
    /// <para>
    /// Structure which supports two types of operations for manipulating a
    /// family of disjoint sets which partition a universe of n elements:
    /// </para>
    /// <para>
    /// - find(v) computes the name of the (unique) set containing the element v.
    /// </para>
    /// <para>
    /// - union(v, w) combines the sets containing the elements v and w into a new set.
    /// </para>
    /// </summary>
    /// <typeparam name="T">
    /// Type of the elements stored in this union-find structure.
    /// </typeparam>
    public interface IUnionFind<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Returns the canonical element of the set containing <paramref name="item"/>.
        /// The question "Are v and w in the same set?" can be reduced to find(v) == find(w).
        /// </summary>
        /// <param name="item">
        /// Item to get the canonical element of.
        /// </param>
        /// <returns>
        /// Canonical element of the set containing <paramref name="item"/>.
        /// </returns>
        T Find(T item);

        /// <summary>
        /// Adds a new singleton set containing the passed item.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        void MakeSet(T item);

        /// <summary>
        /// Merges the sets containing the canonical elements <paramref name="v"/> and <paramref name="w"/>.
        /// </summary>
        /// <param name="v">
        /// An element in the first set to merge.
        /// </param>
        /// <param name="w">
        /// An element in the second set to merge.
        /// </param>
        void Union(T v, T w);

        #endregion
    }
}