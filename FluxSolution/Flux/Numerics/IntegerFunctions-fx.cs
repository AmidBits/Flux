namespace Flux
{
  public static class IntegerFunctions
  {
    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>Converts an integer <paramref name="value"/> using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
      public double ConvertDecimalToFraction<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var fdc = Units.Radix.DigitCount(value, radix);

        var fip = radix.IntegerPow(fdc);

        return double.CreateChecked(value) / double.CreateChecked(fip);
      }

      #region Log.. functions (Log, Log10)

      public (TInteger TowardZero, double LogR, TInteger AwayFromZero) IntegerLog<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var logR = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(value), double.CreateChecked(radix));

        var (tz, afz) = logR.FindSurroundingIntegersWithTolerance(1e-10, 1e-10);

        return (TInteger.CreateChecked(tz), logR, TInteger.CreateChecked(afz));
      }

      public (TInteger TowardZero, double Log10, TInteger AwayFromZero) IntegerLog10()
      {
        var log10 = System.Numerics.BigInteger.Log10(System.Numerics.BigInteger.CreateChecked(value));

        var (tz, afz) = log10.FindSurroundingIntegersWithTolerance(1e-10, 1e-10);

        return (TInteger.CreateChecked(tz), log10, TInteger.CreateChecked(afz));
      }

      #endregion

      #region Pow

      /// <summary>
      /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</para>
      /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
      /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The radix (base) to be raised to the <paramref name="exponent"/>-of.</param>
      /// <param name="exponent">The exponent with which to raise the <paramref name="value"/>.</param>
      /// <returns>The <paramref name="value"/> raised to the <paramref name="exponent"/>-of.</returns>
      /// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TInteger IntegerPow<TExponent>(TExponent exponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(exponent)));

      #endregion

      #region RootN functions

      /// <summary>
      /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The value to find the root of.</param>
      /// <param name="nth">Essentially the radix.</param>
      /// <returns>The integer <paramref name="nth"/> root of <paramref name="value"/>.</returns>
      public TInteger IntegerRootN(TInteger nth)
      {
        if (nth == TInteger.CreateChecked(2)) return IntegerSqrt(value); // Use dedicated square-root function if nth is 2.

        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(nth, TInteger.CreateChecked(2));

        if (value <= TInteger.One) // 0 = 0, 1 = 1.
          return value;

        checked
        {
          var irootnf = TInteger.CreateChecked(IRootN(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(nth), 101));

          return irootnf;
        }
      }

      ////{
      ////  System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      ////  if (nth <= TInteger.One) throw new System.ArgumentOutOfRangeException(nameof(nth), "Must be an integer, greater than or equal to 2.");

      ////  if (value.TryFastIntegerRootN(nth, out TInteger irootnTz, out TInteger irootnAfz, out var _)) // Testing!
      ////    return irootnTz;

      ////  var n = TInteger.CreateChecked(nth);

      ////  var nM1 = n - TInteger.One;
      ////  var c = TInteger.One;
      ////  var d = (nM1 + value) / n;
      ////  var e = (nM1 * d + value / IntegerPow(d, nM1)) / n;

      ////  while (c != d && c != e)
      ////  {
      ////    c = d;
      ////    d = e;
      ////    e = (nM1 * d + value / IntegerPow(d, nM1)) / n;
      ////  }

      ////  return d < e ? d : e;

      ////  #region First brackets the answer between lo and hi by repeatedly multiplying hi by 2 until n is between lo and hi, then uses binary search to compute the exact answer.
      ////  // 

      ////  //var hi = TValue.One;
      ////  //var two = TValue.One + TValue.One;

      ////  //while (IntegerPow(hi, y) < n)
      ////  //  hi *= two;

      ////  //var lo = hi / two;

      ////  //while (hi - lo > TValue.One)
      ////  //{
      ////  //  var mid = (lo + hi) / two;
      ////  //  var midToK = IntegerPow(mid, y);

      ////  //  if (midToK < n)
      ////  //    lo = mid;
      ////  //  else if (n < midToK)
      ////  //    hi = mid;
      ////  //  else
      ////  //    return mid;
      ////  //}

      ////  //if (IntegerPow(hi, y) == n)
      ////  //  return hi;
      ////  //else
      ////  //  return lo;
      ////  #endregion

      ////  #region Newton's method, division-by-zero at the IntegerPow.
      ////  // 

      ////  //var u = TValue.Zero;
      ////  //var s = n;
      ////  //var yM1 = (y - TValue.One);

      ////  //while (u < s)
      ////  //{
      ////  //  s = u;
      ////  //  var t = yM1 * s + n / IntegerPow(s, yM1);
      ////  //  u = t / y;
      ////  //}

      ////  //return s;
      ////  #endregion
      ////}

      /// <summary>
      /// <para>Indicates whether <paramref name="root"/> is a <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The value to </param>
      /// <param name="nth">Essentially the radix.</param>
      /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
      /// <returns></returns>
      public bool IsIntegerRootN(TInteger nth, TInteger root)
        => value >= root.IntegerPow(nth) // If GTE to nth of root.
        && value < (root + TInteger.One).IntegerPow(nth); // And if LT to nth of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="root"/> is a perfect <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The value to </param>
      /// <param name="nth">Essentially the radix.</param>
      /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
      /// <returns></returns>
      public bool IsPerfectIntegerRootN(TInteger nth, TInteger root)
        => value == IntegerPow(root, nth);

      #endregion

      #region Sqrt functions

      /// <summary>
      /// <para>Returns the (floor) square root of the <paramref name="value"/>, using Newton's method (from Wikipedia.com).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Square_root"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Integer_square_root#Using_only_integer_division"/></para>
      /// </summary>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <returns>Returns the integer (i.e. floor) square root of <paramref name="value"/>.</returns>
      /// <remarks>The ceiling square root is <see cref="ISqrt"/> + 1.</remarks>
      public TInteger IntegerSqrt()
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

        if (value <= TInteger.One) // 0 = 0, 1 = 1.
          return value;

        var x0 = value >> 1;

        while (((x0 + value / x0) >> 1) is var x1 && x1 < x0)
          x0 = x1;

        return x0;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public bool IsSqrt(TInteger root)
        => value >= (root * root) // If GTE to square of root.
        && value < (root + TInteger.One) * (root + TInteger.One); // And if LT to square of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="square"/> is a perfect square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="square">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="square"/>.</param>
      /// <returns>Whether the <paramref name="square"/> is a perfect square of <paramref name="root"/>.</returns>
      /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
      public bool IsPerfectSqrt(TInteger root)
        => value == (root * root);

      #endregion
    }

    #region IntegerRootN helpers

    /// <summary>
    /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> root of <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="nth"></param>
    /// <param name="iterationThreshold">100</param>
    /// <returns></returns>
    private static System.Numerics.BigInteger IRootN(System.Numerics.BigInteger value, int nth, int iterationThreshold)
    {
      checked
      {
        var oldValue = System.Numerics.BigInteger.Zero;
        var newValue = IRoughRootN(value, nth);

        for (var i = 0; System.Numerics.BigInteger.Abs(newValue - oldValue) >= 1 && i < iterationThreshold; i++) // I limited iterations to 100, but you may want way less
        {
          oldValue = newValue;
          newValue = ((nth - 1) * oldValue + (value / System.Numerics.BigInteger.Pow(oldValue, nth - 1))) / nth;
        }

        return newValue;
      }
    }

    private static System.Numerics.BigInteger IRoughRootN(System.Numerics.BigInteger value, int nth)
    {
      var bytes = value.ToByteArray();    // get binary representation

      checked
      {
        var bits = (bytes.Length - 1) * 8;  // get # bits in all but msb
        for (var msb = bytes[^1]; msb != 0; msb >>= 1) // add # bit-length in msb
          bits++;

        var rootBits = bits / nth + 1;   // # bits in the root

        var rootBytes = rootBits / 8 + 1;   // # bytes in the root

        var rootArray = new byte[rootBytes + (rootBits % 8 == 7 ? 1 : 0)]; // Avoid making a negative number by adding an extra 0-byte if the high bit is set

        rootArray[rootBytes - 1] = (byte)(1 << (rootBits % 8)); // set the msb

        return new System.Numerics.BigInteger(rootArray);
      }
    }

    #endregion
  }
}
