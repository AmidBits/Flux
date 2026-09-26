//#define DECIMAL_TRANSCENDENTAL_FUNCTIONS
//#define RootFindingAndNumericalSolvers
//#define DECIMAL_SPECIAL_FUNCTIONS

namespace Flux
{
  public enum WrapMode
  {
    /// <summary>
    /// <para>Swap min/max if reversed.</para>
    /// </summary>
    Normalized,
    /// <summary>
    /// <para>Throw if min >= max.</para>
    /// </summary>
    Strict,
    /// <summary>
    /// <para>Symmetric wrap around the midpoint of the interval.</para>
    /// </summary>
    Centered
  }

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

    #region Tables of scale factors (negative and positive)

    private static readonly decimal[] m_decimalScaleFactorsNegative =
    {
      1m,
      0.1m,
      0.01m,
      0.001m,
      0.0001m,
      0.00001m,
      0.000001m,
      0.0000001m,
      0.00000001m,
      0.000000001m,
      0.0000000001m,
      0.00000000001m,
      0.000000000001m,
      0.0000000000001m,
      0.00000000000001m,
      0.000000000000001m,
      0.0000000000000001m,
      0.00000000000000001m,
      0.000000000000000001m,
      0.0000000000000000001m,
      0.00000000000000000001m,
      0.000000000000000000001m,
      0.0000000000000000000001m,
      0.00000000000000000000001m,
      0.000000000000000000000001m,
      0.0000000000000000000000001m,
      0.00000000000000000000000001m,
      0.000000000000000000000000001m,
      0.0000000000000000000000000001m,
    };

    private static readonly decimal[] m_decimalScaleFactorsPositive =
    {
      1m,
      10m,
      100m,
      1000m,
      10000m,
      100000m,
      1000000m,
      10000000m,
      100000000m,
      1000000000m,
      10000000000m,
      100000000000m,
      1000000000000m,
      10000000000000m,
      100000000000000m,
      1000000000000000m,
      10000000000000000m,
      100000000000000000m,
      1000000000000000000m,
      10000000000000000000m,
      100000000000000000000m,
      1000000000000000000000m,
      10000000000000000000000m,
      100000000000000000000000m,
      1000000000000000000000000m,
      10000000000000000000000000m,
      100000000000000000000000000m,
      1000000000000000000000000000m,
      10000000000000000000000000000m
    };

    #endregion

    extension(System.Decimal)
    {
      /// <summary>
      /// <para>The base epsilon 1e-27m → about the scale where double‑precision (binary64) stops reliably distinguishing differences.</para>
      /// <para>A tolerance for “close enough” comparisons — the smallest meaningful difference before rounding noise dominates.</para>
      /// <para>The default epsilon scalar (1e-27m) used for near-integer functions.</para>
      /// </summary>
      public static decimal EngineeringEpsilon => 1e-27m;

      public static decimal MachineEpsilon => 1e-28m;

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

      #region ExtractDecimalFields

      /// <summary>
      /// <para>Gets the components that make up a decimal type, i.e. mantissa, scale and sign.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static (System.Int128 Integer, int Scale, int SignBit) ExtractDecimalFields(System.Decimal value)
      {
        var bits = decimal.GetBits(value);

        //var bytes = System.Runtime.InteropServices.MemoryMarshal.Cast<int, byte>(bits);

        //var integer96 = System.BitConverter.ToInt128(bytes[..12]);
        //var scale = bytes[14];
        //var sign = (bytes[15] & 0x80) >>> 7;

        var scale = (bits[3] >> 16) & 0xFF;
        var sign = bits[3] >>> 31;

        var integer96 = ((System.Int128)bits[2] << 64) | ((System.Int128)bits[1] << 32) | bits[0];

        return (integer96, scale, sign);
      }

      #endregion

      #region GetDecimalParts

