// --------------------------------------------------------------------------------
// <copyright file="UnionFind.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.UnionFind
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Wraps <i>Robert Endre Tarjan</i>'s union-find structure,
    /// using union with size and find with path compression, by mapping
    /// elements to union-find nodes with a dictionary. Use
    /// <see cref="UnionFindStructure{T}"/> for handling the nodes yourself.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the elements of the union-find universe.
    /// </typeparam>
    [CLSCompliant(true)]
    public class UnionFind<T> : IUnionFind<T>
    {
        #region Fields

        /// <summary>
        /// Maps elements of the universe to their respective union-find nodes.
        /// </summary>
        private readonly Dictionary<T, UnionFindNode<T>> nodes = new Dictionary<T, UnionFindNode<T>>();

        /// <summary>
        /// Union-find structure handling unions and finds.
        /// </summary>
        private readonly UnionFindStructure<T> unionFind = new UnionFindStructure<T>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Returns the canonical element of the set containing <paramref name="item"/>.
        /// The question "Are v and w in the same set?" can be reduced to Find(v) == Find(w).
        /// </summary>
        /// <param name="item">
        /// Item to get the canonical element of.
        /// </param>
        /// <returns>
        /// Canonical element of the set containing v.
        /// </returns>
        public T Find(T item)
        {
            UnionFindNode<T> node;

            this.nodes.TryGetValue(item, out node);

            if (node == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Unknown item: {0}. Use MakeSet for adding new elements to this union-find universe.", item));
            }

            var canonical = this.unionFind.Find(node);
            return canonical.Item;
        }

        /// <summary>
        /// Returns an enumerator that can iterate through this union-find structure.
        /// </summary>
        /// <returns>Enumerator that can be used to iterate through this union-find structure.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.nodes.Keys.GetEnumerator();
        }

        /// <summary>
        /// Adds a new singleton set containing the passed item.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        public void MakeSet(T item)
        {
            var set = this.unionFind.MakeSet(item);

            if (!this.nodes.ContainsKey(item))
            {
                this.nodes.Add(item, set);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format(
                        "This union-find universe already contains the element {0}. Elements in union-find structures are unique.", 
                        item));
            }
        }

        /// <summary>
        /// Merges the sets containing the canonical elements
        /// <paramref name="first"/> and <paramref name="second"/>.
        /// </summary>
        /// <param name="first">
        /// An element in the first set to merge.
        /// </param>
        /// <param name="second">
        /// An element in the second set to merge.
        /// </param>
        public void Union(T first, T second)
        {
            UnionFindNode<T> nodeFirst;

            if (!this.nodes.TryGetValue(first, out nodeFirst))
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Unknown item: {0}. Use MakeSet for adding new elements to this union-find universe.", first));
            }

            UnionFindNode<T> nodeSecond;

            if (this.nodes.TryGetValue(second, out nodeSecond))
            {
                this.unionFind.Union(nodeFirst, nodeSecond);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Unknown item: {0}. Use MakeSet for adding new elements to this union-find universe.", second));
            }
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// Returns an enumerator that can iterate through this union-find structure.
        /// </summary>
        /// <returns>Enumerator that can be used to iterate through this union-find structure.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}