#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    public static bool IsPowerOf2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IBitwiseOperators<TSelf, TSelf, TSelf>
      => value > TSelf.Zero && (value & (value - TSelf.One)) == TSelf.Zero;
  }
}
#endif
