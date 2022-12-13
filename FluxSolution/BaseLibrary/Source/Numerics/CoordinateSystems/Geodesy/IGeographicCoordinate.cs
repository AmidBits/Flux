namespace Flux
{
  public static partial class ExtensionMethods
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

    //public static (Quantities.Length altitude, Latitude latitude, Longitude longitude) ToQuantities<TSelf>(this IGeographicCoordinate<TSelf> source)
    //  where TSelf : System.Numerics.IFloatingPoint<TSelf>
    //  => (
    //    new Quantities.Length(double.CreateChecked(source.Altitude)),
    //    new Latitude(double.CreateChecked(source.Latitude)),
    //    new Longitude(double.CreateChecked(source.Longitude))
    //  );
  }

  namespace Numerics
  {
    public interface IGeographicCoordinate<TSelf>
      : System.IFormattable
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
      TSelf Altitude { get; init; }
      /// <summary>The latitude component of the geographic position in degrees. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
      TSelf Latitude { get; init; }
      /// <summary>The longitude component of the geographic position in degrees. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
      TSelf Longitude { get; init; }

      string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().Name} {{ Latitude = {new Quantities.Latitude(double.CreateChecked(Latitude)).ToSexagesimalDegreeString()} ({new Quantities.Angle(double.CreateChecked(Latitude), Quantities.AngleUnit.Degree).ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)}), Longitude = {new Quantities.Longitude(double.CreateChecked(Longitude)).ToSexagesimalDegreeString()} ({new Quantities.Angle(double.CreateChecked(Longitude), Quantities.AngleUnit.Degree).ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)}), Altitude = {new Quantities.Length(double.CreateChecked(Altitude)).ToUnitString(format: format ?? "N1")} }}";
    }
  }
}
