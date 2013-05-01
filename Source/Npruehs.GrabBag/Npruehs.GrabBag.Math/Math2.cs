// --------------------------------------------------------------------------------
// <copyright company="Nick Pruehs" file="Math2.cs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// 
// --------------------------------------------------------------------------------
namespace Npruehs.GrabBag.Math
{
    using System;

    /// <summary>
    /// Name according to Framework Design Guidelines - 3.2.4 Naming New Versions of Existing APIs
    /// </summary>
    [CLSCompliant(true)]
    public static class Math2
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="f">
        /// </param>
        /// <returns>
        /// </returns>
        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt(f);
        }

        #endregion
    }
}