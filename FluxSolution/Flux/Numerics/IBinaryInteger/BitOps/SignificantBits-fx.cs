namespace Flux
{
  public static partial class BitOps
  {
    #region SignificantBits

    /// <summary>
    /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
    /// </summary>
    /// <remarks>The LS1B is the largest power of two that is also a divisor of <paramref name="value"/>.</remarks>
    /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TInteger LeastSignificant1Bit<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value & ((~value) + TInteger.One);
    //=> (value & -value); // This optimized version does not work on unsigned integers.

    /// <summary>
    /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
    /// </summary>
    /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TInteger LeastSignificant1BitClear<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value & (value - TInteger.One);

    /// <summary>
    /// <para>Extracts the highest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
    /// <list type="bullet">
    /// <item>If <paramref name="value"/> equal zero, zero is returned.</item>
    /// <item>If <paramref name="value"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
    /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="value"/>).</item>
    /// </list>
    /// </summary>
    /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="value"/>.</remarks>
    public static TInteger MostSignificant1Bit<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => TInteger.IsZero(value) ? value : TInteger.One << (value.GetBitLength() - 1);

    /// <summary>
    /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
    /// </summary>
    /// <see href="https://aggregate.org/MAGIC/#Most%20Significant%201%20Bit"/>
    public static TInteger MostSignificant1BitClear<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value - value.MostSignificant1Bit();

    #endregion

#if EXCLUDE_SCRATCH

    public static TInteger ScratchLeastSignificant1Bit<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value & ((~value) + TInteger.One); // Works on signed or unsigned integers.

    // => (value ^ (value & (value - TInteger.One))); // Alternative to the above.
    // => (value & -value); // Does not work on unsigned integers.

    public static TInteger ScratchMostSignificant1Bit<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      value = BitFoldRight(value);

      return value & ~(value >> 1);
    }

#endif

  }
}
