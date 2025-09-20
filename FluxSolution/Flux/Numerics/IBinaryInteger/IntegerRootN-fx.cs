namespace Flux
{
  public static partial class BinaryInteger
  {
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
  }
}
