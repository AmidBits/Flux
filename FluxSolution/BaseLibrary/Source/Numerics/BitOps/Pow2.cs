namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Determines whether <paramref name="value"/> is a power-of-2 value, i.e. a single bit only is set in <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsPow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(value);

    public static TSelf NextLargestPow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.BitFoldRight() + TSelf.One;

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction away-from-zero.</para>
    /// <example>
    /// <para>In order to process for example floating point types:</para>
    /// <code>TSelf.CreateChecked(PowOf2AwayFromZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; TSelf.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf Pow2AwayFromZero<TSelf>(this TSelf value, bool unequal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.CopySign(TSelf.Abs(value) is var v && TSelf.IsPow2(v) ? (unequal ? v << 1 : v) : v.MostSignificant1Bit() << 1, value);

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction toward-zero.</para>
    /// <example>
    /// <para>In order to process for example a double:</para>
    /// <code><see cref="double"/>.CreateChecked(PowOf2TowardZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; <see cref="double"/>.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TSelf Pow2TowardZero<TSelf>(this TSelf value, bool unequal)
       where TSelf : System.Numerics.IBinaryInteger<TSelf>
       => TSelf.CopySign(TSelf.Abs(value) is var v && TSelf.IsPow2(v) && unequal ? v >> 1 : v.MostSignificant1Bit(), value);

#if INCLUDE_SWAR

    public static TSelf SwarNextLargestPow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.SwarFoldRight() + TSelf.One;

#endif

  }
}
