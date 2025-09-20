namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</para>
    /// </summary>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero return as false.</remarks>
    public static bool IsIntegerPowOf<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var q = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(value)) / System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(radix));

      var iq = System.Convert.ToInt64(q);

      var eqwt = q.EqualsWithinAbsoluteTolerance(iq, 1e-10) || q.EqualsWithinRelativeTolerance(iq, 1e-10);

      return eqwt;

      //if (radix == TRadix.CreateChecked(2)) // Special case for binary numbers, we can use dedicated IsPow2().
      //  return TInteger.IsPow2(value);

      //try
      //{
      //  var powOfRadix = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      //  while (powOfRadix < value)
      //    powOfRadix = TInteger.CreateChecked(powOfRadix * powOfRadix);

      //  return powOfRadix == value;
      //}
      //catch { }

      //return false;
    }

    // I don't really like the traditional division AND remainder loop.
    //public static bool IsIntegerPowOf<TPower, TRadix>(this TPower value, TRadix radix)
    //  where TPower : System.Numerics.IBinaryInteger<TPower>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //{
    //  if (radix == TRadix.CreateChecked(2)) // Special case for binary numbers, we can use dedicated IsPow2().
    //    return TPower.IsPow2(value);

    //  var rdx = TPower.CreateChecked(Quantities.Radix.AssertMember(radix));

    //  var val = TPower.Abs(value);

    //  if (val >= rdx)
    //    while (TPower.IsZero(val % rdx))
    //      val /= rdx;

    //  return val == TPower.One;
    //}
  }
}
