namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Sign step function that guarantees [-1, 0, 1] for output (not just less-than-zero and greater-than-zero).</para>
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
      #region ArithmeticMean (type of average)

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Arithmetic_mean"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="sumOfTerms"></param>
      /// <param name="terms"></param>
      /// <returns></returns>
      public static TFloat ArithmeticMean<TFloat>(out TFloat sumOfTerms, params System.Collections.Generic.IEnumerable<TNumber> terms)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        sumOfTerms = TFloat.CreateChecked(terms.Sum(out var countOfTerms));

        return sumOfTerms / TFloat.CreateChecked(countOfTerms);
      }

      #endregion

      #region ArithmeticSequence.. (progression)

      /// <summary>
      /// <para>Creates a new sequence of non-zero numbers where each term after the first <paramref name="a1"/> is found by multiplying the previous one by a fixed, non-zero number called the <paramref name="commonDifference"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Arithmetic_progression"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the arithmetic sequence.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Collections.Generic.IEnumerable<TNumber> ArithmeticSequence(TNumber a1, TNumber commonDifference)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(commonDifference);

        for (var n = 0; true; n++) // We can start at zero..
          yield return checked(a1 + TNumber.CreateChecked(n) * commonDifference); // ..and get away with NOT subtracting one from n: (a + n * d)
      }

      /// <summary>
      /// <para>Get the <paramref name="n"/> term of a geometric sequence with the specified <paramref name="commonRatio"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the arithmetic sequence.</param>
      /// <param name="n">The term to retrieve.</param>
      /// <returns></returns>
      public static TNumber ArithmeticSequenceNthTerm<TInteger>(TNumber a1, TNumber commonDifference, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(commonDifference);

        return a1 + TNumber.CreateChecked(n - TInteger.One) * commonDifference; // (a + (n - 1) * d)
      }

      #endregion

      #region ArithmeticSeries.. (sum)

      /// <summary>
      /// <para>Gets the mean of the arithmetic series of an arithmetic sequence with the specified <paramref name="a1"/>, <paramref name="commonDifference"/> and <paramref name="n"/> terms.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Arithmetic_progression#Sum"/></para>
      /// </summary>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the arithmetic sequence.</param>
      /// <param name="n">The number of terms.</param>
      /// <returns></returns>
      public static TNumber ArithmeticSeriesMeanOfNTerms<TInteger>(TNumber a1, TNumber commonDifference, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => (a1 + ArithmeticSequenceNthTerm(a1, commonDifference, n)) / TNumber.CreateChecked(2);

      ///// <summary>
      ///// <para>Gets the arithmetic series (sum) of a arithmetic sequence with infinite terms and the specified <paramref name="d"/>.</para>
      ///// </summary>
      ///// <param name="d">The common ratio of the arithmetic sequence.</param>
      ///// <returns></returns>
      //public static TNumber ArithmeticSeriesOfInfiniteTerms<TNumber>(this TNumber a, TNumber d)
      //  where TNumber : System.Numerics.INumber<TNumber>
      //  => ()

      /// <summary>
      /// <para>Gets the arithmetic series of an arithmetic sequence with the specified <paramref name="a1"/>, <paramref name="commonDifference"/> and <paramref name="n"/> terms.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Arithmetic_progression#Sum"/></para>
      /// </summary>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the arithmetic sequence.</param>
      /// <param name="n">The number of terms.</param>
      /// <returns></returns>
      public static TNumber ArithmeticSeriesOfNTerms<TInteger>(TNumber a1, TNumber commonDifference, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => TNumber.CreateChecked(n) * (a1 + ArithmeticSequenceNthTerm(a1, commonDifference, n)) / TNumber.CreateChecked(2);

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
        => TNumber.Abs(position - value) <= proximity // Inquire whether the difference is within to specified proximity.
        ? position // If so, detent to the position.
        : value; // Otherwise leave it where it is.

      #endregion

      #region EqualsWithin..

      /// <summary>
      /// <para>Perform an absolute equality test.</para>
      /// <para>Absolute equality checks if the absolute difference between <paramref name="value"/> and <paramref name="other"/> is smaller than a predefined <paramref name="absoluteTolerance"/>. This is useful when you want to ensure the numbers are "close enough" without considering their scale.</para>
      /// <para>Absolute equality is simpler and works well for small numbers or fixed tolerances.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="absoluteTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public static bool EqualsWithinAbsoluteTolerance(TNumber value, TNumber other, TNumber absoluteTolerance)
      {
        if (value == other)
          return true;

        if (TNumber.IsNaN(value) || TNumber.IsNaN(other))
          return false;

        if (TNumber.IsInfinity(value) || TNumber.IsInfinity(other))
          return value == other;

        return TNumber.Abs(value - other) <= absoluteTolerance;
      }

      /// <summary>
      /// <para>Perform a relative equality test.</para>
      /// <para>Relative equality considers the scale of the numbers by dividing the absolute difference by the magnitude of the numbers. This is useful when comparing numbers that may vary significantly in scale.</para>
      /// <para>Relative equality is better for larger numbers or numbers with varying scales, as it adjusts the tolerance dynamically.</para>
      /// </summary>
      /// <typeparam name="TRelativeTolerance"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="relativeTolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public static bool EqualsWithinRelativeTolerance<TRelativeTolerance>(TNumber value, TNumber other, TRelativeTolerance relativeTolerance)
        where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
      {
        if (value == other)
          return true;

        if (TNumber.IsNaN(value) || TNumber.IsNaN(other))
          return false;

        if (TNumber.IsInfinity(value) || TNumber.IsInfinity(other))
          return value == other;

        return TRelativeTolerance.CreateChecked(TNumber.Abs(value - other)) <= relativeTolerance * TRelativeTolerance.CreateChecked(TNumber.Abs(TNumber.MaxMagnitude(value, other)));
      }

      /// <summary>
      /// <para>Perform an equality test involving the most (integer part) or the least (fraction part) <typeparamref name="TSignificantDigits"/> using the specified <paramref name="radix"/>.</para>
      /// <para>Negative means most <paramref name="significantDigits"/> tolerance on the fraction part.</para>
      /// <para>Positive means least <paramref name="significantDigits"/> tolerance on the integer part.</para>
      /// <para><see href="https://stackoverflow.com/questions/9180385/is-this-value-valid-float-comparison-that-accounts-for-value-set-number-of-decimal-place"/></para>
      /// </summary>
      /// <typeparam name="TSignificantDigits"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="other"></param>
      /// <param name="significantDigits">The tolerance, as the number of significant digits, considered for equality. A negative value for most significant digits on the right side (fraction part). A positive value for least significant digits on the left side (integer part).</param>
      /// <param name="radix">&gt; 2</param>
      /// <remarks>
      /// <para><see langword="true"/> = EqualsWithinSignificantDigits(1000.02, 1000.015, -2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 0.01 for radix 10.</para>
      /// <para><see langword="true"/> = EqualsWithinSignificantDigits(1334.261, 1235.272, 2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to negative <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 100 for radix 10.</para>
      /// </remarks>
      /// <returns></returns>
      public static bool EqualsWithinSignificantDigits<TSignificantDigits, TRadix>(TNumber value, TNumber other, TSignificantDigits significantDigits, TRadix radix)
        where TSignificantDigits : System.Numerics.IBinaryInteger<TSignificantDigits>
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value == other // The value and other are equal.
        || (double.CreateChecked(TNumber.Abs(value - other)) <= double.Pow(double.CreateChecked(Units.Radix.AssertMember(radix)), double.CreateChecked(significantDigits))); // The difference is LTE to the signed significant digits raised-to-the-power-of radix.

      #endregion

      ///// <summary>
      ///// Finds the exact index of target in arr if present.
      ///// If not present, returns the interpolated fractional index.
      ///// </summary>
      ///// <param name="sorted">Sorted numeric array</param>
      ///// <param name="target">Number to search for</param>
      ///// <returns>Exact index as double, or fractional interpolated position</returns>
      //public static TFloat FindIndexOrInterpolated<TFloat>(System.Span<TNumber> sorted, TFloat target)
      //  where TFloat : System.Numerics.IFloatingPoint<TFloat>
      //{
      //  if (sorted.Length == 0) throw new System.ArgumentException("Array cannot be null or empty.");

      //  // Ensure array is sorted
      //  for (int i = 1; i < sorted.Length; i++)
      //  {
      //    if (sorted[i] < sorted[i - 1])
      //      throw new ArgumentException("Array must be sorted in ascending order.");
      //  }

      //  // Exact match search
      //  for (int i = 0; i < sorted.Length; i++)
      //  {
      //    if (TFloat.CreateChecked(sorted[i]) == target)
      //      return TFloat.CreateChecked(i); // Exact index
      //  }

      //  // Binary search for insertion point
      //  int left = 0, right = sorted.Length;
      //  while (left < right)
      //  {
      //    int mid = (left + right) / 2;
      //    if (TFloat.CreateChecked(sorted[mid]) < target)
      //      left = mid + 1;
      //    else
      //      right = mid;
      //  }

      //  // If before first element
      //  if (left == 0)
      //    return TFloat.Zero;

      //  // If after last element
      //  if (left == sorted.Length)
      //    return TFloat.CreateChecked(sorted.Length);

      //  // Interpolate fractional index
      //  var leftVal = TFloat.CreateChecked(sorted[left - 1]);
      //  var rightVal = TFloat.CreateChecked(sorted[left]);
      //  if (leftVal == rightVal)
      //    return TFloat.CreateChecked(left - 1); // Avoid division by zero

      //  var fraction = (target - leftVal) / (rightVal - leftVal);
      //  return TFloat.CreateChecked(left - 1) + fraction;
      //}

      #region FoldAcross

      /// <summary>
      /// <para>Folds an out-of-bound <paramref name="value"/> (back and forth) across the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public static TNumber FoldAcross(TNumber value, TNumber minValue, TNumber maxValue)
        => (value > maxValue)
        ? (ITruncatedDivRem(value - maxValue, maxValue - minValue) is var (tqHi, remHi) && TNumber.IsEvenInteger(tqHi) ? maxValue - remHi : minValue + remHi)
        : (value < minValue)
        ? (ITruncatedDivRem(minValue - value, maxValue - minValue) is var (tqLo, remLo) && TNumber.IsEvenInteger(tqLo) ? minValue + remLo : maxValue - remLo)
        : value;

      #endregion

      #region GeometricMean (type of average)

      /// <summary>
      /// <para>The geometric mean is a mean or average which indicates a central tendency of a finite collection of positive real numbers by using the product of their values (as opposed to the arithmetic mean, which uses their sum).</para>
      /// <para>Each term in a geometric series is the geometric mean of the term before it and the term after it, in the same way that each term of an arithmetic series is the arithmetic mean of its neighbors.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Geometric_mean"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="productOfTerms">The product of out parameter</param>
      /// <param name="terms"></param>
      /// <returns></returns>

      public static TFloat GeometricMean<TFloat>(out TFloat productOfTerms, params System.Collections.Generic.IEnumerable<TNumber> terms)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IRootFunctions<TFloat>
      {
        productOfTerms = TFloat.CreateChecked(terms.Product(out var countOfTerms));

        return checked(TFloat.RootN(productOfTerms, countOfTerms));
      }

      #endregion

      #region GeometricSequence (progression)

      /// <summary>
      /// <para>Creates a new sequence of non-zero numbers where each term after the first <paramref name="a1"/> is found by multiplying the previous one by a fixed, non-zero number called the <paramref name="commonRatio"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Geometric_progression"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <param name="a1"></param>
      /// <param name="commonRatio"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TNumber> GeometricSequence(TNumber a1, TNumber commonRatio)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(a1);
        System.ArgumentOutOfRangeException.ThrowIfZero(commonRatio);

        while (true)
        {
          yield return a1;

          try { checked { a1 *= commonRatio; } } catch { break; }
        }
      }

      #endregion

      #region GeometricSeries.. (sum)

      /// <summary>
      /// <para></para>
      /// <para>Gets the geometric series of a geometric sequence with infinite terms after the first <paramref name="a1"/> with the specified <paramref name="commonRatio"/>. The sum of a geometric progression's terms is called a geometric series.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Geometric_series"/></para>
      /// </summary>
      /// <param name="commonRatio">The common ratio of the geometric sequence.</param>
      /// <returns></returns>
      public static TFloat GeometricSeriesOfInfiniteTerms<TFloat>(TNumber a1, TFloat commonRatio)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        if (commonRatio >= TFloat.One)
          throw new System.ArithmeticException("The geometric series is divergent.");

        return TFloat.CreateChecked(a1) / (TFloat.One - commonRatio);
      }

      #endregion

      #region IsConsideredPlural

      /// <summary>
      /// <para>Determines whether the number is considered plural in terms of writing.</para>
      /// <para></para>
      /// </summary>
      /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.), except <c>integer</c> types equal to 1, to be plural.</remarks>
      /// <returns></returns>
      public static bool IsConsideredPlural(TNumber value)
        => !(value == TNumber.One && value.GetType().IsIBinaryInteger()); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.

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
        {
          try { checked { if (!condition(startNumber, index)) break; } } catch { yield break; }

          yield return startNumber;

          try { checked { startNumber = iterator(startNumber, index); } } catch { yield break; }
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
              meanNumber = meanNumber + stepSize * ITruncatedDivRem(TNumber.CreateChecked(count), TNumber.One + TNumber.One).Quotient;  // Setup the inital outer edge value for inward iteration.

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
      public static (TNumber MultipleTowardZero, TNumber NearestMultiple, TNumber MultipleAwayFromZero) MultipleOf(TNumber value, TNumber multiple, bool unequal = false, HalfRounding mode = HalfRounding.TowardZero)
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
      /// <para>Decrements a number. If integer, then by +1. If floating-point, then by "bit-decrement". If decimal, by 1e-28m.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber NativeDecrement(TNumber value)
        => value.GetType().IsIBinaryInteger()
        ? checked(value - TNumber.One) // Binary integers are fundamentally the same, so simply subtract one.
        : value switch // Floating point types have structures depending on specific operations to decrement.
        {
          decimal dfp128 => TNumber.CreateChecked(decimal.NativeDecrement(dfp128)),
          double bfp64 => TNumber.CreateChecked(double.NativeDecrement(bfp64)),
          float bfp32 => TNumber.CreateChecked(float.NativeDecrement(bfp32)),
          System.Half bfp16 => TNumber.CreateChecked(System.Half.NativeDecrement(bfp16)),
          System.Runtime.InteropServices.NFloat nf => TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.NativeDecrement(nf)),
          _ => throw new System.NotImplementedException()
        };

      /// <summary>
      /// <para>Increments a number. If integer, then by -1, otherwise by native-increment.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber NativeIncrement(TNumber value)
        => value.GetType().IsIBinaryInteger()
        ? checked(value + TNumber.One) // Binary integers are fundamentally the same, so simply add one.
        : value switch // Floating point types have structures depending on specific operations to increment.
        {
          decimal dfp128 => TNumber.CreateChecked(decimal.NativeIncrement(dfp128)),
          double bfp64 => TNumber.CreateChecked(double.NativeIncrement(bfp64)),
          float bfp32 => TNumber.CreateChecked(float.NativeIncrement(bfp32)),
          System.Half bfp16 => TNumber.CreateChecked(System.Half.NativeIncrement(bfp16)),
          System.Runtime.InteropServices.NFloat nf => TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.NativeIncrement(nf)),
          _ => throw new System.NotImplementedException()
        };

      #endregion

      #region Quantiles

      /// <summary>
      /// <para>Computes by linear interpolation of the EDF.</para>
      /// </summary>
      /// <param name="ordered"></param>
      /// <param name="h"></param>
      /// <returns>An estimated value.</returns>
      /// <exception cref="System.ArgumentNullException"/>
      private static TPercent QuantileEdfLerp<TPercent>(System.Span<TNumber> ordered, TPercent h)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        var fh = TPercent.Floor(h); // Floor of h.
        var ch = TPercent.Ceiling(h); // Ceiling of h.

        var fhi = System.Convert.ToInt32(fh);
        var chi = System.Convert.ToInt32(ch);

        var maxIndex = ordered.Length - 1;

        // Ensure roundings are clamped to quantile rank [0, maxIndex] range (variable 'h' on Wikipedia). There are no adjustments for 0-based indexing.
        fhi = int.Clamp(fhi, 0, maxIndex);
        chi = int.Clamp(chi, 0, maxIndex);

        var fv = ordered[fhi]; // Value at fhi.
        var cv = ordered[chi]; // Value at chi.

        return TPercent.CreateChecked(fv) + (h - fh) * TPercent.CreateChecked(cv - fv); // Linear interpolation between floor and ceiling using difference between h and fh (making it [0-1]).
      }

      /// <summary>
      /// <para>An empirical distribution function (commonly also called an empirical Cumulative Distribution Function, eCDF) is the distribution function associated with the empirical measure of a sample.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Empirical_distribution_function"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="ordered"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileEdf<TPercent>(System.Span<TNumber> ordered, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = p * TPercent.CreateChecked(ordered.Length + 1);

        return QuantileEdfLerp(ordered, h - TPercent.One);
      }

      /// <summary>
      /// <para>Inverse of empirical distribution function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR1<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = TPercent.CreateChecked(numbers.Length) * p;

        var index = System.Convert.ToInt32(TPercent.Ceiling(h));

        index = int.Clamp(index, 1, numbers.Length) - 1; // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.

        return TPercent.CreateChecked(numbers[index]);
      }

      /// <summary>
      /// <para>The same as R1, but with averaging at discontinuities.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR2<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = TPercent.CreateChecked(numbers.Length) * p + TPercent.CreateChecked(0.5);

        var half = TPercent.CreateChecked(0.5);

        var chi = System.Convert.ToInt32(TPercent.Ceiling(h - half)); // ceiling(h - 0.5).
        var fhi = System.Convert.ToInt32(TPercent.Floor(h + half)); // floor(h + 0.5).

        // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.
        chi = int.Clamp(chi, 1, numbers.Length) - 1;
        fhi = int.Clamp(fhi, 1, numbers.Length) - 1;

        return TPercent.CreateChecked(numbers[chi] + numbers[fhi]) / TPercent.CreateChecked(2);
      }

      /// <summary>
      /// <para>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR3<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = TPercent.CreateChecked(numbers.Length) * p - TPercent.CreateChecked(0.5);

        var index = System.Convert.ToInt32(TPercent.Round(h, System.MidpointRounding.ToEven)); // Round h to the nearest integer, choosing the even integer in the case of a tie.

        index = int.Clamp(index, 0, numbers.Length - 1); // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.

        return TPercent.CreateChecked(numbers[index]);
      }

      /// <summary>
      /// <para>Linear interpolation of the empirical distribution function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR4<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = TPercent.CreateChecked(numbers.Length) * p;

        return QuantileEdfLerp(numbers, h - TPercent.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR5<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = TPercent.CreateChecked(numbers.Length) * p + TPercent.CreateChecked(0.5);

        return QuantileEdfLerp(numbers, h - TPercent.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</para>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <remarks>
      /// <list type="bullet">
      /// <item><see href="https://en.wikipedia.org/wiki/Percentile#Third_variant,_C_=_0">Percentile, Third variant C = 0</see> - Microsoft Excel PERCENTILE.EXC function - Python's default "exclusive" method - Primary variant recommended by NIST.</item>
      /// <item><see href="https://en.wikipedia.org/wiki/Quartile#Method_4">Quartile, Method 4</see> - Microsoft Excel QUARTILE.EXC function</item>
      /// </list>
      /// </remarks>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR6<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = TPercent.CreateChecked(numbers.Length + 1) * p;

        return QuantileEdfLerp(numbers, h - TPercent.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Equivalent to</para>
      /// <list type="bullet">
      /// <item><see href="https://en.wikipedia.org/wiki/Percentile#Second_variant,_C_=_1">Percentile - Second variant, C = 1</see> - Microsoft Excel PERCENTILE.INC function - Python's optional "inclusive" method - Noted as an alternative by NIST</item>
      /// <item><see href="https://en.wikipedia.org/wiki/Quartile#Method_3">Quartile Method 3</see> - Microsoft Excel	QUARTILE.INC function</item>
      /// </list>
      /// </remarks>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR7<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = TPercent.CreateChecked(numbers.Length - 1) * p + TPercent.One;

        return QuantileEdfLerp(numbers, h - TPercent.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Linear interpolation of the approximate medians for order statistics.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR8<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = (TPercent.CreateChecked(numbers.Length) + TPercent.CreateChecked(1d / 3d)) * p + TPercent.CreateChecked(1d / 3d);

        return QuantileEdfLerp(numbers, h - TPercent.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="p"></param>
      /// <returns></returns>
      public static TPercent QuantileR9<TPercent>(System.Span<TNumber> numbers, TPercent p)
        where TPercent : System.Numerics.IFloatingPoint<TPercent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(p);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(p, TPercent.One);

        var h = (TPercent.CreateChecked(numbers.Length) + TPercent.CreateChecked(1d / 4d)) * p + TPercent.CreateChecked(3d / 8d);

        return QuantileEdfLerp(numbers, h - TPercent.One); // Adjust for 0-based indexing.
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
      public static (TNumber Quotient, TNumber Remainder) ITruncatedDivRem(TNumber dividend, TNumber divisor)
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
      public string ToEngineeringNotationString(System.Text.StringBuilder? stringBuilder = null, string? unit = null, string? format = null, System.IFormatProvider? formatProvider = null, bool restrictToTriplets = true, UnicodeSpacing spacing = UnicodeSpacing.Space)
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
          stringBuilder.Insert(0, spacing.ToSpacingString());

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
