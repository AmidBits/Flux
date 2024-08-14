namespace Flux
{
  public static partial class Fx
  {

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log ceiling of <paramref name="number"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TSelf IntegerLogAwayFromZero<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.CopySign(TSelf.Abs(IntegerLogTowardZero(number, radix)) is var tz && number.IsPowOf(radix) ? tz : tz + TSelf.One, number);

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log floor of <paramref name="number"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TSelf IntegerLogTowardZero<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Quantities.Radix.AssertMember(radix);

      var v = TSelf.Abs(number);

      var ilog = TSelf.Zero;

      if (!TSelf.IsZero(v))
        while (v >= radix)
        {
          v /= radix;

          ilog++;
        }

      return TSelf.CopySign(ilog, number);
    }
  }
}
