namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the cylindrical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf x, TSelf y, TSelf z) ToCartesianCoordinates<TSelf>(this ICylindricalCoordinate<TSelf> cylindricalCoordinate)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (cylindricalCoordinate.Radius * TSelf.Cos(cylindricalCoordinate.Azimuth), cylindricalCoordinate.Radius * TSelf.Sin(cylindricalCoordinate.Azimuth), cylindricalCoordinate.Height);

    /// <summary>Converts the cylindrical coordinates to polar coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf azimuth) ToPolarCoordinates<TSelf>(this ICylindricalCoordinate<TSelf> cylindricalCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (
        cylindricalCoordinate.Radius,
        cylindricalCoordinate.Azimuth
      );

    public static (Length radius, Angle azimuth, Length height) ToQuantities<TSelf>(this ICylindricalCoordinate<TSelf> cylindricalCoordinate)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Length(double.CreateChecked(cylindricalCoordinate.Radius)),
        new Angle(double.CreateChecked(cylindricalCoordinate.Azimuth)),
        new Length(double.CreateChecked(cylindricalCoordinate.Height))
      );

    /// <summary>Converts the cylindrical coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf inclination, TSelf azimuth) ToSphericalCoordinates<TSelf>(this ICylindricalCoordinate<TSelf> cylindricalCoordinate)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.Sqrt(cylindricalCoordinate.Radius * cylindricalCoordinate.Radius + cylindricalCoordinate.Height * cylindricalCoordinate.Height), TSelf.Atan(cylindricalCoordinate.Radius / cylindricalCoordinate.Height), cylindricalCoordinate.Height);
  }

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
      => $"{GetType().Name} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Azimuth = {new Angle(double.CreateChecked(Azimuth)).ToUnitString(AngleUnit.Degree, format ?? "N3", true)}, Height = {string.Format($"{{0:{format ?? "N1"}}}", Height)} }}";
  }
}