      /// <summary>
      /// <para>Gets the parts that make up a decimal number, i.e. integer, fraction and fraction as an integer.</para>
      /// <para>Also returns the type components as out parameters, i.e. <paramref name="significand"/>, <paramref name="scale"/> and <paramref name="sign"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="significand"></param>
      /// <param name="scale"></param>
      /// <param name="sign"></param>
      /// <returns></returns>
      public static (System.Numerics.BigInteger IntegralPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsInteger) GetDecimalParts(System.Decimal value, out System.Numerics.BigInteger scaleFactor)
      {
        var (significand, scale, _) = ExtractDecimalFields(value);

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
      public static (System.Numerics.BigInteger IntegerPart, decimal FractionalPart, System.Numerics.BigInteger FractionalPartAsWholeNumber) GetDecimalParts(System.Decimal value)
        => GetDecimalParts(value, out var _);

      #endregion

      #region Pow10

      /// <summary>
      /// <para>Computes 10 raised to the power of the specified exponent, which can be in the range [-28, 28].</para>
      /// </summary>
      /// <param name="exponent">Allowed range: [-28, 28]</param>
      /// <returns></returns>
      private static decimal Pow10(int exponent)
      {
        if (exponent >= 0)
        {
          System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);
          System.ArgumentOutOfRangeException.ThrowIfGreaterThan(exponent, 28);

          return m_decimalScaleFactorsPositive[exponent];
        }
        else
        {
          System.ArgumentOutOfRangeException.ThrowIfLessThan(exponent, -28);

          return m_decimalScaleFactorsNegative[-exponent];
        }
      }

      #endregion

      #region ULP functions

      /// <summary>
      /// <para>Gets the unit in the last place (ULP) of a <see langword="decimal"/> number.</para>
      /// <para>Since decimal is not a floating-point type, the ULP is derived from the stored scale inside the 128‑bit layout returned by decimal.GetBits(<paramref name="value"/>).</para>
      /// <para>A decimal never fails to produce a ULP, because every valid decimal value always has a well‑defined ULP, and that ULP is always: 10^-scale.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static decimal GetDecimalUlp(decimal value)
      {
        var bits = decimal.GetBits(value);
        var scale = (bits[3] >> 16) & 0xFF; // 0–28
        return m_decimalScaleFactorsNegative[scale];
      }

      /// <summary>
      /// <para>Decrements a decimal value by one unit in the last place (ULP).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static decimal UlpDecrement(decimal value)
      {
        var ulp = GetDecimalUlp(value);

        return checked(value - ulp);
      }

      /// <summary>
      /// <para>Increments a decimal value by one unit in the last place (ULP).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static decimal UlpIncrement(decimal value)
      {
        var ulp = GetDecimalUlp(value);

        return checked(value + ulp);
      }

      #endregion

      public static int Frscale(decimal value, out decimal significand)
      {
        if (value == 0m)
        {
          significand = 0m;
          return 0;
        }

        var (integer, scale, signBit) = decimal.ExtractDecimalFields(value);

        significand = (decimal)integer;

        if (signBit != 0)
          significand = -significand;

        return scale; // unbiased decimal exponent
      }

      public static decimal Ldscale(decimal significand, int scale)
      {
        scale = int.Clamp(scale, 0, 28);

        return significand * Pow10(-scale);
      }

      public static decimal ScaleD(decimal value, int decimalExponent)
        => value * Pow10(decimalExponent);

      ///// <summary>
      ///// <para>Snaps a decimal value to the nearest integer if it is within one unit in the last place (ULP) of that integer.</para>
      ///// <para>If the value is near an integer → snap. If not → return the original value.</para>
      ///// </summary>
      ///// <param name="value"></param>
      ///// <returns></returns>
      //public static decimal SnapIfNearInteger(decimal value)
      //  => IsNearInteger(value, out var integer) ? integer : value;

      #region WrapToInterval

      /// <summary>
      /// <para>Wraps a decimal value to a specified interval [minValue, maxValue] according to the specified wrap mode and interval notation.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="wrapMode"></param>
      /// <param name="intervalNotation"></param>
      /// <param name="epsilon"></param>
      /// <returns></returns>
      public static decimal WrapToInterval(decimal value, decimal minValue, decimal maxValue, WrapMode wrapMode = WrapMode.Normalized, IntervalNotation intervalNotation = IntervalNotation.HalfOpenRight, decimal epsilon = 0m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(epsilon);

        if (wrapMode == WrapMode.Strict)
          System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);
        else if (wrapMode == WrapMode.Normalized && maxValue < minValue)
          (minValue, maxValue) = (maxValue, minValue);
        else if (wrapMode == WrapMode.Centered)
        {
          var mid = (minValue + maxValue) / 2m;
          var half = (maxValue - minValue) / 2m;

          return mid + WrapToInterval(value - mid, -half, half, WrapMode.Normalized, intervalNotation, epsilon);
        }

        var range = maxValue - minValue;
        if (range == 0m)
          return minValue;

