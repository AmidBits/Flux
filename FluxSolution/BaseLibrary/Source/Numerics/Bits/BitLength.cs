namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the count of bits in the minimal two's-complement representation of the number. If the number is negative, the max number of bits, according to GetByteCount(), is returned.</summary>
    /// <remarks>The number of bits needed to represent the number, if value is positive. If value is negative then -1. A value of zero needs 0 bits.</remarks>
    public static int BitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength();

#else

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>BitLength(value) is equal to 1 + Log2(value).</remarks>
    public static int BitLength(this System.Numerics.BigInteger value) => (int)value.GetBitLength();

    public static int BitLength(this int value) => 1 + System.Numerics.BitOperations.Log2(unchecked((uint)value));
    public static int BitLength(this long value) => 1 + System.Numerics.BitOperations.Log2(unchecked((ulong)value));

#endif
  }
}
