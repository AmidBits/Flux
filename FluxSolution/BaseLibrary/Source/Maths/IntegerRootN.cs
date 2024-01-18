namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> (radix) root of <paramref name="number"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The number to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <returns>The integer <paramref name="nth"/> root of <paramref name="number"/>.</returns>
    public static TSelf IntegerRootN<TSelf>(this TSelf number, TSelf nth)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(number, nameof(number));

      if (nth <= TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(nth), "Must be an integer, greater than or equal to 2.");

      if (TryFastIntegerRootN(number, nth, out TSelf root)) // Testing!
        return root;

      var nM1 = nth - TSelf.One;
      var c = TSelf.One;
      var d = (nM1 + number) / nth;
      var e = (nM1 * d + number / IntegerPow(d, nM1)) / nth;

      while (c != d && c != e)
      {
        c = d;
        d = e;
        e = (nM1 * d + number / IntegerPow(d, nM1)) / nth;
      }

      return d < e ? d : e;

      #region First brackets the answer between lo and hi by repeatedly multiplying hi by 2 until n is between lo and hi, then uses binary search to compute the exact answer.
      // 

      //var hi = TSelf.One;
      //var two = TSelf.One + TSelf.One;

      //while (IntegerPow(hi, y) < n)
      //  hi *= two;

      //var lo = hi / two;

      //while (hi - lo > TSelf.One)
      //{
      //  var mid = (lo + hi) / two;
      //  var midToK = IntegerPow(mid, y);

      //  if (midToK < n)
      //    lo = mid;
      //  else if (n < midToK)
      //    hi = mid;
      //  else
      //    return mid;
      //}

      //if (IntegerPow(hi, y) == n)
      //  return hi;
      //else
      //  return lo;
      #endregion

      #region Newton's method, division-by-zero at the IntegerPow.
      // 

      //var u = TSelf.Zero;
      //var s = n;
      //var yM1 = (y - TSelf.One);

      //while (u < s)
      //{
      //  s = u;
      //  var t = yM1 * s + n / IntegerPow(s, yM1);
      //  u = t / y;
      //}

      //return s;
      #endregion
    }

    /// <summary>
    /// <para>Returns whether <paramref name="root"/> is a perfect <paramref name="nth"/> (radix) root of <paramref name="number"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The number to </param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="number"/>.</param>
    /// <returns></returns>
    public static bool IsPerfectIntegerRootN<TSelf>(TSelf number, TSelf nth, TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number == IntegerPow(root, nth);

    /// <summary>
    /// <para>Attempts to compute the (floor) <paramref name="nth"/> (radix) root of <paramref name="number"/> into the out parameter <paramref name="root"/>. This is a faster but limited version.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The number to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="number"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerRootN<TSelf>(TSelf number, TSelf nth, out TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number.GetBitLength() <= 53)
      {
        root = TSelf.CreateChecked(double.RootN(double.CreateChecked(number), int.CreateChecked(nth)));
        return true;
      }

      root = TSelf.Zero;
      return false;
    }

    /// <summary>
    /// <para>Attempts to compute the (floor) <paramref name="nth"/> (radix) root of <paramref name="number"/> into the out parameter <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number">The number to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="number"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryIntegerRootN<TSelf>(TSelf number, TSelf nth, out TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        root = IntegerRootN(number, nth);

        return true;
      }
      catch
      {
        root = TSelf.Zero;

        return false;
      }
    }

#else


    /// <summary>Returns the the largest integer less than or equal to the <paramref name="n"/>th root of <paramref name="y"/>.</summary>
    public static System.Numerics.BigInteger IRootN(this System.Numerics.BigInteger y, System.Numerics.BigInteger n)
    {
      AssertNonNegative(y, nameof(y));
      AssertRoot(n);

      var nM1 = n - System.Numerics.BigInteger.One;
      var c = System.Numerics.BigInteger.One;
      var d = (nM1 + y) / n;
      var e = (nM1 * d + y / IntegerPow(d, nM1)) / n;

      while (c != d && c != e)
      {
        c = d;
        d = e;
        e = (nM1 * d + y / IntegerPow(d, nM1)) / n;
      }

      return d < e ? d : e;

      #region First brackets the answer between lo and hi by repeatedly multiplying hi by 2 until n is between lo and hi, then uses binary search to compute the exact answer.
      // 

      //var hi = TSelf.One;
      //var two = TSelf.One + TSelf.One;

      //while (IntegerPow(hi, y) < n)
      //  hi *= two;

      //var lo = hi / two;

      //while (hi - lo > TSelf.One)
      //{
      //  var mid = (lo + hi) / two;
      //  var midToK = IntegerPow(mid, y);

      //  if (midToK < n)
      //    lo = mid;
      //  else if (n < midToK)
      //    hi = mid;
      //  else
      //    return mid;
      //}

      //if (IntegerPow(hi, y) == n)
      //  return hi;
      //else
      //  return lo;
      #endregion

      #region Newton's method, division-by-zero at the IntegerPow.
      // 

      //var u = TSelf.Zero;
      //var s = n;
      //var yM1 = (y - TSelf.One);

      //while (u < s)
      //{
      //  s = u;
      //  var t = yM1 * s + n / IntegerPow(s, yM1);
      //  u = t / y;
      //}

      //return s;
      #endregion
    }

    /// <summary>Returns whether <paramref name="x"/> is a perfect <paramref name="n"/>th root of <paramref name="y"/>.</summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="y"></param>
    /// <param name="n"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static bool IsPerfectIRootN(System.Numerics.BigInteger y, System.Numerics.BigInteger n, System.Numerics.BigInteger x)
      => y == IntegerPow(x, n);

    /// <summary>Attempts to compute the (floor) <paramref name="n"/>th root of <paramref name="y"/> into the out parameter <paramref name="x"/>.</summary>
    public static bool TryIRootN(System.Numerics.BigInteger y, System.Numerics.BigInteger n, out System.Numerics.BigInteger x)
    {
      try
      {
        x = IRootN(y, n);

        return true;
      }
      catch
      {
        x = System.Numerics.BigInteger.Zero;

        return false;
      }
    }

#endif
  }
}
