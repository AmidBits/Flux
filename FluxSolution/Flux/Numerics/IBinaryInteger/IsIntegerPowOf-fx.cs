namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</para>
    /// </summary>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero return as false.</remarks>
    public static bool IsIntegerPowOf<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (radix == TRadix.CreateChecked(2)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TNumber.IsPow2(value);

      try
      {
        var powOfRadix = TNumber.CreateChecked(Units.Radix.AssertMember(radix));

        while (powOfRadix < value)
          powOfRadix = TNumber.CreateChecked(powOfRadix * powOfRadix);

        return powOfRadix == value;
      }
      catch { }

      return false;
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
