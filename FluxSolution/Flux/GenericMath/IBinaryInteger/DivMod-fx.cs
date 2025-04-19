namespace Flux
{
  public static partial class GenericMath
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
      where TInteger : System.Numerics.INumber<TInteger>
    {
      var q = dividend / divisor;
      var r = dividend % divisor;

      if (TInteger.IsZero(r))
        return (q, r);

      return TInteger.IsNegative(q) ? (q - TInteger.One, r) : (q + TInteger.One, r);
    }

    /// <summary>
    /// <para>Euclidean division, where the remainder is always positive.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Euclidean_division"/></para>
    /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static (TNumber Quotient, TNumber Remainder) DivModEuclidean<TNumber>(this TNumber dividend, TNumber divisor)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var (q, r) = TNumber.DivRem(dividend, divisor);

      if (TNumber.IsNegative(r))
        return (divisor > TNumber.Zero) ? (q - TNumber.One, r + divisor) : (q + TNumber.One, r - divisor);

      return (q, r);
    }

    /// <summary>
    /// <para>Floored division, where the remainder has the same sign as the divisor.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
    /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static (TNumber Quotient, TNumber Remainder) DivModFloor<TNumber>(this TNumber dividend, TNumber divisor)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var (q, r) = TNumber.DivRem(dividend, divisor);

      if ((r > TNumber.Zero && TNumber.IsNegative(divisor)) || (TNumber.IsNegative(r) && divisor > TNumber.Zero))
        return (q - TNumber.One, r + divisor);

      return (q, r);
    }
  }
}
