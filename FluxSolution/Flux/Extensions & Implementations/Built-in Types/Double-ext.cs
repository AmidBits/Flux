//#define DOUBLE_SPECIAL_FUNCTIONS // This is a special define to include special functions for double, such as gamma, beta, erf, etc.

namespace Flux
{
  public static partial class DoubleExtensions
  {
    extension(System.Double)
    {
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
