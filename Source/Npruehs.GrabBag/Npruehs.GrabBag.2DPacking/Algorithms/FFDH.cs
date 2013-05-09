// --------------------------------------------------------------------------------
// <copyright company="Nick Pruehs" file="FFDH.cs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// 
// --------------------------------------------------------------------------------
namespace Npruehs.GrabBag._2DPacking
{
    using System.Collections.Generic;

    /// <summary>
    /// 1.7OPT + 1
    /// O(n log n)
    /// </summary>
    public class FFDH : PackingAlgorithm
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="stripWidth">
        /// </param>
        /// <param name="items">
        /// </param>
        /// <returns>
        /// </returns>
        public override float FindSolution(float stripWidth, ICollection<PackingItem> items)
        {
            return this.FindSolution(stripWidth, items, 0f);
        }

        /// <summary>
        /// </summary>
        /// <param name="stripWidth">
        /// </param>
        /// <param name="items">
        /// </param>
        /// <param name="maximumStripHeight">
        /// </param>
        /// <returns>
        /// </returns>
        public float FindSolution(float stripWidth, ICollection<PackingItem> items, float maximumStripHeight)
        {
            var currentLevel = this.AddLevel(0f);

            // TODO Sort items by decreasing height.
            var itemList = new List<PackingItem>(items);
            itemList.Sort();

            foreach (var item in itemList)
            {
                // Check if level width exceeded.
                if (currentLevel.Width >= stripWidth)
                {
                    // Add new level on top of current one.
                    var firstItemHeight = currentLevel[0].Rectangle.Height;
                    currentLevel = this.AddLevel(firstItemHeight);
                }

                // Check if maximum strip height is exceeded.
                if (maximumStripHeight > 0 && currentLevel.Y + item.Rectangle.Y > maximumStripHeight)
                {
                    break;
                }

                // Put item into current level.
                item.Rectangle.X = currentLevel.Width;
                item.Rectangle.Y = currentLevel.Y;
                currentLevel.AddItem(item);
            }

            return this.ComputeValue();
        }

        #endregion
    }
}