// --------------------------------------------------------------------------------
// <copyright file="UnionFindNode.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.UnionFind
{
    /// <summary>
    /// Node of <i>Robert Endre Tarjan</i>'s union-find structure.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the item held by this union-find node.
    /// </typeparam>
    public class UnionFindNode<T>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new union-find node containing the passed item.
        /// The new node has no parent and a sub-tree size of 1.
        /// </summary>
        /// <param name="item">
        /// Item held by the new node.
        /// </param>
        public UnionFindNode(T item)
        {
            this.Item = item;
            this.SubtreeSize = 1;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Item held by this union-find node.
        /// </summary>
        public T Item { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Parent of this union-find node.
        /// </summary>
        internal UnionFindNode<T> Parent { get; set; }

        /// <summary>
        /// Size of the sub-tree of this union-find node.
        /// </summary>
        internal int SubtreeSize { get; set; }

        #endregion
    }
}