        var wrapped = (value - minValue) % range;
        if (wrapped < 0m)
          wrapped += range;

        var result = wrapped + minValue;

        var eps = (epsilon != 0m) ? epsilon : GetDecimalUlp(result);

        return intervalNotation switch
        {
          IntervalNotation.Closed => result,
          IntervalNotation.HalfOpenRight => result >= maxValue - eps ? minValue : result,
          IntervalNotation.HalfOpenLeft => result <= minValue + eps ? maxValue : result,
          IntervalNotation.Open => (result <= minValue + eps) ? minValue + eps : (result >= maxValue - eps) ? maxValue - eps : result,
          _ => result,
        };
      }

      #endregion

#if DECIMAL_TRANSCENDENTAL_FUNCTIONS

      #region Exp

      /// <summary>
      /// <para>Computes the exponential of a decimal number using range reduction and a polynomial approximation.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static decimal Exp(decimal x)
      {
        if (x == 0m) return 1m;

        var kReal = x * INV_LN2; // Range reduction: x = k*ln(2) + r.
        var k = (int)decimal.Round(kReal); // k = nearest integer to x / ln(2).

        var r = x - (k * LN2);

        // Compute exp(r) using polynomial
        var expR = ExpReduced(r);

        // Recompose: exp(x) = exp(r) * 2^k
        var twoPowK = PowInteger(2m, k);

        return expR * twoPowK;
      }

      /// <summary>
      /// <para>Computes exp(r) for r in [-0.35, 0.35] using a minimax polynomial approximation.</para>
      /// </summary>
      /// <param name="r"></param>
      /// <returns></returns>
      private static decimal ExpReduced(decimal r)
      {
        // Minimax polynomial for exp(r) on [-0.35, 0.35]
        // Degree 8 — good balance of speed and accuracy
        decimal r2 = r * r;
        decimal r3 = r2 * r;
        decimal r4 = r3 * r;
        decimal r5 = r4 * r;
        decimal r6 = r5 * r;
        decimal r7 = r6 * r;
        decimal r8 = r7 * r;

        return
            1m +
            r +
            r2 / 2m +
            r3 / 6m +
            r4 / 24m +
            r5 / 120m +
            r6 / 720m +
            r7 / 5040m +
            r8 / 40320m;
      }

      #endregion

      #region Log


      /// <summary>
      /// <para>Computes the natural logarithm of a decimal number using Halley's method for root-finding.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static decimal Log(decimal x)
      {
        if (x <= 0m)
          throw new ArgumentOutOfRangeException(nameof(x));

        // Fast path for values near 1
        if (x > 0.9m && x < 1.1m)
          return LogNearOne(x);

        // General path
        return LogHalley(x);
      }

      /// <summary>
      /// <para>Computes the logarithm of a decimal number using Halley's method for root-finding.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      private static decimal LogHalley(decimal x)
      {
        // Initial guess using double precision
        double xd = (double)x;
        double ad = Math.Log(xd);
        decimal a = (decimal)ad;

        for (int i = 0; i < 8; i++)
        {
          decimal ea = Exp(a);     // your full decimal Exp
          decimal num = ea - x;
          decimal den = ea + x;

          if (den == 0m)
            break;

          decimal delta = 2m * (num / den);
          a -= delta;

          if (decimal.Abs(delta) < 1e-28m)
            break;
        }

        return a;
      }

      /// <summary>
      /// <para>Computes the logarithm of a decimal number near 1 using the Taylor series expansion.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      private static decimal LogNearOne(decimal x)
      {
        decimal y = (x - 1m) / (x + 1m);
        decimal y2 = y * y;

        decimal term = y;
        decimal sum = 0m;

        // Odd terms: 1, 3, 5, ..., 39
        for (int n = 1; n <= 39; n += 2)
        {
          sum += term / n;
          term *= y2;
        }

        return 2m * sum;
      }

      /// <summary>
      /// <para>Computes the logarithm of a decimal number with a specified base.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="baseValue"></param>
      /// <returns></returns>
      public static decimal Log(decimal x, decimal baseValue)
        => Log(x) / Log(baseValue);

      /// <summary>
      /// <para>Computes the base-10 logarithm of a decimal number.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static decimal Log10(decimal x)
        => Log(x) / LOG10;

      /// <summary>
      /// <para>Computes the base-2 logarithm of a decimal number.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static decimal Log2(decimal x)
        => Log(x) / LOG2;

