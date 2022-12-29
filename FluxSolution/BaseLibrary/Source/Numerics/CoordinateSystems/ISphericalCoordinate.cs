namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the spherical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this Numerics.ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var (si, ci) = TSelf.SinCos(source.Inclination);
      var (sa, ca) = TSelf.SinCos(source.Azimuth);

      return new(
        source.Radius * si * ca,
        source.Radius * si * sa,
        source.Radius * ci
      );
    }

    /// <summary>Converts the spherical coordinates to cylindrical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.CylindricalCoordinate<TSelf> ToCylindricalCoordinates<TSelf>(this Numerics.ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var (si, ci) = TSelf.SinCos(source.Inclination);

      return new(
        source.Radius * si,
        source.Azimuth,
        source.Radius * ci
      );
    }

    /// <summary>Converts the spherical coordinates to a geographic coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.GeographicCoordinate ToGeographicCoordinates<TSelf>(this Numerics.ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(TSelf.Pi - source.Inclination - TSelf.Pi.Divide(2))),
        Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(source.Azimuth - TSelf.Pi)),
        double.CreateChecked(source.Radius)
      );

    public static (Quantities.Length radius, Quantities.Angle inclination, Quantities.Angle azimuth) ToQuantities<TSelf>(this Numerics.ISphericalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Quantities.Length(double.CreateChecked(source.Radius)),
        new Quantities.Angle(double.CreateChecked(source.Inclination)),
        new Quantities.Angle(double.CreateChecked(source.Azimuth))
      );
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
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
        => $"{GetType().GetNameEx()} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Inclination = {new Quantities.Angle(double.CreateChecked(Inclination)).ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)} (Elevation = {new Quantities.Angle(double.CreateChecked(Elevation)).ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)}), Azimuth = {new Quantities.Angle(double.CreateChecked(Azimuth)).ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)} }}";
    }
  }
}
