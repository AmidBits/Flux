namespace Flux
{
  /// <summary>
  /// <para>Euclidean division, where the remainder is always positive.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
  /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
  /// </summary>
  public static class ModuloOperations
  {
    extension<TInteger>(TInteger a)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Ceiling division, where the remainder has the opposite sign of that of the divisor.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = ceiling(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public (TInteger Quotient, TInteger Remainder) CeilingDivRem(TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = (TInteger.IsNegative(a) == TInteger.IsNegative(n) ? (a + (n - TInteger.CopySign(TInteger.One, n))) : a) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Closest division.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public (TInteger Quotient, TInteger Remainder) ClosestDivRem(TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var ndiv2 = n / TInteger.CreateChecked(2);

        var q = (TInteger.IsNegative(a) == TInteger.IsNegative(n) ? (a + ndiv2) : (a - ndiv2)) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Enveloped (opposite of truncated, in that it envelops the entire fractional side to the next whole integer) division, where the quotient is ceiling for positive and floor for negative.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = envelop(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public (TInteger Quotient, TInteger Remainder) EnvelopedDivRem(TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var onecsn = TInteger.CopySign(TInteger.One, n);

        var q = (TInteger.IsNegative(a) == TInteger.IsNegative(n) ? (a + (n - onecsn)) : (a - (n - onecsn))) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Euclidean division, where the remainder is always positive.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Euclidean_division"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = sgn(n) * floor(a / abs(n))</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public (TInteger Quotient, TInteger Remainder) EuclideanDivRem(TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = TInteger.CreateChecked(TInteger.Sign(n)) * ((TInteger.IsNegative(a) == TInteger.IsNegative(n) ? a : (a - (n - TInteger.CopySign(TInteger.One, n)))) / n);

        return (q, a - TInteger.Abs(n) * q);
      }

      /// <summary>
      /// <para>Floored division, where the remainder has the same sign as the divisor.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = floor(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public (TInteger Quotient, TInteger Remainder) FlooredDivRem(TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = (TInteger.IsNegative(a) != TInteger.IsNegative(n) ? (a - (n - TInteger.CopySign(TInteger.One, n))) : a) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Rounded division, where the sign of the remainder depends on the rounding strategy, which is <see cref="MidpointRounding.ToEven"/>.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = round(a / n)</c> // rounding half to even</para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public (TInteger Quotient, TInteger Remainder) RoundedDivRem(TInteger n)
      {
        var q = FlooredDivRem(a, n).Quotient;

        if (!TInteger.IsEvenInteger(q))
          q = CeilingDivRem(a, n).Quotient;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Truncated division, where the remainder has the same sign as the dividend a.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = trunc(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      [System.Obsolete("Please use the built-in versions from System.Numerics.IBinaryInteger<>.DivRem(left, right) instead. Just provided here for completeness.")]
      public (TInteger Quotient, TInteger Remainder) TruncatedDivRem(TInteger n) => TInteger.DivRem(a, n);
    }
  }
}
