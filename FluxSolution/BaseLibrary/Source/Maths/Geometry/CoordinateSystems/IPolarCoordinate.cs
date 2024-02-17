//namespace Flux.Geometry
//{
//  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
//  /// <see href="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
//  public interface IPolarCoordinate<TSelf>
//#if NET7_0_OR_GREATER
//    where TSelf : System.Numerics.INumber<TSelf>
//#endif
//  {
//    /// <summary>
//    /// <para>Radius, (length) unit of meter. A.k.a. radial coordinate, or radial distance.</para>
//    /// </summary>
//    TSelf Radius { get; }

//    /// <summary>
//    /// <para>Azimuth angle, unit of radian. A.k.a. angular coordinate, or polar angle.</para>
//    /// </summary>
//    /// <remarks>The angle φ is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
//    TSelf Azimuth { get; }
//  }
//}
