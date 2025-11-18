namespace Flux
{
  public static class IntegerFunctions
  {
    extension<TInteger>(TInteger source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region BitLengthToMaxDigitCount

      /// <summary>
      /// <para>Computes the max number of digits that can be represented by the specified <paramref name="value"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
      /// <code>var mdcf = (10).GetMaxDigitCount(10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used in an unsigned value).</code>
      /// <code>var mdct = (10).GetMaxDigitCount(10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
      /// <code>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="value"/> for extension method) IS THE NUMBER OF BITS (to account for).</code>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value">This is the number of bits to take into account.</param>
      /// <param name="radix">This is the radix (base) to use.</param>
      /// <param name="accountForSignBit">Indicates whether <paramref name="value"/> use one bit for the sign.</param>
      /// <returns></returns>
      public int BitLengthToMaxDigitCount<TRadix>(TRadix radix, bool accountForSignBit)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TInteger.IsNegative(source))
          return 0;

        var swar = System.Numerics.BigInteger.CreateChecked(source).CreateBitMaskRight(); // Create a bit-mask representing the greatest value for the bit-length.

        if (Units.Radix.IsSingleDigit(swar, radix)) // If SWAR is less than radix, there is only one digit, otherwise, compute for values higher than radix, and more digits.
          return 1;

        if (accountForSignBit) // If accounting for a sign-bit, shift the SWAR to properly represent the max of a signed type.
          swar >>>= 1;

        var ilogc = swar.ILog(radix).AwayFromZero;

        return int.CreateChecked(ilogc);
      }

      #endregion

      #region ConvertToFraction

      /// <summary>Converts an integer <paramref name="value"/> using base <paramref name="radix"/> to a decimal fraction, e.g. "123 => 0.123".</summary>
      public double ConvertToFraction<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var fdc = Units.Radix.DigitCount(source, radix);

        var fip = radix.IPow(fdc);

        return double.CreateChecked(source) / double.CreateChecked(fip);
      }

      #endregion

      #region GenerateSubRanges

      /// <summary>
      /// <para>Generates a new sequence of <see cref="System.Range"/> objects of <paramref name="subLength"/> (except the last may be shorter) from the total length of a super-sequence. Essentially "splitting" a sequence into specified sub-lengths.</para>
      /// </summary>
      /// <param name="subLength"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<System.Range> GenerateSubRanges(TInteger subLength)
      {
        for (var index = TInteger.Zero; index < source; index += subLength)
          yield return RangeExtensions.FromOffsetAndLength(int.CreateChecked(index), int.CreateChecked(TInteger.Min(subLength, source - index)));
      }

      #endregion

      #region IsIntegerPowOf

      /// <summary>
      /// <para>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</para>
      /// </summary>
      /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero return as false.</remarks>
      public bool IsIntegerPowOf<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var q = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(source)) / System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(radix));

        var iq = System.Convert.ToInt64(q);

        var eqwt = q.EqualsWithinAbsoluteTolerance(iq, 1e-10) || q.EqualsWithinRelativeTolerance(iq, 1e-10);

        return eqwt;

        //if (radix == TRadix.CreateChecked(2)) // Special case for binary numbers, we can use dedicated IsPow2().
        //  return TInteger.IsPow2(value);

        //try
        //{
        //  var powOfRadix = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        //  while (powOfRadix < value)
        //    powOfRadix = TInteger.CreateChecked(powOfRadix * powOfRadix);

        //  return powOfRadix == value;
        //}
        //catch { }

        //return false;
      }

      #endregion

      #region JosephusProblem

      /// <summary>
      /// <para>Calculates the last longest surviving position (it's not a 0-based index) of the Flavius Josephus problem where <paramref name="value"/> people stand in a circle and every <paramref name="k"/> person commits suicide.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Josephus_problem"/></para>
      /// </summary>
      /// <remarks>This is about counting positions, so it is 1-based position that is computed.</remarks>
      /// <param name="value">The number of people in the initial circle.</param>
      /// <param name="k">The count of each step. I.e. k-1 people are skipped and the k-th is executed.</param>
      /// <returns>The 1-indexed position that the survivor occupies.</returns>
      public TInteger JosephusProblem(TInteger k)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(k, source);

        var survivingPosition = TInteger.Zero;

        for (var positionCounter = TInteger.One; positionCounter <= source; positionCounter++)
          survivingPosition = (survivingPosition + k) % positionCounter;

        return survivingPosition + TInteger.One;
      }

      #endregion

      #region Log.. functions (Log, Log10)

      /// <summary>
      /// <para></para>
      /// <para>Uses the <see cref="System.Numerics.BigInteger"/> functionality.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix"></param>
      /// <returns>The (IntegralTowardZero, the "raw" LogR, and IntegralAwayFromZero) of the result.</returns>
      public (TInteger TowardZero, double LogR, TInteger AwayFromZero) ILog<TRadix>(TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var logR = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(source), double.CreateChecked(radix));

        var (tz, afz) = logR.SurroundingIntegralsWithTolerance(1e-10, 1e-10);

        return (TInteger.CreateChecked(tz), logR, TInteger.CreateChecked(afz));
      }

      /// <summary>
      /// <para></para>
      /// <para>Uses the <see cref="System.Numerics.BigInteger"/> functionality.</para>
      /// </summary>
      /// <returns>The (IntegralTowardZero, the "raw" Log10, and IntegralAwayFromZero) of the result.</returns>
      public (TInteger TowardZero, double Log10, TInteger AwayFromZero) ILog10()
      {
        var log10 = System.Numerics.BigInteger.Log10(System.Numerics.BigInteger.CreateChecked(source));

        var (tz, afz) = log10.SurroundingIntegralsWithTolerance(1e-10, 1e-10);

        return (TInteger.CreateChecked(tz), log10, TInteger.CreateChecked(afz));
      }

      #endregion

      #region Pow

      /// <summary>
      /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>.</para>
      /// <para>Uses the <see cref="System.Numerics.BigInteger"/> functionality.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="exponent">The exponent with which to raise the value.</param>
      /// <returns>The value raised to the <paramref name="exponent"/>-of.</returns>
      /// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public TInteger IPow<TExponent>(TExponent exponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(source), int.CreateChecked(exponent)));

      #endregion

      #region RootN functions

      /// <summary>
      /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The value to find the root of.</param>
      /// <param name="nth">The degree or index of root.</param>
      /// <returns>The integer <paramref name="nth"/> root of <paramref name="value"/>.</returns>
      public TInteger IRootN<TNth>(TNth nth)
        where TNth : System.Numerics.IBinaryInteger<TNth>
      {
        var two = TNth.CreateChecked(2);

        if (nth == two) return IntegerSqrt(source); // Use dedicated square-root function if nth is 2.

        System.ArgumentOutOfRangeException.ThrowIfNegative(source);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(nth, two);

        if (source <= TInteger.One) // 0 = 0, 1 = 1.
          return source;

        checked
        {
          var irootnf = TInteger.CreateChecked(IRootN(System.Numerics.BigInteger.CreateChecked(source), int.CreateChecked(nth), 101));

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
      public bool IsIRootN<TNth>(TNth nth, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => source >= root.IPow(nth) // If GTE to nth of root.
        && source < (root + TInteger.One).IPow(nth); // And if LT to nth of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="root"/> is a perfect <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The value to </param>
      /// <param name="nth">Essentially the radix.</param>
      /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
      /// <returns></returns>
      public bool IsPerfectIRootN<TNth>(TNth nth, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => source == IPow(root, nth);

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
        System.ArgumentOutOfRangeException.ThrowIfNegative(source);

        if (source <= TInteger.One) // 0 = 0, 1 = 1.
          return source;

        var x0 = source >> 1;

        while (((x0 + source / x0) >> 1) is var x1 && x1 < x0)
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
        => source >= (root * root) // If GTE to square of root.
        && source < (root + TInteger.One) * (root + TInteger.One); // And if LT to square of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="square"/> is a perfect square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="square">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="square"/>.</param>
      /// <returns>Whether the <paramref name="square"/> is a perfect square of <paramref name="root"/>.</returns>
      /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
      public bool IsPerfectSqrt(TInteger root)
        => source == (root * root);

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
