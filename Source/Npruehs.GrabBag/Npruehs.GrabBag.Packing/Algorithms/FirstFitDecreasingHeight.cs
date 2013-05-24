// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirstFitDecreasingHeight.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Packing.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// <para>
    /// Implementation of the First Fit Decreasing Height packing algorithm.
    /// </para>
    /// <para>
    /// Packs n items in  O(n log n) time, generating an 1.7OPT + 1 solution.
    /// </para>
    /// </summary>
    public class FirstFitDecreasingHeight : PackingAlgorithm
    {
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
        public override float FindSolution(float stripWidth, ICollection<PackingItem> items)
        {
            return this.FindSolution(stripWidth, items, 0f);
        }

        /// <summary>
        /// Finds a solution for the passed packing problem instance data.
        /// </summary>
        /// <param name="stripWidth">
        /// Width of the strip the items have to be packed into.
        /// </param>
        /// <param name="items">
        /// Items that have to be packed.
        /// </param>
        /// <param name="maximumStripHeight">
        /// Maximum strip height before the algorithm stops packing and returns.
        /// </param>
        /// <returns>
        /// Maximum strip height that has been used.
        /// </returns>
        public float FindSolution(float stripWidth, ICollection<PackingItem> items, float maximumStripHeight)
        {
            // Add first level.
            var highestLevel = this.AddLevel(0f);

            // Sort items by decreasing height.
            var itemList = items.OrderByDescending(item => item.Rectangle.Height).ToList();

            foreach (var item in itemList)
            {
                // Find first level the current item fits.
                PackingLevel fittingLevel =
                    this.Levels.FirstOrDefault(level => level.Width + item.Rectangle.Width <= stripWidth);

                if (fittingLevel == null)
                {
                    // Add new level on top.
                    highestLevel = this.AddLevel(highestLevel.Y + highestLevel[0].Rectangle.Height);
                    fittingLevel = highestLevel;
                }

                // Check if maximum strip height is exceeded.
                if (maximumStripHeight > 0 && fittingLevel.Y + item.Rectangle.Height > maximumStripHeight)
                {
                    break;
                }

                // Put item into fitting level.
                item.Rectangle.X = fittingLevel.Width;
                item.Rectangle.Y = fittingLevel.Y;
                fittingLevel.AddItem(item);
            }

            return this.ComputeValue();
        }

        #endregion
    }
}