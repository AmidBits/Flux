namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="source"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source">The value of which to find the log.</param>
    /// <returns>The ceiling integer log2 of <paramref name="source"/>.</returns>
    /// <remarks>
    /// <para>The <c>log2-away-from-zero(<paramref name="source"/>)</c> is equal to <c>bit-length(<paramref name="source"/>)</c>.</para>
    /// </remarks>
    public static TNumber Log2AwayFromZero<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(source) ? source : TNumber.CopySign(TNumber.Abs(source) is var v && TNumber.IsPow2(v) ? TNumber.Log2(v) : TNumber.Log2(v) + TNumber.One, source);

    /// <summary>
    /// <para>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="source"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="source"/>.</para>
    /// </summary>
    /// <param name="source">The value of which to find the log.</param>
    /// <returns>The floor integer-log-2 of <paramref name="source"/>.</returns>
    /// <remarks>
    /// <para>The ceiling log2 of <paramref name="source"/>: <code>(<paramref name="source"/> > 1 ? Log2TowardZero(<paramref name="source"/> - 1) + 1 : 0)</code></para>
    /// <para>The <c>log2-toward-zero(<paramref name="source"/>)</c> is equal to <c>(bit-length(<paramref name="source"/>) - 1)</c>.</para>
    /// </remarks>
    public static TNumber Log2TowardZero<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(source) ? source : TNumber.CopySign(TNumber.Log2(TNumber.Abs(source)), source);

#if INCLUDE_SWAR

    public static TValue SwarLog2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.PopCount(SwarFoldRight(value) >> 1);

#endif

  }
}
