namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes the logarithm of a <paramref name="value"/> in the specified <paramref name="radix"/> (base) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="log"/> (double) is returned as an out parameter.</para>
    /// <para>Uses the built-in <see cref="double.Log(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a negative logarithm. A zero results in a zero.</para>
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
        log = TNumber.IsZero(number) ? 0.0 : double.Log(double.CreateChecked(TNumber.Abs(number)), double.CreateChecked(radix));

        return (log < 1.0) ? TNumber.Zero : TNumber.CopySign(TNumber.CreateChecked(log.Round(mode)), number);
      }
    }

    /// <summary>
    /// <para>Attempts to compute the logarithm of a <paramref name="value"/> in the specified <paramref name="radix"/> (base) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="log"/> (double) and <paramref name="integerLog"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <typeparam name="TLog"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="mode"></param>
    /// <param name="integerLog"></param>
    /// <param name="log"></param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerLog<TValue, TRadix, TLog>(this TValue value, TRadix radix, UniversalRounding mode, out TLog integerLog, out double log)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TLog : System.Numerics.IBinaryInteger<TLog>
    {
      try
      {
        if (value.GetBitLengthEx() <= 53)
        {
          integerLog = TLog.CreateChecked(value.FastIntegerLog(radix, mode, out log));

          return true;
        }
      }
      catch { }

      integerLog = TLog.Zero;
      log = 0.0;

      return false;
    }
  }
}
