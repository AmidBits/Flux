namespace Flux
{
  public interface ISphericalCoordinate
  {
    Length Radius { get; }
    Angle Inclination { get; }
    Azimuth Azimuth { get; }

    Angle Elevatiuon => new(System.Math.PI / 2 - Inclination.Value);

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CartesianCoordinate3R"/>.</summary>
    public (double x, double y, double z) ToCartesianCoordinate3()
    {
      var inclination = Angle.ConvertDegreeToRadian(Inclination.Value);
      var azimuth = Angle.ConvertDegreeToRadian(Azimuth.Value);

      var sinInclination = System.Math.Sin(inclination);

      return (
        Radius.Value * System.Math.Cos(azimuth) * sinInclination,
        Radius.Value * System.Math.Sin(azimuth) * sinInclination,
        Radius.Value * System.Math.Cos(inclination)
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    public ICylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(
        Radius.Value * System.Math.Sin(Inclination.Value),
        Angle.ConvertDegreeToRadian(Azimuth.Value),
        Radius.Value * System.Math.Cos(Inclination.Value)
      );

    /// <summary>Converts the <see cref="ISphericalCoordinate"/> to a <see cref="IGeographicCoordinate"/>.</summary>
    public IGeographicCoordinate ToGeographicCoordinate()
      => new GeographicCoordinate(
        Angle.ConvertRadianToDegree(System.Math.PI - Inclination.Value - Maths.PiOver2),
        Angle.ConvertRadianToDegree(Angle.ConvertDegreeToRadian(Azimuth.Value) - System.Math.PI),
        Radius.Value
      );
  }
}
