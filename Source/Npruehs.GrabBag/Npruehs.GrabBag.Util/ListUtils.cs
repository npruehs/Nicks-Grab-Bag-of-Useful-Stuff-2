// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListUtils.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Util
{
    using System;
    using System.Collections.Generic;

    using Npruehs.GrabBag.Math;

    /// <summary>
    /// Utility methods for lists.
    /// </summary>
    public class ListUtils
    {
        #region Public Methods and Operators

        /// <summary>
        /// Sorts the passed list by the specified key.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the list elements.
        /// </typeparam>
        /// <param name="a">
        /// List to sort.
        /// </param>
        /// <param name="sortKey">
        /// Key to sort the list by.
        /// </param>
        public static void RadixSort<T>(IList<T> a, Func<T, int> sortKey)
        {
            RadixSort(a, 0, a.Count - 1, 31, sortKey);
        }

        /// <summary>
        /// Sorts the passed list by the specified key.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the list elements.
        /// </typeparam>
        /// <param name="a">
        /// List to sort.
        /// </param>
        /// <param name="l">
        /// First list index to consider.
        /// </param>
        /// <param name="r">
        /// Last list index to consider.
        /// </param>
        /// <param name="i">
        /// Bit to consider.
        /// </param>
        /// <param name="sortKey">
        /// Key to sort the list by.
        /// </param>
        public static void RadixSort<T>(IList<T> a, int l, int r, int i, Func<T, int> sortKey)
        {
            // Check for termination.
            if ((i < 0) || (l >= r))
            {
                return;
            }

            int L = l - 1;
            int R = r + 1;

            // Look for the first element to be swapped from the left side.
            do
            {
                L++;
            }
            while (L <= r && Math2.GetBit(sortKey(a[L]), i) == 0);

            // Look for the first element to be swapped from the right side.
            do
            {
                R--;
            }
            while (R >= l && Math2.GetBit(sortKey(a[R]), i) == 1);

            while (L < R)
            {
                // Swap both elements.
                Swap(a, L, R);

                // Look for the next element to be swapped from the left side.
                do
                {
                    L++;
                }
                while (Math2.GetBit(sortKey(a[L]), i) == 0);

                // Look for the next element to be swapped from the right side.
                do
                {
                    R--;
                }
                while (Math2.GetBit(sortKey(a[R]), i) == 1);
            }

            /*
             * Recursively sort the left and right parts of the initial
             * interval, looking at the next bit.
             */
            RadixSort(a, l, R, i - 1, sortKey);
            RadixSort(a, L, r, i - 1, sortKey);
        }

        /// <summary>
        /// Swaps the elements with the specified indices in the passed list.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the list elements to swap.
        /// </typeparam>
        /// <param name="list">
        /// List to swap the elements of.
        /// </param>
        /// <param name="i">
        /// Index of the first element to swap.
        /// </param>
        /// <param name="j">
        /// Index of the second element to swap.
        /// </param>
        public static void Swap<T>(IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        #endregion
    }
}