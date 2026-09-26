namespace Flux
{
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
}