      #endregion

      #region Pow

      /// <summary>
      /// <para>Computes a^b for decimal b using the identity a^b = exp(b * log(a)).</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static decimal Pow(decimal a, decimal b)
      {
        if (b == 0m)
          return 1m;

        if (b == 1m)
          return a;

        if (b == decimal.Truncate(b)) // If integer exponent.
          return PowInteger(a, (int)b);

        return PowDecimal(a, b); // Fractional exponent path.
      }

      /// <summary>
      /// <para>Computes a^b for decimal b using the identity a^b = exp(b * log(a)).</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      private static decimal PowDecimal(decimal a, decimal b)
      {
        if (a <= 0m) throw new System.ArgumentOutOfRangeException(nameof(a), "Fractional powers are only defined here for a > 0.");

        var i = (int)decimal.Truncate(b);
        var f = b - i;

        return PowInteger(a, i) * Exp(f * Log(a));
      }

      /// <summary>
      /// <para>Computes a^n for integer n using exponentiation by squaring.</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      private static decimal PowInteger(decimal a, int n)
      {
        if (n < 0)
          return 1m / PowInteger(a, -n);

        var result = 1m;
        var baseVal = a;

        while (n > 0)
        {
          if ((n & 1) == 1)
            result *= baseVal;

          baseVal *= baseVal;
          n >>= 1;
        }

        return result;
      }

      #endregion

      #region Cos, Sin, Tan

      /// <summary>
      /// <para>Computes the cosine of a decimal number using range reduction and a polynomial approximation.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static decimal Cos(decimal x)
      {
        ReduceToPiRange(ref x);

        // Polynomial approximation for cos(x) on [-π/2, π/2]
        // cos(x) ≈ 1 - x^2/2 + x^4/24 - x^6/720 + x^8/40320
        decimal x2 = x * x;

        decimal result = 1m;
        decimal term = -x2 / 2m;
        result += term;

        term *= -x2 / 12m;   // x^4/24 = previous * (-x^2/12)
        result += term;

        term *= -x2 / 30m;   // x^6/720 = previous * (-x^2/30)
        result += term;

        term *= -x2 / 56m;   // x^8/40320 = previous * (-x^2/56)
        result += term;

        return result;
      }

      /// <summary>
      /// <para>Computes the sine of a decimal number using range reduction and a polynomial approximation.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static decimal Sin(decimal x)
      {
        ReduceToPiRange(ref x);

        // Further reduce into [-π/2, π/2]
        if (x > PI_HALF)
          return Cos(x - PI_HALF);
        if (x < -PI_HALF)
          return -Cos(x + PI_HALF);

        // Polynomial approximation for sin(x) on [-π/2, π/2]
        // sin(x) ≈ x - x^3/6 + x^5/120 - x^7/5040 + x^9/362880
        decimal x2 = x * x;

        decimal term = x;
        decimal result = term;

        term *= -x2 / 6m;
        result += term;

        term *= -x2 / 20m;   // (x^5)/120 = previous * (-x^2/20)
        result += term;

        term *= -x2 / 42m;   // (x^7)/5040 = previous * (-x^2/42)
        result += term;

        term *= -x2 / 72m;   // (x^9)/362880 = previous * (-x^2/72)
        result += term;

        return result;
      }

      /// <summary>
      /// Computes tan(x) for a decimal x using sin(x)/cos(x) with singularity protection.
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      /// <exception cref="System.OverflowException"></exception>
      public static decimal Tan(decimal x)
      {
        ReduceToPiRange(ref x);

        var s = Sin(x);
        var c = Cos(x);

        if (decimal.Abs(c) < 1e-28m) throw new System.OverflowException("tan(x) is undefined near odd multiples of π/2."); // Protect against division near cos(x) = 0.

        return s / c;
      }

      /// <summary>
      /// <para>Reduces a decimal x into the range [-π, π] using modulo arithmetic.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      private static void ReduceToPiRange(ref decimal x)
      {
        x %= TWO_PI;

        if (x < 0m)
          x += TWO_PI;

        if (x > PI)
          x -= TWO_PI;
      }

      #endregion

      #region Acos, Asin, Atan, Atan2

