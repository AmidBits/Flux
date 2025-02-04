namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the the largest integer less than or equal (i.e. floor) to the <paramref name="nth"/> (radix) root of <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <returns>The integer <paramref name="nth"/> root of <paramref name="value"/>.</returns>
    public static TNumber IntegerRootN<TNumber, TNth>(this TNumber value, TNth nth)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
    {
      value.AssertNonNegativeNumber();

      if (nth <= TNth.One) throw new System.ArgumentOutOfRangeException(nameof(nth), "Must be an integer, greater than or equal to 2.");

      if (TryFastIntegerRootN(value, nth, UniversalRounding.WholeTowardZero, out TNumber irootn, out var _)) // Testing!
        return irootn;

      var n = TNumber.CreateChecked(nth);

      var nM1 = n - TNumber.One;
      var c = TNumber.One;
      var d = (nM1 + value) / n;
      var e = (nM1 * d + value / IntegerPow(d, nM1)) / n;

      while (c != d && c != e)
      {
        c = d;
        d = e;
        e = (nM1 * d + value / IntegerPow(d, nM1)) / n;
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
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value">The value to </param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns></returns>
    public static bool IsPerfectIntegerRootN<TNumber, TNth, TRoot>(TNumber value, TNth nth, TRoot root)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      where TRoot : System.Numerics.IBinaryInteger<TRoot>
      => value == TNumber.CreateChecked(IntegerPow(root, nth));
  }
}
