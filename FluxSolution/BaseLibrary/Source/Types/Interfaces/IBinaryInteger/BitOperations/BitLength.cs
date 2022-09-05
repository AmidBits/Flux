#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    public static int ShortestBitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength();

    /// <summary>PREVIEW! Returns the count of bits in the minimal two's-complement representation of the number. If the number is negative, the max number of bits, according to GetByteCount(), is returned.</summary>
    /// <remarks>The number of bits needed to represent the number, if value is positive. If value is negative then -1. A value of zero needs 0 bits.</remarks>
    public static int BitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value < TSelf.Zero // If negative,
      ? (value.GetByteCount() * 8) // return the maximum number of bits, according to GetByteCount(),
      : value.GetShortestBitLength(); // otherwise return the bit-length using GetShortestBitLength().
    //=> value > TSelf.Zero ? ILog2(value) + 1
    //: value < TSelf.Zero ? -1
    //: 0;
  }
}
#endif
