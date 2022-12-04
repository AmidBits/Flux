namespace Flux
{
  #region ExtensionMethods
  public static partial class CoordinateSystems
  {
    /// <summary>Converts the spherical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var sinInclination = TSelf.Sin(source.Inclination);

      return new(
        source.Radius * sinInclination * TSelf.Cos(source.Azimuth),
        source.Radius * sinInclination * TSelf.Sin(source.Azimuth),
        source.Radius * TSelf.Cos(source.Inclination)
      );
    }

    /// <summary>Converts the spherical coordinates to cylindrical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf azimuth, TSelf height) ToCylindricalCoordinates<TSelf>(this ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (
        source.Radius * TSelf.Sin(source.Inclination),
        source.Azimuth,
        source.Radius * TSelf.Cos(source.Inclination)
      );

    /// <summary>Converts the spherical coordinates to a geographic coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static GeographicCoordinate ToGeographicCoordinates<TSelf>(this ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        Angle.ConvertRadianToDegree(double.CreateChecked(TSelf.Pi - source.Inclination - TSelf.Pi.Divide(2))),
        Angle.ConvertRadianToDegree(double.CreateChecked(source.Azimuth - TSelf.Pi)),
        double.CreateChecked(source.Radius)
      );

    public static (Length radius, Angle inclination, Angle azimuth) ToQuantities<TSelf>(this ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Length(double.CreateChecked(source.Radius)),
        new Angle(double.CreateChecked(source.Inclination)),
        new Angle(double.CreateChecked(source.Azimuth))
      );

    public static SphericalCoordinate<TSelf> ToSphericalCoordinate<TSelf>(this ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new SphericalCoordinate<TSelf>(source.Radius, source.Inclination, source.Azimuth);
  }
  #endregion ExtensionMethods

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
