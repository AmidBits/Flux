#if NET7_0_OR_GREATER
using System.Numerics;

namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<TSelf> GetDigitsReversed<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Number.AssertRadix(radix);

      var list = new System.Collections.Generic.List<TSelf>();

      while (value != TSelf.Zero)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
  }
}
#endif
