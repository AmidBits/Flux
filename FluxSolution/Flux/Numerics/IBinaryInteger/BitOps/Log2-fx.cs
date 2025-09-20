namespace Flux
{
  public static partial class BitOps
  {
    #region Log2

    /// <summary>
    /// <para>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The ceiling integer log2 of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>The <c>log2-away-from-zero(<paramref name="value"/>)</c> is equal to <c>bit-length(<paramref name="value"/>)</c>.</para>
    /// </remarks>
    public static TInteger Log2AwayFromZero<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      if (TInteger.IsZero(value))
        return value;

      var log2 = TInteger.Log2(value);

      return TInteger.IsPow2(value) ? log2 : log2 + TInteger.One;
    }

    /// <summary>
    /// <para>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The floor integer-log-2 of <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>The ceiling log2 of <paramref name="value"/>: <code>(<paramref name="value"/> > 1 ? Log2TowardZero(<paramref name="value"/> - 1) + 1 : 0)</code></para>
    /// <para>The <c>log2-toward-zero(<paramref name="value"/>)</c> is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>.</para>
    /// </remarks>
    public static TInteger Log2TowardZero<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      if (TInteger.IsZero(value))
        return value;

      return TInteger.Log2(value);
    }

    public static (TInteger log2Ceiling, TInteger log2Floor, TInteger log2Nearest) IntegerLog2<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      if (TInteger.IsZero(value))
        return (value, value, value);

      var log2f = TInteger.Log2(value);

      var log2c = TInteger.IsPow2(value) ? log2f : log2f + TInteger.One;

      return (log2c, log2f, value.RoundToNearest(UniversalRounding.HalfTowardZero, log2f, log2c));
    }

#if EXCLUDE_SWAR

    /// <summary>
    /// <para><see href="https://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TInteger ScratchLog2<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => TInteger.PopCount(ScratchBitFoldRight(value) >> 1);

#endif

    #endregion
  }
}
