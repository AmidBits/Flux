#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Returns the digit components of the value. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public static System.Span<TSelf> GetPlaceValues<TSelf>(this TSelf self, TSelf radix, bool skipZeroes = false)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var span = GetDigitsReversed(self, radix); // Already asserts radix.

      for (int index = span.Length - 1; index > 0; index--) // Skip index == 0, because it's the 'ones' column.
        if (span[index] != TSelf.Zero || !skipZeroes)
          span[index] *= GenericMath.IntegerPow(radix, index);

      return span;
    }
  }
}
#endif
