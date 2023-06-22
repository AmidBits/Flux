namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the digit place value components of <paramref name="number"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public static System.Collections.Generic.List<TSelf> GetPlaceValues<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var list = GetDigitsReversed(number, radix); // Already asserts radix.

      var power = TSelf.One;

      for (int index = 0; index < list.Count; index++)
      {
        list[index] *= power;

        power *= radix;
      }

      return list;
    }

#else

    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    public static System.Collections.Generic.List<System.Numerics.BigInteger> GetPlaceValues(this System.Numerics.BigInteger value, int radix, bool skipZeroes = false)
    {
      var list = GetDigitsReversed(value, radix);

      for (var index = 0; index < list.Count; index++)
        if (list[index] != 0 || !skipZeroes)
          list[index] *= System.Convert.ToInt32(System.Math.Pow(radix, index));

      return list;
    }

    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    public static System.Collections.Generic.List<int> GetPlaceValues(this int value, int radix, bool skipZeroes = false)
    {
      var list = GetDigitsReversed(value, radix);

      for (var index = 0; index < list.Count; index++)
        if (list[index] != 0 || !skipZeroes)
          list[index] *= System.Convert.ToInt32(System.Math.Pow(radix, index));

      return list;
    }

    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    public static System.Collections.Generic.List<long> GetPlaceValues(this long value, int radix, bool skipZeroes = false)
    {
      var list = GetDigitsReversed(value, radix);

      for (var index = 0; index < list.Count; index++)
        if (list[index] != 0 || !skipZeroes)
          list[index] *= System.Convert.ToInt32(System.Math.Pow(radix, index));

      return list;
    }

#endif
  }
}
