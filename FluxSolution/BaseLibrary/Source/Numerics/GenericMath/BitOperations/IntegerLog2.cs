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
      => TSelf.IsPow2(x) ? GetIntegerLog2Floor(x) : GetIntegerLog2Floor(x) + 1;

    /// <summary>PREVIEW! Computes the floor of the base 2 log of <paramref name="x"/>. This is the common log function.</summary>
    public static int GetIntegerLog2Floor<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.Log2(x));
  }
}
#endif
