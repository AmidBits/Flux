﻿namespace Flux.Geometry
{
  /// <summary>A hex cube/axial coordinate system.</summary>
  /// <see href="https://www.redblobgames.com/grids/hexagons/"/>
  public interface IHexCoordinate<TSelf>
#if NET7_0_OR_GREATER
    where TSelf : System.Numerics.INumber<TSelf>
#endif
  {
    /// <summary>The first component or coordinate.</summary>
    TSelf Q { get; }

    /// <summary>The second component or coordinate.</summary>
    TSelf R { get; }

    /// <summary>The third component or coordinate. It can be derived from Q and R with the formula (-Q - R).</summary>
    TSelf S { get; }
  }
}