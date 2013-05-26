// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EpsteinVanStee.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Packing.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// <para>
    /// Implementation of the packing algorithm presented in "This Side Up!" by
    /// Leah Epstein and Rob van Stee in 2006.
    /// </para>
    /// <para>
    /// Packs n items in  O(n log n) time, generating an 1.5OPT + 3 solution.
    /// </para>
    /// <para>
    /// This algorithm rotates items for approximating a better solution.
    /// </para>
    /// </summary>
    public class EpsteinVanStee : PackingAlgorithm
    {
        #region Constants

        /// <summary>
        /// Maximum width and height of the items processed by Epstein/van Stee.
        /// </summary>
        public const float AssumedMaximumItemWidthAndHeight = 1.0f;

        /// <summary>
        /// Strip width Epstein/van Stee assumes.
        /// </summary>
        public const float AssumedStripWidth = 1.0f;

        /// <summary>
        /// Minimum height of a <i>big item</i>.
        /// </summary>
        private const float BigItemHeight = 0.5f;

        /// <summary>
        /// Minimum width of a <i>big item</i>.
        /// </summary>
        private const float BigItemWidth = 0.5f;

        /// <summary>
        /// Maximum width of the item whose level is the first sub-strip level.
        /// </summary>
        private const float ItemWidthForBeginOfSubstrip = 2.0f / 3.0f;

        /// <summary>
        /// Maximum width of the items that are packed on-top of the sub-strip in two bins.
        /// </summary>
        private const double ItemWidthForTwostrippacking = 1.0d / 3.0d;

        /// <summary>
        /// Width of the small sub-strip the very small items are packed into.
        /// </summary>
        private const float SubstripWidth = 1.0f / 3.0f;

        /// <summary>
        /// Maximum width of the items that are packed into the small sub-strip.
        /// </summary>
        private const float VerySmallItemWidth = 1.0f / 6.0f;

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
        public override float FindSolution(float stripWidth, ICollection<PackingItem> items)
        {
            // Check strip width.
            if (!stripWidth.Equals(AssumedStripWidth))
            {
                throw new ArgumentOutOfRangeException(
                    "stripWidth", string.Format("This algorithm assumes a strip width of {0}.", AssumedStripWidth));
            }

            // Construct lists for big and very small items
            var bigItems = new List<PackingItem>();
            var verySmallItems = new List<PackingItem>();

            // Iterate over all items.
            foreach (var item in items)
            {
                // Check their dimensions.
                double width = item.Rectangle.Width;
                double height = item.Rectangle.Height;

                if ((height > AssumedMaximumItemWidthAndHeight) || (width > AssumedMaximumItemWidthAndHeight))
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(
                            "This algorithm assumes item " + "widths and heights of at most {0}.", 
                            AssumedMaximumItemWidthAndHeight));
                }

                if ((width > BigItemWidth) && (height > BigItemHeight))
                {
                    // Item is a big item.
                    bigItems.Add(item);

                    // Rotate it so that its width is not smaller than its height.
                    if (width < height)
                    {
                        item.Rotate();
                    }
                }
                else if (height < width)
                {
                    // Rotate items so that their height is greater than their width as specified by the long version of the paper.
                    item.Rotate();
                }

                // Check if we have a very small item now.
                if (item.Rectangle.Width <= VerySmallItemWidth)
                {
                    verySmallItems.Add(item);
                }
            }

            // Remove all items that have been assigned to other lists.
            items = new List<PackingItem>(items.Except(bigItems));
            items = new List<PackingItem>(items.Except(verySmallItems));

            // Sort big items by decreasing width.
            bigItems = bigItems.OrderByDescending(item => item.Rectangle.Width).ToList();

            // Remember the height of the strip used for the big items only.
            float h1 = 0;

            // Remember the height of the strip the first item of width at most 2/3 is packed (h1' in the paper).
            float h2 = 0;

            // 1. Place all big items
            foreach (var bigItem in bigItems)
            {
                // Construct new level for the current big item.
                var level = this.AddLevel(h1);

                // Place the item.
                bigItem.Rectangle.X = 0;
                bigItem.Rectangle.Y = h1;

                level.AddItem(bigItem);

                h1 += bigItem.Rectangle.Height;

                // Check whether h2 (h1' in the paper) has to be set.
                if ((h2 <= 0f) && (bigItem.Rectangle.Width <= ItemWidthForBeginOfSubstrip))
                {
                    h2 = h1;
                }
            }

            if (h2 <= 0f)
            {
                h2 = h1;
            }

            // 2. Pack very small items into a substrip, if possible.

            // Remember the height of the substrip for step 3.
            float substripHeight = 0;

            if (h2 < h1)
            {
                // Pack items that have widths in (0, 1/6] inside substrip of width 1/3 using FFDH.
                var ffdh = new FirstFitDecreasingHeight();
                substripHeight = ffdh.FindSolution(SubstripWidth, verySmallItems, h1 - h2);

                // Make substrip start at height h1', at the right side of the strip.
                var substripLevels = ffdh.Levels;

                foreach (var substripLevel in substripLevels)
                {
                    // Construct new mainstrip level for the current substrip level.
                    var level = this.AddLevel(h2 + substripLevel.Y);

                    // Iterate over all items of the substrip level.
                    foreach (var substripItem in substripLevel.Items)
                    {
                        // Adjust position.
                        substripItem.Rectangle.X += ItemWidthForBeginOfSubstrip;
                        substripItem.Rectangle.Y += h2;

                        level.AddItem(substripItem);

                        // Remove item because is has been packed with FFDH.
                        verySmallItems.Remove(substripItem);
                    }
                }
            }

            // 3. If all items that have width in (0, 1/6] have now been packed:
            if (verySmallItems.Count == 0)
            {
                // (a) Stack the remaining items up to height h1.

                // Construct new substrip level.
                var currentLevel = this.AddLevel(h2 + substripHeight);

                // Initialize coordinates for the next item to be placed.
                var nextLevelY = currentLevel.Y;
                var nextItemRightBorderX = AssumedStripWidth;

                // Place items in order of increasing width.
                foreach (var currentItem in items.Reverse())
                {
                    // Check whether it overlaps with previously placed items.
                    currentItem.Rectangle.X = nextItemRightBorderX - currentItem.Rectangle.Width;
                    currentItem.Rectangle.Y = currentLevel.Y;

                    var intersects = false;

                    foreach (var existingLevel in this.Levels)
                    {
                        foreach (var placedItem in existingLevel.Items)
                        {
                            intersects |= currentItem.Rectangle.Intersects(placedItem.Rectangle);

                            if (intersects)
                            {
                                break;
                            }
                        }

                        if (intersects)
                        {
                            break;
                        }
                    }

                    if (intersects)
                    {
                        // If yes, construct new substrip level.
                        currentLevel = this.AddLevel(nextLevelY);
                        nextItemRightBorderX = AssumedStripWidth;
                        currentItem.Rectangle.X = nextItemRightBorderX - currentItem.Rectangle.Width;
                        currentItem.Rectangle.Y = currentLevel.Y;
                    }

                    // Check whether it would be placed (partially) above h1.
                    if (currentLevel.Y + currentItem.Rectangle.Height > h1)
                    {
                        // If yes, stop packing and proceed with (b).
                        break;
                    }

                    // Place the item.
                    currentLevel.AddItem(currentItem);

                    // Compute the x-coordinate of the next item.
                    nextItemRightBorderX -= currentItem.Rectangle.Width;

                    // Update the y-coordinate of the next level, if necessary.
                    nextLevelY = Math.Max(nextLevelY, currentLevel.Y + currentItem.Rectangle.Height);

                    // Mark item as packed.
                    items.Remove(currentItem);
                }

                // (b) Place the unpacked items of width in (1/3, 1/2] in two stacks.

                // Get unpacked items of width in (1/3, 1/2].
                var greaterThanOneThird =
                    items.Where(item => item.Rectangle.Width > ItemWidthForTwostrippacking).ToList();

                items = items.Except(greaterThanOneThird).ToList();

                // Initialize coordinates for the next item to be placed.
                var currentLevelLeftY = h1;
                var currentLevelRightY = h1;

                // Pack the items.
                foreach (var item in greaterThanOneThird)
                {
                    // Add new level.
                    currentLevel = this.AddLevel(h1);

                    if (currentLevelLeftY <= currentLevelRightY)
                    {
                        // Add to the left stack.
                        item.Rectangle.X = 0;
                        item.Rectangle.Y = currentLevelLeftY;

                        currentLevel.AddItem(item);

                        // Adjust height of the left stack.
                        currentLevelLeftY += item.Rectangle.Height;
                    }
                    else
                    {
                        // Add to the right stack.
                        item.Rectangle.X = 0.5f;
                        item.Rectangle.Y = currentLevelRightY;

                        currentLevel.AddItem(item);

                        // Adjust height of the left stack.
                        currentLevelRightY += item.Rectangle.Height;
                    }
                }

                // Get the height of the higher stack.
                var finalStackY = Math.Max(currentLevelLeftY, currentLevelRightY);

                // Pack the unpacked items of width in (1/6, 1/3] using FFDH.
                var ffdh = new FirstFitDecreasingHeight();
                ffdh.FindSolution(AssumedStripWidth, items);

                // Adapt the y-coordinates of all items.
                var remainingLevels = ffdh.Levels;

                foreach (var remainingLevel in remainingLevels)
                {
                    // Construct new mainstrip level for the current level.
                    var level = this.AddLevel(remainingLevel.Y + finalStackY);

                    // Iterate over all items.
                    foreach (var remainingItem in remainingLevel.Items)
                    {
                        // Adjust position.
                        remainingItem.Rectangle.Y += finalStackY;

                        // Place the item.
                        level.AddItem(remainingItem);
                    }
                }
            }
            else
            {
                // 4. Else: Place all remaining items above height h1 with FFDH.
                items = items.Union(verySmallItems).ToList();

                var ffdh = new FirstFitDecreasingHeight();
                ffdh.FindSolution(AssumedStripWidth, items);

                // Adapt the y-coordinates of all items.
                var remainingLevels = ffdh.Levels;

                foreach (var remainingLevel in remainingLevels)
                {
                    // Construct new mainstrip level for the current level.
                    var level = this.AddLevel(h1);

                    // Iterate over all items
                    foreach (var remainingItem in remainingLevel.Items)
                    {
                        // Adjust position.
                        remainingItem.Rectangle.Y += h1;

                        // Place the item.
                        level.AddItem(remainingItem);
                    }
                }
            }

            // Finish and return solution.
            return this.ComputeValue();
        }

        #endregion
    }
}