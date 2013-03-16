﻿// --------------------------------------------------------------------------------
// <copyright file="UnionFind.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.UnionFind
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implementation of <i>Robert Endre Tarjan</i>'s union-find structure,
    /// using union with size and find with path compression.
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

            var canonical = this.FindNode(node);
            return canonical.Item;
        }

        /// <summary>
        /// Returns the canonical element of the set containing v.
        /// The question "Are v and w in the same set?" can be reduced to Find(v) == Find(w).
        /// </summary>
        /// <param name="v">
        /// Node to get the canonical element of.
        /// </param>
        /// <returns>
        /// Canonical element of the set containing v.
        /// </returns>
        public UnionFindNode<T> FindNode(UnionFindNode<T> v)
        {
            // Find root note.
            var root = v;

            while (root.Parent != null)
            {
                root = root.Parent;
            }

            // Path compression.
            var current = v;

            while (current != root)
            {
                var next = current.Parent;
                current.Parent = root;
                current = next;
            }

            return root;
        }

        /// <summary>
        /// Adds a new singleton set containing the passed item.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        public void MakeSet(T item)
        {
            var set = this.MakeSetNode(item);

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
        /// Adds a new singleton set containing the passed item.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        /// <returns>
        /// Container holding the passed item.
        /// </returns>
        public UnionFindNode<T> MakeSetNode(T item)
        {
            return new UnionFindNode<T>(item);
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
                this.UnionNodes(nodeFirst, nodeSecond);
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Unknown item: {0}. Use MakeSet for adding new elements to this union-find universe.", second));
            }
        }

        /// <summary>
        /// Merges the sets containing the canonical elements
        /// <paramref name="v"/> and <paramref name="w"/>.
        /// </summary>
        /// <param name="v">
        /// An element in the first set to merge.
        /// </param>
        /// <param name="w">
        /// An element in the second set to merge.
        /// </param>
        public void UnionNodes(UnionFindNode<T> v, UnionFindNode<T> w)
        {
            var rootV = this.FindNode(v);
            var rootW = this.FindNode(w);

            if (v == w)
            {
                return;
            }

            // Union with size: Make the root of the smaller tree a child of the root of the larger one.
            if (rootV.SubtreeSize < rootW.SubtreeSize)
            {
                rootV.Parent = rootW;
                rootW.SubtreeSize += rootV.SubtreeSize;
            }
            else
            {
                rootW.Parent = rootV;
                rootV.SubtreeSize += rootW.SubtreeSize;
            }
        }

        #endregion
    }
}