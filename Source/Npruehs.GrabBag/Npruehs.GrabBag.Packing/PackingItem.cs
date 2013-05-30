// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackingItem.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Packing
{
    using System;

    using Npruehs.GrabBag.Math.Geometry;

    /// <summary>
    /// Item to be packed into a strip with a fixed width.
    /// </summary>
    [CLSCompliant(true)]
    public class PackingItem : IEquatable<PackingItem>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new item to be packed into a strip with a fixed width.
        /// </summary>
        public PackingItem()
        {
            this.Rectangle = new RectangleF();
        }

        /// <summary>
        /// Constructs a new item to be packed into a strip with a fixed width
        /// with the specified id, width and height.
        /// </summary>
        /// <param name="id">
        /// Unique id of the new item.
        /// </param>
        /// <param name="width">
        /// Width of the new item.
        /// </param>
        /// <param name="height">
        /// Height of the new item.
        /// </param>
        public PackingItem(int id, float width, float height)
        {
            this.Id = id;
            this.Rectangle = new RectangleF(0f, 0f, width, height);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Unique id of this item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Width, height and position of this item.
        /// </summary>
        public RectangleF Rectangle { get; private set; }

        /// <summary>
        /// Whether this item has been rotated before being packed into the
        /// strip, or not.
        /// </summary>
        public bool Rotated { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Checks whether this item is equal to the passed other one.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if this item is equal to the passed other one, and <c>false</c> otherwise.
        /// </returns>
        /// <param name="other">
        /// Item to compare this one to.
        /// </param>
        public bool Equals(PackingItem other)
        {
            return this.Id.Equals(other.Id);
        }

        /// <summary>
        /// Rotates this item, interchanging its width and height.
        /// </summary>
        public void Rotate()
        {
            var oldWidth = this.Rectangle.Width;
            this.Rectangle.Width = this.Rectangle.Height;
            this.Rectangle.Height = oldWidth;

            this.Rotated = !this.Rotated;
        }

        /// <summary>
        /// Returns a <see cref="string"/> representation of this item.
        /// </summary>
        /// <returns>
        /// This item as <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: {0}, {1}", this.Id, this.Rectangle);
        }

        #endregion
    }
}