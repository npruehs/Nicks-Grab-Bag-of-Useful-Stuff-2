// --------------------------------------------------------------------------------
// <copyright company="Nick Pruehs" file="PackingAlgorithm.cs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// 
// --------------------------------------------------------------------------------
namespace Npruehs.GrabBag._2DPacking
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 2D packing algorithm.
    /// </summary>
    public abstract class PackingAlgorithm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new 2D packing algorithm, initializing its list of
        /// item levels.
        /// </summary>
        protected PackingAlgorithm()
        {
            this.Levels = new List<PackingLevel>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Levels the items of the current solution have been packed into.
        /// </summary>
        public List<PackingLevel> Levels { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Finds a solution for the passed packing problem instance data.
        /// </summary>
        /// <param name="stripWidth">
        /// Width of the strip the items have to be packed into.
        /// </param>
        /// <param name="items">
        /// Items that have to be packed.
        /// </param>
        /// <returns>
        /// Maximum strip height that has been used.
        /// </returns>
        public abstract float FindSolution(float stripWidth, ICollection<PackingItem> items);

        #endregion

        #region Methods

        /// <summary>
        /// Computes the value of the current solution, that is the maximum
        /// strip height that has been used for packing all items.
        /// </summary>
        /// <returns>
        /// Maximum strip height that has been used for packing all items.
        /// </returns>
        protected float ComputeValue()
        {
            float value = 0;

            foreach (var level in this.Levels)
            {
                foreach (var item in level.Items)
                {
                    var itemValue = item.Rectangle.Y + item.Rectangle.Height;
                    value = Math.Max(value, itemValue);
                }
            }

            return value;
        }

        protected PackingLevel AddLevel(float y)
        {
            PackingLevel level = new PackingLevel(y);
            this.Levels.Add(level);
            return level;
        }
        #endregion
    }
}