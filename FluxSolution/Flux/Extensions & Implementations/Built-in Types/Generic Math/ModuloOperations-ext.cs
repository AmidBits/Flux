// Modulo operations is just a file organizer. Functions still end up under specific numeric types.
namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region ModuloClosed

      public static TInteger ModuloClosed(TInteger value, TInteger modulus)
        => Number.EuclideanModulo(value, modulus + TInteger.One);

      #endregion

      #region ModuloOpen

      public static TInteger ModuloOpen(TInteger value, TInteger modulus)
        => Number.EuclideanModulo(value - TInteger.One, modulus - TInteger.One) + TInteger.One;

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
    where TNumber : System.Numerics.INumber<TNumber>
    {
      #region CenteredModulo

      /// <summary>
      /// <para>Result range:</para>
      /// <para>Floating-point: [-modulus/2, +modulus/2)</para>
      /// <para>Integer: [-floor(modulus/2), +ceil(modulus/2))</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns></returns>
      public static TNumber CenteredModulo(TNumber value, TNumber modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var halfModulus = modulus / NumberConstants<TNumber>.Two;

        return NormalizeZero(EuclideanModulo(value + halfModulus, modulus) - halfModulus);
      }

      #endregion

      #region EuclideanModulo

      /// <summary>
      /// <para>Euclidean modulo mormalizes a number <paramref name="value"/> to the range [0, <paramref name="modulus"/>), where <paramref name="modulus"/> is a positive modulus.</para>
      /// <para>For a centered/symmetric result in the range [-range/2, +range/2), use <c>EuclideanModulo(x + halfRange, range) - halfRange</c>.</para>
      /// <para>This function is also known as ModuloHalfOpenRight(value, modulus).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns></returns>
      public static TNumber EuclideanModulo(TNumber value, TNumber modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var remainder = value % modulus;

        if (TNumber.IsNegative(remainder) && !TNumber.IsZero(remainder))
          remainder += modulus;

        return NormalizeZero(remainder);
      }

      #endregion

      #region IntegerDivRemCeiling

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemCeiling(TNumber a, TNumber n)
      {
        var q = a / n;

        var qt = q - (q % TNumber.One);

        var condition = TNumber.CopySign(a, q) > TNumber.CopySign(qt * n, q); // If q > qt, which for integers we have to re-compute.

        var qc = condition ? qt + TNumber.One : qt;

        var rc = a - qc * n;

        return (qc, rc);
      }

      #endregion

      #region IntegerDivRemEnveloped

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

      #endregion

      #region IntegerDivRemEuclidean

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemEuclidean(TNumber a, TNumber n)
      {
        var q = a / n; q -= q % TNumber.One;
        var r = a % n;

        if (TNumber.IsNegative(r))
          return (q - TNumber.CopySign(TNumber.One, n), r + TNumber.Abs(n));

        return (q, r);
      }

      #endregion

      #region IntegerDivRemFloored

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemFloored(TNumber a, TNumber n)
      {
        var q = a / n;

        var qt = q - (q % TNumber.One);

        var condition = TNumber.CopySign(a, q) < TNumber.CopySign(qt * n, q); // If q < qt, which for integers we have to re-compute.

        var qf = condition ? qt - TNumber.One : qt;

        return (qf, a - qf * n);
      }

      #endregion

      #region IntegerDivRemRounded

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemRounded(TNumber a, TNumber n, NearestRoundingRule rule = NearestRoundingRule.ToEven)
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
          var o = q + TNumber.CreateChecked(TNumber.Sign(n)); // The other value.

          q = rule switch
          {
            NearestRoundingRule.TowardNegativeInfinity => TNumber.Min(q, o),
            NearestRoundingRule.TowardPositiveInfinity => TNumber.Max(q, o),
            NearestRoundingRule.TowardZero => TNumber.Abs(q) <= TNumber.Abs(o) ? q : o,
            NearestRoundingRule.AwayFromZero => TNumber.Abs(q) >= TNumber.Abs(o) ? q : o,
            NearestRoundingRule.ToEven => TNumber.IsEvenInteger(q) ? q : o,
            NearestRoundingRule.ToOdd => TNumber.IsOddInteger(q) ? q : o,
            NearestRoundingRule.Random => System.Random.Shared.Next(2) == 0 ? q : o,
            _ => throw new System.NotImplementedException(nameof(rule))
          };
        }

        // if (twiceR < absN) then q is already correct, so no else branch is needed for the default condition.

        return (q, a - q * n);
      }

      #endregion

      #region IntegerDivRemTruncated

      public static (TNumber Quotient, TNumber Remainder) IntegerDivRemTruncated(TNumber a, TNumber n)
      {
        var q = a / n;

        var qt = q - (q % TNumber.One);

        return (qt, a - qt * n);
      }

      #endregion

      #region ModuloHalfOpenLeft

      /// <summary>
      /// <para>Computes the half-open left modulo of a number <paramref name="value"/> with respect to a positive modulus <paramref name="modulus"/>.</para>
      /// <para>This is the mirror of <see cref="EuclideanModulo{TNumber}(TNumber, TNumber)"/>, which is also known as HalfOpenRightModulo.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns>value in the range (0, modulus]</returns>
      public static TNumber ModuloHalfOpenLeft(TNumber value, TNumber modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        return EuclideanModulo(value - modulus, modulus) + modulus;
      }

      #endregion 

      #region ReflectedModulo

      /// <summary>
      /// <para>Computes the triangle (folded) modulo of a number <paramref name="value"/> with respect to a positive modulus <paramref name="modulus"/>.</para>
      /// <para>The resulting waveform has period 2 × modulus.</para>
      /// <para>A.K.A. "reflected"-modulo.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns>value in the range [0, modulus]</returns>
      public static TNumber ReflectedModulo(TNumber value, TNumber modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        return NormalizeZero(modulus - TNumber.Abs(EuclideanModulo(value, checked(modulus + modulus)) - modulus));
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
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

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
