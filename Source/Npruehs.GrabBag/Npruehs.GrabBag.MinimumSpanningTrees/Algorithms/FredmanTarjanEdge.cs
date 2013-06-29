// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FredmanTarjanEdge.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Npruehs.GrabBag.MinimumSpanningTrees.Algorithms
{
    using System;

    using Npruehs.GrabBag.Graphs;

    /// <summary>
    ///     Edge with a <see cref="float"/> weight.
    /// </summary>
    [CLSCompliant(true)]
    public class FredmanTarjanEdge : FloatEdge, IFredmanTarjanEdge
    {
    }
}