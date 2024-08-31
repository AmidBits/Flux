namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <returns>The integer <paramref name="nth"/> root of <paramref name="value"/>.</returns>
    public static TValue IntegerRootN<TValue>(this TValue value, TValue nth)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      value.AssertNonNegativeRealNumber();

      if (nth <= TValue.One) throw new System.ArgumentOutOfRangeException(nameof(nth), "Must be an integer, greater than or equal to 2.");

      if (TryFastIntegerRootN(value, nth, out TValue root)) // Testing!
        return root;

      var nM1 = nth - TValue.One;
      var c = TValue.One;
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

      //var hi = TValue.One;
      //var two = TValue.One + TValue.One;

      //while (IntegerPow(hi, y) < n)
      //  hi *= two;

      //var lo = hi / two;

      //while (hi - lo > TValue.One)
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

      //var u = TValue.Zero;
      //var s = n;
      //var yM1 = (y - TValue.One);

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
    /// <para>Returns whether <paramref name="root"/> is a perfect <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The value to </param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns></returns>
    public static bool IsPerfectIntegerRootN<TValue>(TValue value, TValue nth, TValue root)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value == IntegerPow(root, nth);

    /// <summary>
    /// <para>Attempts to compute the (floor) <paramref name="nth"/> (radix) root of <paramref name="value"/> into the out parameter <paramref name="root"/>. This is a faster but limited version.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerRootN<TValue>(TValue value, TValue nth, out TValue root)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (value.GetBitLengthEx() <= 53)
      {
        root = TValue.CreateChecked(double.RootN(double.CreateChecked(value), int.CreateChecked(nth)));
        return true;
      }

      root = TValue.Zero;
      return false;
    }

    /// <summary>
    /// <para>Attempts to compute the (floor) <paramref name="nth"/> (radix) root of <paramref name="value"/> into the out parameter <paramref name="root"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryIntegerRootN<TValue>(TValue value, TValue nth, out TValue root)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      try
      {
        root = IntegerRootN(value, nth);
        return true;
      }
      catch { }

      root = TValue.Zero;
      return false;
    }
  }
}
