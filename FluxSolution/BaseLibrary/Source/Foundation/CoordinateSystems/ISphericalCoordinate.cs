using System.Runtime.CompilerServices;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the spherical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf x, TSelf y, TSelf z) ToCartesianCoordinates<TSelf>(this ISphericalCoordinate<TSelf> sphericalCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var sinInclination = TSelf.Sin(sphericalCoordinate.Inclination);

      return (
        sphericalCoordinate.Radius * sinInclination * TSelf.Cos(sphericalCoordinate.Azimuth),
        sphericalCoordinate.Radius * sinInclination * TSelf.Sin(sphericalCoordinate.Azimuth),
        sphericalCoordinate.Radius * TSelf.Cos(sphericalCoordinate.Inclination)
      );
    }

    /// <summary>Converts the spherical coordinates to cylindrical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf azimuth, TSelf height) ToCylindricalCoordinates<TSelf>(this ISphericalCoordinate<TSelf> sphericalCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (
        sphericalCoordinate.Radius * TSelf.Sin(sphericalCoordinate.Inclination),
        sphericalCoordinate.Azimuth,
        sphericalCoordinate.Radius * TSelf.Cos(sphericalCoordinate.Inclination)
      );

    /// <summary>Converts the spherical coordinates to a geographic coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf latitude, TSelf longitude, TSelf height) ToGeographicCoordinates<TSelf>(this ISphericalCoordinate<TSelf> sphericalCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (
        TSelf.Pi - sphericalCoordinate.Inclination - TSelf.Pi.Div2(),
        sphericalCoordinate.Azimuth - TSelf.Pi,
        sphericalCoordinate.Radius
      );

    public static (Length radius, Angle inclination, Angle azimuth) ToQuantities<TSelf>(this ISphericalCoordinate<TSelf> sphericalCoordinate)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Length(double.CreateChecked(sphericalCoordinate.Radius)),
        new Angle(double.CreateChecked(sphericalCoordinate.Inclination)),
        new Angle(double.CreateChecked(sphericalCoordinate.Azimuth))
      );
  }

  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  public interface ISphericalCoordinate<TSelf>
    : System.IFormattable
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>Radius. A.k.a. radial distance, radial coordinate.</summary>
    /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
    TSelf Radius { get; init; }
    /// <summary>The inclination angle in radians. A.k.a. polar angle, colatitude, zenith angle, normal angle.</summary>
    /// <remarks>If the inclination is zero or 180 degrees (π radians), the azimuth is arbitrary.</remarks>
    TSelf Inclination { get; init; }
    /// <summary>The azimuthal angle in radians.</summary>
    TSelf Azimuth { get; init; }

    /// <summary>The elevation angle in radians. This is an option/alternative to <see cref="Inclination"/>.</summary>
    /// <remarks>The elevation angle is 90 degrees (π/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
    TSelf Elevation { get; init; }

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Inclination = {new Angle(double.CreateChecked(Inclination)).ToUnitString(AngleUnit.Degree, format ?? "N3", true)} (Elevation = {new Angle(double.CreateChecked(Elevation)).ToUnitString(AngleUnit.Degree, format ?? "N3", true)}), Azimuth = {new Angle(double.CreateChecked(Azimuth)).ToUnitString(AngleUnit.Degree, format ?? "N3", true)} }}";
  }
}
