namespace Flux.Geometry
{
  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  public interface IPolarCoordinate<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
  {
    /// <summary>Radius. A.k.a. radial coordinate, or radial distance.</summary>
    TSelf Radius { get; init; }

    /// <summary>The azimuth angle in radians. A.k.a. angular coordinate, or polar angle.</summary>
    /// <remarks>The angle φ is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
    TSelf Azimuth { get; init; }
  }
}
