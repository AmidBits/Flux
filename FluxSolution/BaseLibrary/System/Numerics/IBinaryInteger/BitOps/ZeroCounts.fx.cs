namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>A.k.a. count-leading-zero's (clz), counts the number of zero bits preceding the most-significant-1-bit in <paramref name="value"/>. I.e. the number of most-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.LeadingZeroCount(TValue)"/>.</remarks>
    public static int GetLeadingZeroCount<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => int.CreateChecked(TValue.LeadingZeroCount(value));

    /// <summary>
    /// <para>A.k.a. called count-trailing-zero's (ctz), counts the number of zero bits trailing the least-significant-1-bit in <paramref name="value"/>. I.e. the number of least-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.TrailingZeroCount(TValue)"/>.</remarks>
    public static int GetTrailingZeroCount<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => int.CreateChecked(TValue.TrailingZeroCount(value));

#if INCLUDE_SWAR

    public static int SwarLeadingZeroCount<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => GetBitCount(value) - GetPopCount(SwarFoldRight(value));

#endif

  }
}