      /// <summary>
      /// <para>Computes the arccosine of a decimal number using the identity acos(x) = atan2( sqrt(1 - x^2), x ).</para>
      /// </summary>
      /// <param name="x">[-1, 1]</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static decimal Acos(decimal x)
      {
        // Domain check
        if (x < -1m || x > 1m)
          throw new ArgumentOutOfRangeException(nameof(x), "acos(x) is only defined for -1 ≤ x ≤ 1.");

        // acos(x) = atan2( sqrt(1 - x^2), x )
        // This identity is stable for decimal and avoids cancellation.
        decimal oneMinusX2 = 1m - x * x;

        // sqrt(1 - x^2)
        decimal y = Sqrt(oneMinusX2);

        // Handle endpoints exactly
        if (x == 1m)
          return 0m;
        if (x == -1m)
          return PI;

        return Atan2(y, x);
      }

      /// <summary>
      /// <para>Computes the arcsine of a decimal number using the identity asin(x) = atan( x / sqrt(1 - x^2) ).</para>
      /// </summary>
      /// <param name="x">[-1, 1]</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static decimal Asin(decimal x)
      {
        // Domain check
        if (x < -1m || x > 1m)
          throw new ArgumentOutOfRangeException(nameof(x), "asin(x) is only defined for -1 ≤ x ≤ 1.");

        // Odd symmetry: asin(-x) = -asin(x)
        if (x < 0m)
          return -Asin(-x);

        // asin(x) = atan( x / sqrt(1 - x^2) )
        // This identity is stable for decimal and avoids cancellation.
        decimal oneMinusX2 = 1m - x * x;

        // sqrt(1 - x^2)
        decimal denom = Sqrt(oneMinusX2);

        // Handle x = 1 exactly
        if (denom == 0m)
          return PI_HALF;

        return Atan(x / denom);
      }

      /// <summary>
      /// <para>Computes the arctangent of a decimal number using a rational approximation.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static decimal Atan(decimal x)
      {
        // Handle sign symmetry: atan(-x) = -atan(x)
        if (x < 0m)
          return -Atan(-x);

        // For very large x, atan(x) → π/2
        if (x > 1e28m)
          return PI_HALF;

        // Range reduction:
        // If x > 1, use atan(x) = π/2 - atan(1/x)
        if (x > 1m)
          return PI_HALF - Atan(1m / x);

        // Now x ∈ [0, 1]
        // Use a minimax rational approximation:
        //
        // atan(x) ≈ x * (a1 + a2*x^2 + a3*x^4 + a4*x^6) /
        //               (1 + b1*x^2 + b2*x^4 + b3*x^6)
        //
        // Coefficients adapted for decimal precision.

        decimal x2 = x * x;

        decimal a1 = 1.0m;
        decimal a2 = -0.3333314528m;
        decimal a3 = 0.1999355085m;
        decimal a4 = -0.1420889944m;

        decimal b1 = 1.0m;
        decimal b2 = 0.4667168334m;
        decimal b3 = 0.2566383220m;

        decimal numerator =
            a1 + a2 * x2 + a3 * (x2 * x2) + a4 * (x2 * x2 * x2);

        decimal denominator =
            b1 + b2 * x2 + b3 * (x2 * x2);

        return x * (numerator / denominator);
      }

      /// <summary>
      /// <para>Computes the arctangent of y/x using the signs of both arguments to determine the correct quadrant.</para>
      /// </summary>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException"></exception>
      /// <exception cref="InvalidOperationException"></exception>
      public static decimal Atan2(decimal y, decimal x)
      {
        // Handle x > 0: simple case
        if (x > 0m)
          return Atan(y / x);

        // x < 0: adjust by π
        if (x < 0m)
        {
          if (y >= 0m)
            return Atan(y / x) + PI;
          else
            return Atan(y / x) - PI;
        }

        // x == 0: vertical axis
        if (x == 0m)
        {
          if (y > 0m)
            return PI_HALF;
          if (y < 0m)
            return -PI_HALF;

          // x == 0 and y == 0: undefined
          throw new ArgumentException("atan2(0,0) is undefined.");
        }

        // Should never reach here
        throw new InvalidOperationException("Unexpected atan2 case.");
      }

      #endregion

#endif

#if RootFindingAndNumericalSolvers

      /// <summary>
      /// <para>Computes the cube root of a decimal number using Newton's method.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static decimal Cbrt(decimal x)
      {
        if (x == 0m || x == 1m) // Cube root is defined for all real x.
          return x;

        var y = (decimal)double.Cbrt((double)x); // Initial guess using double precision.

        for (int i = 0; i < 20; i++) // Newton iteration.
        {
          var yPrev = y;

          var y2 = y * y; // y^2

          y = (2m * y + x / y2) / 3m; // Newton update: y = (2y + x / y^2) / 3

          if (decimal.Abs(y - yPrev) < 1e-28m)
            break;
        }

        return y;
      }

