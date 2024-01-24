//namespace Flux.Geometry
//{
//  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
//  /// <see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
//  public interface ICylindricalCoordinate<TSelf>
//#if NET7_0_OR_GREATER
//    where TSelf : System.Numerics.INumber<TSelf>
//#endif
//  {
//    /// <summary>Radius. A.k.a. radial distance, or axial distance.</summary>
//    TSelf Radius { get; init; }

//    /// <summary>The azimuth angle in radians. A.k.a. angular position.</summary>
//    TSelf Azimuth { get; init; }

//    /// <summary>Height. A.k.a. altitude (if the reference plane is considered horizontal), longitudinal position, axial position, or axial coordinate.</summary>
//    TSelf Height { get; init; }
//  }
//}
