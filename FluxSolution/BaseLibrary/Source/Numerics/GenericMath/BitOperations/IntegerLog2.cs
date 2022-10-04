#if NET7_0_OR_GREATER
using Flux.AmbOps;

namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>PREVIEW! Computes the ceiling of the base 2 log of <paramref name="x"/>.</summary>
    public static int GetIntegerLog2Ceiling<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      // No zero check is necessary because it's not a power of two.
      => TSelf.IsPow2(x.AssertNonNegativeValue()) ? x.GetShortestBitLength() - 1 : x.GetShortestBitLength();

    /// <summary>PREVIEW! Computes the floor of the base 2 log of <paramref name="x"/>. This is the common log function.</summary>
    public static int GetIntegerLog2Floor<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(x) ? 0 : x.AssertNonNegativeValue().GetShortestBitLength() - 1;

    /// <summary>PREVIEW! Computes the floor and ceiling of the base 2 log of <paramref name="x"/>, using the .NET try paradigm.</summary>
    public static bool TryGetIntegerLog2<TSelf>(this TSelf x, out int log2Floor, out int log2Ceiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      log2Floor = 0;
      log2Ceiling = 0;

      if (x.IsNonNegativeValue())
      {
        if (!TSelf.IsZero(x))
        {
          log2Floor = x.GetShortestBitLength() - 1;
          log2Ceiling = TSelf.IsPow2(x) ? log2Floor : x.GetShortestBitLength();
        }

        return true;
      }
      else
        return false;
    }
  }
}
#endif
