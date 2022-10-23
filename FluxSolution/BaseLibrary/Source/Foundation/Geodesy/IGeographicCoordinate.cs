namespace Flux
{
  public interface IGeographicCoordinate
  {
    Length Altitude { get; }
    Latitude Latitude { get; }
    Longitude Longitude { get; }

    ///// <summary></summary>
    ///// <param name="altitude">Altitude in meters.</param>
    ///// <param name="latitude">Latitude in degrees.</param>
    ///// <param name="longitude">Longitude in degrees.</param>
    //abstract IGeographicCoordinate Create(double altitude, double latitude, double longitude);

    ///// <summary>Return the components of the <see cref="IGeographicCoordinate"/>.</summary>
    //(Length altitude, Latitude latitude, Longitude longitude) ToUnits()
    //=> (Altitude, Latitude, Longitude);

    ///// <summary>Converts the <see cref="IGeographicCoordinate"/> to a <see cref="ISphericalCoordinate"/>.</summary>
    //public SphericalCoordinate ToSphericalCoordinate()
    // => ISphericalCoordinate.FromUnits(
    //   Altitude,
    //   new Angle(System.Math.PI - (Angle.ConvertDegreeToRadian(Latitude.Value) + System.Math.PI / 2)),
    //   Azimuth.FromRadians(Angle.ConvertDegreeToRadian(Longitude.Value) + System.Math.PI)
    // );

    ///// <summary>Return the <see cref="IGeographicCoordinate"/> from the specified components.</summary>
    //static IGeographicCoordinate FromUnits(Length altitude, Latitude latitude, Longitude longitude)
    //  => new GeographicCoordinate(
    //    altitude.Value,
    //    latitude.Value,
    //    longitude.Value
    //  );
  }
}
