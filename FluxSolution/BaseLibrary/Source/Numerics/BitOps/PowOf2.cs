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

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction away-from-zero.</para>
    /// <example>
    /// <para>In order to process for example floating point types:</para>
    /// <code>TSelf.CreateChecked(PowOf2AwayFromZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; TSelf.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="proper"></param>
    /// <returns></returns>
    public static TSelf PowOf2AwayFromZero<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.CopySign(TSelf.Abs(value) is var v && TSelf.IsPow2(v) ? (proper ? v << 1 : v) : v.MostSignificant1Bit() << 1, value);

    /// <summary>
    /// <para>Computes the closest power-of-2 in the direction toward-zero.</para>
    /// <example>
    /// <para>In order to process for example a double:</para>
    /// <code><see cref="double"/>.CreateChecked(PowOf2TowardZero(System.Numerics.BigInteger.CreateChecked(value), proper &amp;&amp; <see cref="double"/>.IsInteger(value)))</code>
    /// </example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <param name="proper"></param>
    /// <returns></returns>
    public static TSelf PowOf2TowardZero<TSelf>(this TSelf value, bool proper)
       where TSelf : System.Numerics.IBinaryInteger<TSelf>
       => TSelf.CopySign(TSelf.Abs(value) is var v && TSelf.IsPow2(v) && proper ? v >> 1 : v.MostSignificant1Bit(), value);

    /// <summary>
    /// <para>Compute the two closest (toward-zero and away-from-zero) power-of-2 of <paramref name="value"/>. Specify <paramref name="proper"/> to ensure results that are not equal to value.</para>
    /// </summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="proper">Proper means nearest but do not include <paramref name="value"/> if it's a power-of-2, i.e. the two power-of-2 will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <returns>The two closest (toward-zero and away-from-zero) power-of-2 to <paramref name="value"/>.</returns>
    public static (TSelf powOf2TowardsZero, TSelf powOf2AwayFromZero) PowOf2<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var powOf2TowardsZero = TSelf.Abs(value);
      var powOf2AwayFromZero = powOf2TowardsZero;

      if (!TSelf.IsPow2(powOf2TowardsZero) && powOf2TowardsZero.MostSignificant1Bit() is var ms1b)
      {
        powOf2TowardsZero = ms1b;
        powOf2AwayFromZero = ms1b << 1;
      }
      else if (proper)
      {
        powOf2TowardsZero >>= 1;
        powOf2AwayFromZero <<= 1;
      }

      return (TSelf.CopySign(powOf2TowardsZero, value), TSelf.CopySign(powOf2AwayFromZero, value));
    }

    /// <summary>
    /// <para>Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</para>
    /// </summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="powOf2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="powOf2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TSelf PowOf2<TSelf>(this TSelf value, bool proper, RoundingMode mode, out TSelf powOf2TowardsZero, out TSelf powOf2AwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      (powOf2TowardsZero, powOf2AwayFromZero) = PowOf2(value, proper);

      return value.RoundToBoundaries(mode, powOf2TowardsZero, powOf2AwayFromZero);
    }
  }
}
