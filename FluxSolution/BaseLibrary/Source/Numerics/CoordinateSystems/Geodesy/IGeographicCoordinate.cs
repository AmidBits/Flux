namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the geographic coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf inclination, TSelf azimuth) ToSphericalCoordinates<TSelf>(this IGeographicCoordinate geographicCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (
        TSelf.CreateChecked(geographicCoordinate.Altitude.Value),
        TSelf.Pi - (TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(geographicCoordinate.Latitude.Value)) + TSelf.Pi.Divide(2)),
        TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(geographicCoordinate.Longitude.Value)) + TSelf.Pi
      );

    public static (Quantities.Length altitude, Latitude latitude, Longitude longitude) ToQuantities(this IGeographicCoordinate geographicCoordinate)
      => (
        new Quantities.Length(double.CreateChecked(geographicCoordinate.Altitude.Value)),
        new Latitude(double.CreateChecked(geographicCoordinate.Latitude.Value)),
        new Longitude(double.CreateChecked(geographicCoordinate.Longitude.Value))
      );
  }

  public interface IGeographicCoordinate
    : System.IFormattable
  {
    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    Quantities.Length Altitude { get; }
    /// <summary>The latitude component of the geographic position in degrees. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    Latitude Latitude { get; }
    /// <summary>The longitude component of the geographic position in degrees. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    Longitude Longitude { get; }

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ Latitude = {Latitude.ToSexagesimalDegreeString()} ({Latitude.ToAngle().ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)}), Longitude = {Longitude.ToSexagesimalDegreeString()} ({Longitude.ToAngle().ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)}), Altitude = {Altitude.ToUnitString(format: format ?? "N1")} }}";
  }
}
