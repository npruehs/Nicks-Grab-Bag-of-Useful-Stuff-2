// --------------------------------------------------------------------------------
// <copyright file="UnionFindStructure.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.UnionFind
{
    using System;

    /// <summary>
    /// Implementation of <i>Robert Endre Tarjan</i>'s union-find structure,
    /// using union with size and find with path compression.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the elements of the union-find universe.
    /// </typeparam>
    [CLSCompliant(true)]
    public class UnionFindStructure<T>
    {
        #region Public Methods and Operators

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
        public UnionFindNode<T> Find(UnionFindNode<T> v)
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
        /// <returns>
        /// Container holding the passed item.
        /// </returns>
        public UnionFindNode<T> MakeSet(T item)
        {
            return new UnionFindNode<T>(item);
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
        public void Unite(UnionFindNode<T> v, UnionFindNode<T> w)
        {
            var rootV = this.Find(v);
            var rootW = this.Find(w);

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