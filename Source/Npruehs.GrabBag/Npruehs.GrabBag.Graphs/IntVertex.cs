// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntVertex.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.Graphs
{
    /// <summary>
    /// <see cref="int"/> wrapper that implements the graph vertex
    /// interface.
    /// </summary>
    public class IntVertex : IVertex
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new vertex with the specified index.
        /// </summary>
        /// <param name="index">
        /// Index of the new vertex.
        /// </param>
        public IntVertex(int index)
        {
            this.Index = index;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Index of this vertex.
        /// </summary>
        public int Index { get; private set; }

        #endregion
    }
}