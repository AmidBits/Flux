namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region FibonacciSequence

      /// <summary>
      /// <para>Creates a new sequence of <typeparamref name="TInteger"/> with Fibonacci numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> FibonacciSequence()
      {
        var n1 = TInteger.Zero;
        var n2 = TInteger.One;

        while (true)
        {
          yield return n1;

          try { checked { n1 += n2; } } catch { break; }

          yield return n2;

          try { checked { n2 += n1; } } catch { break; }
        }
      }

      #endregion

      #region IsFibonacciNumber

      /// <summary>
      /// <para>Determines whether the <paramref name="number"/> is a Fibonacci number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static bool IsFibonacciNumber(TInteger n)
      {
        checked
        {
          var four = TInteger.CreateChecked(4);

          var fivens = TInteger.CreateChecked(5) * n * n;
          var fp4 = fivens + four;
          var fp4sr = SquareRoot(fp4);
          var fm4 = fivens - four;
          var fm4sr = SquareRoot(fm4);

          return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
        }
      }

      #endregion

      #region LeonardoSequence

      /// <summary>
      /// <para>Creates a new sequence with Leonardo numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Leonardo_number"/></para>
      /// </summary>
      /// <param name="first"></param>
      /// <param name="second"></param>
      /// <param name="step"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> LeonardoSequence(TInteger first, TInteger second, TInteger step)
      {
        while (true)
        {
          yield return first;

          checked { (first, second) = (second, first + second + step); }
        }
      }

      #endregion

      #region MoserDeBruijnSequence

      /// <summary>Creates a sequence of Moser/DeBruijn numbers.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
      /// <seealso cref="https://www.geeksforgeeks.org/moser-de-bruijn-sequence/"/>
      public static System.Collections.Generic.List<TInteger> MoserDeBruijnSequence(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var sequence = new System.Collections.Generic.List<TInteger>(int.CreateChecked(n)) { TInteger.Zero };

        if (n > TInteger.Zero)
          sequence.Add(TInteger.One);

        for (var i = TInteger.CreateChecked(2); i < n; i++)
        {
          var (q, r) = TInteger.DivRem(i, TInteger.CreateChecked(2));

          var next = TInteger.CreateChecked(4) * sequence[int.CreateChecked(q)];

          if (!TInteger.IsZero(r)) // Zero remainder: S(2 * n) = 4 * S(n)
            next++; // Non-zero remainder: S(2 * n + 1) = 4 * S(n) + 1

          sequence.Add(next);
        }

        return sequence;
      }

      #endregion

      #region PadovanSequence

      /// <summary>
      /// <para>Creates a new sequence with Padovan numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Padovan_sequence"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> PadovanSequence()
      {
        TInteger p1 = TInteger.One, p2 = TInteger.One, p3 = TInteger.One;

        yield return p1;
        yield return p2;
        yield return p3;

        TInteger pn;

        while (true)
        {
          try
          {
            pn = checked(p2 + p3);
          }
          catch { break; }

          yield return pn;

          p3 = p2;
          p2 = p1;
          p1 = pn;
        }
      }

      #endregion

      #region PerrinNumbers

      /// <summary>
      /// <para>Creates an indefinite sequence of Perrin numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Perrin_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> PerrinNumbers()
      {
        TInteger a = TInteger.CreateChecked(3), b = TInteger.Zero, c = TInteger.CreateChecked(2);

        yield return a;
        yield return b;
        yield return c;

        TInteger p;

        while (true)
        {
          try
          {
            p = checked(a + b);
          }
          catch { break; }

          a = b;
          b = c;
          c = p;

          yield return p;
        }
      }

      #endregion

      #region VanEcksSequence

      /// <summary>
      /// <para>Creates a new Van Eck's sequence, starting with the specified <paramref name="minNumber"/> (where 0 yields the original sequence).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/></para>
      /// </summary>
      /// <param name="minNumber"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Collections.Generic.IEnumerable<TInteger> VanEcksSequence(TInteger minNumber)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(minNumber);

        var lasts = new System.Collections.Generic.Dictionary<TInteger, TInteger>();
        var last = minNumber;

        checked
        {
          for (var index = TInteger.Zero; ; index++)
          {
            yield return last;

            TInteger next = TInteger.Zero;

            if (!lasts.TryAdd(last, index))
            {
              next = index - lasts[last];

              lasts[last] = index;
            }

            last = next;
          }
        }
      }

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region HarmonicMean (type of average)

      /// <summary>
      /// <para>The harmonic mean is a type of average.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_mean"/></para>
      /// </summary>
      /// <param name="terms"></param>
      /// <returns></returns>
      public static TFloat HarmonicMean(out int countOfTerms, out TFloat sumOfHarmonicTerms, params System.Collections.Generic.IEnumerable<TFloat> terms)
      {
        sumOfHarmonicTerms = terms.Select(n => TFloat.One / n).Sum(out countOfTerms);

        return TFloat.CreateChecked(countOfTerms) / sumOfHarmonicTerms;
      }

      #endregion

      #region HarmonicMeanOfTwoTerms

      /// <summary>
      /// <para>The harmonic mean of two terms is a special case.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_mean"/></para>
      /// </summary>
      /// <param name="x1"></param>
      /// <param name="x2"></param>
      /// <returns></returns>
      public static TFloat HarmonicMeanOfTwoTerms(TFloat x1, TFloat x2)
      {
        if (TFloat.IsZero(x1 + x2)) throw new System.ArithmeticException("The harmonic mean is undefined when the sum of the two terms is zero.");

        return (TFloat.CreateChecked(2) * x1 * x2) / (x1 + x2);
      }

      #endregion

      #region HarmonicMeanOfThreeTerms

      /// <summary>
      /// <para>The harmonic mean of three terms is a special case.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_mean"/></para>
      /// </summary>
      /// <param name="x1"></param>
      /// <param name="x2"></param>
      /// <param name="x3"></param>
      /// <returns></returns>
      public static TFloat HarmonicMeanOfThreeTerms(TFloat x1, TFloat x2, TFloat x3)
        => (TFloat.CreateChecked(3) * x1 * x2 * x3) / (x1 * x2 + x2 * x3 + x3 * x1);

      #endregion

      #region HarmonicSequence (progression)

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_progression_(mathematics)"/></para>
      /// </summary>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the harmmonic sequence.</param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TFloat> HarmonicSequence(TFloat a1, TFloat commonDifference)
        => Number.ArithmeticSequence(a1, commonDifference).Select(an => TFloat.One / an);

      #endregion

      #region HarmonicSequenceNthTerm

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_series_(mathematics)"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the harmmonic sequence.</param>
      /// <param name="n">The term to retrieve.</param>
      /// <returns></returns>
      public static TFloat HarmonicSequenceNthTerm<TInteger>(TFloat a1, TFloat commonDifference, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => TFloat.One / (a1 + (TFloat.CreateChecked(n) - TFloat.One) * commonDifference);

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region HarmonicSeriesOfNTerms

      /// <summary>
      /// <para>Gets the harmonic series (sum) of a geometric sequence with <paramref name="nth"/> terms and the specified <paramref name="commonRatio"/>.</para>
      /// </summary>
      /// <param name="commonRatio">The common ratio of the geometric sequence.</param>
      /// <param name="nth">The term of which to find the sum up until.</param>
      /// <returns></returns>
      public static TFloat HarmonicSeriesOfNTerms<TInteger>(TFloat a, TFloat d, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        var two = TFloat.CreateChecked(2);

        return TFloat.One / d * TFloat.Log((two * a + (two * TFloat.CreateChecked(n) - TFloat.One) * d) / (two * a - d));
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IExponentialFunctions<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region GeometricMean (type of average)

      /// <summary>
      /// <para>The geometric mean is a mean or average which indicates a central tendency of a finite collection of positive real numbers by using the product of their values (as opposed to the arithmetic mean, which uses their sum).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Geometric_mean"/></para>
      /// </summary>
      /// <remarks>This implementation uses <see cref="TFloat.Exp(TFloat)"/> and <see cref="TFloat.Log(TFloat)"/> to avoid arithmetic overflow or underflow.</remarks>
      /// <param name="terms"></param>
      /// <returns></returns>
      public static TFloat GeometricMean(params System.Collections.Generic.IEnumerable<TFloat> terms)
        => TFloat.Exp(terms.Select(TFloat.Log).Sum(out var count) / TFloat.CreateChecked(count));

      #endregion
    }
  }

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
