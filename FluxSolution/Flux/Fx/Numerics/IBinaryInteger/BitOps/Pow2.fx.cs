namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Determines whether <paramref name="source"/> is a power-of-2 value, i.e. a single bit only is set in <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsPow2<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.IsPow2(source);

    public static TNumber NextLargestPow2<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value.BitFoldToLsb() + TNumber.One;

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction away-from-zero.</para>
    /// <example>
    /// <para>In order to process for example floating point types:</para>
    /// <code>TValue.CreateChecked(PowOf2AwayFromZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; TValue.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber Pow2AwayFromZero<TNumber>(this TNumber source, bool unequal)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => TNumber.CopySign(TNumber.Abs(source) is var v && TNumber.IsPow2(v) ? (unequal ? v << 1 : v) : v.MostSignificant1Bit() << 1, source);

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction toward-zero.</para>
    /// <example>
    /// <para>In order to process for example a double:</para>
    /// <code><see cref="double"/>.CreateChecked(PowOf2TowardZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; <see cref="double"/>.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber Pow2TowardZero<TNumber>(this TNumber source, bool unequal)
       where TNumber : System.Numerics.IBinaryInteger<TNumber>
       => TNumber.CopySign(TNumber.Abs(source) is var v && TNumber.IsPow2(v) && unequal ? v >> 1 : v.MostSignificant1Bit(), source);

#if INCLUDE_SWAR

    public static TValue SwarNextLargestPow2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => SwarFoldRight(value) + TValue.One;

#endif

  }
}
