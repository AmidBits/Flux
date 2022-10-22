namespace Flux
{
  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
  public interface IPolarCoordinate
  {
    double Radius { get; }
    double Azimuth { get; }

    abstract IPolarCoordinate Create(double radius, double azimuth);

    /// <summary>Converts the <see cref="IPolarCoordinate"/> to a <see cref="CartesianCoordinate2R">CartesianCoordinate2</see>.</summary>
    ICartesianCoordinate2 ToCartesianCoordinate2()
     => new CartesianCoordinate2R(Radius * System.Math.Cos(Azimuth), Radius * System.Math.Sin(Azimuth));

    /// <summary>Converts the <see cref="IPolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    System.Numerics.Complex ToComplex()
     => System.Numerics.Complex.FromPolarCoordinates(
       Radius,
       Azimuth
     );
  }
}
