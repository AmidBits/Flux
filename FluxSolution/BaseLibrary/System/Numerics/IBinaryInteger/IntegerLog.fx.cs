namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log ceiling of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TValue IntegerLogAwayFromZero<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TValue.CopySign(TValue.Abs(IntegerLogTowardZero(value, radix)) is var tz && value.IsPowOf(radix) ? tz : tz + TValue.One, value);

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log floor of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TValue IntegerLogTowardZero<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      var v = TValue.Abs(value);

      var ilog = TValue.Zero;

      if (!TValue.IsZero(v))
        while (v >= rdx)
        {
          v /= rdx;

          ilog++;
        }

      return TValue.CopySign(ilog, value);
    }
  }
}
