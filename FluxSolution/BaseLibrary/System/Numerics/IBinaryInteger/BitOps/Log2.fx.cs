namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The ceiling integer log2 of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>The <c>log2-away-from-zero(<paramref name="value"/>)</c> is equal to <c>bit-length(<paramref name="value"/>)</c>.</para>
    /// </remarks>
    public static TValue Log2AwayFromZero<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.IsZero(value) ? value : TValue.CopySign(TValue.Abs(value) is var v && TValue.IsPow2(v) ? TValue.Log2(v) : TValue.Log2(v) + TValue.One, value);

    /// <summary>
    /// <para>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The floor integer-log-2 of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>The ceiling log2 of <paramref name="value"/>: <code>(<paramref name="value"/> > 1 ? IntegerLog2Floor(<paramref name="value"/> - 1) + 1 : 0)</code></para>
    /// <para>The <c>log2-toward-zero(<paramref name="value"/>)</c> is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
    /// </remarks>
    public static TValue Log2TowardZero<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.IsZero(value) ? value : TValue.CopySign(TValue.Log2(TValue.Abs(value)), value);

#if INCLUDE_SWAR

    public static TValue SwarLog2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.PopCount(SwarFoldRight(value) >> 1);

#endif

  }
}
