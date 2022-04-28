namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Round to a multiple of the provided positive basis.</summary>
    /// <param name="value">Number to be rounded.</param>
    /// <param name="radix">The basis to whose powers to round to. Must be positive.</param>
    public static double RoundToNearestPower(this double value, double radix)
      => value < 0
      ? -System.Math.Pow(radix, System.Math.Round(System.Math.Log(-value, radix), System.MidpointRounding.AwayFromZero))
      : System.Math.Pow(radix, System.Math.Round(System.Math.Log(value, radix), System.MidpointRounding.AwayFromZero));

    /// <summary>Round to a multiple of the provided positive basis.</summary>
    /// <param name="value">Number to be rounded.</param>
    /// <param name="radix">The basis to whose powers to round to. Must be positive.</param>
    public static float RoundToNearestPower(this float value, float radix)
      => (float)RoundToNearestPower((double)value, radix);
  }
}
