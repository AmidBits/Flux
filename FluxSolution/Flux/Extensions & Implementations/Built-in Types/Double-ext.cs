//#define DOUBLE_SPECIAL_FUNCTIONS // This is a special define to include special functions for double, such as gamma, beta, erf, etc.

namespace Flux
{
  public static partial class DoubleExtensions
  {
    extension(System.Double)
    {
      /// <summary>
      /// <para>The base epsilon 1e‑12d → about the scale where double‑precision (binary64) stops reliably distinguishing differences.</para>
      /// <para>A tolerance for “close enough” comparisons — the smallest meaningful difference before rounding noise dominates.</para>
      /// </summary>
      public static double EngineeringEpsilon => 1e-12d;

      /// <summary>
      /// <para></para>
      /// </summary>
      public static double MachineEpsilon => 2.220446049250313e-16;

      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>9,007,199,254,740,992</c>.</para>
      /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit mantissa and 15-16 digits of precision, which means it can precisely represent integers up to 9,007,199,254,740,992 = <c>(1 &lt;&lt; 53)</c> = 2⁵³, before precision starts to degrade.</para>
      /// </summary>
      public static double MaxExactInteger => +9007199254740992;

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Double"/> without losing precision is <c>-9,007,199,254,740,992</c>.</para>
      /// <para>This is because a <see cref="System.Double"/> is a base-2/binary double-precision floating point with a 53-bit mantissa and 15-16 digits of precision, which means it can precisely represent integers down to -9,007,199,254,740,992 = <c>-(1 &lt;&lt; 53)</c> = -2⁵³, before precision starts to degrade.</para>
      /// </summary>
      public static double MinExactInteger => -9007199254740992;

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a double.</para>
      /// </summary>
      public static double MaxExactPrimeNumber => 9007199254740881;

      #region SafeExactInteger shortcuts (remarked out for now)

      ///// <summary>
      ///// <para>Checks if a double is an exact integer within the safe IEEE 754 range.</para>
      ///// </summary>
      ///// <param name="value"></param>
      ///// <param name="exact"></param>
      ///// <returns></returns>
      //private static bool IsSafeExactInteger(double value, out long exact)
      //{
      //  exact = 0;

      //  if (double.IsNaN(value) || double.IsInfinity(value))
      //    return false;

      //  if (value >= double.MinExactInteger && value <= double.MaxExactInteger)
      //  {
      //    var rounded = double.Round(value);

      //    if (double.Abs(value - rounded) < double.Epsilon)
      //    {
      //      exact = (long)rounded;

      //      return true;
      //    }
      //  }

      //  return false;
      //}

      //public static bool TrySafeExactIntegerCbrt(double x, out long exactInteger)
      //{
      //  exactInteger = 0;

      //  return !double.IsNaN(x) && !double.IsInfinity(x)
      //    && x >= 0 && x <= get_MaxExactInteger() && IsSafeExactInteger(double.Cbrt(x), out exactInteger);
      //}

      //public static bool TrySafeExactIntegerLog(double x, int newBase, out long exactInteger)
      //{
      //  exactInteger = 0;

      //  return !double.IsNaN(x) && !double.IsInfinity(x)
      //    && x > 0 && x <= get_MaxExactInteger() && newBase > 0 && newBase != 1 && IsSafeExactInteger(double.Log(x, newBase), out exactInteger);
      //}

      //public static bool TrySafeExactIntegerRootN(double x, int n, out long exactInteger)
      //{
      //  exactInteger = 0;

      //  return !double.IsNaN(x) && !double.IsInfinity(x)
      //    && x >= 0 && x <= get_MaxExactInteger() && n > 0 && IsSafeExactInteger(double.RootN(x, n), out exactInteger);
      //}

      //public static bool TrySafeExactIntegerSqrt(double x, out long exactInteger)
      //{
      //  exactInteger = 0;

      //  return !double.IsNaN(x) && !double.IsInfinity(x)
      //    && x >= 0 && x <= get_MaxExactInteger() && IsSafeExactInteger(double.Sqrt(x), out exactInteger);
      //}

      //public static bool TrySafeExactIntegerPow(double x, double y, out long exactInteger)
      //{
      //  exactInteger = 0;

      //  return !double.IsNaN(x) && !double.IsNaN(y) && !double.IsInfinity(x) && !double.IsInfinity(y)
      //    && (y % 1 == 0 || x >= 0)
      //    && IsSafeExactInteger(double.Pow(x, y), out exactInteger);
      //}

      #endregion

      #region Log2FactorialStirlingApproximation

      /// <summary>
      /// <para>Approximate log2(n!) using Stirling's formula.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      internal static double Log2FactorialStirlingApproximation(double n)
      {
        const double log2e = 1.4426950408889634;
        const double log2_2pi = 1.6514961294723187; // log2(2π)

        return n * (double.Log(n) * log2e - log2e) + 0.5 * (log2_2pi + double.Log(n) * log2e);
      }

      #endregion

      #region DoubleUlp/TryGet

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <see cref="System.Double"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static double DoubleUlp(double value)
        => double.IsNaN(value)
        ? double.NaN
        : double.IsInfinity(value)
        ? double.PositiveInfinity
        : double.BitIncrement(value) - value;

      /// <summary>
      /// <para>Try to get the unit in the last place (ULP) of a <see cref="System.Double"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp64"></param>
      /// <returns></returns>
      public static bool TryGetDoubleUlp(double value, out double ulp64)
      {
        ulp64 = DoubleUlp(value);

        return double.IsFinite(ulp64);
      }

