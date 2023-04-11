namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER
    /// <summary>Returns the count of bits in the minimal two's-complement representation of the number. If the number is negative, the max number of bits, according to GetByteCount(), is returned.</summary>
    /// <remarks>The number of bits needed to represent the number, if value is positive. If value is negative then -1. A value of zero needs 0 bits.</remarks>
    public static int GetBitLengthEx<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength();
    //=> TSelf.IsNegative(value)
    //? (value.GetByteCount() * 8)
    //: value.GetShortestBitLength();
    //=> value > TSelf.Zero ? IntegerLog2(value) + 1 : value < TSelf.Zero ? -1 : 0;
#else
    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>BitLength(value) is equal to 1 + Log2(value).</remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetShortestBitLength(this System.Numerics.BigInteger value)
      => (int)value.GetBitLength();
#endif
  }
}