      /// <summary>
      /// <para>Computes the n-th root of a decimal number using Newton's method.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static decimal RootN(decimal x, int n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        if (x < 0m && (n % 2 == 0))
          throw new System.ArgumentOutOfRangeException(nameof(x), "Even roots of negative numbers are not real.");

        if (x == 0m || x == 1m)
          return x;

        var y = (decimal)double.Pow((double)x, 1.0 / n); // Initial guess using double.

        for (var i = 0; i < 20; i++) // Newton iteration.
        {
          var yPrev = y;

          var yPow = PowInteger(y, n - 1); // y^(n-1)

          y = ((n - 1) * y + x / yPow) / n;

          if (decimal.Abs(y - yPrev) < 1e-28m)
            break;
        }

        return y;
      }

      /// <summary>
      /// <para>Computes the square root of a decimal number using Newton's method.</para>
      /// <para>Quadratic convergence, stable for all positive x.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static decimal Sqrt(decimal x)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(x);

        if (x == 0m || x == 1m)
          return x;

        var y = (decimal)double.Sqrt((double)x); // Initial guess using double precision.

        for (var i = 0; i < 20; i++) // Newton iteration.
        {
          var yPrev = y;

          y = 0.5m * (y + x / y);

          if (decimal.Abs(y - yPrev) < 1e-28m)
            break;
        }

        return y;
      }

#endif