      /// <summary>
      /// <para>Try to get the unit in the last place (ULP) of a <see cref="System.Double"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp64FromExponent"></param>
      /// <returns></returns>
      public static bool TryGetDoubleUlp(double value, out double ulp64FromBitIncrement, out double ulp64FromExponent)
      {
        var hasMeaningfulUlp = TryGetDoubleUlp(value, out ulp64FromBitIncrement);

        if (hasMeaningfulUlp)
        {
          var bits = System.BitConverter.DoubleToInt64Bits(value);
          var exponent = (int)((bits >> 52) & 0x7FF);

          if (exponent == 0) // Subnormal: ULP = 2^-1074
          {
            ulp64FromExponent = double.BitIncrement(0.0);
          }
          else // Normal number: ULP = 2^(e - 52)
          {
            var unbiased = exponent - 1023;
            ulp64FromExponent = System.BitConverter.Int64BitsToDouble((long)(1023 + unbiased - 52) << 52);
          }
        }
        else
          ulp64FromExponent = ulp64FromBitIncrement;

        return hasMeaningfulUlp;
      }

      #endregion

      #region DoubleUlpDistance

      public static long DoubleUlpDistance(double left, double right)
      {
        var a = BitConverter.DoubleToInt64Bits(left);
        var b = BitConverter.DoubleToInt64Bits(right);

        if (a < 0)
          a = long.MinValue - a;

        if (b < 0)
          b = long.MinValue - b;

        return long.Abs(a - b);
      }

      #endregion

      #region WrapToInterval

      /// <summary>
      /// <para>Wraps a double value to a specified interval [minValue, maxValue] according to the specified wrap mode and interval notation.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="wrapMode"></param>
      /// <param name="intervalNotation"></param>
      /// <param name="epsilon"></param>
      /// <returns></returns>
      public static double WrapToInterval(double value, double minValue, double maxValue, WrapMode wrapMode = WrapMode.Normalized, IntervalNotation intervalNotation = IntervalNotation.HalfOpenRight, double epsilon = 0d)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(epsilon);

        if (wrapMode == WrapMode.Strict)
          System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);
        else if (wrapMode == WrapMode.Normalized && maxValue < minValue)
          (minValue, maxValue) = (maxValue, minValue);
        else if (wrapMode == WrapMode.Centered)
        {
          var mid = (minValue + maxValue) * 0.5;
          var half = (maxValue - minValue) * 0.5;

          return mid + WrapToInterval(value - mid, -half, half, WrapMode.Normalized, intervalNotation, epsilon);
        }

        var range = maxValue - minValue;
        if (range == 0d)
          return minValue;

        var wrapped = (value - minValue) % range;
        if (wrapped < 0d)
          wrapped += range;

        var result = wrapped + minValue;

        TryGetDoubleUlp(result, out var ulp64FromBitIncrement, out var _);

        var eps = (epsilon != 0d) ? epsilon : ulp64FromBitIncrement;

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

#if DOUBLE_SPECIAL_FUNCTIONS

      /// <summary>
      /// <para>The "Standard" Lanczos beta approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      public static double Beta(double x, double y)
        => double.Exp(LogBeta(x, y));

      /// <summary>
      /// <para>This is the classic Abramowitz–Stegun approximation for the error function, accurate to about 1.5×10−7 for all real x.</para>
      /// <para>erf(x), max abs error ~ 1.5e-7</para>
      /// <para>Coefficients for A&amp;S 7.1.26</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static double Erf(double x)
      {
        var ax = x < 0 ? -x : x;

        var t = 1.0 / (1.0 + 0.3275911 * ax);

        var poly = ((((1.061405429 * t - 1.453152027) * t + 1.421413741) * t - 0.284496736) * t + 0.254829592) * t;

        var y = 1.0 - poly * Math.Exp(-x * x);

        return x >= 0 ? y : -y;
      }

      public static double Erfc(double x)
      {
        var ax = x < 0 ? -x : x;

        var t = 1.0 / (1.0 + 0.3275911 * ax);

        var poly = ((((1.061405429 * t - 1.453152027) * t + 1.421413741) * t - 0.284496736) * t + 0.254829592) * t;

        var y = poly * Math.Exp(-x * x);

        return x >= 0 ? y : 2.0 - y;
      }

      /// <summary>
      /// <para>The "Standard" Lanczos factorial approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static double Factorial(int n)
        => Gamma(n + 1.0);

      /// <summary>
      /// <para>The "Standard" Lanczos gamma approximation with g = 7, N = 9.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static double Gamma(double x)
        => double.Exp(LogGamma(x));

      /// <summary>
      /// <para>The "Standard" Lanczos log-beta approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      public static double LogBeta(double x, double y)
        => LogGamma(x) + LogGamma(y) - LogGamma(x + y);

      /// <summary>
      /// <para>The "Standard" Lanczos log-factorial approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static double LogFactorial(int n)
        => LogGamma(n + 1d);

      /// <summary>
      /// <para>The "Standard" Lanczos log-gamma approximation with g = 7, N = 9.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static double LogGamma(double x)
      {
        if (x < 0.5) // Reflection formula.
          return double.Log(double.Pi) - double.Log(double.SinPi(x)) - LogGamma(1.0 - x);

        x -= 1.0;

        var t = x + 7.5; // g + 0.5

        var sum =
              0.99999999999980993 +
            676.5203681218851 / (x + 1.0) +
          -1259.1392167224028 / (x + 2.0) +
            771.32342877765313 / (x + 3.0) +
           -176.61502916214059 / (x + 4.0) +
             12.507343278686905 / (x + 5.0) +
             -0.13857109526572012 / (x + 6.0) +
              9.9843695780195716e-6 / (x + 7.0) +
              1.5056327351493116e-7 / (x + 8.0);

        return 0.91893853320467274178032973640562 + double.Log(sum) + (x + 0.5) * double.Log(t) - t;
      }

#endif
    }
  }
}
