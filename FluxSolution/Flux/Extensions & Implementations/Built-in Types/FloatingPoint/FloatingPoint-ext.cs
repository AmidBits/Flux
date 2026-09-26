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

      #region Factorial functions (generalized)

      /// <summary>
      /// <para>Generalized rising factorial: x^(n)_rising(h) = x * (x + h) * (x + 2h) * ... * (x + (n-1)h)</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <param name="h">Step size, or increment between factors. Determines how far apart the terms are spaced.</param>
      /// <returns></returns>
      public static TFloat FactorialPower<TInteger>(TFloat x, TInteger n, TFloat h)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        TFloat result = TFloat.One;

        while (n-- > TInteger.Zero)
        {
          result *= x;

          x += h; // increment x by step h
        }

        return result;
      }

      #endregion

      #region GeodeticToGeocentricLatitude

      public static double GeodeticToGeocentricLatitude(double geodeticLatitude, double equatorRadius, double polarRadius)
      {
        var flatteningCorrection = (polarRadius * polarRadius) / (equatorRadius * equatorRadius); // b^2 / a^2

        return double.Atan(flatteningCorrection * double.Tan(geodeticLatitude)); // The forward direction "shrinks" the tangent.
      }

      #endregion

      #region GeocentricToGeodeticLatitude

      public static double GeocentricToGeodeticLatitude(double geocentricLatitude, double equatorRadius, double polarRadius)
      {
        var inverseFlatteningCorrection = (equatorRadius * equatorRadius) / (polarRadius * polarRadius); // a^2 / b^2

        return double.Atan(inverseFlatteningCorrection * double.Tan(geocentricLatitude)); // The reverse direction "unshrinks" the tangent.
      }

      #endregion

      #region Interpolate.. functions

      /// <summary>
      /// <para>Cubic interpolation.</para>
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
      /// <para>Cubic interpolation a'la Paul Burke.</para>
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
      /// <para>Hermite interpolation.</para>
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
      /// <param name="y0"></param>
      /// <param name="y1"></param>
      /// <param name="mu"></param>
      /// <returns></returns>
      public static TFloat InterpolateLinear(TFloat y0, TFloat y1, TFloat mu)
        => (TFloat.One - mu) * y0 + mu * y1;

      #endregion

      #region LogisticMap

      /// <summary>
      /// <para>This nonlinear difference equation is intended to capture two effects.<list type="number"><item>Reproduction where the population will increase at a rate proportional to the current population when the population size is small.</item><item>Starvation (density-dependent mortality) where the growth rate will decrease at a rate proportional to the value obtained by taking the theoretical "carrying capacity" of the environment less the current population.</item></list></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logistic_map"/></para>
      /// <seealso cref="Statistics.PopulationModelRicker(double, double, double)"/>
      /// </summary>
      /// <param name="Xn">The ratio of existing population to maximum possible population (Xn).</param>
      /// <param name="r">A value in the range [0, 4] (r).</param>
      /// <returns>The ratio of population to max possible population in the next generation (Xn + 1)</returns>
      public static TFloat LogisticMap(TFloat Xn, TFloat r)
        => r * Xn * (TFloat.One - Xn);

      #endregion

      #region Percent..ToPercent..

      public static TFloat PercentAddedToPercentRemove(TFloat percentAdded)
       => TFloat.One / (TFloat.One / percentAdded + TFloat.One);

      public static TFloat PercentRemovedToPercentAdd(TFloat percentAdded)
        => TFloat.One / (TFloat.One / percentAdded - TFloat.One);

      #endregion

      #region ProbabilityToOdds

      /// <summary>
      /// <para>Computes the odds (p / (1 - p)) of a probability p.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logit"/></para>
      /// </summary>
      /// <param name="probability">The probability in the range [0, 1].</param>
      /// <returns>The odds of the specified probability in the range [-infinity, +infinity].</returns>
      public static Units.Ratio ProbabilityToOdds(TFloat probability)
        => new(double.CreateChecked(probability), double.CreateChecked(TFloat.One - probability));

      #endregion

      #region RoundMidpoint functions

      public static TFloat RoundMidpointAlternating(TFloat value, ref bool alternatingState)
      {
        var cmp = CompareFractionToMidpoint(value);

        var floor = TFloat.Floor(value);

        if (cmp < 0)
          return floor;

        var ceiling = TFloat.Ceiling(value);

        if (cmp > 0)
          return ceiling;

        return (alternatingState = !alternatingState) ? floor : ceiling;
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
        var range = (maxValue - minValue) + Number.GetUlp(minValue);

        var nrem = Number.EuclideanModulo(value - minValue, range);

        return minValue + nrem;

        //var range = (maxValue - minValue) + Number.GetUlp(minValue);

        //return minValue + (value - minValue - TFloat.Floor((value - minValue) / range) * range);
      }

      public static TFloat WrapHalfOpenLeft(TFloat value, TFloat minValue, TFloat maxValue)
      {
        var openLeftMinValue = minValue + Number.GetUlp(minValue);
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
          return minValue + Number.GetUlp(minValue);

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
      #region Expit

      /// <summary>The expit, which is the inverse of the natural logit, yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
      /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
      public static TFloat Expit(TFloat x)
      => TFloat.One / (TFloat.Exp(-x) + TFloat.One);

      #endregion

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

      #region Logistic

      /// <summary>
      /// <para>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logistic_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Sigmoid_function"/></para>
      /// </summary>
      /// <remarks>The standard logistic function is the logistic function with parameters (k = 1, x0 = 0, L = 1), a.k.a. sigmoid function.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity] (x).</param>
      /// <param name="k">The logistic growth rate or steepness of the curve (k). Default of (1).</param>
      /// <param name="x0">The x-value of the sigmoid's midpoint (x0). Default of (0)</param>
      /// <param name="L">The curve's maximum value (L).</param>
      /// <returns></returns>
      public static TFloat Logistic(TFloat x, TFloat k, TFloat x0, TFloat L)
        => L / (TFloat.Exp(-(k * (x - x0))) + TFloat.One);

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IFloatingPointConstants<TFloat>
    {
      #region GeographicToSpherical

      /// <summary>Converts a geographic-coordinate into a spherical-coordinate.</summary>
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
      #region Logit

      /// <summary>
      /// <para>The logit function, which is the inverse of expit (or the logistic function), is the logarithm of the odds (p / (1 - p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity].</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logit"/></para>
      /// </summary>
      /// <param name="probability">The probability in the range [0, 1].</param>
      /// <returns>The odds of the specified probability in the range [-infinity, +infinity].</returns>
      public static TFloat Logit(TFloat probability)
        => TFloat.Log(TFloat.CreateChecked(ProbabilityToOdds(probability).Value));

      #endregion

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

      #region RoundByPrecision

      /// <summary>
      /// <para>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/> in base <paramref name="radix"/>. The <paramref name="nearestRoundingTies"/> specifies the halfway rounding strategy to use.</para>
      /// <example>
      /// <code>var r = RoundByPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundByTruncatedPrecision{TSelf}(TSelf, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="nearestRoundingTies"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RoundByPrecision<TRadix>(TFloat x, NearestRoundingRule nearestRoundingTies, int significantDigits, TRadix radix)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var scalar = TFloat.Pow(TFloat.CreateChecked(radix), TFloat.CreateChecked(significantDigits));

        return RoundToNearestInteger(x * scalar, nearestRoundingTies) / scalar;
      }

      #endregion

      #region RoundByTruncatedPrecision

      /// <summary>
      /// <para>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> in base <paramref name="radix"/> and then round using the <paramref name="nearestRoundingTies"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</para>
      /// <para><seealso href="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/></para>
      /// <example>
      /// <code>var r = RoundByTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundByPrecision{TValue}(TValue, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="x"></param>
      /// <param name="nearestRoundingTies"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TFloat RoundByTruncatedPrecision<TRadix>(TFloat x, NearestRoundingRule nearestRoundingTies, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var scalar = TFloat.Pow(TFloat.CreateChecked(radix), TFloat.CreateChecked(significantDigits + 1));

        return RoundByPrecision(TFloat.Truncate(x * scalar) / scalar, nearestRoundingTies, significantDigits, radix);
      }

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
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IFloatingPointConstants<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      #region Atan2.. functions

      /// <summary>
      /// <para>This is a signed Atan2 which means the output is in the interval [-PI, +PI] (i.e. the radian equivalent of -180 to 180 degrees).</para>
      /// </summary>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      public static TFloat Atan2Signed(TFloat y, TFloat x)
      {
        if (TFloat.IsNaN(x) || TFloat.IsNaN(y))
          return TFloat.CreateChecked(double.NaN);

        var zero = TFloat.Zero;

        if (x == zero) // Handle x == 0 separately (vertical axis)
        {
          if (y == zero)
            return zero;

          var halfPi = TFloat.Pi / TFloat.CreateChecked(2);

          return y > zero ? halfPi : -halfPi;
        }

        var angle = TFloat.Atan(y / x); // Compute the base angle.

        return x > zero ? angle // Quadrants I and IV → angle is already correct.
          : y >= zero ? angle + TFloat.Pi : angle - TFloat.Pi; // Quadrant II and III → Correction needed: add 180 degrees if in the second quadrant (x < 0, y >= 0) or subtract 180 degrees if in the third quadrant (x < 0, y < 0).
      }

      /// <summary>
      /// <para>This is an unsigned Atan2 which means the output is in the interval [0, 2*PI] (i.e. the radian equivalent of 0 to 360 degrees).</para>
      /// </summary>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      public static TFloat Atan2Unsigned(TFloat y, TFloat x)
        => Atan2Signed(y, x) is var a && a < TFloat.Zero ? a + TFloat.Tau : a;

      #endregion

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

      #region InterpolateCosine

      /// <summary>
      /// <para>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</para>
      /// <para><see href="http://paulbourke.net/miscellaneous/interpolation/"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="y0">Source point.</param>
      /// <param name="y1">Target point.</param>
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
