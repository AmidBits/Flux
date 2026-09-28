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

      #region Percent..ToPercent..

      public static TFloat PercentAddedToPercentRemove(TFloat percentAdded)
       => TFloat.One / (TFloat.One / percentAdded + TFloat.One);

      public static TFloat PercentRemovedToPercentAdd(TFloat percentAdded)
        => TFloat.One / (TFloat.One / percentAdded - TFloat.One);

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

      #region Temperature conversions

      /// <summary>Convert the temperature specified in Celsius to Fahrenheit.</summary>
      public static TFloat CelsiusToFahrenheit(TFloat celsius) => celsius * TFloat.CreateChecked(1.8) + TFloat.CreateChecked(32);
      /// <summary>Convert the temperature specified in Celsius to Kelvin.</summary>
      public static TFloat CelsiusToKelvin(TFloat celsius) => celsius + TFloat.CreateChecked(273.15);
      /// <summary>Convert the temperature specified in Celsius to Rankine.</summary>
      public static TFloat CelsiusToRankine(TFloat celsius) => (celsius + TFloat.CreateChecked(273.15)) * TFloat.CreateChecked(1.8);
      /// <summary>Convert the temperature specified in Fahrenheit to Celsius.</summary>
      public static TFloat FahrenheitToCelsius(TFloat fahrenheit) => (fahrenheit - TFloat.CreateChecked(32)) / TFloat.CreateChecked(1.8);
      /// <summary>Convert the temperature specified in Fahrenheit to Kelvin.</summary>
      public static TFloat FahrenheitToKelvin(TFloat fahrenheit) => (fahrenheit + TFloat.CreateChecked(459.67)) / TFloat.CreateChecked(1.8);
      /// <summary>Convert the temperature specified in Fahrenheit to Rankine.</summary>
      public static TFloat FahrenheitToRankine(TFloat fahrenheit) => fahrenheit + TFloat.CreateChecked(459.67);
      /// <summary>Convert the temperature specified in Kelvin to Celsius.</summary>
      public static TFloat KelvinToCelsius(TFloat kelvin) => kelvin - TFloat.CreateChecked(273.15);
      /// <summary>Convert the temperature specified in Kelvin to Fahrenheit.</summary>
      public static TFloat KelvinToFahrenheit(TFloat kelvin) => kelvin * TFloat.CreateChecked(1.8) - TFloat.CreateChecked(459.67);
      /// <summary>Convert the temperature specified in Kelvin to Rankine.</summary>
      public static TFloat KelvinToRankine(TFloat kelvin) => kelvin * TFloat.CreateChecked(1.8);
      /// <summary>Convert the temperature specified in Rankine to Celsius.</summary>
      public static TFloat RankineToCelsius(TFloat rankine) => (rankine - TFloat.CreateChecked(459.67)) / TFloat.CreateChecked(1.8);
      /// <summary>Convert the temperature specified in Rankine to Kelvin.</summary>
      public static TFloat RankineToKelvin(TFloat rankine) => rankine / TFloat.CreateChecked(1.8);
      /// <summary>Convert the temperature specified in Rankine to Fahrenheit.</summary>
      public static TFloat RankineToFahrenheit(TFloat rankine) => rankine - TFloat.CreateChecked(459.67);

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
