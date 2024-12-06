namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TValue"/>, based on byte-count (times 8).</para>
    /// </summary>
    /// <remarks>
    /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
    /// </remarks>
    public static int GetBitLength<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.IsNegative(value)
      ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
      : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

    /// <summary>
    /// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>The bit-length(<paramref name="value"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="value"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="value"/>)</c>.</para>
    /// </remarks>
    public static int GetShortestBitLength<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value.GetShortestBitLength();
  }
}
