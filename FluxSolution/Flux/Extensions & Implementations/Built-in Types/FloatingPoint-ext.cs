namespace Flux
{
  public enum LanczosMode
  {
    /// <summary>
    /// <para>Cephes/Boost-style.</para>
    /// </summary>
    Standard,
    /// <summary>
    /// <para>Numerical Recipes-style.</para>
    /// </summary>
    NumericalRecipes
  }

  public static class FloatingPoint
  {
    #region RoundMidpointToAlternating (state variable)

    private static bool m_roundMidpointAlternatingState; // Internal state.

    #endregion

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      public static TFloat GetBaseEpsilon()
        => TFloat.Zero switch
        {
          decimal => TFloat.CreateChecked(decimal.DefaultBaseEpsilon), // ~28-29 digits precision
          double => TFloat.CreateChecked(double.DefaultBaseEpsilon), // ~15-16 digits precision
          float => TFloat.CreateChecked(float.DefaultBaseEpsilon), // ~7 digits precision
          System.Runtime.InteropServices.NFloat when System.Runtime.InteropServices.NFloat.Size == 8 => TFloat.CreateChecked(double.DefaultBaseEpsilon), // ~15-16 digits precision
          System.Runtime.InteropServices.NFloat when System.Runtime.InteropServices.NFloat.Size == 4 => TFloat.CreateChecked(float.DefaultBaseEpsilon), // ~7 digits precision
          System.Half => TFloat.CreateChecked(System.Half.DefaultBaseEpsilon), // ~3 digits precision
          _ => throw new NotImplementedException()
        };

      #region CompareToFraction..

      /// <summary>
      /// <para>Compare the fraction part of <paramref name="value"/> to it's midpoint (i.e. its .5).</para>
      /// </summary>
      /// <param name="x">The value to be compared.</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 if <paramref name="x"/> is less-than 0.5.</para>
      /// <para>0 if <paramref name="x"/> is equal-to 0.5.</para>
      /// <para>+1 if <paramref name="x"/> is greater-than 0.5.</para>
      /// </returns>
      public static int CompareToFractionMidpoint(TFloat x)
        => Number.Sign((x - TFloat.Floor(x)).CompareTo(TFloat.CreateChecked(0.5)));

      /// <summary>
      /// <para>Compares the fraction part of <paramref name="x"/> to the specified <paramref name="percent"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
      /// </summary>
      /// <param name="x">The value to be compared.</param>
      /// <param name="percent">Percent in the range [0, 1].</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 when <paramref name="x"/> is less than <paramref name="percent"/>.</para>
      /// <para>0 when <paramref name="x"/> is equal to <paramref name="percent"/>.</para>
      /// <para>+1 when <paramref name="x"/> is greater than <paramref name="percent"/>.</para>
      /// </returns>
      public static int CompareToFractionPercent(TFloat x, TFloat percent)
        => Number.Sign((x - TFloat.Floor(x)).CompareTo(percent));

      #endregion

      #region Envelop..

      /// <summary>
      /// <para>Envelops a value.</para>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="x"></param>
      /// <returns></returns>
      public static TFloat Envelop(TFloat x)
        => TFloat.IsNegative(x)
        ? TFloat.Floor(x)
        : TFloat.Ceiling(x);

      /// <summary>
      /// <para>Envelops a value at the specified number of <paramref name="significantDigits"/> (decimal places).</para>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x"></param>
      /// <param name="significantDigits"></param>
      /// <returns></returns>
      public static TFloat Envelop(TFloat x, int significantDigits)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var m = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, significantDigits));

        return Envelop(x * m) / m;
      }

      #endregion

      #region FallingFactorial (generalized)

      /// <summary>
      /// <para>Generalized factorial power: <code>x^(n)_falling(h) = x * (x - h) * (x - 2h) * ... * (x - (n-1)h)</code></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <param name="h">Step size, or increment between factors. Determines how far apart the terms are spaced.</param>
      /// <returns></returns>
      public static TFloat FactorialPower<TInteger>(TFloat x, TInteger n, TFloat h)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        TFloat result = TFloat.One;

        while (n-- > TInteger.Zero)
        {
          result *= x;

          x -= h; // Decrement x by step h.
        }

        return result;
      }

      #endregion

      #region HarmonicMean.. (type of average)

      /// <summary>
      /// <para>The harmonic mean is a type of average.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_mean"/></para>
      /// </summary>
      /// <param name="terms"></param>
      /// <returns></returns>
      public static TFloat HarmonicMean(params System.Collections.Generic.IEnumerable<TFloat> terms)
      {
        var sum = terms.Select(n => TFloat.One / n).Sum(out var count);

        return TFloat.CreateChecked(count) / sum;
      }

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

      #region HarmonicSequence.. (progression)

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_progression_(mathematics)"/></para>
      /// </summary>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the arithmetic sequence.</param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TFloat> HarmonicSequence(TFloat a1, TFloat commonDifference)
        => Number.ArithmeticSequence(a1, commonDifference).Select(an => TFloat.One / an);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Harmonic_series_(mathematics)"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a1">The first term.</param>
      /// <param name="commonDifference">The common difference of the arithmetic sequence.</param>
      /// <param name="n">The term to retrieve.</param>
      /// <returns></returns>
      public static TFloat HarmonicSequenceNthTerm<TInteger>(TFloat a1, TFloat commonDifference, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => TFloat.One / (a1 + (TFloat.CreateChecked(n) - TFloat.One) * commonDifference);

      #endregion

      #region Interpolate.. (cubic, cubicpb, hermite, linear)

      /// <summary>
      /// <para></para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="y2"></param>
      /// <param name="y3"></param>
      /// <param name="mu"></param>
      /// <returns></returns>
      public static TFloat InterpolateCubic(TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu)
      {
        var mu2 = mu * mu;

        var a0 = y3 - y2 - y0 + y1;
        var a1 = y0 - y1 - a0;
        var a2 = y2 - y0;
        var a3 = y1;

        return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
      }

      /// <summary>
      /// <para></para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="y2"></param>
      /// <param name="y3"></param>
      /// <param name="mu"></param>
      /// <returns></returns>
      public static TFloat InterpolateCubicPb(TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu)
      {
        var two = TFloat.CreateChecked(2);
        var half = TFloat.One / two;
        var oneAndHalf = two - half;

        var mu2 = mu * mu;

        var a0 = -half * y0 + oneAndHalf * y1 - oneAndHalf * y2 + half * y3;
        var a1 = y0 - (two + half) * y1 + two * y2 - half * y3;
        var a2 = -half * y0 + half * y2;
        var a3 = y1;

        return mu * mu2 * a0 + mu2 * a1 + mu * a2 + a3;
      }

      /// <summary>
      /// <para></para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="y2"></param>
      /// <param name="y3"></param>
      /// <param name="mu"></param>
      /// <param name="tension"></param>
      /// <param name="bias"></param>
      /// <returns></returns>
      public static TFloat InterpolateHermite(TFloat y0, TFloat y1, TFloat y2, TFloat y3, TFloat mu, TFloat tension, TFloat bias)
      {
        var one = TFloat.One;
        var two = one + one;
        var three = two + one;

        var mu2 = mu * mu;
        var mu3 = mu2 * mu;

        var biasP = (TFloat.One + bias) * (TFloat.One - tension);
        var biasN = (TFloat.One - bias) * (TFloat.One - tension);

        var m0 = (y1 - y0) * biasP / two + (y2 - y1) * biasN / two;
        var m1 = (y2 - y1) * biasP / two + (y3 - y2) * biasN / two;

        var a0 = two * mu3 - three * mu2 + one;
        var a1 = mu3 - two * mu2 + mu;
        var a2 = mu3 - mu2;
        var a3 = -two * mu3 + three * mu2;

        return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
      }

      /// <summary>
      /// <para>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      public static TFloat InterpolateLinear(TFloat y0, TFloat y1, TFloat mu)
        => (TFloat.One - mu) * y0 + mu * y1;

      #endregion

      #region Native..

      /// <summary>
      /// <para>Decrements a value. If integer, then by 1. If floating-point, then by "bit-decrement". If decimal, by 1e-28m.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <remarks>Infimum, for integer types equal (<paramref name="value"/> - 1) and for floating point types equal (<paramref name="value"/> - epsilon). Other types are not implemented at this time.</remarks>
      public static TFloat NativeDecrement(TFloat value)
        => value switch
        {
          decimal dfp128 => TFloat.CreateChecked(decimal.NativeDecrement(dfp128)),
          double bfp64 => TFloat.CreateChecked(double.NativeDecrement(bfp64)),
          float bfp32 => TFloat.CreateChecked(float.NativeDecrement(bfp32)),
          System.Half bfp16 => TFloat.CreateChecked(System.Half.NativeDecrement(bfp16)),
          System.Runtime.InteropServices.NFloat nf => TFloat.CreateChecked(System.Runtime.InteropServices.NFloat.NativeDecrement(nf)),
          _ => throw new System.NotImplementedException(),
        };

      /// <summary>
      /// <para>Increments a value. If integer, then by 1. If floating-point, then by "bit-increment". If decimal, by 1e-28m.</para>
      /// </summary>
      /// <remarks>Supremum, for integer types equal (<paramref name="value"/> + 1) and for floating point types equal (<paramref name="value"/> + epsilon). Other types are not implemented at this time.</remarks>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TFloat NativeIncrement(TFloat value)
        => value switch
        {
          decimal dfp128 => TFloat.CreateChecked(decimal.NativeIncrement(dfp128)),
          double bfp64 => TFloat.CreateChecked(double.NativeIncrement(bfp64)),
          float bfp32 => TFloat.CreateChecked(float.NativeIncrement(bfp32)),
          System.Half bfp16 => TFloat.CreateChecked(System.Half.NativeIncrement(bfp16)),
          System.Runtime.InteropServices.NFloat nf => TFloat.CreateChecked(System.Runtime.InteropServices.NFloat.NativeIncrement(nf)),
          _ => throw new System.NotImplementedException(),
        };

      #endregion

      #region ..NearInteger

      /// <summary>
      /// <para>Indicates whether a <paramref name="value"/> is near an integer and if so outputs the <paramref name="integer"/> as a parameter.</para>
      /// <para>The algorithm applies the specified <paramref name="baseEpsilon"/> for custom tolerance.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="integer"></param>
      /// <param name="baseEpsilon"></param>
      /// <returns></returns>
      public static bool IsNearInteger(TFloat value, out TFloat integer, TFloat baseEpsilon)
      {
        var half = TFloat.CreateChecked(0.5);

        integer = value >= TFloat.Zero ? TFloat.Floor(value + half) : TFloat.Ceiling(value - half); // Find mathematically nearest integer without midpoint bias.

        return TFloat.Abs(value - integer) <= baseEpsilon * (TFloat.One + value);
      }

      /// <summary>
      /// <para>Indicates whether a <paramref name="value"/> is near an integer and if so outputs the <paramref name="integer"/> as a parameter.</para>
      /// <para>The algorithm queries <see cref="GetBaseEpsilon{TFloat}"/> for a default tolerance level.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="integer"></param>
      /// <returns></returns>
      public static bool IsNearInteger(TFloat value, out TFloat integer)
        => IsNearInteger(value, out integer, GetBaseEpsilon<TFloat>());

      /// <summary>
      /// <para>If a value is near an integer, round to that integer, otherwise return the value.</para>
      /// <para>The algorithm applies the specified <paramref name="baseEpsilon"/> for custom tolerance.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseEpsilon"></param>
      /// <returns></returns>
      public static TFloat RoundNearInteger(TFloat value, TFloat baseEpsilon)
        => IsNearInteger(value, out var integer, baseEpsilon) ? integer : value;

      /// <summary>
      /// <para>If a value is near an integer, round to that integer, otherwise return the value.</para>
      /// <para>The algorithm queries <see cref="GetBaseEpsilon{TFloat}"/> for a default tolerance level.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TFloat RoundNearInteger(TFloat value)
        => IsNearInteger(value, out var integer, GetBaseEpsilon<TFloat>()) ? integer : value;

      #endregion

      #region ..NearNumber

      /// <summary>
      /// <para>Perform both an absolute and a relative equality test for more robust comparisons. Returns true if any test is considered equal, otherwise false.</para>
      /// <para>The algorithm applies the specified <paramref name="baseEpsilon"/> for custom tolerance.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="number"></param>
      /// <param name="baseEpsilon">E.g. 1e-12</param>
      /// <returns></returns>
      public static bool IsNearNumber(TFloat value, TFloat number, TFloat baseEpsilon)
      {
        if (value == number)
          return true;

        if (TFloat.IsNaN(value) || TFloat.IsNaN(number))
          return false;

        if (TFloat.IsInfinity(value) || TFloat.IsInfinity(number))
          return value == number;

        var difference = TFloat.Abs(value - number);
        var tolerance = baseEpsilon * (TFloat.One + TFloat.Abs(TFloat.MaxMagnitude(value, number)));

        return difference <= tolerance;
      }

      /// <summary>
      /// <para>Perform both an absolute and a relative equality test for more robust comparisons. Returns true if any test is considered equal, otherwise false.</para>
      /// <para>The algorithm queries <see cref="GetBaseEpsilon{TFloat}"/> for tolerance.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="number"></param>
      /// <returns></returns>
      public static bool IsNearNumber(TFloat value, TFloat number)
        => IsNearNumber(value, number, GetBaseEpsilon<TFloat>());

      /// <summary>
      /// <para>If a value is near a number, round to that number, otherwise return the value.</para>
      /// <para>The algorithm applies the specified <paramref name="baseEpsilon"/> for custom tolerance.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseEpsilon"></param>
      /// <returns></returns>
      public static TFloat RoundNearNumber(TFloat value, TFloat number, TFloat baseEpsilon)
        => IsNearNumber(value, number, baseEpsilon) ? number : value;

      /// <summary>
      /// <para>If a value is near a number, round to that number, otherwise return the value.</para>
      /// <para>The algorithm queries <see cref="GetBaseEpsilon{TFloat}"/> for a default tolerance level.</para>
      /// <para><see cref="GetBaseEpsilon{TFloat}"/> is the default tolerance and essentially corresponds to calculation errors.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TFloat RoundNearNumber(TFloat value, TFloat number)
        => IsNearNumber(value, number, GetBaseEpsilon<TFloat>()) ? number : value;

      #endregion

      #region FallingFactorial (generalized)

      /// <summary>
      /// <para>Generalized rising factorial: x^(n)_rising(h) = x * (x + h) * (x + 2h) * ... * (x + (n-1)h)</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <param name="h">Step size, or increment between factors. Determines how far apart the terms are spaced.</param>
      /// <returns></returns>
      public static TFloat RisingFactorial<TInteger>(TFloat x, TInteger n, TFloat h)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        TFloat result = TFloat.One;

        while (n-- > TInteger.Zero)
        {
          result *= x;

          x += h; // increment x by step h
        }

        return result;
      }

      #endregion

      #region Percent..ToPercent..

      public static TFloat PercentAddedToPercentRemove(TFloat percentAdded)
       => TFloat.One / (TFloat.One / percentAdded + TFloat.One);

      public static TFloat PercentRemovedToPercentAdd(TFloat percentAdded)
        => TFloat.One / (TFloat.One / percentAdded - TFloat.One);

      #endregion

      #region RoundMidpoint..

      /// <summary>
      /// <para>Rounds a value to the nearest integer, resolving halfway cases using the specified <see cref="MidpointRoundingEx"/> <paramref name="mode"/>.</para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TFloat RoundMidpoint(TFloat x, MidpointRoundingEx mode)
        => mode switch
        {
          MidpointRoundingEx.ToEven or
          MidpointRoundingEx.AwayFromZero or
          MidpointRoundingEx.TowardZero or
          MidpointRoundingEx.ToNegativeInfinity or
          MidpointRoundingEx.ToPositiveInfinity => TFloat.Round(x, (MidpointRounding)(int)mode), // Use built-in .NET functionality for standard cases.
          MidpointRoundingEx.ToAlternating => RoundMidpointToAlternating(x),
          MidpointRoundingEx.ToOdd => RoundMidpointToOdd(x),
          MidpointRoundingEx.ToRandom => RoundMidpointToRandom(x),
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
        };

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value"></param>
      /// <param name="state"></param>
      /// <returns></returns>
      public static TFloat RoundMidpointToAlternating(TFloat value)
      {
        var cmp = CompareToFractionMidpoint(value);

        var floor = TFloat.Floor(value);

        if (cmp < 0)
          return floor;

        var ceiling = TFloat.Ceiling(value);

        if (cmp > 0)
          return ceiling;

        return (m_roundMidpointAlternatingState = !m_roundMidpointAlternatingState) ? floor : ceiling;
      }

      /// <summary>
      /// <para>Common rounding: round half, bias: odd.</para>
      /// <para><see cref="MidpointRoundingEx.ToOdd"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TFloat RoundMidpointToOdd(TFloat value)
      {
        var cmp = CompareToFractionMidpoint(value);

        var floor = TFloat.Floor(value);

        if (cmp < 0)
          return floor;

        var ceiling = TFloat.Ceiling(value);

        if (cmp > 0)
          return ceiling;

        return TFloat.IsOddInteger(floor) ? floor : ceiling;
      }

      /// <summary>
      /// <para><see cref="MidpointRoundingEx.ToRandom"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      public static TFloat RoundMidpointToRandom(TFloat value)
      {
        var cmp = CompareToFractionMidpoint(value);

        var floor = TFloat.Floor(value);

        if (cmp < 0)
          return floor;

        var ceiling = TFloat.Ceiling(value);

        if (cmp > 0)
          return ceiling;

        return RandomNumberGenerators.SscRng.Shared.Next(2) == 0 ? floor : ceiling;
      }

      #endregion

      #region Truncate..

      /// <summary>
      /// <para>Truncates a value at the specified number of <paramref name="significantDigits"/> (decimal places).</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="significantDigits"></param>
      /// <returns></returns>
      public static TFloat Truncate(TFloat x, int significantDigits)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var m = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, significantDigits));

        return TFloat.Truncate(x * m) / m;
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IFloatingPointConstants<TFloat>
    {
      #region GeographicToSpherical

      /// <summary>Creates a new <see cref="SphericalCoordinate"/> from the <see cref="GeographicCoordinate"/>.</summary>
      public static (TFloat radius, TFloat inclination, TFloat azimuth) GeographicToSpherical(TFloat latitude, TFloat longitude, TFloat altitude)
      // Translates the geographic coordinate to spherical coordinate transparently. I cannot recall the reason for the System.Math.PI involvement (see remarks).
      {
        return new(
          altitude,
          (TFloat.Pi / TFloat.CreateChecked(2)) - latitude, // Add 90 degrees to convert from [-90..+90] (elevation, lat/lon) to [+0..+180] (inclination).
          longitude
        );
      }

      #endregion

      #region SphericalToGeographic

      /// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public static (TFloat latitude, TFloat longitude, TFloat altitude) SphericalToGeographic(TFloat radius, TFloat inclination, TFloat azimuth)
        => new(
          TFloat.Pi / TFloat.CreateChecked(2) - inclination,
          azimuth,
          radius
        );

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region HarmonicSeries.. (sum)

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

      #region RescaleLogarithmicToLinear

      /// <summary>
      /// <para>Rescale logarithmic (Y) to linear (X).</para>
      /// <example>
      /// <code>var x = (1000.0).RescaleLogarithmicToLinear(300, 3000, 10, 12, 2);</code>
      /// <code>x = 11.045757490560675</code>
      /// </example>
      /// </summary>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="x0"></param>
      /// <param name="x1"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RescaleLogarithmicToLinear(TFloat y, TFloat y0, TFloat y1, TFloat x0, TFloat x1, TFloat radix)
        => Number.Rescale(TFloat.Log(y, radix), TFloat.Log(y0, radix), TFloat.Log(y1, radix), x0, x1); // Extract the numbers and use the standard Rescale() function for the math.

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>, System.Numerics.IExponentialFunctions<TFloat>
    {
      #region GeometricMean

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

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
      #region RescaleLinearToLogarithmic

      /// <summary>
      /// <para>Rescale linear (X) to logarithmic (Y).</para>
      /// <example>
      /// <code>var y = (7.5).RescaleLinearToLogarithmic(0.1, 10, 0.1, 10, 2);</code>
      /// <code>y = 3.1257158496882371</code>
      /// </example>
      /// </summary>
      /// <param name="x0"></param>
      /// <param name="x1"></param>
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RescaleLinearToLogarithmic(TFloat x, TFloat x0, TFloat x1, TFloat y0, TFloat y1, TFloat radix)
      => TFloat.Pow(radix, Number.Rescale(x, x0, x1, TFloat.Log(y0, radix), TFloat.Log(y1, radix))); // Extract the numbers and use the standard Rescale() function for the math.

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
      #region Financial functions

      /// <summary>
      /// <para>The interest on loans and mortgages that are amortized, i.e. have a smooth monthly payment until the loan has been paid off, is often compounded monthly.</para>
      /// <para>The fixed monthly payment for a fixed rate mortgage is the amount paid by the borrower every month that ensures that the loan is paid off in full with interest at the end of its term. The monthly payment formula is based on the annuity formula.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Compound_interest#Monthly_amortized_loan_or_mortgage_payments"/></para>
      /// <example>For example, for a home loan of $200,000 with a fixed yearly interest rate of 6.5% for 30 years, the principal is <param name="principalAmount"/> = 200,000, the monthly interest rate is <paramref name="monthlyInterestRate"/> = 0.065 / 12, the number of monthly payments is <paramref name="numberOfPaymentPeriods"/> = 30 * 12 = 360, the fixed monthly payment equals $1,264.14: <code>AmortizedMonthlyPayment(200000, 0.065 / 12, 30 * 12);</code></example>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="principalAmount">The principal. The amount borrowed, known as the loan's principal.</param>
      /// <param name="monthlyInterestRate">The monthly interest rate. Since the quoted yearly percentage rate is not a compounded rate, the monthly percentage rate is simply the yearly percentage rate divided by 12.</param>
      /// <param name="numberOfPaymentPeriods">The number of payment periods. (E.g. the number of monthly payments, called the loan's term.)</param>
      /// <returns>The monthly payment (c).</returns>
      public static TFloat AmortizedMonthlyPayment(TFloat principalAmount, TFloat monthlyInterestRate, TFloat numberOfPaymentPeriods)
        => monthlyInterestRate * principalAmount / (TFloat.One - (TFloat.One / TFloat.Pow(TFloat.One + monthlyInterestRate, numberOfPaymentPeriods)));

      /// <summary>
      /// <para>Compound interest is interest accumulated from a principal sum and previously accumulated interest. It is the result of reinvesting or retaining interest that would otherwise be paid out, or of the accumulation of debts from a borrower.</para>
      /// <para>The accumulation function shows what $1 grows to after any length of time.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Compound_interest#Accumulation_function"/></para>
      /// </summary>
      /// <remarks>
      /// <para>The total accumulated value, including the principal sum P plus compounded interest I is given by: <code>A = P * CompoundInterest(r, n, t);</code></para>
      /// <para>The total compound interest generated is the final value (A) minus the initial principal: <code>I = P * CompoundInterest(r, n, t) - P;</code> or <code>I = A - P;</code></para>
      /// </remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="nominalInterestRate">The nominal (annual, usually) interest rate.</param>
      /// <param name="compoundingFrequency">The compounding frequency (1: annually, 12: monthly, 52: weekly, 365: daily).</param>
      /// <param name="overallLengthOfTime">The overall length of time the interest is applied (expressed using the same time units as <paramref name="compoundingFrequency"/>, usually years).</param>
      /// <returns>The accumulative compound interest of <paramref name="nominalInterestRate"/>.</returns>
      public static TFloat CompoundInterest(TFloat nominalInterestRate, TFloat compoundingFrequency, TFloat overallLengthOfTime)
        => TFloat.Pow(TFloat.One + (nominalInterestRate / compoundingFrequency), overallLengthOfTime * compoundingFrequency);

      #endregion

      #region RoundBy..

      /// <summary>
      /// <para>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/> in base <paramref name="radix"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</para>
      /// <example>
      /// <code>var r = RoundByPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundByTruncatedPrecision{TSelf}(TSelf, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RoundByPrecision<TRadix>(TFloat x, MidpointRoundingEx mode, int significantDigits, TRadix radix)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var scalar = TFloat.Pow(TFloat.CreateChecked(Units.Radix.AssertMember(radix)), TFloat.CreateChecked(significantDigits));

        return RoundMidpoint(x * scalar, mode) / scalar;
      }

      /// <summary>
      /// <para>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> in base <paramref name="radix"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</para>
      /// <para><seealso href="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/></para>
      /// <example>
      /// <code>var r = RoundByTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundByPrecision{TValue}(TValue, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="x"></param>
      /// <param name="mode"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TFloat RoundByTruncatedPrecision<TRadix>(TFloat x, MidpointRoundingEx mode, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var scalar = TFloat.Pow(TFloat.CreateChecked(Units.Radix.AssertMember(radix)), TFloat.CreateChecked(significantDigits + 1));

        return RoundByPrecision(TFloat.Truncate(x * scalar) / scalar, mode, significantDigits, radix);
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IRootFunctions<TFloat>
    {
      #region HelmertsExpansionParameterK1

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
      /// </summary>
      public static TFloat HelmertsExpansionParameterK1(TFloat x)
        => TFloat.Sqrt(TFloat.One + x * x) is var k ? (k - TFloat.One) / (k + TFloat.One) : throw new System.ArithmeticException();

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      #region CylindricalToCartesian

      /// <summary>Creates cartesian 3D coordinates from the <see cref="CylindricalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public static (TFloat x, TFloat y, TFloat z) CylindricalToCartesian(TFloat radius, TFloat azimuth, TFloat height)
      {
        var (sin, cos) = TFloat.SinCos(azimuth);

        return (
          radius * cos,
          radius * sin,
          height
        );
      }

      #endregion

      #region Interpolate.. (cosine)

      /// <summary>
      /// <para>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y1">Source point.</param>
      /// <param name="y2">Target point.</param>
      /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
      /// <returns></returns>
      public static TFloat InterpolateCosine(TFloat y0, TFloat y1, TFloat mu)
      {
        var mu2 = (TFloat.One - TFloat.CosPi(mu)) / TFloat.CreateChecked(2);

        return InterpolateLinear(y0, y1, mu2);
      }

      #endregion

      #region PolarToCartesian

      /// <summary>
      /// <para>Creates cartesian-coordinates (x, y) from polar-coordinates (radius, azimuth).</para>
      /// <list type="bullet">
      /// <item>When <c><paramref name="originStandardPosition"/> = true</c> then 'right-center' is 0 (i.e. positive-x and zero-y) with a counter-clockwise positive rotation angle [0, PI*2]. Looking at the face of a clock it's counter-clockwise from 3 o'clock.</item>
      /// <item>When <c><paramref name="originStandardPosition"/> = false</c> then 'center-up' is 0 (i.e. zero-x and positive-y) with a clockwise positive rotation angle [0, PI*2]. Looking at the face of a clock (or a compass) it's clockwise from 12 o'clock (noon).</item>
      /// </list>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (TFloat x, TFloat y) PolarToCartesian(TFloat radius, TFloat azimuth, bool originStandardPosition)
      {
        var (sin, cos) = TFloat.SinCos(azimuth);

        return originStandardPosition ? (radius * cos, radius * sin) : (radius * sin, radius * cos);
      }

      #endregion

      #region SphericalToCartesian

      /// <summary>
      /// <para>Creates cartesian-coordinates from spherical-coordinates.</para>
      /// <remarks>All angles in radians.</remarks>
      /// </summary>
      /// <param name="radius"></param>
      /// <param name="inclination">If only elevation is known, then pass "<c>(TFloat.Pi / 2) - elevation</c>" (i.e. <c>90 - elevation</c>, in radians) as inclination.</param>
      /// <param name="azimuth"></param>
      /// <returns></returns>
      public static (TFloat x, TFloat y, TFloat z) SphericalToCartesian(TFloat radius, TFloat inclination, TFloat azimuth)
      {
        var (si, ci) = TFloat.SinCos(inclination);
        var (sa, ca) = TFloat.SinCos(azimuth);

        return (
          radius * si * ca,
          radius * si * sa,
          radius * ci
        );
      }

      #endregion

      #region SphericalToCylindrical

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      public static (TFloat radius, TFloat azimuth, TFloat height) SphericalToCylindrical(TFloat radius, TFloat inclination, TFloat azimuth)
      {
        var (si, ci) = TFloat.SinCos(inclination);

        return new(
          radius * si,
          azimuth,
          radius * ci
        );
      }

      #endregion

      #region SphericalTriaxialToCartesian

      /// <summary>
      /// <para>Creates cartesian-coordinates from spherical-coordinates, but as a triaxial ellipsoid with three radii for each of the X (A), Y (B) and Z (C) axis, instead of a single radius.</para>
      /// <remarks>All angles in radians.</remarks>
      /// </summary>
      /// <param name="radiusA"></param>
      /// <param name="radiusB"></param>
      /// <param name="radiusC"></param>
      /// <param name="polarAngle">The polar angle (inclination). <c>[0, Pi]</c></param>
      /// <param name="azimuth">The azimuth angle. <c>[0, Tau)</c></param>
      /// <returns></returns>
      public static (double x, double y, double z) SphericalTriaxialToCartesian(double radiusA, double radiusB, double radiusC, double polarAngle, double azimuth)
      {
        var (si, ci) = double.SinCos(polarAngle);
        var (sa, ca) = double.SinCos(azimuth);

        return (
          radiusA * si * ca,
          radiusB * si * sa,
          radiusC * ci
        );
      }

      #endregion

      ///// <summary>
      ///// <para>Creates cartesian-coordinates from the latitude (elevation) and longitude (azimuth) of a spherical-coordinate, but with triaxial ellipsoid as three radii for the X (A), Y (B) and Z (C) axis instead of just a single radius.</para>
      ///// <remarks>All angles in radians.</remarks>
      ///// </summary>
      ///// <param name="radiusA"></param>
      ///// <param name="radiusB"></param>
      ///// <param name="radiusC"></param>
      ///// <param name="latitude">The reduced latitude, parametric latitude, or eccentric anomaly. <c>[-Pi/2, +Pi/2]</c></param>
      ///// <param name="longitude">The azimuth or longitude. <c>[0, Tau)</c></param>
      ///// <returns></returns>
      //public static (double x, double y, double z) SphericalByEquatorToCartesian(double radiusA, double radiusB, double radiusC, double latitude, double longitude)
      //{
      //  var (slat, clat) = double.SinCos(latitude);
      //  var (slon, clon) = double.SinCos(longitude);

      //  return (
      //    radiusA * clat * clon,
      //    radiusB * clat * slon,
      //    radiusC * slat
      //  );
      //}
    }

    #region Lanczos coefficients (hardcoded for double and float)

    private static readonly double[] m_coefficientsLanczosDouble = new double[]
    {
        0.99999999999980993,
        676.5203681218851,
        -1259.1392167224028,
        771.32342877765313,
        -176.61502916214059,
        12.507343278686905,
        -0.13857109526572012,
        9.9843695780195716e-6,
        1.5056327351493116e-7
    };

    private static readonly float[] m_coefficientsLanczosSingle = new float[]
    {
      1.000000000f,
      76.18009173f,
      -86.50532033f,
      24.01409822f,
      -1.231739516f,
      0.00120858003f
    };

    #endregion

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPointIeee754<TFloat>
    {
      #region CartesianToCylindrical

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public static (TFloat radius, TFloat azimuth, TFloat height) CartesianToCylindrical(TFloat x, TFloat y, TFloat z)
      => (
        TFloat.Sqrt(x * x + y * y),
        TFloat.Atan2(y, x) % TFloat.Pi,
        z
      );

      #endregion

      #region CartesianToPolar

      /// <summary>
      /// <para>Creates polar-coordinates (radius, azimuth) from cartesian-coordinates (x, y).</para>
      /// <list type="bullet">
      /// <item>When <c><paramref name="originStandardPosition"/> = true</c> then 'right-center' is 0 (i.e. positive-x and zero-y) with a counter-clockwise positive rotation angle [0, Tau]. Looking at the face of a clock it's counter-clockwise from 3 o'clock.</item>
      /// <item>When <c><paramref name="originStandardPosition"/> = false</c> then 'center-up' is 0 (i.e. zero-x and positive-y) with a clockwise positive rotation angle [0, Tau]. Looking at the face of a clock (or a compass) it's clockwise from 12 o'clock (noon).</item>
      /// </list>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (TFloat radius, TFloat azimuth) CartesianToPolar(TFloat x, TFloat y, bool originStandardPosition)
      {
        var azimuth = originStandardPosition ? TFloat.Atan2(y, x) : TFloat.Atan2(x, y);

        if (TFloat.IsNegative(azimuth))
          azimuth += TFloat.Tau;

        return (
          TFloat.Sqrt(x * x + y * y),
          azimuth
        );
      }

      #endregion

      #region CartesianToSpherical

      /// <summary>Creates a new <see cref="SphericalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public static (TFloat radius, TFloat inclination, TFloat azimuth) CartesianToSpherical(TFloat x, TFloat y, TFloat z)
      {
        var x2y2 = x * x + y * y;

        return (
          TFloat.Sqrt(x2y2 + z * z),
          TFloat.Atan2(TFloat.Sqrt(x2y2), z),
          TFloat.Atan2(y, x)
        );
      }

      #endregion // Conversion methods

      #region CylindricalToSpherical

      /// <summary>
      /// <para>Creates a new <see cref="SphericalCoordinate"/> from the <see cref="CylindricalCoordinate"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system#Spherical_coordinates"/></para>
      /// </summary>
      /// <remarks>All angles in radians.</remarks>
      public static (TFloat radius, TFloat inclination, TFloat azimuth) CylindricalToSpherical(TFloat radius, TFloat azimuth, TFloat height)
      {
        var r = radius;
        var h = height;

        return new(
          TFloat.Sqrt(r * r + h * h),
          (TFloat.Pi / TFloat.CreateChecked(2)) - TFloat.Atan(h / r), // "double.Atan(m_radius / m_height);", does NOT work for Takapau, New Zealand. Have to use elevation math instead of inclination, and investigate.
          azimuth
        );
      }

      #endregion

      #region LanczosGamma

      /// <summary>
      /// <para>Lanczos gamma approximation.</para>
      /// </summary>
      /// <param name="z"></param>
      /// <returns></returns>
      public static TFloat LanczosGamma(TFloat z)
        => TFloat.Exp(LanczosLogGamma(z));

      /// <summary>
      /// <para>Lanczos log-gamma approximation.</para>
      /// </summary>
      /// <param name="z"></param>
      /// <returns></returns>
      /// <exception cref="System.NotSupportedException"></exception>
      public static TFloat LanczosLogGamma(TFloat z)
      {
        if (z is double dz)
          return TFloat.CreateChecked(LanczosLogGamma(dz, m_coefficientsLanczosDouble, 7, LanczosMode.Standard));
        else if (z is float fz)
          return TFloat.CreateChecked(LanczosLogGamma(fz, m_coefficientsLanczosSingle, 5, LanczosMode.NumericalRecipes));
        else
          throw new System.NotSupportedException($"LanczosGamma is not supported for type {typeof(TFloat)}.");
      }

      #endregion

      #region LanczosLogGamma

      /// <summary>
      /// <para>Lanczos approximation of LogGamma using any <paramref name="g"/> and <paramref name="coefficients"/>.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="g"></param>
      /// <param name="coefficients"></param>
      /// <returns></returns>
      private static TFloat LanczosLogGamma(TFloat x, System.ReadOnlySpan<TFloat> coefficients, int g, LanczosMode mode)
      {
        var half = TFloat.CreateChecked(0.5);

        if (x < half) // Reflection formula for x < 0.5
          return TFloat.Log(TFloat.Pi) - TFloat.Log(TFloat.Abs(TFloat.SinPi(x))) - LanczosLogGamma(TFloat.One - x, coefficients, g, mode); // log(π / sin(πz)) - LogGamma(1 - z)

        x -= TFloat.One; // Shift so the series uses z-1.

        switch (mode)
        {
          case LanczosMode.Standard:
            {
              // Lanczos sum
              TFloat sum = coefficients[0];
              for (var i = 1; i < coefficients.Length; i++)
              {
                sum += coefficients[i] / (x + TFloat.CreateChecked(i));
              }

              var t = x + TFloat.CreateChecked(g) + half;

              return TFloat.Log(TFloat.Sqrt(TFloat.Tau)) + (x + half) * TFloat.Log(t) - t + TFloat.Log(sum);
            }
          case LanczosMode.NumericalRecipes:
            {
              var y = x;

              var tmp = x + TFloat.CreateChecked(g) + half;
              tmp -= (x + half) * TFloat.Log(tmp);

              var ser = TFloat.One;

              for (var i = 1; i < coefficients.Length; i++)
              {
                y += TFloat.One;
                ser += coefficients[i] / y;
              }

              return -tmp + TFloat.Log(TFloat.CreateChecked(TFloat.Sqrt(TFloat.Tau)) * ser);
            }
          default:
            throw new NotImplementedException();
        }
      }

      #endregion

      #region SpougeGamma

      /// <summary>
      /// <para>Spouge's approximation of Gamma using any <paramref name="coefficients"/>.</para>
      /// </summary>
      /// <param name="z"></param>
      /// <param name="coefficients"></param>
      /// <returns></returns>
      public static TFloat SpougeGamma(TFloat z, TFloat[] coefficients)
      {
        var half = TFloat.CreateChecked(0.5);

        if (z < half) // Reflection for z < 0.5
          return TFloat.Pi / (TFloat.SinPi(z) * SpougeGamma(TFloat.One - z, coefficients));

        var sum = coefficients[0];
        for (var k = 1; k < coefficients.Length; k++)
          sum += coefficients[k] / (z + TFloat.CreateChecked(k - 1));

        var t = z + TFloat.CreateChecked(coefficients.Length - 1);

        return TFloat.Pow(t, z - half) * TFloat.Exp(-t) * sum;
      }

      /// <summary>
      /// <para>Spouge's approximation of Gamma. The error of this approximation is less than 2e-10 for a = 12, and decreases rapidly as a increases.</para>
      /// <para>The parameter <paramref name="a"/> controls the accuracy and convergence speed of the approximation. A larger <paramref name="a"/> generally leads to better accuracy but slower convergence, while a smaller <paramref name="a"/> may converge faster but with less accuracy.</para>
      /// </summary>
      /// <param name="z"></param>
      /// <param name="a">
      /// <list type="table">
      /// <item>For <see cref="float"/> <c>a = 6</c> is sufficient.</item>
      /// <item>For <see cref="double"/> <c>a = 12</c> is the sweet spot, and <c>a = 15</c> gives slightly more accuracy but risks cancellation.</item>
      /// <item>For <see cref="decimal"/> <c>a = 10–12</c> works. The decimal type has high precision but a tiny exponent range, so Exp may overflow.</item>
      /// <item>For arbitrary-precision types choose a proportional to the number of digits you want, e.g. for 100 digits, <c>a ≈ 30–40</c> is typical.</item>
      /// </list>
      /// </param>
      /// <returns></returns>
      public static TFloat SpougeGamma(TFloat z, int a)
        => SpougeGamma(z, SpougeCoefficients<TFloat>(a));

      #endregion

      #region SpougeLogGamma

      /// <summary>
      /// <para>Spouge's approximation of LogGamma using any <paramref name="coefficients"/>.</para>
      /// </summary>
      /// <param name="z"></param>
      /// <param name="coefficients"></param>
      /// <returns></returns>
      public static TFloat SpougeLogGamma(TFloat z, TFloat[] coefficients)
      {
        var half = TFloat.CreateChecked(0.5);

        if (z < half) // Reflection for z < 0.5
          return TFloat.Log(TFloat.Pi) - TFloat.Log(TFloat.SinPi(z)) - SpougeLogGamma(TFloat.One - z, coefficients);

        var sum = coefficients[0]; // Compute the sum S = c0 + Σ c[k] / (z + k)
        for (var k = 1; k < coefficients.Length; k++)
          sum += coefficients[k] / (z + TFloat.CreateChecked(k));

        var t = z + TFloat.CreateChecked(coefficients.Length);

        return (z - half) * TFloat.Log(t) - t + TFloat.Log(sum);
      }

      /// <summary>
      /// <para>Spouge's approximation of LogGamma. The error of this approximation is less than 2e-10 for a = 12, and decreases rapidly as a increases.</para>
      /// </summary>
      /// <param name="z"></param>
      /// <param name="a">
      /// <list type="table">
      /// <item>For <see cref="float"/> <c>a = 6</c> is sufficient.</item>
      /// <item>For <see cref="double"/> <c>a = 12</c> is the sweet spot, and <c>a = 15</c> gives slightly more accuracy but risks cancellation.</item>
      /// <item>For <see cref="decimal"/> <c>a = 10–12</c> works. The decimal type has high precision but a tiny exponent range, so Exp may overflow.</item>
      /// <item>For arbitrary-precision types choose a proportional to the number of digits you want, e.g. for 100 digits, <c>a ≈ 30–40</c> is typical.</item>
      /// </list>
      /// </param>
      /// <returns></returns>
      public static TFloat SpougeLogGamma(TFloat z, int a)
        => SpougeLogGamma(z, SpougeCoefficients<TFloat>(a));

      #endregion

      #region SpougeCoefficients

      /// <summary>
      /// <para>Spouge's coefficients.</para>
      /// </summary>
      /// <param name="a">
      /// <list type="table">
      /// <item>For <see cref="float"/> <c>a = 6</c> is sufficient.</item>
      /// <item>For <see cref="double"/> <c>a = 12</c> is the sweet spot, and <c>a = 15</c> gives slightly more accuracy but risks cancellation.</item>
      /// <item>For <see cref="decimal"/> <c>a = 10–12</c> works. The decimal type has high precision but a tiny exponent range, so Exp may overflow.</item>
      /// <item>For arbitrary-precision types choose a proportional to the number of digits you want, e.g. for 100 digits, <c>a ≈ 30–40</c> is typical.</item>
      /// </list>
      /// </param>
      /// <returns></returns>
      public static TFloat[] SpougeCoefficients(int a)
      {
        var c = new TFloat[a];

        c[0] = TFloat.Sqrt(TFloat.Tau);

        for (var k = 1; k < a; k++)
        {
          var sign = ((k - 1) % 2 == 0) ? TFloat.One : -TFloat.One;

          var exponent = TFloat.CreateChecked(a - k);

          var numerator = sign * TFloat.Pow(exponent, TFloat.CreateChecked(k) - TFloat.CreateChecked(0.5)) * TFloat.Exp(exponent);
          var denominator = TFloat.CreateChecked(BinaryInteger.Factorial(k - 1));

          c[k] = numerator / denominator;
        }

        return c;
      }

      #endregion

      #region StirlingGamma

      /// <summary>
      /// <para>Stirling's approximation of Gamma. The error of this approximation is less than 1.5e-7 for n ≥ 1, and decreases rapidly as n increases.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling%27s_approximation"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// </summary>
      /// <param name="z"></param>
      /// <returns></returns>
      public static TFloat StirlingGamma(TFloat z)
        => TFloat.Exp(StirlingLogGamma(z));// TFloat.Sqrt(TFloat.Tau / x) * TFloat.Pow(x / TFloat.E, x);

      #endregion

      #region StirlingLogFactorial

      /// <summary>
      /// <para>Stirling's approximation of LogFactorial. The error of this approximation is less than 1.5e-7 for n ≥ 1, and decreases rapidly as n increases.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling%27s_approximation"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TFloat StirlingLogFactorial<TInteger>(TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        if (n <= TInteger.One)
          return TFloat.Zero;

        var x = TFloat.CreateChecked(n);

        return x * TFloat.Log(x) - x + TFloat.CreateChecked(0.5) * TFloat.Log(TFloat.Tau * x);
      }

      #endregion

      #region StirlingLogGamma

      /// <summary>
      /// <para>Stirling's approximation of LogGamma. The error of this approximation is less than 1.5e-7 for n ≥ 1, and decreases rapidly as n increases.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling%27s_approximation"/></para>
      /// </summary>
      /// <param name="z"></param>
      /// <returns></returns>
      public static TFloat StirlingLogGamma(TFloat z)
      {
        var half = TFloat.CreateChecked(0.5);

        // Reflection for small z
        if (z < half)
          return TFloat.Log(TFloat.Pi) - TFloat.Log(TFloat.SinPi(z)) - StirlingLogGamma(TFloat.One - z);

        var result = (z - half) * TFloat.Log(z) - z + half * TFloat.Log(TFloat.Tau); // Core Stirling term.

        var z2 = z * z; // Correction terms (Bernoulli numbers)
        var z3 = z2 * z;
        var z5 = z3 * z2;
        var z7 = z5 * z2;

        result += TFloat.One / (TFloat.CreateChecked(12) * z);
        result -= TFloat.One / (TFloat.CreateChecked(360) * z3);
        result += TFloat.One / (TFloat.CreateChecked(1260) * z5);
        result -= TFloat.One / (TFloat.CreateChecked(1680) * z7);

        return result;
      }

      #endregion
    }

    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region ToBigRational

      /// <summary>
      /// <para>Creates a new <see cref="Numerics.BigRational"/> by approximating over a specified number of iterations.</para>
      /// </summary>
      /// <param name="maxApproximationIterations"></param>
      /// <returns></returns>
      public Numerics.BigRational ToBigRational(int maxApproximationIterations = 101)
      {
        if (TFloat.IsZero(x)) return Numerics.BigRational.Zero;
        if (TFloat.IsInteger(x)) return new Numerics.BigRational(System.Numerics.BigInteger.CreateChecked(x));

        var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
        var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

        System.Numerics.BigInteger A;
        System.Numerics.BigInteger B;

        var a = System.Numerics.BigInteger.Zero;
        var b = System.Numerics.BigInteger.Zero;

        if (x > TFloat.One)
        {
          var xW = TFloat.Truncate(x);

          var ar = ToBigRational(x - xW, maxApproximationIterations);

          return ar + System.Numerics.BigInteger.CreateChecked(xW);
        }

        while (--maxApproximationIterations >= 0 && !TFloat.IsZero(x))
        {
          var r = TFloat.One / x;
          var rR = TFloat.Round(r);

          var rT = System.Numerics.BigInteger.CreateChecked(rR);

          A = Am.Item2 + rT * Am.Item1;
          B = Bm.Item2 + rT * Bm.Item1;

          if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
            break;

          a = A;
          b = B;

          Am = (A, Am.Item1);
          Bm = (B, Bm.Item1);

          x = r - rR;
        }

        return new(a, b);
      }

      #endregion

      #region ToStringWithCustomDecimals

      public string ToStringWithCustomDecimals(int numberOfDecimals = 339)
        => x.ToString(BinaryInteger.CreateFormatStringWithCountDecimals(numberOfDecimals), null);

      #endregion
    }
  }
}
