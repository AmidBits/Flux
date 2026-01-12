namespace Flux
{
  public static partial class Numbers
  {
    /// <summary>
    /// <para>Sign step function that guarantees [-1, 0, 1] for output.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Sign_function"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Step_function"/></para>
    /// </summary>
    /// <remarks>LT zero = -1, EQ zero = 0, GT zero = +1.</remarks>
    /// <returns></returns>
    public static TNumber Sign<TNumber>(TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsZero(value) ? value : TNumber.CopySign(TNumber.One, value);

    extension<TNumber>(TNumber)
    where TNumber : System.Numerics.INumber<TNumber>
    {
      #region Arithmetic progression

      public static TFloat ArithmeticMean<TFloat>(out TFloat sumOfTerms, params System.Collections.Generic.IEnumerable<TNumber> terms)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        sumOfTerms = TFloat.CreateChecked(terms.Sum(out var countOfTerms));

        return checked(sumOfTerms / TFloat.CreateChecked(countOfTerms));
      }

      public static TFloat ArithmeticMeanOfTwoTerms<TFloat>(out TFloat sumOfTerms, TNumber a, TNumber b)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        sumOfTerms = TFloat.CreateChecked(a + b);

        return sumOfTerms / TFloat.CreateChecked(2);
      }

      /// <summary>
      /// <para>Creates a new sequence of non-zero numbers where each term after the first <paramref name="firstTerm"/> is found by multiplying the previous one by a fixed, non-zero number called the <paramref name="d"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Geometric_progression"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="a"></param>
      /// <param name="d"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Collections.Generic.IEnumerable<TNumber> ArithmeticSequence(TNumber a, TNumber d)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(a);
        System.ArgumentOutOfRangeException.ThrowIfZero(d);

        for (var n = 0; true; n++) // We can start at zero..
          yield return checked(a + TNumber.CreateChecked(n) * d); // ..and get away with NOT subtracting one from n: (a + n * d)
      }

      /// <summary>
      /// <para>Get the <paramref name="nth"/> term of a geometric sequence with the specified <paramref name="commonRatio"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a"></param>
      /// <param name="d"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TNumber ArithmeticSequenceNthTerm<TInteger>(TNumber a, TNumber d, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => a + TNumber.CreateChecked(n - TInteger.One) * d; // (a + (n - 1) * d)

      /// <summary>
      /// <para>Gets the geometric series (sum) of a geometric sequence with infinite terms and the specified <paramref name="d"/>.</para>
      /// </summary>
      /// <param name="d">The common ratio of the geometric sequence.</param>
      /// <returns></returns>
      public static TNumber ArithmeticSeriesMeanOfNTerms<TInteger>(TNumber a, TNumber d, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => (a + ArithmeticSequenceNthTerm(a, d, n)) / TNumber.CreateChecked(2);

      ///// <summary>
      ///// <para>Gets the geometric series (sum) of a geometric sequence with infinite terms and the specified <paramref name="d"/>.</para>
      ///// </summary>
      ///// <param name="d">The common ratio of the geometric sequence.</param>
      ///// <returns></returns>
      //public static TNumber ArithmeticSeriesOfInfiniteTerms<TNumber>(this TNumber a, TNumber d)
      //  where TNumber : System.Numerics.INumber<TNumber>
      //  => ()

      /// <summary>
      /// <para>Gets the geometric series (sum) of a geometric sequence with <paramref name="n"/> terms and the specified <paramref name="d"/>.</para>
      /// </summary>
      /// <param name="d">The common ratio of the geometric sequence.</param>
      /// <param name="n">The term of which to find the sum up until.</param>
      /// <returns></returns>
      public static TNumber ArithmeticSeriesOfNTerms<TInteger>(TNumber a, TNumber d, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => TNumber.CreateChecked(n) * (a + ArithmeticSequenceNthTerm(a, d, n)) / TNumber.CreateChecked(2);

      #endregion

      #region Detent

      /// <summary>
      /// <para>Snaps the <paramref name="value"/> to the nearest <paramref name="interval"/> if it's within the specified <paramref name="proximity"/> of an <paramref name="interval"/> position, otherwise unaltered.</para>
      /// </summary>
      /// <remarks>This is similar to a knob that has notches which latches the knob at certain positions.</remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="interval">The number will snap to any multiple of the specified <paramref name="interval"/>.</param>
      /// <param name="proximity">This is the absolute tolerance of proximity, on either side of an <paramref name="interval"/>.</param>
      /// <returns></returns>
      public static TNumber DetentInterval(TNumber value, TNumber interval, TNumber proximity)
        => TNumber.CreateChecked(int.CreateChecked(value / interval)) * interval is var tzInterval && TNumber.Abs(tzInterval - value) <= proximity
        ? tzInterval
        : tzInterval + interval is var afzInterval && TNumber.Abs(afzInterval - value) <= proximity
        ? afzInterval
        : value;

      /// <summary>
      /// <para>Snaps a <paramref name="value"/> to a <paramref name="position"/> if it's within the specified <paramref name="proximity"/> of the <paramref name="position"/>, otherwise unaltered.</para>
      /// </summary>
      /// <remarks>This is similar to a knob that has a notch which latches the knob at a certain position.</remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="position">E.g. a 0 snaps the <paramref name="value"/> to zero within the <paramref name="proximity"/>.</param>
      /// <param name="proximity">This is the absolute tolerance of proximity, on either side of the <paramref name="position"/>.</param>
      /// <returns></returns>
      public static TNumber DetentPosition(TNumber value, TNumber position, TNumber proximity)
        => EqualsWithinAbsoluteTolerance(position, value, proximity)
        ? position // Detent to the position.
        : value;

      #endregion

      #region EqualsWithin..

      /// <summary>
      /// <para>Perform an absolute equality test.</para>
      /// <para>Absolute equality checks if the absolute difference between <paramref name="value"/> and <paramref name="other"/> is smaller than a predefined <paramref name="absoluteTolerance"/>. This is useful when you want to ensure the numbers are "close enough" without considering their scale.</para>
      /// <para>Absolute equality is simpler and works well for small numbers or fixed tolerances.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="absoluteTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public static bool EqualsWithinAbsoluteTolerance(TNumber value, TNumber other, TNumber absoluteTolerance)
        => value == other
        || TNumber.Abs(value - other) <= absoluteTolerance;

      /// <summary>
      /// <para>Perform a relative equality test.</para>
      /// <para>Relative equality considers the scale of the numbers by dividing the absolute difference by the magnitude of the numbers. This is useful when comparing numbers that may vary significantly in scale.</para>
      /// <para>Relative equality is better for large numbers or numbers with varying scales, as it adjusts the tolerance dynamically.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TRelativeTolerance"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="relativeTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public static bool EqualsWithinRelativeTolerance<TRelativeTolerance>(TNumber value, TNumber other, TRelativeTolerance relativeTolerance)
        where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
        => value == other
        || TRelativeTolerance.CreateChecked(TNumber.Abs(value - other)) <= relativeTolerance * TRelativeTolerance.CreateChecked(TNumber.Abs(TNumber.MaxMagnitude(value, other)));

      /// <summary>
      /// <para>Perform both an absolute and a relative equality test for more robust comparisons. Returns true if any test is considered equal, otherwise false.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TRelativeTolerance"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="absoluteTolerance">E.g. 1e-10.</param>
      /// <param name="relativeTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public static bool EqualsWithinTolerance<TRelativeTolerance>(TNumber value, TNumber other, TNumber absoluteTolerance, TRelativeTolerance relativeTolerance)
        where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
        => EqualsWithinAbsoluteTolerance(value, other, absoluteTolerance) || EqualsWithinRelativeTolerance(value, other, relativeTolerance);

      /// <summary>
      /// <para>Perform an equality test involving the most (integer part) or the least (fraction part) <typeparamref name="TSignificantDigits"/> using the specified <paramref name="radix"/>.</para>
      /// <para>Positive means most <paramref name="significantDigits"/> tolerance on the fraction part.</para>
      /// <para>Negative means least <paramref name="significantDigits"/> tolerance on the integer part.</para>
      /// <para><see href="https://stackoverflow.com/questions/9180385/is-this-value-valid-float-comparison-that-accounts-for-value-set-number-of-decimal-place"/></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TSignificantDigits"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="significantDigits">The tolerance, as the number of significant digits, considered for equality. A positive value for most significant digits on the right side (fraction part). A negative value for least significant digits on the left side (integer part).</param>
      /// <param name="radix"></param>
      /// <remarks>
      /// <para>EqualsWithinSignificantDigits(1000.02, 1000.015, 2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 0.01 for radix 10.</para>
      /// <para>EqualsWithinSignificantDigits(1334.261, 1235.272, -2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to negative <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 100 for radix 10.</para>
      /// </remarks>
      /// <returns></returns>
      public static bool EqualsWithinSignificantDigits<TSignificantDigits, TRadix>(TNumber value, TNumber other, TSignificantDigits significantDigits, TRadix radix)
        where TSignificantDigits : System.Numerics.IBinaryInteger<TSignificantDigits>
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value == other
        || (double.CreateChecked(TNumber.Abs(value - other)) <= double.Pow(double.CreateChecked(Units.Radix.AssertMember(radix)), -double.CreateChecked(significantDigits)));

      #endregion

      #region FoldAcross

      /// <summary>
      /// <para>Folds an out-of-bound <paramref name="value"/> (back and forth) across the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public static TNumber FoldAcross(TNumber value, TNumber minValue, TNumber maxValue)
        => (value > maxValue)
        ? (TruncMod(value - maxValue, maxValue - minValue) is var (tqHi, remHi) && TNumber.IsEvenInteger(tqHi) ? maxValue - remHi : minValue + remHi)
        : (value < minValue)
        ? (TruncMod(minValue - value, maxValue - minValue) is var (tqLo, remLo) && TNumber.IsEvenInteger(tqLo) ? minValue + remLo : maxValue - remLo)
        : value;

      #endregion

      #region Geometric progression

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="productOfTerms"></param>
      /// <param name="terms"></param>
      /// <returns></returns>
      public static TFloat GeometricMean<TFloat>(out TFloat productOfTerms, params System.Collections.Generic.IEnumerable<TNumber> terms)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IRootFunctions<TFloat>
      {
        productOfTerms = TFloat.CreateChecked(terms.Product(out var countOfTerms));

        return checked(TFloat.RootN(productOfTerms, countOfTerms));
      }

      /// <summary>
      /// <para>Creates a new sequence of non-zero numbers where each term after the first <paramref name="firstTerm"/> is found by multiplying the previous one by a fixed, non-zero number called the <paramref name="commonRatio"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Geometric_progression"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="firstTerm"></param>
      /// <param name="commonRatio"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Collections.Generic.IEnumerable<TNumber> GeometricSequence(TNumber a1, TNumber commonRatio)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(a1);
        System.ArgumentOutOfRangeException.ThrowIfZero(commonRatio);

        while (true)
        {
          yield return a1;

          try
          {
            checked { a1 *= commonRatio; }
          }
          catch { break; }
        }
      }

      /// <summary>
      /// <para>Gets the geometric series (sum) of a geometric sequence with infinite terms and the specified <paramref name="commonRatio"/>.</para>
      /// </summary>
      /// <param name="commonRatio">The common ratio of the geometric sequence.</param>
      /// <returns></returns>
      public static TNumber GeometricSeriesOfInfiniteTerms(TNumber a1, TNumber commonRatio)
        => commonRatio < TNumber.One
        ? a1 / (TNumber.One - commonRatio)
        : throw new System.ArithmeticException();

      #endregion

      #region IsConsideredPlural

      /// <summary>
      /// <para>Determines whether the number is considered plural in terms of writing.</para>
      /// <para></para>
      /// </summary>
      /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.), except <c>integer</c> types equal to 1, to be plural.</remarks>
      /// <returns></returns>
      public static bool IsConsideredPlural(TNumber value)
        => value != TNumber.One || !value.GetType().IsNumericsIBinaryInteger(); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.

      #endregion

      #region KroneckerDelta

      /// <summary>
      /// <para>The Kronecker delta is a function of two variables, usually just non-negative integers. The function is 1 if the variables are equal, and 0 otherwise.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Kronecker_delta"/></para>
      /// </summary>
      /// <param name="b"></param>
      /// <returns></returns>
      public static TNumber KroneckerDelta(TNumber value, TNumber other)
        => value == other ? TNumber.One : TNumber.Zero;

      #endregion

      #region Log..

      /// <summary>
      /// <para>Computes the logarithm of a value in the specified radix (base) and returns the largest integral LTE (less-than-or-equal) and the smallest integral GTE (greater-than-or-equal) to value.</para>
      /// <para>Uses the <see cref="System.Double"/> functionality.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix">Can be any </param>
      /// <returns>The (IntegralTowardZero, the "raw" LogR, and IntegralAwayFromZero) of the result.</returns>
      public static (TNumber IntegralTowardZero, TNumber IntegralAwayFromZero, TNumber NearestIntegral, double LogR) IntegerLog<TRadix>(TNumber value, TRadix radix)
        where TRadix : System.Numerics.INumber<TRadix>
      {
        var logr = double.Log(double.CreateChecked(value), double.CreateChecked(radix));

        var (tz, afz) = FloatingPoints.GetSurroundingIntegrals(logr);

        if (afz == tz) afz++;

        var ni = RoundToNearest(logr, HalfRounding.ToEven, false, [tz, afz]);

        return (TNumber.CreateChecked(tz), TNumber.CreateChecked(afz), TNumber.CreateChecked(ni), logr);
      }

      /// <summary>
      /// <para>Computes the base-10 logarithm of a value.</para>
      /// <para>Uses the <see cref="System.Double"/> functionality.</para>
      /// </summary>
      /// <returns>The (IntegralTowardZero, the "raw" Log10, and IntegralAwayFromZero) of the result.</returns>
      public static (TNumber IntegralTowardZero, TNumber IntegralAwayFromZero, TNumber NearestIntegral, double Log10) IntegerLog10(TNumber value)
      {
        var log10 = double.Log10(double.CreateChecked(value));

        var (tz, afz) = FloatingPoints.GetSurroundingIntegrals(log10);

        if (afz == tz) afz++;

        var ni = RoundToNearest(log10, HalfRounding.ToEven, false, [tz, afz]);

        return (TNumber.CreateChecked(tz), TNumber.CreateChecked(afz), TNumber.CreateChecked(ni), log10);
      }

      /// <summary>
      /// <para>Computes the natural (base-E) logarithm of a value.</para>
      /// <para>Uses the <see cref="System.Double"/> functionality.</para>
      /// </summary>
      /// <returns>The (IntegralTowardZero, the "raw" LogE, and IntegralAwayFromZero) of the result.</returns>
      public static (TNumber IntegralTowardZero, TNumber IntegralAwayFromZero, TNumber NearestIntegral, double LogE) IntegerLogE(TNumber value)
      {
        var log = double.Log(double.CreateChecked(value));

        var (tz, afz) = FloatingPoints.GetSurroundingIntegrals(log);

        var ni = RoundToNearest(log, HalfRounding.ToEven, false, [tz, afz]);

        return (TNumber.CreateChecked(tz), TNumber.CreateChecked(afz), TNumber.CreateChecked(ni), log);
      }

      #endregion

      #region Loops/iterations

      /// <summary>
      /// <para>Creates a sequence of numbers based on the standard for statement using <see cref="System.Func{T, TResult}"/> (for <paramref name="number"/>) and <see cref="System.Func{T1, T2, TResult}"/> (for <paramref name="condition"/> and <paramref name="iterator"/>).</para>
      /// </summary>
      /// <param name="startNumber">Initializes the current-loop-value.</param>
      /// <param name="condition">Conditionally allows/denies the loop to continue. In parameters are (current-loop-value, index). A false condition terminates the custom loop.</param>
      /// <param name="iterator">Advances the loop. In parameters (current-loop-value, index).</param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TNumber> LoopCustom(TNumber startNumber, System.Func<TNumber, int, bool> condition, System.Func<TNumber, int, TNumber> iterator)
      {
        for (var index = 0; ; index++)
          checked
          {
            if (!condition(startNumber, index)) break;

            startNumber = iterator(startNumber, index);

            yield return startNumber;
          }
      }

      /// <summary>
      /// <para>Loop toward or away-from and back-and-forth over <paramref name="direction"/>, in <paramref name="stepSize"/> for <paramref name="count"/> times.</para>
      /// <para>E.g. a direction = away-from, mean = 0, stepSize = -3 and count = 5, would yield the sequence [0, -3, 3, -6, 6].</para>
      /// <para>If the loop logic overflows/underflows for any reason, an exception occurs.</para>
      /// </summary>
      /// <typeparam name="TCount"></typeparam>
      /// <param name="meanNumber">The order of alternating numbers, either from mean to the outer limit, or from the outer limit to mean.</param>
      /// <param name="direction">This is the direction of looping in reference to number.</param>
      /// <param name="stepSize">The increasing (positive) and decreasing (negative) step size. Note, the min/max value of the loop inherits the same sign as step-size.</param>
      /// <param name="count">The number of numbers in the sequence.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Collections.Generic.IEnumerable<TNumber> LoopPivot<TCount>(TNumber meanNumber, CoordinateSystems.ReferenceRelativeOrientationTAf direction, TNumber stepSize, TCount count)
        where TCount : System.Numerics.IBinaryInteger<TCount>
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        checked
        {
          switch (direction)
          {
            case CoordinateSystems.ReferenceRelativeOrientationTAf.AwayFrom:
              if (TCount.IsOddInteger(count)) stepSize = -stepSize;

              for (var index = TCount.One; index <= count; index++)
              {
                yield return meanNumber;

                meanNumber += stepSize * TNumber.CreateChecked(index);
                stepSize = -stepSize;
              }
              break;
            case CoordinateSystems.ReferenceRelativeOrientationTAf.Toward:
              meanNumber = meanNumber + stepSize * Numbers.TruncMod(TNumber.CreateChecked(count), TNumber.One + TNumber.One).Quotient;  // Setup the inital outer edge value for inward iteration.

              for (var index = count - TCount.One; index >= TCount.Zero; index--)
              {
                yield return meanNumber;

                meanNumber -= stepSize * TNumber.CreateChecked(index);
                stepSize = -stepSize;
              }
              break;
            default:
              throw new System.ArgumentOutOfRangeException(nameof(direction));
          }
        }
      }

      /// <summary>
      /// <para>Creates a new sequence of <paramref name="count"/> numbers (or as many as possible) using <typeparamref name="TNumber"/> starting at <paramref name="number"/> and spaced by <paramref name="stepSize"/>.</para>
      /// <para>If the loop logic overflows/underflows for any reason, the enumeration is simply terminated, no exceptions are thrown.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TCount"></typeparam>
      /// <param name="number"></param>
      /// <param name="stepSize"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TNumber> LoopRange<TCount>(TNumber startNumber, TNumber stepSize, TCount count)
        where TCount : System.Numerics.IBinaryInteger<TCount>
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        var index = TCount.Zero;

        TNumber number;

        while (index < count)
          checked
          {
            number = startNumber + TNumber.CreateChecked(index) * stepSize;

            yield return number;

            index++;
          }
      }

      /// <summary>
      /// <para>Creates a new sequence with as many numbers as possible, starting at <paramref name="startNumber"/> and spaced by <paramref name="stepSize"/>.</para>
      /// <para>If the loop logic overflows/underflows for any reason, e.g. type limits, etc., the enumeration is simply terminated, no exceptions are thrown.</para>
      /// </summary>
      /// <remarks>Please note! If <typeparamref name="TNumber"/> is unlimited in nature (e.g. <see cref="System.Numerics.BigInteger"/>) enumeration is indefinite to the extent of system resources.</remarks>
      /// <param name="startNumber"></param>
      /// <param name="stepSize"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TNumber> LoopVerge(TNumber startNumber, TNumber stepSize)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);

        var index = TNumber.Zero;

        TNumber loopNumber;

        while (true)
          checked
          {
            try { loopNumber = startNumber + index * stepSize; } catch { break; }

            yield return loopNumber;

            try { index++; } catch { break; }
          }
      }

      #endregion

      #region Modulo operation (remainder)

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
      public static (TNumber Remainder, TNumber RemainderNoZero, TNumber ReverseRemainder, TNumber ReverseRemainderNoZero) Modulo(TNumber value, TNumber modulus)
      {
        var remainder = value % modulus;

        var copySign = TNumber.CopySign(modulus, value);

        if (TNumber.IsZero(remainder))
          return (remainder, copySign, remainder, copySign);

        var minusRemainder = copySign - remainder;

        return (remainder, remainder, minusRemainder, minusRemainder);
      }

      #endregion

      #region ..MultipleOf

      /// <summary>
      /// <para>Determines whether the <paramref name="value"/> is of a <paramref name="multiple"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="multiple">The multiple to which <paramref name="value"/> is measured.</param>
      /// <returns></returns>
      public static bool IsMultipleOf(TNumber value, TNumber multiple)
        => TNumber.IsZero(value % multiple);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="value"></param>
      /// <param name="multiple">The multiple to which <paramref name="value"/> is measured.</param>
      /// <param name="unequal"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      public static (TNumber TowardZero, TNumber Nearest, TNumber AwayFromZero) MultipleOf(TNumber value, TNumber multiple, bool unequal = false, HalfRounding mode = HalfRounding.TowardZero)
      {
        var csmv = TNumber.CopySign(multiple, value);

        var motz = value - (value % multiple);
        var moafz = motz;

        if (unequal && motz == value)
          motz -= csmv;

        if (unequal || moafz != value)
          moafz += csmv;

        return (motz, RoundToNearest(value, mode, false, [motz, moafz]), moafz);
      }

      #endregion

      #region Native..

      /// <summary>
      /// <para>Decrements a value. If integer, then by 1. If floating-point, then by "bit-decrement". If decimal, by 1e-28m.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <remarks>Infimum, for integer types equal (<paramref name="value"/> - 1) and for floating point types equal (<paramref name="value"/> - epsilon). Other types are not implemented at this time.</remarks>
      public static TNumber NativeDecrement(TNumber value)
      {
        if (value.GetType().IsNumericsIBinaryInteger()) // All integers are the same, and therefor subtract one.
          return checked(value - TNumber.One);
        else if (value is decimal dfp128) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(dfp128 - 1e-28m);
        else if (value is double bfp64) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(double.BitDecrement(bfp64));
        else if (value is float bfp32) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(float.BitDecrement(bfp32));
        else if (value is System.Half bfp16) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(System.Half.BitDecrement(bfp16));
        else if (value is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(nf));
        else
          throw new System.NotImplementedException();
      }

      /// <summary>
      /// <para>Increments a value. If integer, then by 1. If floating-point, then by "bit-increment". If decimal, by 1e-28m.</para>
      /// </summary>
      /// <remarks>Supremum, for integer types equal (<paramref name="value"/> + 1) and for floating point types equal (<paramref name="value"/> + epsilon). Other types are not implemented at this time.</remarks>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber NativeIncrement(TNumber value)
      {
        if (value.GetType().IsNumericsIBinaryInteger()) // All integers are the same, and therefor add one.
          return checked(value + TNumber.One);
        else if (value is decimal dfp128) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(dfp128 + 1e-28m);
        else if (value is double bfp64) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(double.BitIncrement(bfp64));
        else if (value is float bfp32) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(float.BitIncrement(bfp32));
        else if (value is System.Half bfp16) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(System.Half.BitIncrement(bfp16));
        else if (value is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(nf));
        else
          throw new System.NotImplementedException();
      }

      #endregion

      #region Pow

      /// <summary>
      /// <para>Computes a value raised to to a given <paramref name="exponent"/>.</para>
      /// <para>Uses the <see cref="System.Double"/> functionality.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TExponent"></typeparam>
      /// <param name="exponent"></param>
      /// <returns>The (IntegralTowardZero, the "raw" Pow, and IntegralAwayFromZero) of the result.</returns>
      public static (TNumber IntegralTowardZero, TNumber IntegralAwayFromZero, TNumber NearestIntegral, double Pow) Pow<TExponent>(TNumber value, TExponent exponent)
        where TExponent : System.Numerics.INumber<TExponent>
      {
        var pow = double.Pow(double.CreateChecked(value), double.CreateChecked(exponent));

        var (tz, afz) = FloatingPoints.GetSurroundingIntegrals(pow);

        var ni = RoundToNearest(pow, HalfRounding.ToEven, false, [tz, afz]);

        return (TNumber.CreateChecked(tz), TNumber.CreateChecked(afz), TNumber.CreateChecked(ni), pow);
      }

      #endregion

      /// <summary>
      /// <para>Proportionally rescale the <paramref name="value"/> from the closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to the closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>].</para>
      /// <para>The <paramref name="value"/> retains its proportional interval ratio, e.g. a 5 from the closed interval [0, 10] becomes 50 when rescaled to the closed interval [0, 100].</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="minSource"></param>
      /// <param name="maxSource"></param>
      /// <param name="minTarget"></param>
      /// <param name="maxTarget"></param>
      /// <returns></returns>
      public static TNumber Rescale(TNumber value, TNumber minSource, TNumber maxSource, TNumber minTarget, TNumber maxTarget)
        => minTarget + (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource);

      #region ..Root functions (Cube, Nth, Square)

      public static (TNumber IntegralTowardZero, TNumber IntegralAwayFromZero, TNumber NearestIntegral, double CubeRoot) Cbrt(TNumber value)
      {
        var cbrt = double.Cbrt(double.CreateChecked(value));

        var (tz, afz) = FloatingPoints.GetSurroundingIntegrals(cbrt);

        var ni = RoundToNearest(cbrt, HalfRounding.ToEven, false, [tz, afz]);

        return (TNumber.CreateChecked(tz), TNumber.CreateChecked(afz), TNumber.CreateChecked(ni), cbrt);
      }

      public static (TNumber IntegralTowardZero, TNumber IntegralAwayFromZero, TNumber NearestIntegral, double NthRoot) RootN<TNth>(TNumber value, TNth nth)
        where TNth : System.Numerics.IBinaryInteger<TNth>
      {
        var rootn = double.RootN(double.CreateChecked(value), int.CreateChecked(nth));

        var (tz, afz) = FloatingPoints.GetSurroundingIntegrals(rootn);

        var ni = RoundToNearest(rootn, HalfRounding.ToEven, false, [tz, afz]);

        return (TNumber.CreateChecked(tz), TNumber.CreateChecked(afz), TNumber.CreateChecked(ni), rootn);
      }

      public static (TNumber IntegralTowardZero, TNumber IntegralAwayFromZero, TNumber NearestIntegral, double SquareRoot) Sqrt(TNumber value)
      {
        var sqrt = double.Sqrt(double.CreateChecked(value));

        var (tz, afz) = FloatingPoints.GetSurroundingIntegrals(sqrt);

        var ni = RoundToNearest(sqrt, HalfRounding.ToEven, false, [tz, afz]);

        return (TNumber.CreateChecked(tz), TNumber.CreateChecked(afz), TNumber.CreateChecked(ni), sqrt);
      }

      #endregion

      #region RoundToNearest

      public static TNumber RoundToNearest(TNumber value, HalfRounding mode, bool proper, params System.ReadOnlySpan<TNumber> values)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(values.Length);

        var closestValues = new System.Collections.Generic.List<TNumber>() { values[0] };
        var closestDistance = TNumber.Abs(value - TNumber.CreateChecked(values[0]));

        for (var i = 1; i < values.Length; i++)
        {
          var currentValue = values[i];
          var currentDistance = TNumber.Abs(value - TNumber.CreateChecked(currentValue));

          if ((!proper || currentValue != value) && currentDistance <= closestDistance)
          {
            if (currentDistance < closestDistance)
            {
              closestValues.Clear();
              closestDistance = currentDistance;
            }

            if (!closestValues.Contains(currentValue))
              closestValues.Add(currentValue);
          }
        }

        return closestValues.Count == 0
          ? closestValues[0]
          : mode switch // If the distances are equal, i.e. the value is exactly halfway to all closestValues, we use the appropriate rounding strategy to resolve a winner.
          {
            HalfRounding.ToEven => closestValues.FirstOrValue(closestValues[0], TNumber.IsEvenInteger).Item,
            HalfRounding.AwayFromZero => closestValues.AsSpan().InfimumSupremum(value, v => v, false) is var (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue) && value >= TNumber.Zero ? (supremumIndex > -1 ? supremumValue : infimumValue) : (infimumIndex > -1 ? infimumValue : supremumValue),
            HalfRounding.TowardZero => closestValues.AsSpan().InfimumSupremum(value, v => v, false) is var (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue) && value >= TNumber.Zero ? (infimumIndex > -1 ? infimumValue : supremumValue) : (supremumIndex > -1 ? supremumValue : infimumValue),
            HalfRounding.ToNegativeInfinity => closestValues.Min() ?? throw new System.NullReferenceException(),
            HalfRounding.ToPositiveInfinity => closestValues.Max() ?? throw new System.NullReferenceException(),
            HalfRounding.ToOdd => closestValues.FirstOrValue(closestValues[0], TNumber.IsOddInteger).Item,
            HalfRounding.ToRandom => closestValues.AsSpan().GetRandomElement(),
            HalfRounding.ToAlternating => closestValues.AsSpan().GetAlternatingElement(),
            _ => throw new NotImplementedException(),
          };
      }

      #endregion

      #region Spread

      /// <summary>
      /// <para>Spreads an in-interval value around the outside edges of the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
      /// <para>This is the opposite of a <see cref="System.Numerics.INumber{TSelf}.Clamp(TSelf, TSelf, TSelf)"/> ( [<paramref name="minValue"/>, <paramref name="maxValue"/>] ) which is inclusive, making the Spread( either [NegativeInfinity, <paramref name="minValue"/>) or (<paramref name="maxValue"/>, PositiveInfinity] ), i.e. exclusive of <paramref name="minValue"/> and <paramref name="maxValue"/>.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="mode"></param>
      /// <param name="margin"></param>
      /// <returns></returns>
      public static TNumber Spread(TNumber value, TNumber minValue, TNumber maxValue, HalfRounding mode, TNumber margin)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(margin);

        if (value < minValue || value > maxValue)
          return value; // If number is already spread, nothing to do but return it.

        var nearestValue = RoundToNearest(value, mode, false, [minValue, maxValue]);

        return (nearestValue == minValue)
          ? minValue - margin
          : (nearestValue == maxValue)
          ? maxValue + margin
          : nearestValue;
      }

      /// <summary>
      /// <para>Spreads an in-interval value around the outside edges of the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>].</para>
      /// <para>This is the opposite of a <see cref="System.Numerics.INumber{TSelf}.Clamp(TSelf, TSelf, TSelf)"/> ( [<paramref name="minValue"/>, <paramref name="maxValue"/>] ) which is inclusive, making the Spread( either [NegativeInfinity, <paramref name="minValue"/>) or (<paramref name="maxValue"/>, PositiveInfinity] ), i.e. exclusive of <paramref name="minValue"/> and <paramref name="maxValue"/>.</para>
      /// <para>A native function means that the difference will be based on the smallest native difference. For integers it is 1, and for floating point (double and float) it is the bit increment/decrement.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      public static TNumber SpreadNative(TNumber value, TNumber minValue, TNumber maxValue, HalfRounding mode)
      {
        if (value < minValue || value > maxValue)
          return value; // If number is already spread, nothing to do but return it.

        var nearestValue = RoundToNearest(value, mode, false, [minValue, maxValue]);

        return (nearestValue == minValue)
          ? NativeDecrement(minValue)
          : (nearestValue == maxValue)
          ? NativeIncrement(maxValue)
          : nearestValue;
      }

      #endregion

      #region TruncMod

      /// <summary>
      /// <para>Computes the integer (i.e. the truncated or floor) quotient and remainder of (<paramref name="dividend"/> / <paramref name="divisor"/>).</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="dividend"></param>
      /// <param name="divisor"></param>
      /// <returns>Returns the integer (i.e. the truncated or floor) quotient and remainder.</returns>
      public static (TNumber Quotient, TNumber Remainder) TruncMod(TNumber dividend, TNumber divisor)
      {
        var remainder = dividend % divisor;

        return ((dividend - remainder) / divisor, remainder);
      }

      #endregion

      #region UnitSign

      /// <summary>
      /// <para>The unit sign step function, i.e. zero is treated as a positive unit value of one.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Step_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Sign_function"/></para>
      /// </summary>
      /// <remarks>LT 0 (negative) = -1, GTE 0 (not negative) = +1.</remarks>
      /// <returns></returns>
      public static TNumber UnitSign(TNumber value)
        => TNumber.CopySign(TNumber.One, value);

      #endregion

      #region WrapAround

      /// <summary>
      /// <para>Wraps a value around, to the opposite side, if the value exceeds either boundary.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public static TNumber WrapAround(TNumber value, TNumber minValue, TNumber maxValue)
        => (value > maxValue)
        ? minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One))
        : (value < minValue)
        ? maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One))
        : value;

      #endregion
    }

    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
    {
      #region Geometric progression

      /// <summary>
      /// <para>Get the <paramref name="nth"/> term of a geometric sequence with the specified <paramref name="commonRatio"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="commonRatio"></param>
      /// <param name="nth"></param>
      /// <returns></returns>
      public static TNumber GeometricSequenceNthTerm<TInteger>(TNumber a1, TNumber commonRatio, TInteger nth)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => a1 * TNumber.Pow(commonRatio, TNumber.CreateChecked(nth - TInteger.One));

      /// <summary>
      /// <para>Gets the geometric series (sum) of a geometric sequence with <paramref name="nth"/> terms and the specified <paramref name="commonRatio"/>.</para>
      /// </summary>
      /// <param name="commonRatio">The common ratio of the geometric sequence.</param>
      /// <param name="nth">The term of which to find the sum up until.</param>
      /// <returns></returns>
      public static TNumber GeometricSeriesOfNTerms<TInteger>(TNumber a1, TNumber commonRatio, TInteger nth)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => a1 * (TNumber.One - TNumber.Pow(commonRatio, TNumber.CreateChecked(nth))) / (TNumber.One - commonRatio);

      #endregion
    }

    extension<TNumber>(TNumber value)
    where TNumber : System.Numerics.INumber<TNumber>
    {
      #region ToEngineeringNotationString

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="stringBuilder"></param>
      /// <param name="unit"></param>
      /// <param name="format"></param>
      /// <param name="formatProvider"></param>
      /// <param name="restrictToTriplets"></param>
      /// <returns></returns>
      public string ToEngineeringNotationString(System.Text.StringBuilder? stringBuilder = null, string? unit = null, string? format = null, System.IFormatProvider? formatProvider = null, bool restrictToTriplets = true)
      {
        stringBuilder ??= new System.Text.StringBuilder();

        if (!string.IsNullOrWhiteSpace(unit))
          stringBuilder.Insert(0, unit);

        var engineeringNotationPrefix = Units.MetricPrefix.Unprefixed;

        var engineeringNotationValue = decimal.CreateChecked(value);

        if (engineeringNotationValue != 0)
          checked
          {
            engineeringNotationPrefix = double.Log10(double.Abs(double.CreateChecked(engineeringNotationValue))) is var log10 && restrictToTriplets
              ? (Units.MetricPrefix)int.CreateChecked(double.Floor(log10 / 3) * 3)
              : System.Enum.GetValues<Units.MetricPrefix>().InfimumSupremum(mp => (int)mp, int.CreateChecked(double.Floor(log10)), true).InfimumItem;

            engineeringNotationValue = engineeringNotationValue * (decimal)double.Pow(10, -(int)engineeringNotationPrefix);
          }

        var symbol = engineeringNotationPrefix.GetMetricPrefixSymbol(false);

        if (!string.IsNullOrWhiteSpace(symbol))
          stringBuilder.Insert(0, symbol);

        if (stringBuilder.Length > 0)
          stringBuilder.Insert(0, ' ');

        stringBuilder.Insert(0, TNumber.CreateChecked(engineeringNotationValue).ToString(format, formatProvider));

        return stringBuilder.ToString();
      }

      #endregion
    }

    #region Gaps in sequence

    public static System.Collections.Generic.IEnumerable<TNumber> GetGapsInSequence<TNumber>(this System.Collections.Generic.IEnumerable<TNumber> source, bool includeLastFirstGap)
      where TNumber : System.Numerics.INumber<TNumber>
      => source.PartitionTuple2(includeLastFirstGap, (leading, trailing, index) => trailing - leading);

    #endregion

    #region MeanMedianAbsoluteDeviation

    /// <summary>
    /// <para>The mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</para>
    /// <para>In this function, both M(AD)ean, M(AD)edian and the regular mode are computed.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Average_absolute_deviation"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static (double MeanAbsoluteDeviation, double MedianAbsoluteDeviation) MeanMedianAbsoluteDeviation<TNumber>(this System.Collections.Generic.IList<TNumber> values)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var mmo = new Statistics.OnlineMeanMedianMode<TNumber>(values);

      var madMean = 0d;
      var madMedian = 0d;

      foreach (var value in values.Select(v => double.CreateChecked(v)))
      {
        madMean += double.Abs(value - mmo.Mean);
        madMedian += double.Abs(value - mmo.Median);
      }

      madMean /= mmo.Count;
      madMedian /= mmo.Count;

      return (madMean, madMedian);
    }

    #endregion

    //#region PowOf

    ///// <summary>
    ///// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <typeparam name="TRadix"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="radix"></param>
    ///// <param name="unequal"></param>
    ///// <returns></returns>
    //public static TNumber PowOfAwayFromZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //  => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
    //  ? value
    //  : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(radix, out var _).IlogTz, out var _).IpowTz is var p && (p == v ? p : p * r) is var afz && (unequal || !TNumber.IsInteger(value)) && afz == v ? afz * r : afz, value);

    ///// <summary>
    ///// <para>Rounds the <paramref name="value"/> to the closest power-of-<paramref name="radix"/> (i.e. of <paramref name="powOfTowardsZero"/> and <paramref name="powOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="value"/> resilient.</para>
    ///// </summary>
    ///// <param name="value">The value for which the nearest power-of-<paramref name="radix"/> of will be found.</param>
    ///// <param name="radix">The radix (base) of the power-of-<paramref name="radix"/>.</param>
    ///// <param name="unequal">Proper means nearest but <paramref name="unequal"/> to <paramref name="value"/> if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-radix will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <param name="powOfTowardsZero">Outputs the power-of-<paramref name="radix"/> of that is closer to zero.</param>
    ///// <param name="powOfAwayFromZero">Outputs the power-of-<paramref name="radix"/> of that is farther from zero.</param>
    ///// <returns>The nearest two power-of-<paramref name="radix"/> as out parameters and the the nearest of those two is returned.</returns>
    //public static TNumber PowOf<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal, HalfRounding mode, out TNumber powOfTowardsZero, out TNumber powOfAwayFromZero)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //{
    //  powOfTowardsZero = value.PowOfTowardZero(radix, unequal);
    //  powOfAwayFromZero = value.PowOfAwayFromZero(radix, unequal);

    //  return value.RoundToNearest(mode, powOfTowardsZero, powOfAwayFromZero);
    //}

    ///// <summary>
    ///// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <typeparam name="TRadix"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="radix"></param>
    ///// <param name="unequal"></param>
    ///// <returns></returns>
    //public static TNumber PowOfTowardZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //  => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
    //  ? value
    //  : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(radix, out var _).IlogTz, out var _).IpowTz is var p && (unequal && TNumber.IsInteger(value)) && p == v ? p / r : p, value);

    //#endregion
  }
}
