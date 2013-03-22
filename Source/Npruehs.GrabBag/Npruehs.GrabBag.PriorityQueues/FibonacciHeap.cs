// --------------------------------------------------------------------------------
// <copyright file="FibonacciHeap.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------

namespace Npruehs.GrabBag.PriorityQueues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// <para>
    /// Implementation of a Fibonacci heap (abbreviated F-heap) by
    /// Michael L. Fredman and Robert Endre Tarjan which represents a
    /// very fast priority queue.
    /// </para>
    /// <para>
    /// Provides insertion, finding the minimum, melding and decreasing keys in
    /// constant amortized time, and deleting from an n-item heap in O(log n)
    /// amortized time.
    /// </para>
    /// </summary>
    /// <typeparam name="T">
    /// Type of the items held by this Fibonacci heap.
    /// </typeparam>
    [CLSCompliant(true)]
    public class FibonacciHeap<T>
    {
        #region Fields

        /// <summary>
        /// Root containing the item with the minimum key in this heap.
        /// </summary>
        private TreeNode minimumNode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Whether this heap is empty, or not.
        /// </summary>
        public bool Empty
        {
            get
            {
                return this.minimumNode == null;
            }
        }

        /// <summary>
        /// Number of elements of this heap.
        /// </summary>
        public int Size { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Clears this Fibonacci heap, removing all items.
        /// </summary>
        public void Clear()
        {
            this.minimumNode = null;
            this.Size = 0;
        }

        /// <summary>
        /// Decreases the key of the specified item in this heap by subtracting
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
        /// This heap is empty.
        /// </exception>
        public void DecreaseKey(FibonacciHeapItem<T> item, double delta)
        {
            if (delta < 0)
            {
                throw new ArgumentOutOfRangeException("delta", "delta has to be non-negative.");
            }

            if (this.Empty)
            {
                throw new InvalidOperationException("This heap is empty.");
            }

            // Subtract delta from the key of the passed item.
            item.Key -= delta;

            // Cut the edge joining the containing node x to its parent p.
            var x = item.ContainingNode;

            if (x.Parent != null)
            {
                // If x is not a root, remove it from the list of children of
                // its parent, decrease its parent's rank, and add it to the list
                // of roots of this heap in order to preserve the heap order.
                x.CutEdgeToParent(true, this);
            }

            // Redefine the minimum node of this heap, if necessary.
            if (item.Key < this.minimumNode.Item.Key)
            {
                this.minimumNode = x;
            }
        }

        /// <summary>
        /// Decreases the key of the specified item in this heap to the passed
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
        /// This heap is empty.
        /// </exception>
        public void DecreaseKeyTo(FibonacciHeapItem<T> item, double newKey)
        {
            this.DecreaseKey(item, item.Key - newKey);
        }

        /// <summary>
        /// Deletes the specified item from this heap.
        /// </summary>
        /// <param name="item">
        /// Item to delete.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// This heap is empty.
        /// </exception>
        public void Delete(FibonacciHeapItem<T> item)
        {
            if (this.Empty)
            {
                throw new InvalidOperationException("This heap is empty.");
            }

            // Cut the edge joining the containing node x to its parent p.
            var x = item.ContainingNode;

            if (x.Parent == null)
            {
                // x is originally a root - just remove it from the list of roots.
                if (x == this.minimumNode)
                {
                    this.DeleteMin();
                    return;
                }

                x.LeftSibling.RightSibling = x.RightSibling;
                x.RightSibling.LeftSibling = x.LeftSibling;
            }
            else
            {
                // As x is not a root, remove it from the list of children of
                // its parent and decrease its parent's rank.
                x.CutEdgeToParent(false, this);

                // Form a new list of roots by concatenating the list of children of
                // x with the original list of roots.
                if (x.SomeChild != null)
                {
                    this.minimumNode.RightSibling.LeftSibling = x.SomeChild.LeftSibling;
                    x.SomeChild.LeftSibling.RightSibling = this.minimumNode.RightSibling;

                    this.minimumNode.RightSibling = x.SomeChild;
                    x.SomeChild.LeftSibling = this.minimumNode;
                }
            }

            this.Size--;
        }

        /// <summary>
        /// Deletes the item with the minimum key in this heap and returns it.
        /// </summary>
        /// <returns>Item with the minimum key in this heap.</returns>
        /// <exception cref="InvalidOperationException">
        /// This heap is empty.
        /// </exception>
        public FibonacciHeapItem<T> DeleteMin()
        {
            if (this.Empty)
            {
                throw new InvalidOperationException("This heap is empty.");
            }

            // Remove the minimum node x from this heap and concatenate the list
            // of children of x with the list of roots of this heap other than x.
            var x = this.minimumNode;
            var minimumItem = x.Item;

            this.Size--;

            // Remember some node to start with the linking step later on.
            TreeNode someListNode;

            if (x.RightSibling == x)
            {
                if (x.SomeChild == null)
                {
                    // The heap consists of only the root node.
                    this.minimumNode = null;

                    return minimumItem;
                }

                // Root has no siblings - apply linking step to list of children.
                someListNode = x.SomeChild;
            }
            else
            {
                if (x.SomeChild == null)
                {
                    // Root has no children - apply linking step to list of siblings.
                    x.LeftSibling.RightSibling = x.RightSibling;
                    x.RightSibling.LeftSibling = x.LeftSibling;

                    someListNode = x.LeftSibling;
                }
                else
                {
                    // Concatenate children and siblings and apply linking step.
                    x.LeftSibling.RightSibling = x.SomeChild;
                    x.SomeChild.LeftSibling.RightSibling = x.RightSibling;
                    x.RightSibling.LeftSibling = x.SomeChild.LeftSibling;
                    x.SomeChild.LeftSibling = x.LeftSibling;

                    someListNode = x.SomeChild;
                }
            }

            // Linking Step.
            // Create a dictionary indexed by rank, from one up to the maximum
            // possible rank, each entry pointing to a tree root.
            var rankIndexedRoots = new Dictionary<int, TreeNode>();

            // Insert the roots one-by-one into the appropriate table positions.
            var nextOldRoot = someListNode;

            do
            {
                var toBeInserted = nextOldRoot;
                nextOldRoot = nextOldRoot.RightSibling;

                while (toBeInserted != null)
                {
                    // If the position is already occupied, perform a linking
                    // step and attempt to insert the root of the new tree into
                    // the next higher position.
                    if (rankIndexedRoots.ContainsKey(toBeInserted.Rank))
                    {
                        var other = rankIndexedRoots[toBeInserted.Rank];
                        rankIndexedRoots.Remove(toBeInserted.Rank);
                        toBeInserted = toBeInserted.Link(other);
                    }
                    else
                    {
                        rankIndexedRoots.Add(toBeInserted.Rank, toBeInserted);
                        toBeInserted = null;
                    }
                }
            }
            while (nextOldRoot != someListNode);

            // Form a list of the remaining roots, in the process finding a root
            // containing an item of minimum key to serve as the minimum node of the
            // modified heap.
            var newRoots = rankIndexedRoots.Values.ToList();

            // Start with the first new root.
            var firstNewRoot = newRoots[0];
            this.minimumNode = firstNewRoot;
            this.minimumNode.Parent = null;

            var currentNewRoot = firstNewRoot;

            for (var i = 1; i < newRoots.Count; i++)
            {
                // Get the next new root.
                var previousNewRoot = currentNewRoot;
                currentNewRoot = newRoots[i];

                // Update pointers.
                previousNewRoot.RightSibling = currentNewRoot;
                currentNewRoot.LeftSibling = previousNewRoot;
                currentNewRoot.Parent = null;

                // Check for new minimum node.
                if (currentNewRoot.Item.Key < this.minimumNode.Item.Key)
                {
                    this.minimumNode = currentNewRoot;
                }
            }

            currentNewRoot.RightSibling = firstNewRoot;
            firstNewRoot.LeftSibling = currentNewRoot;

            return minimumItem;
        }

        /// <summary>
        /// Returns the item with the minimum key in this heap.
        /// </summary>
        /// <returns>Item with the minimum key in this heap.</returns>
        /// <exception cref="InvalidOperationException">
        /// This heap is empty.
        /// </exception>
        public FibonacciHeapItem<T> FindMin()
        {
            if (this.Empty)
            {
                throw new InvalidOperationException("This heap is empty.");
            }

            return this.minimumNode.Item;
        }

        /// <summary>
        /// Inserts the passed item with the specified key into this heap.
        /// </summary>
        /// <param name="item">
        /// Item to insert.
        /// </param>
        /// <param name="key">
        /// Key of the item to insert.
        /// </param>
        /// <returns>
        /// Container that holds the passed item.
        /// </returns>
        public FibonacciHeapItem<T> Insert(T item, double key)
        {
            // Construct a new container for the passed item.
            var newItem = new FibonacciHeapItem<T>(item, key);

            // Create a new heap consisting of one node containing the passed item.
            var newHeap = new FibonacciHeap<T> { minimumNode = new TreeNode(newItem), Size = 1 };

            // Meld this heap and the new one.
            this.Meld(newHeap);

            return newItem;
        }

        /// <summary>
        /// <para>
        /// Takes the union of the passed heap and this one. Assumes that both heaps
        /// are item-disjoint.
        /// </para>
        /// <para>
        /// This operation destroys the passed heap.
        /// </para>
        /// </summary>
        /// <param name="other">
        /// Other heap to take the union of.
        /// </param>
        public void Meld(FibonacciHeap<T> other)
        {
            // If the other heap is empty, there is nothing to do.
            if (other.Empty)
            {
                return;
            }

            if (this.Empty)
            {
                // If this heap is empty, return the other heap.
                this.minimumNode = other.minimumNode;
            }
            else
            {
                // Combine the root lists of both heaps into a single list.
                this.minimumNode.RightSibling.LeftSibling = other.minimumNode.LeftSibling;
                other.minimumNode.LeftSibling.RightSibling = this.minimumNode.RightSibling;

                this.minimumNode.RightSibling = other.minimumNode;
                other.minimumNode.LeftSibling = this.minimumNode;

                // Set the minimum node of the resulting heap.
                if (this.minimumNode.Item.Key > other.minimumNode.Item.Key)
                {
                    this.minimumNode = other.minimumNode;
                }
            }

            this.Size += other.Size;
        }

        #endregion

        /// <summary>
        /// Node of an heap-ordered tree. Contains an item with a key which allows
        /// comparing it to other heap items for order. Provides pointers to its
        /// parent node, to its left and right siblings, and to one of its children.
        /// Can be marked in order to decide whether to make a cascading cut after
        /// the edge to this node's parent has been cut, or not.
        /// </summary>
        public class TreeNode
        {
            #region Constructors and Destructors

            /// <summary>
            /// Constructs a new heap-ordered tree node holding the passed item.
            /// Initially, the node has no siblings.
            /// </summary>
            /// <param name="item">
            /// Container holding this node's item and its key.
            /// </param>
            public TreeNode(FibonacciHeapItem<T> item)
            {
                this.Item = item;
                item.ContainingNode = this;

                this.LeftSibling = this;
                this.RightSibling = this;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Container holding this node's item and its key.
            /// </summary>
            internal FibonacciHeapItem<T> Item { get; set; }

            /// <summary>
            /// Left sibling of this node.
            /// </summary>
            internal TreeNode LeftSibling { get; set; }

            /// <summary>
            /// Whether to perform a cascading cut after the edge to this node's
            /// parent has been cut, or not.
            /// </summary>
            internal bool Marked { get; set; }

            /// <summary>
            /// Parent of this node.
            /// </summary>
            internal TreeNode Parent { get; set; }

            /// <summary>
            /// Number of children of this node.
            /// </summary>
            internal int Rank { get; set; }

            /// <summary>
            /// Right sibling of this node.
            /// </summary>
            internal TreeNode RightSibling { get; set; }

            /// <summary>
            /// One of the children of this node.
            /// </summary>
            internal TreeNode SomeChild { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Adds the passed heap-ordered tree node to the list of this node's
            /// children, increasing the rank of this node.
            /// </summary>
            /// <param name="node">
            /// New child of this node.
            /// </param>
            public void AddChild(TreeNode node)
            {
                // Update the rank of this node.
                this.Rank++;

                // Set the parent of the new node.
                node.Parent = this;

                if (this.SomeChild == null)
                {
                    // New node is the only child (has no siblings).
                    node.LeftSibling = node;
                    node.RightSibling = node;
                }
                else
                {
                    // Append new node to the right.
                    node.LeftSibling = this.SomeChild;
                    node.RightSibling = this.SomeChild.RightSibling;
                    node.RightSibling.LeftSibling = node;
                    this.SomeChild.RightSibling = node;
                }

                this.SomeChild = node;
            }

            /// <summary>
            /// Cuts the edge to this node's parent, decreasing the rank of its
            /// parent. Performs a cascading cut if necessary.
            /// </summary>
            /// <param name="addToRootList">
            /// Whether this node should be added to the list of roots of its
            /// heap, or not.
            /// </param>
            /// <param name="heap">
            /// Heap whose root list this node is added to, if
            /// <paramref name="addToRootList"/> is set to true.
            /// </param>
            public void CutEdgeToParent(bool addToRootList, FibonacciHeap<T> heap)
            {
                // Remove this node from the list of children of its parent.
                if (this.LeftSibling != this)
                {
                    this.LeftSibling.RightSibling = this.RightSibling;
                    this.RightSibling.LeftSibling = this.LeftSibling;

                    this.Parent.SomeChild = this.LeftSibling;
                }
                else
                {
                    this.Parent.SomeChild = null;
                }

                // Decrease the rank of this node's parent.
                this.Parent.Rank--;

                if (this.Parent.Parent != null)
                {
                    // Parent is not a root.
                    if (!this.Parent.Marked)
                    {
                        // Mark it if it is unmarked.
                        this.Parent.Marked = true;
                    }
                    else
                    {
                        // Cut the edge to its parent if it is marked.
                        this.Parent.CutEdgeToParent(true, heap);
                    }
                }

                this.Parent = null;

                if (!addToRootList)
                {
                    return;
                }

                // Add this node to the list of roots of this heap.
                heap.minimumNode.RightSibling.LeftSibling = this;
                this.RightSibling = heap.minimumNode.RightSibling;

                heap.minimumNode.RightSibling = this;
                this.LeftSibling = heap.minimumNode;
            }

            /// <summary>
            /// Combines the heap-ordered tree represented by the passed root node
            /// with the tree represented by this one. Assumes that both trees are
            /// item-disjoint.
            /// </summary>
            /// <param name="otherTreeRoot">
            /// Root of the other tree to combine with this one.
            /// </param>
            /// <returns>
            /// Root of the resulting heap-ordered tree.
            /// </returns>
            public TreeNode Link(TreeNode otherTreeRoot)
            {
                if (this.Item.Key < otherTreeRoot.Item.Key)
                {
                    this.AddChild(otherTreeRoot);
                    otherTreeRoot.Marked = false;
                    return this;
                }

                otherTreeRoot.AddChild(this);
                this.Marked = false;
                return otherTreeRoot;
            }

            #endregion
        }
    }
}