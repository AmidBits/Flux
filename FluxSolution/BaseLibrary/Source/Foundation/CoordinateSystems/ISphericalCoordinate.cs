namespace Flux
{
  public interface ISphericalCoordinate
  {
    double Radius { get; }
    double Inclination { get; }
    double Azimuth { get; }

    double Elevatiuon => 90.0 - Inclination;

    abstract ISphericalCoordinate Create(double radius, double inclination, double azimuth);

    /// <summary>Converts the <see cref="ISphericalCoordinate"/> to a <see cref="System.ValueTuple{double,double,double}">CartesianCoordinate3</see>.</summary>
    ICartesianCoordinate3 ToCartesianCoordinate3()
    {
      var sinInclination = System.Math.Sin(Inclination);

      return new CartesianCoordinate3R(
        Radius * System.Math.Cos(Azimuth) * sinInclination,
        Radius * System.Math.Sin(Azimuth) * sinInclination,
        Radius * System.Math.Cos(Inclination)
      );
    }

    /// <summary>Converts the <see cref="ISphericalCoordinate"/> to a <see cref="ICylindricalCoordinate"/>.</summary>
    ICylindricalCoordinate ToCylindricalCoordinate()
     => new CylindricalCoordinate(
       Radius * System.Math.Sin(Inclination),
       Azimuth,
       Radius * System.Math.Cos(Inclination)
     );

    /// <summary>Converts the <see cref="ISphericalCoordinate"/> to a <see cref="IGeographicCoordinate"/>.</summary>
    IGeographicCoordinate ToGeographicCoordinate()
     => new GeographicCoordinate(
       Angle.ConvertRadianToDegree(System.Math.PI - Inclination - System.Math.PI / 2),
       Angle.ConvertRadianToDegree(Azimuth - System.Math.PI),
       Radius
     );
  }
}
