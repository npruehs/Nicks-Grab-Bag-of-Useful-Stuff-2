// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigurationSpace.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.RapidlyExploringRandomTrees
{
    using System;

    /// <summary>
    /// Topological space that has a distance function imposed on.
    /// </summary>
    /// <typeparam name="TConfiguration">
    /// Type of the elements of the configuration space.
    /// </typeparam>
    /// <typeparam name="TInput">
    /// Type of the actions that can affect a configuration.
    /// </typeparam>
    [CLSCompliant(true)]
    public interface IConfigurationSpace<TConfiguration, TInput>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Returns the distance between the two passed elements of this
        /// configuration space in O(1).
        /// </summary>
        /// <param name="q1">
        /// First configuration.
        /// </param>
        /// <param name="q2">
        /// Second configuration.
        /// </param>
        /// <returns>
        /// Distance between both configurations.
        /// </returns>
        IComparable GetDistance(TConfiguration q1, TConfiguration q2);

        /// <summary>
        /// Returns a random element of this configuration space.
        /// </summary>
        /// <returns>
        /// Any element of this configuration space.
        /// </returns>
        TConfiguration GetRandomState();

        /// <summary>
        /// Makes a motion towards q by applying an input uNew to qNear,
        /// resulting in qNew.
        /// </summary>
        /// <param name="q">
        /// Configuration to move towards.
        /// </param>
        /// <param name="qNear">
        /// Configuration to start at.
        /// </param>
        /// <param name="qNew">
        /// Configuration that is as close as possible to q.
        /// </param>
        /// <param name="uNew">
        /// Motion made towards q.
        /// </param>
        /// <returns>
        /// Whether a new configuration could be found, or not.
        /// </returns>
        bool NewState(TConfiguration q, TConfiguration qNear, out TConfiguration qNew, out TInput uNew);

        #endregion
    }
}