#if NET7_0_OR_GREATER
using System.Numerics;

namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the place value digits (as numbers) of x using base b, in reverse order.</summary>
    public static System.Span<TSelf> GetDigitsReversed<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(b);

      var list = new System.Collections.Generic.List<TSelf>();

      while (!TSelf.IsZero(x))
      {
        list.Add(x % b);
        x /= b;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
  }
}
#endif
