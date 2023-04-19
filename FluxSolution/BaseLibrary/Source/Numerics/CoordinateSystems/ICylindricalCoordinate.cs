namespace Flux
{
  #region ExtensionMethods
  public static partial class NumericsExtensionMethods
  {
    /// <summary>Converts the cylindrical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.ICartesianCoordinate3<double> ToCartesianCoordinate3(this Numerics.ICylindricalCoordinate source)
    {
      var (sa, ca) = System.Math.SinCos(source.Azimuth);

      return new Numerics.CartesianCoordinate3<double>(
            source.Radius * ca,
            source.Radius * sa,
            source.Height
          );
    }

    public static Numerics.CylindricalCoordinate ToCylindricalCoordinate(this Numerics.ICylindricalCoordinate source)
      => new(source.Radius, source.Azimuth, source.Height);

    /// <summary>Converts the cylindrical coordinates to polar coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.PolarCoordinate ToPolarCoordinate(this Numerics.ICylindricalCoordinate source)
      => new(
        source.Radius,
        source.Azimuth
      );

    /// <summary>Converts the cylindrical coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.SphericalCoordinate ToSphericalCoordinate(this Numerics.ICylindricalCoordinate source)
      => new(
        System.Math.Sqrt(source.Radius * source.Radius + source.Height * source.Height),
        System.Math.Atan(source.Radius / source.Height),
        source.Height
      );
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
    public interface ICylindricalCoordinate
      : System.IFormattable
    {
      /// <summary>Radius. A.k.a. radial distance, or axial distance.</summary>
      double Radius { get; init; }
      /// <summary>The azimuth angle in radians. A.k.a. angular position.</summary>
      double Azimuth { get; init; }
      /// <summary>Height. A.k.a. altitude (if the reference plane is considered horizontal), longitudinal position, axial position, or axial coordinate.</summary>
      double Height { get; init; }

      string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().GetNameEx()} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Azimuth = {new Units.Angle(Azimuth).ToUnitString(Units.AngleUnit.Degree, format ?? "N3", true)}, Height = {string.Format($"{{0:{format ?? "N1"}}}", Height)} }}";
    }
  }
}
