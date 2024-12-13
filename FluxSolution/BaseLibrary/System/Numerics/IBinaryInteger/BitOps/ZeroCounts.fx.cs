namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>A.k.a. count-leading-zero's (clz), counts the number of zero bits preceding the most-significant-1-bit in <paramref name="source"/>. I.e. the number of most-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.LeadingZeroCount(TValue)"/>.</remarks>
    public static int GetLeadingZeroCount<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => int.CreateChecked(TNumber.LeadingZeroCount(source));

    /// <summary>
    /// <para>A.k.a. called count-trailing-zero's (ctz), counts the number of zero bits trailing the least-significant-1-bit in <paramref name="source"/>. I.e. the number of least-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.TrailingZeroCount(TValue)"/>.</remarks>
    public static int GetTrailingZeroCount<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => int.CreateChecked(TNumber.TrailingZeroCount(source));

#if INCLUDE_SWAR

    public static int SwarLeadingZeroCount<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => GetBitCount(value) - GetPopCount(SwarFoldRight(value));

#endif

  }
}
