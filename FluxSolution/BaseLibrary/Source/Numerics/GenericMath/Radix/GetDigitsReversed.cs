#if NET7_0_OR_GREATER
using System.Numerics;

namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the place value digits (as numbers) of a value in the specified radix, in reverse order.</summary>
    public static System.Span<TSelf> GetDigitsReversed<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<TSelf>();

      while (!TSelf.IsZero(value))
      {
        list.Add(value % radix);
        value /= radix;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
  }
}
#endif