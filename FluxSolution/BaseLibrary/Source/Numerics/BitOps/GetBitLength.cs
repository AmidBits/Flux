namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TSelf"/>, based on byte-count (times 8).</para>
    /// <para>The bit-length is equal to the bit position (i.e. one-based indexing) of the most-significant-1-bit of a <paramref name="value"/>, hence a zero-based index is: <code>GetBitLength(<paramref name="value"/>) - 1</code></para>
    /// </summary>
    public static int GetBitLengthEx<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsNegative(value)
      ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
      : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

    /// <summary>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.GetShortestBitLength()"/>.</summary>
    public static int GetShortestBitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.
  }
}
