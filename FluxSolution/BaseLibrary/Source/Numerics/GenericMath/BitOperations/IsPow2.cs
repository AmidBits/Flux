#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>PREVIEW! Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    public static bool IsPow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(value);
    // value > TSelf.Zero && TSelf.IsZero(value & (value - TSelf.One));
  }
}
#endif
