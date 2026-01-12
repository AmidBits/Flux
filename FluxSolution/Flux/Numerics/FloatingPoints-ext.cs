namespace Flux
{
  public static class FloatingPoints
  {
    private const double DefaultTolerance = 1e-10;

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region CompareToFraction..

      /// <summary>
      /// <para>Compare the fraction part of <paramref name="value"/> to it's midpoint (i.e. its .5).</para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value">The value to be compared.</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 if <paramref name="value"/> is less-than 0.5.</para>
      /// <para>0 if <paramref name="value"/> is equal-to 0.5.</para>
      /// <para>1 if <paramref name="value"/> is greater-than 0.5.</para>
      /// </returns>
      public static int CompareToFractionMidpoint(TFloat x)
        => CompareToFractionPercent(x, TFloat.CreateChecked(0.5));

      /// <summary>
      /// <para>Compares the fraction part of <paramref name="value"/> to the specified <paramref name="percent"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
      /// </summary>
      /// <typeparam name="Float"></typeparam>
      /// <param name="value">The value to be compared.</param>
      /// <param name="percent">Percent in the range [0, 1].</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 when <paramref name="value"/> is less than <paramref name="percent"/>.</para>
      /// <para>0 when <paramref name="value"/> is equal to <paramref name="percent"/>.</para>
      /// <para>1 when <paramref name="value"/> is greater than <paramref name="percent"/>.</para>
      /// </returns>
      public static int CompareToFractionPercent(TFloat x, TFloat percent)
        => (x - TFloat.Floor(x)).CompareTo(percent);

      #endregion

      #region Conversions (decimal degrees, sexagesimal unit subDivision, DM/DMS, etc.)

      /// <summary>
      /// <para>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (wholeDegrees, decimalMinutes), e.g. (32, 13.3).</para>
      /// </summary>
      /// <param name="decimalDegrees"></param>
      /// <returns></returns>
      public static (TFloat wholeDegrees, TFloat decimalMinutes) DecimalDegreesToSexagesimalUnitSubdivisionsDm(TFloat decimalDegrees)
      {
        var absDegrees = TFloat.Abs(decimalDegrees);

        var floorAbsDegrees = TFloat.Floor(absDegrees);

        var wholeDegrees = TFloat.CopySign(floorAbsDegrees, decimalDegrees);
        var decimalMinutes = TFloat.CreateChecked(60) * (absDegrees - floorAbsDegrees);

        return (wholeDegrees, decimalMinutes);
      }

      /// <summary>
      /// <para>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (wholeDegrees, wholeMinutes, decimalSeconds), e.g. (32, 13, 18).</para>
      /// </summary>
      /// <param name="decimalDegrees"></param>
      /// <returns></returns>
      public static (TFloat wholeDegrees, TFloat wholeMinutes, TFloat decimalSeconds) DecimalDegreesToSexagesimalUnitSubdivisionsDms(TFloat decimalDegrees)
      {
        var (wholeDegrees, decimalMinutes) = DecimalDegreesToSexagesimalUnitSubdivisionsDm(decimalDegrees);

        var wholeMinutes = TFloat.Floor(decimalMinutes);
        var decimalSeconds = TFloat.CreateChecked(60) * (decimalMinutes - wholeMinutes);

        return (wholeDegrees, wholeMinutes, decimalSeconds);
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="degrees"></param>
      /// <param name="minutes"></param>
      /// <returns></returns>
      public static TFloat SexagesimalUnitSubdivisionsDmToDecimalDegrees(TFloat degrees, TFloat minutes)
        => degrees + minutes / TFloat.CreateChecked(60);

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="degrees"></param>
      /// <param name="minutes"></param>
      /// <param name="seconds"></param>
      /// <returns></returns>
      public static TFloat SexagesimalUnitSubdivisionsDmsToDecimalDegrees(TFloat degrees, TFloat minutes, TFloat seconds)
        => degrees + minutes / TFloat.CreateChecked(60) + seconds / TFloat.CreateChecked(3600);

      ///// <summary>
      ///// <para>Convert decimal degrees to the traditional sexagsimal unit subdivisions, a.k.a. DMS notation.</para>
      ///// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
      ///// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
      ///// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
      ///// </summary>
      ///// <param name="decimalDegrees"></param>
      ///// <returns></returns>
      //public static (TFloat wholeDegrees, TFloat decimalMinutes, TFloat wholeMinutes, TFloat decimalSeconds) DecimalDegreesToSexagesimalUnitSubdivisions(TFloat decimalDegrees)
      //{
      //  var sixty = TFloat.CreateChecked(60);

      //  var absDegrees = TFloat.Abs(decimalDegrees);
      //  var floorAbsDegrees = TFloat.Floor(absDegrees);
      //  var wholeDegrees = TFloat.CopySign(floorAbsDegrees, decimalDegrees);
      //  var decimalMinutes = sixty * (absDegrees - floorAbsDegrees);
      //  var wholeMinutes = TFloat.Floor(decimalMinutes);
      //  var decimalSeconds = sixty * (decimalMinutes - wholeMinutes);

      //  return (wholeDegrees, decimalMinutes, wholeMinutes, decimalSeconds);
      //}

      ///// <summary>
      ///// <para>Convert the traditional sexagsimal unit subdivisions, a.k.a. DMS notation, to decimal degrees.</para>
      ///// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
      ///// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
      ///// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
      ///// </summary>
      ///// <param name="degrees"></param>
      ///// <param name="minutes"></param>
      ///// <param name="seconds"></param>
      ///// <returns></returns>
      //public static TFloat SexagesimalUnitSubdivisionsToDecimalDegrees(TFloat degrees, TFloat minutes, TFloat seconds)
      //  => degrees + minutes / TFloat.CreateChecked(60) + seconds / TFloat.CreateChecked(3600);

      #endregion

      #region Envelop

      /// <summary>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="x"></param>
      /// <returns></returns>
      public static TFloat Envelop(TFloat x)
        => TFloat.IsNegative(x) ? TFloat.Floor(x) : TFloat.Ceiling(x);

      /// <summary>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x"></param>
      /// <param name="digits"></param>
      /// <returns></returns>
      public static TFloat Envelop(TFloat x, int digits)
      {
        var m = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, digits));

        return Envelop(x * m) / m;
      }

      #endregion

      #region Harmonic progression

      public static TFloat HarmonicMean(params System.Collections.Generic.IEnumerable<TFloat> terms)
      {
        var sum = terms.Select(n => TFloat.One / n).Sum(out var count);

        return TFloat.CreateChecked(count) / sum;
      }

      public static TFloat HarmonicMeanOfTwoTerms(TFloat a, TFloat b)
        => (TFloat.CreateChecked(2) * a * b) / (a + b);

      public static TFloat HarmonicMeanOfThreeTerms(TFloat a, TFloat b, TFloat c)
        => (TFloat.CreateChecked(3) * a * b * c) / (a * b + b * c + c * a);

      public static System.Collections.Generic.IEnumerable<TFloat> HarmonicSequence(TFloat a, TFloat d)
        => Numbers.ArithmeticSequence(a, d).Select(an => TFloat.One / an);

      public static TFloat HarmonicSequenceNthTerm<TInteger>(TFloat a, TFloat d, TInteger n)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
        => TFloat.One / (a + (TFloat.CreateChecked(n) - TFloat.One) * d);

      #endregion

      #region Interpolate..

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

      //public static (TInteger IntegralTowardZero, TInteger IntegralAwayFromZero, TInteger NearestIntegral, TFloat Value) RoundToIntegrals<TInteger>(TFloat value, TFloat absoluteTolerance, TFloat relativeTolerance, System.MidpointRounding midpointRounding = MidpointRounding.ToEven)
      //  where TInteger : System.Numerics.INumber<TInteger>
      //{
      //  var (itz, iafz) = value.GetSurroundingIntegrals(absoluteTolerance, relativeTolerance, midpointRounding);

      //  var ni = NumberFunctions.RoundToNearest(value, (HalfRounding)midpointRounding, false, [itz, iafz]);

      //  return (TInteger.CreateChecked(itz), TInteger.CreateChecked(iafz), TInteger.CreateChecked(ni), value);
      //}

      //public static (TInteger IntegralTowardZero, TInteger IntegralAwayFromZero, TInteger NearestIntegral, TFloat Value) RoundToIntegrals<TInteger>(TFloat value)
      //  where TInteger : System.Numerics.INumber<TInteger>
      //{
      //  var tolerance = TFloat.CreateChecked(DefaultTolerance);

      //  return RoundToIntegrals<TFloat, TInteger>(value, tolerance, tolerance);
      //}

      /// <summary>
      /// <para>Machine epsilon computed on the fly.</para>
      /// </summary>
      public static TFloat MachineEpsilon()
      {
        var epsilon = TFloat.One;

        while (epsilon / TFloat.CreateChecked(2) is var halfEpsilon && (TFloat.One + halfEpsilon) > TFloat.One)
          epsilon = halfEpsilon;

        return epsilon;
      }

      #region RoundHalf

      /// <summary>
      /// <para>Rounds a value to the nearest integer, resolving halfway cases using the specified <see cref="HalfRounding"/> <paramref name="mode"/>.</para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TFloat RoundHalf(TFloat x, HalfRounding mode)
        => mode switch
        {
          HalfRounding.ToEven or
          HalfRounding.AwayFromZero or
          HalfRounding.TowardZero or
          HalfRounding.ToNegativeInfinity or
          HalfRounding.ToPositiveInfinity => TFloat.Round(x, (MidpointRounding)(int)mode), // Use built-in .NET functionality for standard cases.
          HalfRounding.ToAlternating => RoundHalfToAlternating(x),
          HalfRounding.ToOdd => RoundHalfToOdd(x),
          HalfRounding.ToRandom => RoundHalfToRandom(x),
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
        };

      #endregion

      #region SurroundingIntegralsWithTolerance

      /// <summary>
      /// <para>Gets the next larger and smaller integrals.</para>
      /// </summary>
      /// <param name="absoluteTolerance"></param>
      /// <param name="relativeTolerance"></param>
      /// <param name="midpointRounding"></param>
      /// <returns></returns>
      public static (TFloat Floor, TFloat Ceiling) GetSurroundingIntegrals(TFloat x, TFloat absoluteTolerance, TFloat relativeTolerance, System.MidpointRounding midpointRounding = MidpointRounding.ToEven)
      {
        if (!TFloat.IsZero(x))
        {
          var ivalue = TFloat.Round(x, midpointRounding);

          var eqwt = Numbers.EqualsWithinAbsoluteTolerance(x, ivalue, absoluteTolerance) || Numbers.EqualsWithinRelativeTolerance(x, ivalue, relativeTolerance);

          var ivalueafz = TFloat.Ceiling(x);
          var ivaluetz = TFloat.Floor(x);

          if (eqwt)
          {
            if (x < ivalue) // The value and its integer is considered equal, but the specified value is less, so the floor will be off, slightly less, and needs to be equal to ceiling. (The ceiling, same issue, happens to be correct.)
              ivaluetz = ivalueafz;
            else if (x > ivalue) // The value and its integer is considered equal, but the specified value is greater, so the ceiling will be off, slightly greater, and needs to be equal to floor. (The floor, same issue, happens to be correct.)
              ivalueafz = ivaluetz;
          }

          return TFloat.IsNegative(x)
            ? (ivalueafz, ivaluetz)
            : (ivaluetz, ivalueafz);
        }

        return (x, x);
      }

      /// <summary>
      /// <para>Gets the next larger and smaller integrals.</para>
      /// </summary>
      /// <returns></returns>
      public static (TFloat Floor, TFloat Ceiling) GetSurroundingIntegrals(TFloat x)
      {
        var tolerance = TFloat.CreateChecked(DefaultTolerance);

        return GetSurroundingIntegrals(x, tolerance, tolerance);
      }

      #endregion

      #region Truncate

      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x"></param>
      /// <param name="digits"></param>
      /// <returns></returns>
      public static TFloat Truncate(TFloat x, int digits)
      {
        var m = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, digits));

        return TFloat.Truncate(x * m) / m;
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region Harmonic progression

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
        => Numbers.Rescale(TFloat.Log(y, radix), TFloat.Log(y0, radix), TFloat.Log(y1, radix), x0, x1); // Extract the numbers and use the standard Rescale() function for the math.
    }

    extension<TFloat>(TFloat)
    where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
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
      => TFloat.Pow(radix, Numbers.Rescale(x, x0, x1, TFloat.Log(y0, radix), TFloat.Log(y1, radix))); // Extract the numbers and use the standard Rescale() function for the math.
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
      public static TFloat RoundByPrecision<TRadix>(TFloat x, HalfRounding mode, int significantDigits, TRadix radix)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var scalar = TFloat.Pow(TFloat.CreateChecked(Units.Radix.AssertMember(radix)), TFloat.CreateChecked(significantDigits));

        return RoundHalf(x * scalar, mode) / scalar;
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
      public static TFloat RoundByTruncatedPrecision<TRadix>(TFloat x, HalfRounding mode, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var scalar = TFloat.Pow(TFloat.CreateChecked(Units.Radix.AssertMember(radix)), TFloat.CreateChecked(significantDigits + 1));

        return RoundByPrecision(TFloat.Truncate(x * scalar) / scalar, mode, significantDigits, radix);
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
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
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
    }

    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region ToBigRational

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

        for (var counter = 0; counter < maxApproximationIterations && !TFloat.IsZero(x); counter++)
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

    #region RoundHalfToAlternating

    private static bool m_roundHalfAlternatingState; // Internal state.

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    private static TFloat RoundHalfToAlternating<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      var cmp = CompareToFractionMidpoint(value);

      var floor = TFloat.Floor(value);

      if (cmp < 0)
        return floor;

      var ceiling = TFloat.Ceiling(value);

      if (cmp > 0)
        return ceiling;

      return (m_roundHalfAlternatingState = !m_roundHalfAlternatingState) ? floor : ceiling;
    }

    #endregion

    #region RoundHalfToOdd

    /// <summary>
    /// <para>Common rounding: round half, bias: odd.</para>
    /// <para><see cref="HalfRounding.ToOdd"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    private static TFloat RoundHalfToOdd<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
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

    #endregion

    #region RoundHalfToRandom

    /// <summary>
    /// <para><see cref="HalfRounding.ToRandom"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    private static TFloat RoundHalfToRandom<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
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

    public static string ToStringWithCountDecimals<TFloat>(this TFloat value, int numberOfDecimals = 339)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => value.ToString(BinaryIntegers.GetFormatStringWithCountDecimals(numberOfDecimals), null);
  }
}
