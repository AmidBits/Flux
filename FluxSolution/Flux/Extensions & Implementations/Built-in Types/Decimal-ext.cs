//#define DECIMAL_SPECIAL_FUNCTIONS
namespace Flux
{
  public static partial class DecimalExtensions
  {
#if DECIMAL_SPECIAL_FUNCTIONS

    private static readonly decimal[] ErfChebCoeffs = new decimal[]
    {
          0.0000000000000000000000000000000m,
          1.1283791670955125738961589031215m,
         -0.3761263890318375246224717447140m,
          0.1128379167095512573896158903122m,
         -0.0268661706451312517600242224070m,
          0.0052239776254421878421051001510m,
         -0.0008248521068510384252821328530m,
          0.0001064647928874315891424279510m,
         -0.0000110827638097849214539060000m
    };

#endif

    extension(System.Decimal)
    {
      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Decimal"/> without losing precision is 79,228,162,514,264,337,593,543,950,335.</para>
      /// <para>The <see cref="System.Decimal"/> type is a base-10 high-precision decimal, which means it can precisely represent integers up to 79,228,162,514,264,337,593,543,950,335 (approximately 7.9×10²⁸). This is because the decimal type has a precision of 28-29 significant digits and does not use floating-point approximations for integers within this range. Beyond this value, precision may be lost.</para>
      /// </summary>
      public static decimal MaxExactInteger => 79228162514264337593543950335m;

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Decimal"/> without losing precision is -79,228,162,514,264,337,593,543,950,335.</para>
      /// <para>The <see cref="System.Decimal"/> type is a base-10 high-precision decimal, which means it can precisely represent integers down to -79,228,162,514,264,337,593,543,950,335 (approximately -7.9×10²⁸). This is because the decimal type has a precision of 28-29 significant digits and does not use floating-point approximations for integers within this range. Beyond this value, precision may be lost.</para>
      /// </summary>
      public static decimal MinExactInteger => -79228162514264337593543950335m;

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a decimal.</para>
      /// </summary>
      public static decimal MaxExactPrimeNumber => 79228162514264337593543950297m;

      /// <summary>
      /// <para>The decimal type has a precision of 28-29 significant digits.</para>
      /// </summary>
      public static int MaxExactSignificantDigits => 28;

      /// <summary>
      /// <para>The default epsilon scalar (1e-27m) used for near-integer functions.</para>
      /// </summary>
      public static decimal NearEqualityEpsilon => 1e-27m;

      #region GetComponents

      /// <summary>
      /// <para>Gets the components that make up a decimal type, i.e. mantissa, scale and sign.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static (System.Numerics.BigInteger Significand, byte Scale, bool Sign) GetComponents(System.Decimal value)
      {
        var bits = decimal.GetBits(value);

        var bytes = System.Runtime.InteropServices.MemoryMarshal.Cast<int, byte>(bits);

        var significand = new System.Numerics.BigInteger(bytes[..12]);
        var scale = bytes[14];
        var sign = bytes[15] != 0;

        return (significand, scale, sign);
      }

      #endregion

      #region GetParts

      /// <summary>
      /// <para>Gets the parts that make up a decimal number, i.e. integer, fraction and fraction as an integer.</para>
      /// <para>Also returns the type components as out parameters, i.e. <paramref name="significand"/>, <paramref name="scale"/> and <paramref name="sign"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="significand"></param>
      /// <param name="scale"></param>
      /// <param name="sign"></param>
      /// <returns></returns>
      public static (System.Numerics.BigInteger IntegerPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsInteger) GetParts(System.Decimal value, out System.Numerics.BigInteger significand, out byte scale, out System.Numerics.BigInteger scaleFactor, out bool sign)
      {
        (significand, scale, sign) = GetComponents(value);

        scaleFactor = System.Numerics.BigInteger.Pow(10, scale);

        var integerPart = significand / scaleFactor;

        var fractionalPartAsInteger = significand - (integerPart * scaleFactor);

        return (
          integerPart,
          value - (decimal)integerPart,
          fractionalPartAsInteger
        );
      }

      /// <summary>
      /// <para>Get the <paramref name="integralPart"/> and the <paramref name="fractionalPart"/> as out parameters, and return the fractional part as a whole number (i.e. with the decimal point stripped).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// </summary>
      /// <param name="number"></param>
      /// <returns>The parts of the decimal number (whole number or integer part, fractional part, fractional part as a whole number).</returns>
      /// <remarks>This operation is unique to decimal.</remarks>
      public static (System.Numerics.BigInteger IntegerPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsWholeNumber) GetParts(System.Decimal value)
        => GetParts(value, out var _, out var _, out var _, out var _);

