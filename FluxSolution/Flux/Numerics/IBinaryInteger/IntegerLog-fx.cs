namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log ceiling of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TInteger IntegerLogAwayFromZero<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var tz = IntegerLogTowardZero(value, radix);

      if (!value.IsIntegerPowOf(radix))
        tz++;

      return TInteger.CopySign(tz, value);
    }

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log floor of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TInteger IntegerLogTowardZero<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (FastIntegerMath.TryFastIntegerLog(value, radix, out TInteger ilogTz, out TInteger iLogAfz, out var _))
        return ilogTz;

      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      if (!TInteger.IsZero(value))
      {
        ilogTz = TInteger.Zero;

        for (var v = value; v >= rdx; v /= rdx)
          ilogTz++;

        return ilogTz;
      }

      return value;
    }

    public static (TInteger ILogFloor, TInteger ILogCeiling) IntegerLog<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (value.TryFastIntegerLog(radix, out TInteger ilogTz, out TInteger ilogAfz, out var _))
        return (ilogTz, ilogAfz);

      var log = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(radix));

      if (log >= 0)
      {
        var ilogc = double.Ceiling(log);
        var ilogf = double.Floor(log);

        var ilog = System.Convert.ToInt64(log);

        var eqwt = log.EqualsWithinAbsoluteTolerance(ilog, 1e-10) || log.EqualsWithinRelativeTolerance(ilog, 1e-10);

        if (eqwt && log < ilog)
          ilogf = ilogc;

        if (eqwt && log > ilog)
          ilogc = ilogf;

        return (TInteger.CreateChecked(ilogf), TInteger.CreateChecked(ilogc));
      }

      return (TInteger.Zero, TInteger.Zero);
    }
  }
}
