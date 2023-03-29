namespace Flux
{
  public static partial class NumericsExtensionMethods
  {
    /// <summary>Converts the geographic coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static Numerics.SphericalCoordinate<TSelf> ToSphericalCoordinate<TSelf>(this Numerics.IGeographicCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        source.Altitude,
        TSelf.Pi - (TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(double.CreateChecked(source.Latitude))) + TSelf.Pi.Divide(2)),
        TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(double.CreateChecked(source.Longitude))) + TSelf.Pi
      );

    public static (Quantities.Length altitude, Quantities.Latitude latitude, Quantities.Longitude longitude) ToQuantities<TSelf>(this Numerics.IGeographicCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Quantities.Length(double.CreateChecked(source.Altitude)),
        new Quantities.Latitude(double.CreateChecked(source.Latitude)),
        new Quantities.Longitude(double.CreateChecked(source.Longitude))
      );
  }

  namespace Numerics
  {
    public interface IGeographicCoordinate<TSelf>
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
      TSelf Altitude { get; init; }
      /// <summary>The latitude component of the geographic position in degrees. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
      TSelf Latitude { get; init; }
      /// <summary>The longitude component of the geographic position in degrees. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
      TSelf Longitude { get; init; }
    }
  }
}
