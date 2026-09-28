namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region EuclideanModulo

      /// <summary>
      /// <para>Euclidean modulo mormalizes a number <paramref name="value"/> to the range [0, <paramref name="modulus"/>), where <paramref name="modulus"/> is a positive modulus.</para>
      /// <para>For a centered/symmetric result in the range [-range/2, +range/2), use <c>EuclideanModulo(x + halfRange, range) - halfRange</c>.</para>
      /// <para>This function is also known as HalfOpenRightModulo.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns></returns>
      public static TNumber EuclideanModulo(TNumber value, TNumber modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var remainder = value % modulus;

        return TNumber.IsNegative(remainder) ? remainder + TNumber.Abs(modulus) : remainder;
      }

      #endregion

      #region HalfOpenLeftModulo

      /// <summary>
      /// <para>Computes the half-open left modulo of a number <paramref name="value"/> with respect to a positive modulus <paramref name="modulus"/>.</para>
      /// <para>This is the mirror of <see cref="EuclideanModulo{TNumber}(TNumber, TNumber)"/>, which is also known as HalfOpenRightModulo.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns>value in the range (0, modulus]</returns>
      public static TNumber HalfOpenLeftModulo(TNumber value, TNumber modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var em = EuclideanModulo(value, modulus);

        return TNumber.IsZero(em) ? modulus : em;
      }

      #endregion

      #region IntegerDivRem.. functions

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemCeiling(TNumber a, TNumber n)
      {
        var q = a / n;

        var qt = q - (q % TNumber.One);

        var condition = TNumber.CopySign(a, q) > TNumber.CopySign(qt * n, q); // If q > qt, which for integers we have to re-compute.

        var qc = condition ? qt + TNumber.One : qt;

        var rc = a - qc * n;

        return (qc, rc);
      }

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemEnveloped(TNumber a, TNumber n)
      {
        var q = a / n;

        var qt = q - (q % TNumber.One);

        var z = qt * n == a;

        var qe = z ? qt : TNumber.Sign(q) switch
        {
          < 0 => qt - TNumber.One,
          > 0 => qt + TNumber.One,
          _ => qt
        };

        return (qe, a - qe * n);
      }

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemEuclidean(TNumber a, TNumber n)
      {
        var q = a / n; q -= q % TNumber.One;
        var r = a % n;

        if (TNumber.IsNegative(r))
          return (q - TNumber.CopySign(TNumber.One, n), r + TNumber.Abs(n));

        return (q, r);
      }

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemFloored(TNumber a, TNumber n)
      {
        var q = a / n;

        var qt = q - (q % TNumber.One);

        var condition = TNumber.CopySign(a, q) < TNumber.CopySign(qt * n, q); // If q < qt, which for integers we have to re-compute.

        var qf = condition ? qt - TNumber.One : qt;

        return (qf, a - qf * n);
      }

      //public static (TNumber Quotient, TNumber Remainder) IntegerDivRemRound(TNumber a, TNumber n, NearestRoundingRule rule)
      //{
      //  var (q, r) = IntegerDivRemFloored(a, n);

      //  if (TNumber.Abs(r + r) >= TNumber.Abs(n))
      //    q += TNumber.Sign(r) == TNumber.Sign(n) ? TNumber.One : -TNumber.One;

      //  return (q, a - q * n);

      //  //var q = a / n;

      //  //var qr = FloatingPoint.RoundToNearestInteger(q, rule);

      //  //return (qr, a - qr * n);
      //}

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemRound(TNumber a, TNumber n, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        var (q, r) = IntegerDivRemFloored(a, n);

        var twiceR = TNumber.Abs(r + r);
        var absN = TNumber.Abs(n);

        if (twiceR > absN)
        {
          q += TNumber.Sign(r) == TNumber.Sign(n) ? TNumber.One : -TNumber.One;
        }
        else if (twiceR == absN)
        {
          var qafz = q + TNumber.CreateChecked(TNumber.Sign(n));

          q = rule switch
          {
            NearestRoundingRule.TowardNegativeInfinity => TNumber.Min(q, qafz),
            NearestRoundingRule.TowardPositiveInfinity => TNumber.Max(q, qafz),
            NearestRoundingRule.TowardZero => TNumber.Abs(q) <= TNumber.Abs(qafz) ? q : qafz,
            NearestRoundingRule.AwayFromZero => TNumber.Abs(q) >= TNumber.Abs(qafz) ? q : qafz,
            NearestRoundingRule.ToEven => (q % (TNumber.One + TNumber.One)) == TNumber.Zero ? q : qafz,
            NearestRoundingRule.ToOdd => (q % (TNumber.One + TNumber.One)) != TNumber.Zero ? q : qafz,
            NearestRoundingRule.Random => System.Random.Shared.Next() == 0 ? q : qafz,
            _ => throw new System.NotImplementedException(nameof(rule))
          };
        }

        // if (twiceR > absN) then q is already correct, so no else branch is needed for the default condition.

        return (q, a - q * n);
      }

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemTruncated(TNumber a, TNumber n)
      {
        var q = a / n;

        var qt = q - (q % TNumber.One);

        return (qt, a - qt * n);
      }

      #endregion

      #region RemainderAnalysis (compute all remainder interpretations)

      /// <summary>
      /// <para>Remainder is the standard remainder.</para>
      /// <para>RemainderNoZero is the same as standard except no zero, and instead returns the divisor with the sign of the dividend.</para>
      /// <para>ReverseRemainder is the reverse order of the standard remainder, except for 0, which is still in same.</para>
      /// <para>ReverseRemainderNoZero is the same as ReverseRemainder except no zero, and instead returns the divisor with the sign of dividend.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns></returns>
      public static (TNumber Remainder, TNumber RemainderNoZero, TNumber ReverseRemainder, TNumber ReverseRemainderNoZero) RemainderAnalysis(TNumber value, TNumber modulus)
      {
        var remainder = value % modulus;

        var copySign = TNumber.CopySign(modulus, value);

        if (TNumber.IsZero(remainder))
          return (remainder, copySign, remainder, copySign);

        var minusRemainder = copySign - remainder;

        return (remainder, remainder, minusRemainder, minusRemainder);
      }

      #endregion
    }
  }
}
