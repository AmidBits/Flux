namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="source"/> is positive. If <paramref name="source"/> is negative, the bit-length represents the storage size of the <typeparamref name="TNumber"/>, based on byte-count (times 8).</para>
    /// </summary>
    /// <remarks>
    /// <para>The <c>bit-length(<paramref name="source"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="source"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="source"/>) - 1)</c>.</para>
    /// </remarks>
    public static int GetBitLength<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsNegative(source)
      ? source.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
      : source.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

    /// <summary>
    /// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>The bit-length(<paramref name="source"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="source"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="source"/>)</c>.</para>
    /// </remarks>
    public static int GetShortestBitLength<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => source.GetShortestBitLength();
  }
}
