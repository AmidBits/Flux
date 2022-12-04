namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>Returns the count of bits in the minimal two's-complement representation of the number. If the number is negative, the max number of bits, according to GetByteCount(), is returned.</summary>
    /// <remarks>The number of bits needed to represent the number, if value is positive. If value is negative then -1. A value of zero needs 0 bits.</remarks>
    public static int GetBitLengthEx<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsNegative(value)
      ? (value.GetByteCount() * 8) // GetByteCount() times 8.
      : value.GetShortestBitLength();
    //=> value > TSelf.Zero ? IntegerLog2(value) + 1 : value < TSelf.Zero ? -1 : 0;

    ///// <summary>Projects the built-in GetShortestBitLength as an extension method so that it works for all integer types, including <see cref="System.Numerics.BigInteger"/>.</summary>
    //public static int gGetShortestBitLength<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //  => value.GetShortestBitLength();
  }
}
