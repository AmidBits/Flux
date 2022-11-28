namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the cartesian 2D coordinates to rounded cartesian 2D coordinates.</summary>
    public static (TSelf x, TSelf y) ToCartesianCoordinates<TSelf>(this ICartesianCoordinate2<TSelf> cartesianCoordinate, INumberRoundable<TSelf> rounding)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (rounding.RoundNumber(cartesianCoordinate.X), rounding.RoundNumber(cartesianCoordinate.Y));

    /// <summary>Converts the cartesian 2D coordinates to polar coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (TSelf radius, TSelf azimuth) ToPolarCoordinates<TSelf>(this ICartesianCoordinate2<TSelf> cartesianCoordinate)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => (
        TSelf.Sqrt(cartesianCoordinate.X * cartesianCoordinate.X + cartesianCoordinate.Y * cartesianCoordinate.Y),
        TSelf.Atan2(cartesianCoordinate.Y, cartesianCoordinate.X)
      );
  }

  /// <summary>Cartesian 2D coordinate using integers.</summary>
  public interface ICartesianCoordinate2<TSelf>
    : System.IFormattable
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)} }}";
  }
}
