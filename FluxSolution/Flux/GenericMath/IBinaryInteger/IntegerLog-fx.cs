namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log ceiling of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TNumber IntegerLogAwayFromZero<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var tz = TNumber.Abs(IntegerLogTowardZero(value, radix));

      if (!value.IsIntegerPowOf(radix))
        tz++;

      return TNumber.CopySign(tz, value);
    }

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log floor of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TNumber IntegerLogTowardZero<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (value.TryFastIntegerLog(radix, UniversalRounding.WholeTowardZero, out TNumber ilog, out var _)) // Testing!
        return ilog;

      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(radix));

      if (!TNumber.IsZero(value)) // If not zero...
      {
        var log = TNumber.Zero;

        for (var val = TNumber.Abs(value); val >= rdx; val /= rdx)
          log++;

        return TNumber.CopySign(log, value);
      }
      else return value; // ...otherwise return zero.
    }
  }
}
