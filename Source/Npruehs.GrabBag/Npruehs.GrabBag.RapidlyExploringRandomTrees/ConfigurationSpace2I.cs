// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationSpace2I.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.RapidlyExploringRandomTrees
{
    using System;

    using Npruehs.GrabBag.Math.Vectors;

    /// <summary>
    /// Bounded 2-dimensional Euclidean configuration space.
    /// </summary>
    [CLSCompliant(true)]
    public class ConfigurationSpace2I : IConfigurationSpace<Vector2I, Vector2I>
    {
        #region Fields

        /// <summary>
        /// Pseudo-random number generator used for finding random
        /// configurations within this space.
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// Maximum distance to cover when making a motion from one
        /// configuration in this space to another.
        /// </summary>
        private readonly int stepSize;

        /// <summary>
        /// Width and height of this 2-dimensional configuration space.
        /// </summary>
        private Vector2I size;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new 2-dimensional Euclidean configuration space
        /// bounded to the specified width and height.
        /// </summary>
        /// <param name="width">
        /// Width of the 2-dimensional configuration space.
        /// </param>
        /// <param name="height">
        /// Height of the 2-dimensional configuration space.
        /// </param>
        /// <param name="stepSize">
        /// Maximum distance to cover when making a motion from one
        /// configuration in the space to another.
        /// </param>
        public ConfigurationSpace2I(int width, int height, int stepSize)
        {
            this.size = new Vector2I(width, height);
            this.stepSize = stepSize;

            this.random = new Random();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Returns the Euclidean distance between the two passed points in
        /// this configuration space in O(1).
        /// </summary>
        /// <param name="q1">
        /// First point.
        /// </param>
        /// <param name="q2">
        /// Second point.
        /// </param>
        /// <returns>
        /// Euclidean distance between both points.
        /// </returns>
        public IComparable GetDistance(Vector2I q1, Vector2I q2)
        {
            return q1.Distance(q2);
        }

        /// <summary>
        /// Returns a random point in this configuration space.
        /// </summary>
        /// <returns>
        /// Any point in this configuration space.
        /// </returns>
        public Vector2I GetRandomState()
        {
            var x = this.random.Next(this.size.X);
            var y = this.random.Next(this.size.Y);

            return new Vector2I(x, y);
        }

        /// <summary>
        /// Makes a motion towards q by applying an input uNew to qNear,
        /// resulting in qNew. The maximum length of the vector represented
        /// by uNew is the <c>stepSize</c> specified at construction time.
        /// </summary>
        /// <param name="q">
        /// Point to move towards.
        /// </param>
        /// <param name="qNear">
        /// Point to start at.
        /// </param>
        /// <param name="qNew">
        /// Point that is as close as possible to q.
        /// </param>
        /// <param name="uNew">
        /// Motion made towards q.
        /// </param>
        /// <returns>
        /// Whether a new point could be found, or not.
        /// </returns>
        public bool NewState(Vector2I q, Vector2I qNear, out Vector2I qNew, out Vector2I uNew)
        {
            // Get the direction to make a motion in.
            var dirX = Math.Sign(q.X - qNear.X);
            var dirY = Math.Sign(q.Y - qNear.Y);

            // Compute the motion to make, with maximum length of stepSize.
            var x = Math.Min(Math.Abs(q.X - qNear.X), this.stepSize) * dirX;
            var y = Math.Min(Math.Abs(q.Y - qNear.Y), this.stepSize) * dirY;
            uNew = new Vector2I(x, y);

            // Compute new point.
            x = qNear.X + uNew.X;
            y = qNear.Y + uNew.Y;
            qNew = new Vector2I(x, y);

            return qNew.X >= 0 && qNew.X < this.size.X && qNew.Y >= 0 && qNew.Y < this.size.Y;
        }

        #endregion
    }
}