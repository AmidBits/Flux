namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Enveloped (opposite of truncated, inclusive) division, where the quotient is ceiling for positive and floor for negative.</para>
    /// <para>If the remainder is anything but zero, the quotient is equal to "floor/truncated quotient" + 1.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static (TInteger QuotientEnveloped, TInteger Remainder) DivModEnveloped<TInteger>(this TInteger dividend, TInteger divisor)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var (q, r) = TInteger.DivRem(dividend, divisor);

      if (TInteger.IsZero(r))
        return (q, r);

      //return (q + TInteger.CopySign(TInteger.One, q), r);
      return TInteger.IsNegative(q) ? (q - TInteger.One, r) : (q + TInteger.One, r);
    }

    /// <summary>
    /// <para>Euclidean division, where the remainder is always positive.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Euclidean_division"/></para>
    /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static (TInteger Quotient, TInteger Remainder) DivModEuclidean<TInteger>(this TInteger dividend, TInteger divisor)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var (q, r) = TInteger.DivRem(dividend, divisor);

      if (TInteger.IsNegative(r))
        return (divisor > TInteger.Zero) ? (q - TInteger.One, r + divisor) : (q + TInteger.One, r - divisor);

      return (q, r);
    }

    /// <summary>
    /// <para>Floored division, where the remainder has the same sign as the divisor.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
    /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static (TInteger Quotient, TInteger Remainder) DivModFloor<TInteger>(this TInteger dividend, TInteger divisor)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var (q, r) = TInteger.DivRem(dividend, divisor);

      if ((r > TInteger.Zero && TInteger.IsNegative(divisor)) || (TInteger.IsNegative(r) && divisor > TInteger.Zero))
        return (q - TInteger.One, r + divisor);

      return (q, r);
    }
  }
}
