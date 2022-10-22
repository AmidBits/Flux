namespace Flux
{
  public interface ICylindricalCoordinate
  {
    double Radius { get; }
    double Azimuth { get; }
    double Height { get; }

    abstract ICylindricalCoordinate Create(double radius, double azimuth, double height);

    /// <summary>Converts the <see cref="ICylindricalCoordinate"/> to a <see cref="System.ValueTuple{double,double,double}">CartesianCoordinate3</see>..</summary>
    ICartesianCoordinate3 ToCartesianCoordinate3()
     => new CartesianCoordinate3R(
       Radius * System.Math.Cos(Azimuth),
       Radius * System.Math.Sin(Azimuth),
       Height
     );

    /// <summary>Converts the <see cref="ICylindricalCoordinate"/> to a <see cref="IPolarCoordinate"/>.</summary>
    IPolarCoordinate ToPolarCoordinate()
     => new PolarCoordinate(
       Radius,
       Azimuth
     );

    /// <summary>Converts the <see cref="ICylindricalCoordinate"/> to a <see cref="ISphericalCoordinate"/>.</summary>
    ISphericalCoordinate ToSphericalCoordinate()
     => new SphericalCoordinate(
       System.Math.Sqrt(Radius * Radius + Height * Height),
       System.Math.Atan2(Radius, Height),
       Azimuth
     );
  }
}
