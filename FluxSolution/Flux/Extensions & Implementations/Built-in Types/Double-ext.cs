namespace Flux
{
  public static partial class DoubleExtensions
  {
    extension(System.Double)
    {
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

      /// <summary>
      /// <para>A <see cref="System.Double"/> has a precision of about 15-17 significant digits.</para>
      /// </summary>
      public static int MaxExactSignificantDigits => 15;

      /// <summary>
      /// <para>The default base epsilon (1e-12d) used for near-equality functions.</para>
      /// </summary>
      public static double DefaultBaseEpsilon => 1e-12d;

      #region GetComponents

      /// <summary>
      /// <para>Get the three binary64 parts of a 64-bit floating point both raw (but shifted to LSB) as out parameters and returned adjusted (see below).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Double-precision_floating-point_format"/></para>
      /// </summary>
      /// <param name="binary64SignBit">This is 1 single sign bit. 0 = positive, 1 = negative.</param>
      /// <param name="binary64ExponentBiased">This is an 8-bit exponent in biased form, where the values [1, 2046] (-1022 to +1023) represents the actual exponent. The two remaining values 0 (-1023) and 2047 (+1024) are reserved for special numbers.</param>
      /// <param name="binary64Significand52"></param>
      /// <returns>
      /// <para>The three adjusted binary64 parts as a tuple: <c>(int Binary64Sign = 1 or -1, int Binary64ExponentUnbiased = [−1022, +1023], long Binary64Significand53 = [0, <see cref="MaxPreciseInteger"/>])</c>.</para>
      /// </returns>
      public static (int Sign, int ExponentUnbiased, long Significand53) GetComponents(System.Double value, out int signBit, out int exponentBiased, out long significand52)
      {
        var bits = System.BitConverter.DoubleToUInt64Bits(value);

        signBit = (int)((bits & 0x8000000000000000UL) >>> 63);

        exponentBiased = (int)((bits & 0x7FF0000000000000UL) >>> 52);

        significand52 = (long)(bits & 0x000FFFFFFFFFFFFFUL);

        var sign = signBit == 0 ? 1 : -1;

        var exponentUnbiased = exponentBiased - 1023;

        var significand53 = 0x0010000000000000L | significand52; // This is the significandPrecision above with the hidden 53-bit added.

        return (sign, exponentUnbiased, significand53);
      }

      #endregion

      #region GetParts

      /// <summary>
      /// <para>Get the integral part and the fractional part of a <see cref="System.Double"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
      /// </summary>
      /// <returns>
      /// <para>The integral (integer) part and the fractional part of a 64-bit floating point value.</para>
      /// </returns>
      public static (double IntegralPart, double FractionalPart) GetParts(System.Double value)
      {
        var integralPart = double.Truncate(value);
        var fractionalPart = value - integralPart;

        return (integralPart, fractionalPart);
      }

      #endregion

      #region Native..

      public static double NativeDecrement(double value)
        => double.IsNaN(value)
        ? throw new System.ArithmeticException(value.ToString())
        : double.IsNegativeInfinity(value)
        ? throw new System.OverflowException(value.ToString())
        : double.IsPositiveInfinity(value)
        ? double.MaxValue
        : double.BitDecrement(value);

      public static double NativeIncrement(double value)
        => double.IsNaN(value)
        ? throw new System.ArithmeticException(value.ToString())
        : double.IsPositiveInfinity(value)
        ? throw new System.OverflowException(value.ToString())
        : double.IsNegativeInfinity(value)
        ? double.MinValue
        : double.BitIncrement(value);

      #endregion

#if DOUBLE_SPECIAL_FUNCTIONS 

      /// <summary>
      /// <para>The "Standard" Lanczos beta approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      public static double Beta(double x, double y) => double.Exp(LogBeta(x, y));

      /// <summary>
      /// <para>erf(x), max abs error ~ 1.5e-7</para>
      /// <para>Coefficients for A&amp;S 7.1.26</para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static double Erf(double x)
      {
        var t = 1.0 / (1.0 + 0.3275911 * double.Abs(x));

        var y = 1.0 - (((((1.061405429 * t - 1.453152027) * t + 1.421413741) * t - 0.284496736) * t + 0.254829592) * t) * double.Exp(-x * x);

        return x >= 0.0 ? y : -y;
      }

      public static double Erfc(double x) => 1.0 - Erf(x);

      /// <summary>
      /// <para>The "Standard" Lanczos factorial approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static double Factorial(int n) => Gamma(n + 1.0);

      /// <summary>
      /// <para>The "Standard" Lanczos gamma approximation with g = 7, N = 9.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Gamma_function"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public static double Gamma(double x) => double.Exp(LogGamma(x));

      /// <summary>
      /// <para>The "Standard" Lanczos log-beta approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      public static double LogBeta(double x, double y) => LogGamma(x) + LogGamma(y) - LogGamma(x + y);

      /// <summary>
      /// <para>The "Standard" Lanczos log-factorial approximation with g = 7, N = 9.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static double LogFactorial(int n) => LogGamma(n + 1d);

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
