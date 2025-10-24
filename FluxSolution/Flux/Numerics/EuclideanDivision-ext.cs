namespace Flux
{
  /// <summary>
  /// <para>Euclidean division, where the remainder is always positive.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Euclidean_division"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
  /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
  /// </summary>
  public static class EuclideanDivision
  {
    extension<TInteger>(TInteger dividend)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Enveloped (opposite of truncated, inclusive) division, where the quotient is ceiling for positive and floor for negative.</para>
      /// <para>If the remainder is anything but zero, the quotient is equal to "floor/truncated quotient" + 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="dividend"></param>
      /// <param name="divisor"></param>
      /// <returns></returns>
      public (TInteger QuotientEnveloped, TInteger Remainder) EnvelopedDivRem(TInteger divisor)
      {
        var (q, r) = TInteger.DivRem(dividend, divisor);

        if (TInteger.IsZero(r))
          return (q, r);

        return (q + TInteger.CopySign(TInteger.One, divisor), r);
        //return TInteger.IsNegative(q) ? (q - TInteger.One, r) : (q + TInteger.One, r);
      }

      /// <summary>
      /// <para>Euclidean division, where the remainder is always positive.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Euclidean_division"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="dividend"></param>
      /// <param name="divisor"></param>
      /// <returns></returns>
      public (TInteger Quotient, TInteger Remainder) EuclideanDivRem(TInteger divisor)
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
      public (TInteger Quotient, TInteger Remainder) FloorDivRem(TInteger divisor)
      {
        var (q, r) = TInteger.DivRem(dividend, divisor);

        if ((r > TInteger.Zero && TInteger.IsNegative(divisor)) || (TInteger.IsNegative(r) && divisor > TInteger.Zero))
          return (q - TInteger.One, r + divisor);

        return (q, r);
      }
    }
  }
}
