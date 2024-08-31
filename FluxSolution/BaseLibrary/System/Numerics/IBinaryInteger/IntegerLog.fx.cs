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
    public static TValue IntegerLogAwayFromZero<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
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
    public static TValue IntegerLogTowardZero<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      Quantities.Radix.AssertMember(radix);

      var v = TValue.Abs(value);

      var ilog = TValue.Zero;

      if (!TValue.IsZero(v))
        while (v >= radix)
        {
          v /= radix;

          ilog++;
        }

      return TValue.CopySign(ilog, value);
    }
  }
}
