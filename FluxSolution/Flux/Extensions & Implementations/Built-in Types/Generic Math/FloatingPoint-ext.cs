namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region CompareToFractionMidpoint

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
      public static int CompareFractionToMidpoint(TFloat value)
        => CompareFractionToThreshold(value, TFloat.CreateChecked(0.5));

      #endregion

      #region CompareFractionToThreshold

      /// <summary>
      /// <para>Compares the fraction part of <paramref name="value"/> to the specified <paramref name="threshold"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
      /// </summary>
      /// <param name="value">The value to be compared.</param>
      /// <param name="threshold">Must be in the unit interval [0, 1].</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 when <paramref name="value"/> is less than <paramref name="threshold"/>.</para>
      /// <para>0 when <paramref name="value"/> is equal to <paramref name="threshold"/>.</para>
      /// <para>+1 when <paramref name="value"/> is greater than <paramref name="threshold"/>.</para>
      /// </returns>
      public static int CompareFractionToThreshold(TFloat value, TFloat threshold)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(threshold);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(threshold, TFloat.One);

        var floor = TFloat.Floor(value);

        return (value - floor).CompareTo(threshold);
      }

      #endregion

      #region Envelop

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

      #region Special functions

#if DECIMAL_SPECIAL_FUNCTIONS || DOUBLE_SPECIAL_FUNCTIONS
      public static TFloat Gamma(TFloat x)
        => x switch
        {
#if DECIMAL_SPECIAL_FUNCTIONS
          decimal dfp128 => TFloat.CreateChecked(decimal.Gamma(dfp128)),
#endif
#if DOUBLE_SPECIAL_FUNCTIONS
          double fp64 => TFloat.CreateChecked(double.Gamma(fp64)),
          float fp32 => TFloat.CreateChecked(double.Gamma(fp32)),
#endif
          _ => throw new System.NotImplementedException($"Type: {x.GetType()}"),
        };

      public static TFloat LogGamma(TFloat x)
        => x switch
        {
#if DECIMAL_SPECIAL_FUNCTIONS
          decimal dfp128 => TFloat.CreateChecked(decimal.LogGamma(dfp128)),
#endif
#if DOUBLE_SPECIAL_FUNCTIONS
          double fp64 => TFloat.CreateChecked(double.LogGamma(fp64)),
          float fp32 => TFloat.CreateChecked(double.LogGamma(fp32)),
#endif
          _ => throw new System.NotImplementedException($"Type: {x.GetType()}"),
        };
#endif

      #endregion

      #region Truncate

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

      public static TFloat Wrap(TFloat value, TFloat minValue, TFloat maxValue, IntervalNotation notation)
      {
        return notation switch
        {
          IntervalNotation.Closed => WrapClosed(value, minValue, maxValue),
          IntervalNotation.HalfOpenLeft => WrapHalfOpenLeft(value, minValue, maxValue),
          IntervalNotation.HalfOpenRight => WrapHalfOpenRight(value, minValue, maxValue),
          IntervalNotation.Open => WrapOpen(value, minValue, maxValue),
          _ => throw new System.ArgumentOutOfRangeException(nameof(notation)),
        };
      }

      public static TFloat WrapClosed(TFloat value, TFloat minValue, TFloat maxValue)
      {
        var range = (maxValue - minValue) + Number.Ulp(minValue);

        var nrem = Number.EuclideanModulo(value - minValue, range);

        return minValue + nrem;

        //var range = (maxValue - minValue) + Number.GetUlp(minValue);

        //return minValue + (value - minValue - TFloat.Floor((value - minValue) / range) * range);
      }

      public static TFloat WrapHalfOpenLeft(TFloat value, TFloat minValue, TFloat maxValue)
      {
        var openLeftMinValue = minValue + Number.Ulp(minValue);
        var range = maxValue - openLeftMinValue;

        var nrem = Number.EuclideanModulo(value - openLeftMinValue, range);

        return openLeftMinValue + nrem;

        //var shift = minValue + Number.GetUlp(minValue);
        //var range = maxValue - shift;

        //return shift + (value - shift - TFloat.Floor((value - shift) / range) * range);
      }

      public static TFloat WrapHalfOpenRight(TFloat value, TFloat minValue, TFloat maxValue)
      {
        var range = maxValue - minValue;

        var nrem = Number.EuclideanModulo(value - minValue, range);

        return minValue + nrem;

        //var range = maxValue - minValue;

        //return minValue + (value - minValue - TFloat.Floor((value - minValue) / range) * range);
      }

      public static TFloat WrapOpen(TFloat value, TFloat minValue, TFloat maxValue)
      {
        var range = maxValue - minValue;

        var nrem = Number.EuclideanModulo(value - minValue, range);

        var w = minValue + nrem;

        if (w == minValue)
          return minValue + Number.Ulp(minValue);

        return w;

        //var range = maxValue - minValue;

        //var w = minValue + (value - minValue - TFloat.Floor((value - minValue) / range) * range);

        //if (w == minValue)
        //  return minValue + Number.GetUlp(minValue);

        //return w;
      }
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IExponentialFunctions<TFloat>
    {
      /// <summary>
      /// <para>Exponential wave function.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TFloat ExponentialWave(TFloat value, TFloat modulus, TFloat k)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var phase = ModulusOperators.EuclideanModulo(value, modulus); // Phase in [0, modulus).

        var unitInterval = phase / modulus; // Normalize to [0, 1).

        var x = k * unitInterval; // Scale to [0, k].

        return TFloat.Exp(x);
      }
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      /// <summary>
      /// <para>Logarithmic wave function.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TFloat LogarithmicWave<T>(TFloat value, TFloat modulus, TFloat k)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var phase = ModulusOperators.EuclideanModulo(value, modulus); // Phase in [0, modulus).

        var unitInterval = phase / modulus; // Normalize to [0, 1).

        var x = TFloat.One + k * unitInterval; // Shift to [1, 1 + k] to ensure domain is always valid: argument >= 1.

        return TFloat.Log(x);
      }
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IRootFunctions<TFloat>
    {
      #region HelmertsExpansionParameterK1

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
      /// </summary>
      public static TFloat HelmertsExpansionParameterK1(TFloat x)
        => TFloat.Sqrt(TFloat.One + x * x) is var k ? (k - TFloat.One) / (k + TFloat.One) : throw new System.ArithmeticException();

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
    }
  }
}
