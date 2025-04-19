namespace Flux
{
  public static partial class GenericMathFast
  {
    /// <summary>
    /// <para>Attempts to compute the logarithm of a <paramref name="value"/> in the specified <paramref name="radix"/> (base) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="log"/> (double) and <paramref name="integerLog"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TINumber"></typeparam>
    /// <typeparam name="TIRadix"></typeparam>
    /// <typeparam name="TILog"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="mode"></param>
    /// <param name="integerLog"></param>
    /// <param name="log"></param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerLog<TINumber, TIRadix, TILog>(this TINumber value, TIRadix radix, UniversalRounding mode, out TILog integerLog, out double log)
      where TINumber : System.Numerics.IBinaryInteger<TINumber>
      where TIRadix : System.Numerics.IBinaryInteger<TIRadix>
      where TILog : System.Numerics.IBinaryInteger<TILog>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          integerLog = TILog.CreateChecked(value.FastIntegerLog(radix, mode, out log));

          return true;
        }
      }
      catch { }

      integerLog = TILog.Zero;
      log = 0.0;

      return false;
    }
  }
}
