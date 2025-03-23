namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Computes the logarithm of a <paramref name="value"/> in the specified <paramref name="radix"/> (base) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="log"/> (double) is returned as an out parameter.</para>
    /// <para>Uses the built-in <see cref="double.Log(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative logarithm. A zero results in a zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TNewBase"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <param name="mode"></param>
    /// <param name="log"></param>
    /// <returns>The resulting integer-log.</returns>
    /// <remarks>
    /// <para>Find the number of digits in a number: <c>(FastLog(number, radix, UniversalRounding.FullTowardZero, out var _) + 1)</c></para>
    /// </remarks>
    public static TNumber FastIntegerLog<TNumber, TNewBase>(this TNumber number, TNewBase radix, UniversalRounding mode, out double log)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNewBase : System.Numerics.INumber<TNewBase>
    {
      checked
      {
        log = TNumber.IsZero(number) ? 0.0 : double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(Units.Radix.AssertWithin(radix)));

        return (log < 1.0) ? TNumber.Zero : TNumber.CopySign(TNumber.CreateChecked(log.RoundUniversal(mode)), number);
      }
    }
  }
}