      #endregion

      #region Native..

      public static decimal NativeDecrement(decimal value)
        => checked(value - 1e-28m);

      public static decimal NativeIncrement(decimal value)
        => checked(value + 1e-28m);

      #endregion

#if DECIMAL_SPECIAL_FUNCTIONS

      public static decimal Beta(decimal x, decimal y)
      {
        var lg = LogGamma(x) + LogGamma(y) - LogGamma(x + y);

        return Exp(lg);
      }

      public static decimal Digamma(decimal x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(x);

        var result = 0m;

        while (x < 8m) // Step 1: Recurrence until x >= 8
        {
          result -= 1m / x;
          x += 1m;
        }

        var inv = 1m / x; // Step 2: Asymptotic expansion
        var inv2 = inv * inv;

        var series = Log(x)
          - 0.5m * inv
          - inv2 / 12m
          + inv2 * inv2 / 120m
          - inv2 * inv2 * inv2 / 252m; // ψ(x) ≈ ln(x) - 1/(2x) - 1/(12x²) + 1/(120x⁴) - 1/(252x⁶)

        return result + series;
      }

      private static decimal ErfAS(decimal x)
      {
        const decimal a1 = 0.254829592m; // Abramowitz–Stegun 7.1.26 coefficients
        const decimal a2 = -0.284496736m;
        const decimal a3 = 1.421413741m;
        const decimal a4 = -1.453152027m;
        const decimal a5 = 1.061405429m;
        const decimal p = 0.3275911m;

        var sign = decimal.UnitSign(x);

        x = decimal.Abs(x);

        var t = 1m / (1m + p * x);

        var poly = (((((a5 * t + a4) * t + a3) * t + a2) * t + a1) * t); // Polynomial evaluation.

        return sign * (1m - poly * Exp(-x * x)); // erf(x) = sign * (1 - poly * exp(-x^2))
      }

      private static decimal ErfChebyshev(decimal x)
      {
        if (x == 0m)
          return 0m;

        var sign = decimal.UnitSign(x);

        x = decimal.Abs(x);

        if (x > 1m) // For large x, use erfc asymptotic
          return sign * (1m - Erfc(x));

        var t = 2m * x - 1m; // x in [0,1] → t in [-1,1]

        decimal b0 = 0m, b1 = 0m, b2 = 0m; // Clenshaw-like evaluation of Chebyshev series.

        for (var j = ErfChebCoeffs.Length - 1; j >= 0; j--)
        {
          b2 = b1;
          b1 = b0;
          b0 = 2m * t * b1 - b2 + ErfChebCoeffs[j];
        }

        var value = 0.5m * (b0 - b2); // Chebyshev sum.

        return sign * value;
      }

      public static decimal Erf(decimal x)
      {
        if (x == 0m)
          return 0m;

        decimal sign = decimal.UnitSign(x);
        decimal ax = decimal.Abs(x);

        if (ax <= 1m)
          return sign * ErfChebyshev(ax);

        if (ax <= 4m)
          return sign * ErfAS(ax);

        return sign * (1m - ErfcAsymptotic(ax)); // large x → use erfc asymptotic
      }

      public static decimal Erfc(decimal x)
      {
        if (x < 0m)
          return 1m + Erf(x); // erfc(-x) = 2 - erfc(x)

        if (x <= 4m)
          return 1m - Erf(x);

        return ErfcAsymptotic(x);
      }

      private static decimal ErfcAsymptotic(decimal x)
      {
        const decimal SqrtPi = 1.7724538509055160272981674833411m; // sqrt(pi)

        if (x <= 0m)
          return 1m - Erf(x); // use regular path for non-large or negative

        if (x < 4m)
          return Erfc(x);     // standard implementation is fine here

        decimal x2 = x * x;
        decimal inv = 1m / x;
        decimal inv2 = 1m / x2;

        // 1 - 1/(2x^2) + 3/(4x^4) - 15/(8x^6)
        decimal series =
            1m
            - 0.5m * inv2
            + 0.75m * inv2 * inv2
            - 1.875m * inv2 * inv2 * inv2;

        decimal prefactor = Exp(-x2) * inv / SqrtPi;

        return prefactor * series;
      }

