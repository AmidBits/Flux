namespace Flux
{
  #region ExtensionMethods
  public static partial class GeometryExtensionMethods
  {
    /// <summary>Converts the cylindrical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Geometry.ICartesianCoordinate3<double> ToCartesianCoordinate3(this Geometry.ICylindricalCoordinate source)
    {
      var (sa, ca) = System.Math.SinCos(source.Azimuth);

      return new Geometry.CartesianCoordinate3<double>(
            source.Radius * ca,
            source.Radius * sa,
            source.Height
          );
    }

    public static Geometry.CylindricalCoordinate ToCylindricalCoordinate(this Geometry.ICylindricalCoordinate source)
      => new(source.Radius, source.Azimuth, source.Height);

    /// <summary>Converts the cylindrical coordinates to polar coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Geometry.PolarCoordinate ToPolarCoordinate(this Geometry.ICylindricalCoordinate source)
      => new(
        source.Radius,
        source.Azimuth
      );

    /// <summary>Converts the cylindrical coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Geometry.SphericalCoordinate ToSphericalCoordinate(this Geometry.ICylindricalCoordinate source)
      => new(
        System.Math.Sqrt(source.Radius * source.Radius + source.Height * source.Height),
        System.Math.Atan(source.Radius / source.Height),
        source.Height
      );
  }
  #endregion ExtensionMethods

  namespace Geometry
  {
    /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
    public interface ICylindricalCoordinate
    {
      /// <summary>Radius. A.k.a. radial distance, or axial distance.</summary>
      double Radius { get; init; }
      /// <summary>The azimuth angle in radians. A.k.a. angular position.</summary>
      double Azimuth { get; init; }
      /// <summary>Height. A.k.a. altitude (if the reference plane is considered horizontal), longitudinal position, axial position, or axial coordinate.</summary>
      double Height { get; init; }
    }
  }
}
