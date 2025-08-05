namespace Flux
{
  public static partial class BitOps
  {
    #region BitLength

    /// <summary>
    /// <para>A bit-count is the number of storage bits for the type <typeparamref name="TBitLength"/>.</para>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TBitLength ConvertBitLengthToBitCount<TBitLength>(this TBitLength source)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      => source.ConvertBitLengthToByteCount() * TBitLength.CreateChecked(8);

    /// <summary>
    /// <para>A byte-count is the number of storage bytes for the type <typeparamref name="TBitLength"/>.</para>
    /// </summary>
    /// <typeparam name="TBitLength"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TBitLength ConvertBitLengthToByteCount<TBitLength>(this TBitLength source)
      where TBitLength : System.Numerics.IBinaryInteger<TBitLength>
      => TBitLength.CreateChecked(double.Ceiling(double.CreateChecked(source) / 8));

    /// <summary>
    /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TNumber"/>, based on byte-count (times 8).</para>
    /// </summary>
    /// <remarks>
    /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>, hence a zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
    /// </remarks>
    public static int GetBitLength<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsNegative(value)
      ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
      : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

    ///// <summary>
    ///// <para>Uses the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetShortestBitLength()"/>.</para>
    ///// </summary>
    ///// <remarks>
    ///// <para>The bit-length(<paramref name="source"/>) is equal to <c>(1 + log2-toward-zero(<paramref name="source"/>))</c> and is also equal to <c>log2-away-from-zero(<paramref name="source"/>)</c>.</para>
    ///// </remarks>
    //public static int GetShortestBitLength<TNumber>(this TNumber source)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
    //  => source.GetShortestBitLength();

    #endregion
  }
}