      public static decimal Exp(decimal x)
      {
        // Handle extreme cases safely

        if (x > 60m)  // exp(60) ≈ 1.1e26, near decimal max
          throw new OverflowException("exp(x) overflows decimal range.");

        if (x < -60m) // exp(-60) ≈ 8e-27, near decimal min
          return 0m;

        decimal low = 0m; // exp(x) is always in (0, +∞)
        decimal high = 1m;

        // Increase high until log(high) >= x
        while (Log(high) < x)
          high *= 2m;

        // Binary search for y such that log(y) = x
        for (int i = 0; i < 93; i++) // Theory says ~93 iterations to reach decimal’s precision limit.
        {
          decimal mid = (low + high) / 2m;
          decimal lm = Log(mid);

          if (lm < x)
            low = mid;
          else
            high = mid;
        }

        return (low + high) / 2m;
      }

      public static decimal Gamma(decimal x)
        => Exp(LogGamma(x));

      public static decimal Log(decimal x)
      {
        const decimal Ln2 = 0.6931471805599453094172321214581765680755001343602552m; // High-precision ln(2) for decimal.

        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(x);

        var k = 0; // --- Range reduction: x = m * 2^k, with m in [0.5, 1) ---

        while (x >= 1m) // Scale down
        {
          x /= 2m;
          k++;
        }

        while (x < 0.5m) // Scale up
        {
          x *= 2m;
          k--;
        }

        // Now x is in [0.5, 1)
        // --- High-precision atanh-style series ---
        // ln(x) = 2 * (t + t^3/3 + t^5/5 + ...)
        // where t = (x - 1) / (x + 1)

        var t = (x - 1m) / (x + 1m);
        var t2 = t * t;

        var sum = 0m;

        var term = t;
        var n = 1;

        while (true) // Converges extremely fast in this interval.
        {
          var add = term / n;

          sum += add;

          if (add == 0m) // Termination: decimal has ~28 digits; once add is 0, we're done.
            break;

          term *= t2;
          n += 2;
        }

        return 2m * sum + k * Ln2;
      }

      public static decimal LogGamma(decimal x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(x);

        const int a = 10;
        var sum = 0m;

        // Precompute coefficients c_k
        // c_0 = sqrt(2π)
        var c0 = 2.506628274631000502415765284811m; // sqrt(2π)
        var c = new decimal[a];
        c[0] = c0;

        for (var k = 1; k < a; k++)
        {
          // c_k = (-1)^(k-1) / ( (k-1)! ) * (a-k)^(k-1/2)
          decimal sign = ((k % 2) == 1) ? 1m : -1m;

          decimal fact = 1m;
          for (int i = 1; i < k; i++)
            fact *= i;

          decimal ak = a - k;
          decimal pow = Pow(ak, k - 0.5m);

          c[k] = sign * pow / fact;
        }

        // Spouge sum
        for (int k = 1; k < a; k++)
          sum += c[k] / (x + k - 1m);

        var xp = x + a - 0.5m;

        return (x - 0.5m) * Log(xp) - xp + Log(c0 + sum);
      }

      public static decimal Polygamma(int n, decimal x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(x);

        var fact = 1m; // n! as decimal.
        for (int i = 2; i <= n; i++)
          fact *= i;

        var sign = ((n % 2) == 1) ? 1m : -1m;

        var result = 0m;

        while (x < 8m) // Step 1: Recurrence until x >= 8.
        {
          result += sign * fact / Pow(x, n + 1);
          x += 1m;
        }

        var inv = 1m / x; // Step 2: Asymptotic expansion.
        var sum = inv;

        var inv2 = inv * inv; // Add a few Bernoulli-based correction terms.

        sum += inv2 / 2m;
        sum += inv2 * inv / 6m;
        sum -= inv2 * inv2 * inv / 30m;
        sum += inv2 * inv2 * inv2 * inv / 42m;

        return result + sign * fact * sum;
      }

      public static decimal Pow(decimal a, decimal b)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(a);

        return Exp(b * Log(a));
      }

      public static decimal Sqrt(decimal x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(x);

        if (x == 0m)
          return 0m;

        var r = (decimal)double.Sqrt((double)x); // Initial guess from double.

        for (int i = 0; i < 8; i++) // Newton refinement.
          r = (r + x / r) / 2m;

        return r;
      }

      public static decimal Zeta(decimal s)
      {
        if (s <= 1m) throw new System.ArgumentOutOfRangeException(nameof(s), "Zeta(s) requires s > 1.");

        var eta = 0m; // Compute eta(s) = Σ (-1)^(n-1) / n^s.
        var sign = 1;

        for (var n = 1; ; n++)
        {
          var term = sign * (1m / Pow(n, s));
          if (term == 0m)
            break;

          eta += term;
          sign = -sign;
        }

        var denom = 1m - Pow(2m, 1m - s); // ζ(s) = η(s) / (1 - 2^(1-s))

        return eta / denom;
      }

#endif
    }
  }
}