#if DECIMAL_SPECIAL_FUNCTIONS

      /// <summary>
      /// Computes the Beta function B(x, y) using Gamma and LogGamma.
      /// Domain: x > 0, y > 0
      /// </summary>
      public static decimal Beta(decimal x, decimal y)
      {
        if (x <= 0m || y <= 0m) throw new System.ArgumentOutOfRangeException("Beta(x,y) is only defined for x > 0 and y > 0.");

        // Use log form for stability:
        // B(x,y) = exp( logGamma(x) + logGamma(y) - logGamma(x+y) )
        decimal lg = LogGamma(x) + LogGamma(y) - LogGamma(x + y);
        return Exp(lg);
      }

      /// <summary>
      /// Computes erf(x) for decimal x using the Abramowitz-Stegun
      /// minimax rational approximation (formula 7.1.26).
      /// </summary>
      public static decimal Erf(decimal x)
      {
        // erf(-x) = -erf(x)
        if (x < 0m)
          return -Erf(-x);

        // Constants for the approximation
        const decimal a1 = 0.254829592m;
        const decimal a2 = -0.284496736m;
        const decimal a3 = 1.421413741m;
        const decimal a4 = -1.453152027m;
        const decimal a5 = 1.061405429m;
        const decimal p = 0.3275911m;

        // Abramowitz-Stegun formula:
        // erf(x) ≈ 1 - (((((a5*t + a4)*t + a3)*t + a2)*t + a1)*t) * exp(-x*x)
        //
        // where t = 1 / (1 + p*x)

        decimal t = 1m / (1m + p * x);
        decimal x2 = x * x;

        decimal poly = a5;
        poly = poly * t + a4;
        poly = poly * t + a3;
        poly = poly * t + a2;
        poly = poly * t + a1;
        poly = poly * t;

        return 1m - poly * Exp(-x2);
      }

      /// <summary>
      /// Computes erfc(x) = 1 - erf(x) using the Abramowitz-Stegun
      /// minimax rational approximation (formula 7.1.26).
      /// </summary>
      public static decimal Erfc(decimal x)
      {
        // erfc(-x) = 2 - erfc(x)
        if (x < 0m)
          return 2m - Erfc(-x);

        // Constants for the approximation
        const decimal a1 = 0.254829592m;
        const decimal a2 = -0.284496736m;
        const decimal a3 = 1.421413741m;
        const decimal a4 = -1.453152027m;
        const decimal a5 = 1.061405429m;
        const decimal p = 0.3275911m;

        // Abramowitz-Stegun formula:
        // erfc(x) ≈ (((((a5*t + a4)*t + a3)*t + a2)*t + a1)*t) * exp(-x*x)
        //
        // where t = 1 / (1 + p*x)

        decimal t = 1m / (1m + p * x);
        decimal x2 = x * x;

        decimal poly = a5;
        poly = poly * t + a4;
        poly = poly * t + a3;
        poly = poly * t + a2;
        poly = poly * t + a1;
        poly = poly * t;

        return poly * Exp(-x2);
      }

      /// <summary>
      /// Computes the standard normal cumulative distribution function Φ(x)
      /// using the complementary error function.
      /// </summary>
      public static decimal NormalCDF(decimal x)
      {
        // Φ(x) = 0.5 * erfc( -x / sqrt(2) )
        return 0.5m * Erfc(-x / SQRT2);
      }

      public static decimal Gamma(decimal x)
      {
        // Poles at non-positive integers
        if (x <= 0m && x == decimal.Truncate(x)) throw new System.ArgumentOutOfRangeException(nameof(x), "Gamma has poles at non-positive integers.");

        // Use reflection formula for negative x
        if (x < 0.5m)
          return ReflectionGamma(x);

        // Use Lanczos for positive x
        return LanczosGamma(x);
      }

      private static decimal LanczosGamma(decimal x)
      {
        decimal sum = lanczosCoefficients[0];

        for (int i = 1; i < lanczosCoefficients.Length; i++)
          sum += lanczosCoefficients[i] / (x + i);

        decimal t = x + lanczosG - 0.5m;

        // Γ(x) ≈ sqrt(2π) * t^(x-0.5) * e^(-t) * sum
        return SQRT_TWO_PI * Pow(t, x - 0.5m) * Exp(-t) * sum;
      }

      private static decimal ReflectionGamma(decimal x)
      {
        // Γ(x) = π / (sin(πx) * Γ(1-x))
        decimal sinPix = Sin(PI * x);
        if (decimal.Abs(sinPix) < 1e-28m)
          throw new OverflowException("Gamma(x) is singular at negative integers.");

        return PI / (sinPix * LanczosGamma(1m - x));
      }

      public static decimal LogGamma(decimal x)
      {
        // Poles at non-positive integers
        if (x <= 0m && x == decimal.Truncate(x))
          throw new ArgumentOutOfRangeException(nameof(x), "LogGamma has poles at non-positive integers.");

        // Reflection formula for negative x
        if (x < 0.5m)
          return ReflectionLogGamma(x);

        // Lanczos for positive x
        return LanczosLogGamma(x);
      }

      private static decimal ReflectionLogGamma(decimal x)
      {
        var sinPix = Sin(PI * x);

        if (decimal.Abs(sinPix) < 1e-28m) throw new System.OverflowException("LogGamma(x) is singular at negative integers.");

        return Log(PI) - Log(decimal.Abs(sinPix)) - LanczosLogGamma(1m - x);
      }

      private static decimal LanczosLogGamma(decimal x)
      {
        decimal sum = lanczosCoefficients[0];

        for (int i = 1; i < lanczosCoefficients.Length; i++)
          sum += lanczosCoefficients[i] / (x + i);

        decimal t = x + lanczosG - 0.5m;

        // logGamma = (x - 0.5)*log(t) - t + log(sqrt(2π)*sum)
        return (x - 0.5m) * Log(t)
             - t
             + Log(SQRT_TWO_PI * sum);
      }

#endif

    }

    private const decimal LN2 = 0.693147180559945309417232121458m;
    private const decimal INV_LN2 = 1m / LN2;

    private const decimal LOG10 = 2.3025850929940459010936137929093m;
    private const decimal LOG2 = 0.69314718055994530941723212145818m;

    private const decimal PI = 3.1415926535897932384626433833m;
    private const decimal TWO_PI = 6.2831853071795864769252867666m;
    private const decimal PI_HALF = 1.5707963267948966192313216916m;

#if DECIMAL_SPECIAL_FUNCTIONS
    private const decimal SQRT2 = 1.41421356237309504880168872421m;

    private static readonly decimal[] lanczosCoefficients =
    {
        0.99999999999980993m,
        676.5203681218851m,
        -1259.1392167224028m,
        771.32342877765313m,
        -176.61502916214059m,
        12.507343278686905m,
        -0.13857109526572012m,
        9.9843695780195716e-6m,
        1.5056327351493116e-7m
    };

    private const decimal lanczosG = 7m;
    private const decimal SQRT_TWO_PI = 2.506628274631000502415765284811m;
#endif
  }
}
