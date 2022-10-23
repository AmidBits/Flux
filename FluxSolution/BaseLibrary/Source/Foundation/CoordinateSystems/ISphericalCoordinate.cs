namespace Flux
{
  public interface ISphericalCoordinate
  {
    /// <summary>Radius in meters.</summary>
    double Radius { get; }
    /// <summary>Inclination in radians.</summary>
    double Inclination { get; }
    /// <summary>Azimuth in radians.</summary>
    double Azimuth { get; }

    /// <summary>Elevation in radians.</summary>
    double Elevatiuon => System.Math.PI / 2 - Inclination;

    abstract ISphericalCoordinate Create(double radius, double inclination, double azimuth);

    /// <summary>Return the components of the <see cref="ISphericalCoordinate"/>.</summary>
    (Length radius, Angle inclination, Azimuth azimuth) ToUnits()
      => (new Length(Radius), new Angle(Inclination), new Angle(Azimuth).ToAzimuth());

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

    /// <summary>Return the <see cref="ISphericalCoordinate"/> from the specified components.</summary>
    static ISphericalCoordinate FromUnits(Length radius, Angle inclination, Azimuth azimuth)
      => new SphericalCoordinate(radius.Value, inclination.Value, azimuth.ToRadians());
  }
}
