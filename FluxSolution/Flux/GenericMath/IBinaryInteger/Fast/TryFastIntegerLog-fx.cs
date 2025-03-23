namespace Flux
{
  public static partial class GenericMath
  {
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
        if (value.GetBitLength() <= 53)
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
