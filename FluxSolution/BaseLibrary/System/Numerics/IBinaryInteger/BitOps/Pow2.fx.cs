namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Determines whether <paramref name="value"/> is a power-of-2 value, i.e. a single bit only is set in <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsPow2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.IsPow2(value);

    public static TValue NextLargestPow2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value.BitFoldToLsb() + TValue.One;

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction away-from-zero.</para>
    /// <example>
    /// <para>In order to process for example floating point types:</para>
    /// <code>TValue.CreateChecked(PowOf2AwayFromZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; TValue.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TValue Pow2AwayFromZero<TValue>(this TValue value, bool unequal)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.CopySign(TValue.Abs(value) is var v && TValue.IsPow2(v) ? (unequal ? v << 1 : v) : v.MostSignificant1Bit() << 1, value);

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction toward-zero.</para>
    /// <example>
    /// <para>In order to process for example a double:</para>
    /// <code><see cref="double"/>.CreateChecked(PowOf2TowardZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; <see cref="double"/>.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TValue Pow2TowardZero<TValue>(this TValue value, bool unequal)
       where TValue : System.Numerics.IBinaryInteger<TValue>
       => TValue.CopySign(TValue.Abs(value) is var v && TValue.IsPow2(v) && unequal ? v >> 1 : v.MostSignificant1Bit(), value);

#if INCLUDE_SWAR

    public static TValue SwarNextLargestPow2<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => SwarFoldRight(value) + TValue.One;

#endif

  }
}
