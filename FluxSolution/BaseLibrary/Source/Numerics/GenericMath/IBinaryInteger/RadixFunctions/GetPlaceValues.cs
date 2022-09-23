#if NET7_0_OR_GREATER
using System.Numerics;

namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    public static System.Span<TSelf> GetPlaceValues<TSelf>(this TSelf self, TSelf radix, bool skipZeroes = false)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var span = GetDigitsReversed(self, radix);

      for (var index = 0; index < span.Length; index++)
        if (span[index] != TSelf.Zero || !skipZeroes)
          span[index] *= IntegerPow(radix, index);

      return span;
    }
  }
}
#endif
