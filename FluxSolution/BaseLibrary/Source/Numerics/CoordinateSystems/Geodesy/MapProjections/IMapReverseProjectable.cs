﻿namespace Flux.Numerics.MapProjections
{
  public interface IMapReverseProjectable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>Creates a new <see cref="IGeographicCoordinate{TSelf}"/> from the <see cref="ICartesianCoordinate3{TSelf}"/> (where the Altitude component equals the Z component, without any manipulations).</summary>
    public Numerics.IGeographicCoordinate<TSelf> ProjectReverse(Numerics.ICartesianCoordinate3<TSelf> project);
  }
}