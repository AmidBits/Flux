namespace Flux
{
  public static partial class BitOps
  {
    #region ZeroCounts

    /// <summary>
    /// <para>A.k.a. count-leading-zero's (clz), counts the number of zero bits leading (preceding) the most-significant-1-bit in <paramref name="value"/>. I.e. the number of most-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.LeadingZeroCount(TValue)"/>.</remarks>
    public static int GetLeadingZeroCount<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => int.CreateChecked(TInteger.LeadingZeroCount(value));

    /// <summary>
    /// <para>A.k.a. called count-trailing-zero's (ctz), counts the number of zero bits trailing the least-significant-1-bit in <paramref name="value"/>. I.e. the number of least-significant-0-bits.</para>
    /// </summary>
    /// <remarks>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.TrailingZeroCount(TValue)"/>.</remarks>
    public static int GetTrailingZeroCount<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => int.CreateChecked(TInteger.TrailingZeroCount(value));

    #endregion

#if INCLUDE_SCRATCH

    public static int ScratchLeadingZeroCount<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var count = value.GetBitCount();

      if (!TInteger.IsZero(value))
        count -= ScratchGetPopCount(ScratchBitFoldRight(value));

      return count;
    }

    public static int ScratchTrailingZeroCount<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TInteger.IsZero(value))
        return value.GetBitCount();

      var count = 0;

      value = (value & ((~value) + TInteger.One)) >> 1;  // Set trailing 0's to 1's and zero the rest.

      for (count = 0; value > TInteger.Zero; count++)
        value >>= 1;

      return count;
    }

#endif
  }
}
