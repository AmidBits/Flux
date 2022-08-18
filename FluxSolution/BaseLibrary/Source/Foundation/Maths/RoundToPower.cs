namespace Flux
{
  public static partial class Maths
  {
    public static System.Numerics.BigInteger RoundToNearestPower(this System.Numerics.BigInteger value, int radix, System.MidpointRounding mode)
      => value < 0
      ? -System.Numerics.BigInteger.Pow(radix, System.Convert.ToInt32(System.Math.Round(System.Numerics.BigInteger.Log(-value, radix), mode)))
      : System.Numerics.BigInteger.Pow(radix, System.Convert.ToInt32(System.Math.Round(System.Numerics.BigInteger.Log(value, radix), mode)));

    /// <summary>Round to a multiple of the provided positive basis.</summary>
    /// <param name="value">Number to be rounded.</param>
    /// <param name="radix">The basis to whose powers to round to. Must be positive.</param>
    public static double RoundToNearestPower(this double value, int radix, System.MidpointRounding mode)
      => value < 0
      ? -System.Math.Pow(radix, System.Math.Round(System.Math.Log(-value, radix), mode))
      : System.Math.Pow(radix, System.Math.Round(System.Math.Log(value, radix), mode));

    /// <summary>Round to a multiple of the provided positive basis.</summary>
    /// <param name="value">Number to be rounded.</param>
    /// <param name="radix">The basis to whose powers to round to. Must be positive.</param>
    public static float RoundToNearestPower(this float value, int radix, System.MidpointRounding mode)
      => (float)RoundToNearestPower((double)value, radix, mode);

    public static int RoundToNearestPower(this int value, int radix, System.MidpointRounding mode)
      => System.Convert.ToInt32(RoundToNearestPower((double)value, radix, mode));
    public static long RoundToNearestPower(this long value, int radix, System.MidpointRounding mode)
      => System.Convert.ToInt64(RoundToNearestPower((double)value, radix, mode));
  }
}
