namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the digit place value components of <paramref name="number"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public static System.Collections.Generic.List<TSelf> GetPlaceValues<TSelf, TRadix>(this TSelf number, TRadix radix, bool skipZeroes = false)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var list = GetDigitsReversed(number, radix); // Already asserts radix.

      for (int index = list.Count - 1; index > 0; index--) // Skip index == 0, because it's the 'ones' column.
        if (!TSelf.IsZero(list[index]) || !skipZeroes)
          list[index] *= IntegerPow(TSelf.CreateChecked(radix), index);

      return list;
    }
  }
}
