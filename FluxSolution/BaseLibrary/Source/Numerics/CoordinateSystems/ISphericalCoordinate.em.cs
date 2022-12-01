namespace Flux
{
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
}
