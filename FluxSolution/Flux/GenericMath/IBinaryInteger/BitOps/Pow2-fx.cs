namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction away-from-zero, optionally <paramref name="unequal"/> if <paramref name="source"/> is already a power-of-2.</para>
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
      => TNumber.CopySign(TNumber.Abs(source) is var abs && TNumber.IsPow2(abs) ? (unequal ? abs << 1 : abs) : abs.MostSignificant1Bit() << 1, source);

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction toward-zero, optionally <paramref name="unequal"/> if <paramref name="source"/> is already a power-of-2.</para>
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
       => TNumber.CopySign(TNumber.Abs(source) is var abs && TNumber.IsPow2(abs) && unequal ? abs >> 1 : abs.MostSignificant1Bit(), source);

#if INCLUDE_SWAR

    public static TValue SwarNextLargestPow2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => SwarFoldRight(value) + TValue.One;

#endif

  }
}
