namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the cartesian 3D coordinates to rounded cartesian 3D coordinates.</summary>
    public static (TSelf x, TSelf y, TSelf z) ToCartesianCoordinates<TSelf>(this ICartesianCoordinate3<TSelf> cartesianCoordinate, INumberRoundable<TSelf> rounding)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (rounding.RoundNumber(cartesianCoordinate.X), rounding.RoundNumber(cartesianCoordinate.Y), rounding.RoundNumber(cartesianCoordinate.Z));

    /// <summary>Converts the cartesian 3D coordinates to cylindrical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf inclination, TSelf azimuth) ToCylindricalCoordinates<TSelf>(this ICartesianCoordinate3<TSelf> cartesianCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (TSelf.Sqrt(cartesianCoordinate.X * cartesianCoordinate.X + cartesianCoordinate.Y * cartesianCoordinate.Y), (TSelf.Atan2(cartesianCoordinate.Y, cartesianCoordinate.X) + TSelf.Tau) % TSelf.Tau, cartesianCoordinate.Z);

    /// <summary>Converts the cartesian 3D coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf inclination, TSelf azimuth) ToSphericalCoordinates<TSelf>(this ICartesianCoordinate3<TSelf> cartesianCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var x2y2 = cartesianCoordinate.X * cartesianCoordinate.X + cartesianCoordinate.Y * cartesianCoordinate.Y;

      return (TSelf.Sqrt(x2y2 + cartesianCoordinate.Z * cartesianCoordinate.Z), TSelf.Atan2(TSelf.Sqrt(x2y2), cartesianCoordinate.Z) + TSelf.Pi, TSelf.Atan2(cartesianCoordinate.Y, cartesianCoordinate.X) + TSelf.Pi);
    }
  }

  /// <summary>Cartesian 3D coordinate.</summary>
  public interface ICartesianCoordinate3<TSelf>
    : System.IFormattable, ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf Z { get; }

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N6"}}}", Z)} }}";
  }
}
