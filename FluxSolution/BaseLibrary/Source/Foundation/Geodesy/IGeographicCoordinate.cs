namespace Flux
{
  public interface IGeographicCoordinate
  {
    Length Altitude { get; }
    Latitude Latitude { get; }
    Longitude Longitude { get; }

    //abstract IGeographicCoordinate Create(double altitude, double latitude, double longitude);

    (Length altitude, Latitude latitude, Longitude longitude) To()
    => (Altitude, Latitude, Longitude);

    /// <summary>Converts the <see cref="IGeographicCoordinate"/> to a <see cref="ISphericalCoordinate"/>.</summary>
    ISphericalCoordinate ToSphericalCoordinate()
     => new SphericalCoordinate(
       Altitude.Value,
       System.Math.PI - (Angle.ConvertDegreeToRadian(Latitude.Value) + System.Math.PI / 2),
       Angle.ConvertDegreeToRadian(Longitude.Value) + System.Math.PI
     );

    static IGeographicCoordinate From(Length altitude, Latitude latitude, Longitude longitude)
      => new GeographicCoordinate(altitude.Value, latitude.Value, longitude.Value);
  }
}
