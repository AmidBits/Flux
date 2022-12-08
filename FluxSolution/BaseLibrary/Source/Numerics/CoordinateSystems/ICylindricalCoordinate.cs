namespace Flux
{
  #region ExtensionMethods
  public static partial class CoordinateSystems
  {
    /// <summary>Converts the cylindrical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        source.Radius * TSelf.Cos(source.Azimuth),
        source.Radius * TSelf.Sin(source.Azimuth),
        source.Height
      );

    public static CylindricalCoordinate<TSelf> ToCylindricalCoordinate<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(source.Radius, source.Azimuth, source.Height);

    /// <summary>Converts the cylindrical coordinates to polar coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static PolarCoordinate<TSelf> ToPolarCoordinate<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        source.Radius,
        source.Azimuth
      );

    public static (Quantities.Length radius, Quantities.Angle azimuth, Quantities.Length height) ToQuantities<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Quantities.Length(double.CreateChecked(source.Radius)),
        new Quantities.Angle(double.CreateChecked(source.Azimuth)),
        new Quantities.Length(double.CreateChecked(source.Height))
      );

    /// <summary>Converts the cylindrical coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static SphericalCoordinate<TSelf> ToSphericalCoordinate<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        TSelf.Sqrt(source.Radius * source.Radius + source.Height * source.Height),
        TSelf.Atan(source.Radius / source.Height),
        source.Height
      );
  }
  #endregion ExtensionMethods

  /// <summary>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/>
  public interface ICylindricalCoordinate<TSelf>
    : System.IFormattable
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>Radius. A.k.a. radial distance, or axial distance.</summary>
    TSelf Radius { get; init; }
    /// <summary>The azimuth angle in radians. A.k.a. angular position.</summary>
    TSelf Azimuth { get; init; }
    /// <summary>Height. A.k.a. altitude (if the reference plane is considered horizontal), longitudinal position, axial position, or axial coordinate.</summary>
    TSelf Height { get; init; }

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Azimuth = {new Quantities.Angle(double.CreateChecked(Azimuth)).ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)}, Height = {string.Format($"{{0:{format ?? "N1"}}}", Height)} }}";
  }
}
