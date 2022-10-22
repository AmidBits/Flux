namespace Flux
{
  public interface ISphericalCoordinate
  {
    Length Radius { get; }
    Angle Inclination { get; }
    Azimuth Azimuth { get; }

    Angle Elevatiuon => new(System.Math.PI / 2 - Inclination.Value);

#if NET7_0_OR_GREATER
    abstract ISphericalCoordinate Create(Length radius, Angle inclination, Azimuth azimuth);
#endif

    /// <summary>Converts the <see cref="ISphericalCoordinate"/> to a <see cref="System.ValueTuple{double,double,double}">CartesianCoordinate3</see>.</summary>
    public (double x, double y, double z) ToCartesianCoordinate3()
    {
      var inclination = Inclination.Value;
      var azimuth = Azimuth.ToRadians();

      var sinInclination = System.Math.Sin(inclination);

      return (
        Radius.Value * System.Math.Cos(azimuth) * sinInclination,
        Radius.Value * System.Math.Sin(azimuth) * sinInclination,
        Radius.Value * System.Math.Cos(inclination)
      );
    }

    /// <summary>Converts the <see cref="ISphericalCoordinate"/> to a <see cref="ICylindricalCoordinate"/>.</summary>
    public ICylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(
        Radius.Value * System.Math.Sin(Inclination.Value),
        Azimuth.ToRadians(),
        Radius.Value * System.Math.Cos(Inclination.Value)
      );

    /// <summary>Converts the <see cref="ISphericalCoordinate"/> to a <see cref="IGeographicCoordinate"/>.</summary>
    public IGeographicCoordinate ToGeographicCoordinate()
      => new GeographicCoordinate(
        Angle.ConvertRadianToDegree(System.Math.PI - Inclination.Value - Maths.PiOver2),
        Angle.ConvertRadianToDegree(Azimuth.ToRadians() - System.Math.PI),
        Radius.Value
      );
  }
}
