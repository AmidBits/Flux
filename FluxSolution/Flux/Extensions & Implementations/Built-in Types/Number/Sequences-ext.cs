namespace Flux
{
  public static partial class Number
  {
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
      public static TFloat ArithmeticMean<TFloat>(out int countOfTerms, out TFloat sumOfTerms, params System.Collections.Generic.IEnumerable<TNumber> terms)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        sumOfTerms = TFloat.CreateChecked(terms.Sum(out countOfTerms));

        return sumOfTerms / TFloat.CreateChecked(countOfTerms);
      }

      #endregion

      #region ArithmeticSequence (progression)

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

      #endregion

      #region ArithmeticSequenceNthTerm

      /// <summary>
      /// <para>Get the <paramref name="n"/> term of a arithmetic sequence with the specified <paramref name="a1"/> and <paramref name="commonDifference"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Arithmetic_progression"/></para>
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

      #region ArithmeticSeriesMeanOfNTerms

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

      #endregion

      #region ArithmeticSeriesOfNTerms

      /// <summary>
      /// <para>Gets the arithmetic series (sum) of an arithmetic sequence with the specified <paramref name="a1"/>, <paramref name="commonDifference"/> and <paramref name="n"/> terms.</para>
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
      public static TFloat GeometricMean<TFloat>(out int countOfTerms, out TFloat productOfTerms, params System.Collections.Generic.IEnumerable<TNumber> terms)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IRootFunctions<TFloat>
      {
        productOfTerms = TFloat.CreateChecked(terms.Product(out countOfTerms));

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

      #region GeometricSeriesOfInfiniteTerms

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

      #region Loop functions

      public static System.Collections.Generic.IEnumerable<(int Index, TNumber Value, TNumber OpposingValue)> LoopCross(TNumber startValue, TNumber step, int count)
      {
        var minValue = ArithmeticSequenceNthTerm(startValue, step, 1);
        var maxValue = ArithmeticSequenceNthTerm(startValue, step, count);

        return ArithmeticSequence(minValue, step).Take(count).Select((n, i) => (i, n, minValue + maxValue - n));
      }

      /// <summary>
      /// <para>Creates a sequence of numbers that are controlled through three methods: <paramref name="initialization"/>(), <paramref name="condition"/>() and <paramref name="updation"/>().</para>
      /// </summary>
      /// <param name="initialization">Initializes a current-loop-value.</param>
      /// <param name="condition">Conditionally allows/denies the loop to continue. In parameters are (current-loop-value, index). A false condition terminates the custom loop.</param>
      /// <param name="updation">Advances the loop. In parameters (current-loop-value, index).</param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TNumber Value, int Index)> LoopCustom(System.Func<TNumber> initialization, System.Func<TNumber, int, bool> condition, System.Func<TNumber, int, TNumber> updation)
      {
        TNumber value;

        try { value = initialization(); } catch { yield break; }

        for (var index = 0; ; index++)
        {
          try { checked { if (!condition(value, index)) break; } } catch { yield break; }

          yield return (value, index);

          try { checked { value = updation(value, index); } } catch { yield break; }
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
      public static System.Collections.Generic.IEnumerable<TNumber> LoopPivot(TNumber meanNumber, CoordinateSystems.ReferenceRelativeOrientationTAf direction, TNumber stepSize, int count)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        checked
        {
          switch (direction)
          {
            case CoordinateSystems.ReferenceRelativeOrientationTAf.AwayFrom:
              if (int.IsOddInteger(count)) stepSize = -stepSize;

              for (var index = 1; index <= count; index++)
              {
                yield return meanNumber;

                meanNumber += stepSize * TNumber.CreateChecked(index);
                stepSize = -stepSize;
              }
              break;
            case CoordinateSystems.ReferenceRelativeOrientationTAf.Toward:
              meanNumber += stepSize * IntegerDivRemTruncated(TNumber.CreateChecked(count), TNumber.One + TNumber.One).Quotient;  // Setup the inital outer edge value for inward iteration.

              for (var index = count - 1; index >= 0; index--)
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
    }

    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
    {
      #region GeometricSequenceNthTerm

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

      #endregion

      #region GeometricSeriesOfNTerms

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
  }
}
