namespace Flux
{
  public static partial class NumberFast
  {
    /// <summary>
    /// <para>Computes the logarithm of a <paramref name="value"/> in the specified <paramref name="radix"/> (base) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="log"/> (double) is returned as an out parameter.</para>
    /// <para>Uses the built-in <see cref="double.Log(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative logarithm. A zero results in a zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="number">The number for which to find the log.</param>
    /// <param name="radix">The radix for which to find the log.</param>
    /// <param name="mode">The integer rounding strategy to use.</param>
    /// <param name="log">The actual floating-point log-of <paramref name="number"/> as an out parameter.</param>
    /// <returns>The integer log.</returns>
    /// <remarks>
    /// <para>Find the number of digits in a number: <c>(FastLog(number, radix, UniversalRounding.FullTowardZero, out var _) + 1)</c></para>
    /// </remarks>
    public static TNumber FastIntegerLog<TNumber, TRadix>(this TNumber number, TRadix radix, UniversalRounding mode, out double log)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.INumber<TRadix>
    {
      checked
      {
        log = TNumber.IsZero(number) ? 0.0 : double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(Units.Radix.AssertMember(radix)));

        return (log < 1.0) ? TNumber.Zero : TNumber.CopySign(TNumber.CreateChecked(log.RoundUniversal(mode)), number);
      }
    }
  }
}
