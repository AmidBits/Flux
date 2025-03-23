namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Extracts the lowest numbered element of a bit set (<paramref name="source"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TNumber LeastSignificant1Bit<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => source & ((~source) + TNumber.One);
    //=> (value & -value); // This optimized version does not work on unsigned integers.

    /// <summary>
    /// <para>Clear <paramref name="source"/> of its least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TNumber LeastSignificant1BitClear<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => source & (source - TNumber.One);

    /// <summary>
    /// <para>Extracts the highest numbered element of a bit set (<paramref name="source"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
    /// <list type="bullet">
    /// <item>If <paramref name="source"/> equal zero, zero is returned.</item>
    /// <item>If <paramref name="source"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
    /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="source"/>).</item>
    /// </list>
    /// </summary>
    /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="source"/>.</remarks>
    public static TNumber MostSignificant1Bit<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsZero(source) ? source : TNumber.One << (source.GetBitLength() - 1);

    /// <summary>
    /// <para>Clear <paramref name="source"/> of its least-significant-1-bit.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static TNumber MostSignificant1BitClear<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => source - source.MostSignificant1Bit();

#if INCLUDE_SWAR

    public static TValue SwarMostSignificant1Bit<TValue>(this TValue source)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      source = SwarFoldRight(source);

      return (source & ~(source >> 1));
    }

#endif

  }
}
