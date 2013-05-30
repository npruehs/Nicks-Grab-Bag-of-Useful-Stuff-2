// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackingLevel.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Packing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Strip level of a solution to a specific instance of a 2D packing problem.
    /// Contains one or more items that have been packed here, as well as their respective positions.
    /// </summary>
    [CLSCompliant(true)]
    public class PackingLevel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new, empty strip level.
        /// </summary>
        public PackingLevel()
        {
            this.Items = new List<PackingItem>();
        }

        /// <summary>
        /// Constructs a new, empty strip level beginning at the specified
        /// y-position.
        /// </summary>
        /// <param name="y">
        /// Y-component of the position of the level in the strip.
        /// </param>
        public PackingLevel(float y)
            : this()
        {
            this.Y = y;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Items packed into this level.
        /// </summary>
        public List<PackingItem> Items { get; private set; }

        /// <summary>
        /// Total width of the items of this level.
        /// </summary>
        public float Width { get; private set; }

        /// <summary>
        /// Y-component of the position of this level in the strip.
        /// </summary>
        public float Y { get; private set; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets the item with the specified index in this level.
        /// </summary>
        /// <param name="index">
        /// Index of the item to get.
        /// </param>
        /// <returns>
        /// Item with the specified index in this level.
        /// </returns>
        public PackingItem this[int index]
        {
            get
            {
                return this.Items[index];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the specified item to this strip level.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        public void AddItem(PackingItem item)
        {
            this.Items.Add(item);
            this.Width += item.Rectangle.Width;
        }

        #endregion
    }
}