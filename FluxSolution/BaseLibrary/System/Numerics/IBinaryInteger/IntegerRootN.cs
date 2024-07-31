namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value">The number to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <returns>The integer <paramref name="nth"/> root of <paramref name="value"/>.</returns>
    public static TSelf IntegerRootN<TSelf>(this TSelf value, TSelf nth)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(value, nameof(value));

      if (nth <= TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(nth), "Must be an integer, greater than or equal to 2.");

      if (TryFastIntegerRootN(value, nth, out TSelf root)) // Testing!
        return root;

      var nM1 = nth - TSelf.One;
      var c = TSelf.One;
      var d = (nM1 + value) / nth;
      var e = (nM1 * d + value / IntegerPow(d, nM1)) / nth;

      while (c != d && c != e)
      {
        c = d;
        d = e;
        e = (nM1 * d + value / IntegerPow(d, nM1)) / nth;
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
      if (number.GetBitLengthEx() <= 53)
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
      catch { }

      root = TSelf.Zero;
      return false;
    }
  }
}
