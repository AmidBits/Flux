namespace Flux
{
  public static partial class IntegerFunctions
  {
    #region IntegerLog

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log ceiling of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TInteger IntegerLogAwayFromZero<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var tz = IntegerLogTowardZero(value, radix);

      if (!value.IsIntegerPowOf(radix))
        tz++;

      return TInteger.CopySign(tz, value);
    }

    /// <summary>
    /// <para>Integer-log mitigates approximations with floating point logs.</para>
    /// <para>A.k.a. the integer-log floor of <paramref name="value"/> in base <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero simply returns zero.</remarks>
    public static TInteger IntegerLogTowardZero<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (FastIntegerMath.TryFastIntegerLog(value, radix, out TInteger ilogTz, out TInteger iLogAfz, out var _))
        return ilogTz;

      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      if (!TInteger.IsZero(value))
      {
        ilogTz = TInteger.Zero;

        for (var v = value; v >= rdx; v /= rdx)
          ilogTz++;

        return ilogTz;
      }

      return value;
    }

    public static (TInteger ILogFloor, TInteger ILogCeiling) IntegerLog<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (value.TryFastIntegerLog(radix, out TInteger ilogTz, out TInteger ilogAfz, out var _))
        return (ilogTz, ilogAfz);

      var log = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(radix));

      if (log >= 0)
      {
        var ilogc = double.Ceiling(log);
        var ilogf = double.Floor(log);

        var ilog = System.Convert.ToInt64(log);

        var eqwt = log.EqualsWithinAbsoluteTolerance(ilog, 1e-10) || log.EqualsWithinRelativeTolerance(ilog, 1e-10);

        if (eqwt && log < ilog)
          ilogf = ilogc;

        if (eqwt && log > ilog)
          ilogc = ilogf;

        return (TInteger.CreateChecked(ilogf), TInteger.CreateChecked(ilogc));
      }

      return (TInteger.Zero, TInteger.Zero);
    }

    #endregion

    #region IntegerPow

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
    public static TInteger IntegerPow<TInteger, TExponent>(this TInteger value, TExponent exponent)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      => value.TryFastIntegerPow(exponent, out var tz, out var afz, out var _)
      ? tz
      : TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(exponent)));
    //{
    //  if (value.TryFastIntegerPow(power, out TInteger ipow, out TInteger _, out var _)) // Testing!
    //    return ipow;

    //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(power);

    //  if (TInteger.IsZero(value) || TPower.IsZero(power))
    //    return TInteger.One; // If either value or exponent is zero, one is customary.

    //  if (value == TInteger.CreateChecked(2))
    //    return TInteger.One << int.CreateChecked(power);

    //  checked
    //  {
    //    var result = TInteger.One;

    //    while (power != TPower.One)
    //    {
    //      if (TPower.IsOddInteger(power))
    //        result *= value;

    //      power >>= 1;
    //      value *= value;
    //    }

    //    return result * value;
    //  }
    //}

    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of absolute(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="double.Pow(double, double)"/>.</para>
    /// </summary>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <param name="reciprocal">The reciprocal of <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>, i.e. 1 divided by the resulting value.</param>
    /// <returns>The <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>.</returns>
    /// <remarks>If <paramref name="radix"/> and/or <paramref name="exponent"/> are zero, 1 is returned.</remarks>
    public static TRadix IntegerPowRec<TRadix, TExponent, TReciprocal>(this TRadix radix, TExponent exponent, out TReciprocal reciprocal)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      where TReciprocal : System.Numerics.IFloatingPoint<TReciprocal>
    {
      var ipow = radix.IntegerPow(TExponent.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }

    #endregion

    #region IntegerRootN

    /// <summary>
    /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> root of <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="nth"></param>
    /// <param name="iterationThreshold">100</param>
    /// <returns></returns>
    private static System.Numerics.BigInteger IntegerRootN(this System.Numerics.BigInteger value, int nth, int iterationThreshold)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);
      System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(nth, 1);

      var oldValue = System.Numerics.BigInteger.Zero;
      var newValue = RoughRoot();

      for (var i = 0; System.Numerics.BigInteger.Abs(newValue - oldValue) >= 1 && i < iterationThreshold; i++) // I limited iterations to 100, but you may want way less
      {
        oldValue = newValue;
        newValue = ((nth - 1) * oldValue + (value / System.Numerics.BigInteger.Pow(oldValue, nth - 1))) / nth;
      }

      return newValue;

      System.Numerics.BigInteger RoughRoot()
      {
        var bytes = value.ToByteArray();    // get binary representation

        var bits = (bytes.Length - 1) * 8;  // get # bits in all but msb
        for (var msb = bytes[bytes.Length - 1]; msb != 0; msb >>= 1) // add # bit-length in msb
          bits++;

        var rootBits = bits / nth + 1;   // # bits in the root

        var rootBytes = rootBits / 8 + 1;   // # bytes in the root

        var rootArray = new byte[rootBytes + (rootBits % 8 == 7 ? 1 : 0)]; // Avoid making a negative number by adding an extra 0-byte if the high bit is set

        rootArray[rootBytes - 1] = (byte)(1 << (rootBits % 8)); // set the msb

        return new System.Numerics.BigInteger(rootArray);
      }
    }

    /// <summary>
    /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <returns>The integer <paramref name="nth"/> root of <paramref name="value"/>.</returns>
    public static (TInteger IRootTz, TInteger IRootAfz) IntegerRootN<TInteger>(this TInteger value, TInteger nth)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (value.TryFastIntegerRootN(nth, out var irootnTz, out var irootnAfz, out var _))
        return (irootnTz, irootnAfz);

      var rootntz = TInteger.CreateChecked(System.Numerics.BigInteger.CreateChecked(value).IntegerRootN(int.CreateChecked(nth), 101));
      var rootnafz = rootntz + TInteger.One;

      return (rootntz, rootnafz);
    }

    //{
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);

    //  if (nth <= TInteger.One) throw new System.ArgumentOutOfRangeException(nameof(nth), "Must be an integer, greater than or equal to 2.");

    //  if (value.TryFastIntegerRootN(nth, out TInteger irootnTz, out TInteger irootnAfz, out var _)) // Testing!
    //    return irootnTz;

    //  var n = TInteger.CreateChecked(nth);

    //  var nM1 = n - TInteger.One;
    //  var c = TInteger.One;
    //  var d = (nM1 + value) / n;
    //  var e = (nM1 * d + value / IntegerPow(d, nM1)) / n;

    //  while (c != d && c != e)
    //  {
    //    c = d;
    //    d = e;
    //    e = (nM1 * d + value / IntegerPow(d, nM1)) / n;
    //  }

    //  return d < e ? d : e;

    //  #region First brackets the answer between lo and hi by repeatedly multiplying hi by 2 until n is between lo and hi, then uses binary search to compute the exact answer.
    //  // 

    //  //var hi = TValue.One;
    //  //var two = TValue.One + TValue.One;

    //  //while (IntegerPow(hi, y) < n)
    //  //  hi *= two;

    //  //var lo = hi / two;

    //  //while (hi - lo > TValue.One)
    //  //{
    //  //  var mid = (lo + hi) / two;
    //  //  var midToK = IntegerPow(mid, y);

    //  //  if (midToK < n)
    //  //    lo = mid;
    //  //  else if (n < midToK)
    //  //    hi = mid;
    //  //  else
    //  //    return mid;
    //  //}

    //  //if (IntegerPow(hi, y) == n)
    //  //  return hi;
    //  //else
    //  //  return lo;
    //  #endregion

    //  #region Newton's method, division-by-zero at the IntegerPow.
    //  // 

    //  //var u = TValue.Zero;
    //  //var s = n;
    //  //var yM1 = (y - TValue.One);

    //  //while (u < s)
    //  //{
    //  //  s = u;
    //  //  var t = yM1 * s + n / IntegerPow(s, yM1);
    //  //  u = t / y;
    //  //}

    //  //return s;
    //  #endregion
    //}

    /// <summary>
    /// <para>Returns whether <paramref name="root"/> is a perfect <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The value to </param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns></returns>
    public static bool IsPerfectIntegerRootN<TInteger>(TInteger value, TInteger nth, TInteger root)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value == TInteger.CreateChecked(IntegerPow(root, nth));

    #endregion

    #region IntegerSqrt

    /// <summary>
    /// <para>Returns the (floor) square root of the <paramref name="value"/>, using Newton's method (from Wikipedia.com).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Square_root"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Integer_square_root#Using_only_integer_division"/></para>
    /// </summary>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <returns>Returns the integer (i.e. floor) square root of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling square root is <see cref="IntegerSqrt"/> + 1.</remarks>
    public static TInteger IntegerSqrt<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (value.TryFastIntegerSqrt(out TInteger isqrtTz, out TInteger _, out var _)) // First try to use double.Sqrt
        return isqrtTz;

      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      if (value <= TInteger.One)
        return value;

      var x0 = value >> 1;

      while (((x0 + value / x0) >> 1) is var x1 && x1 < x0)
        x0 = x1;

      return x0;
    }

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
    public static bool IsIntegerSqrt<TInteger>(this TInteger value, TInteger root)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value >= (root * root) // If GTE to square of root.
      && value < (++root * root); // And if LT to square of (root + 1).

    /// <summary>
    /// <para>Returns whether <paramref name="value"/> is a perfect square of <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
    /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the <paramref name="value"/> is a perfect square of <paramref name="root"/>.</returns>
    /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
    public static bool IsPerfectIntegerSqrt<TInteger>(this TInteger value, TInteger root)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value == (root * root);

    #endregion
  }
}
