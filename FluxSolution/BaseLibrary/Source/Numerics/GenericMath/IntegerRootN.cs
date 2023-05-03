namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the the largest integer less than or equal to the <paramref name="n"/>th root of <paramref name="y"/>.</summary>
    public static TSelf IntegerRootN<TSelf>(this TSelf y, TSelf n)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(y, nameof(y));
      AssertRoot(n);

      var nM1 = n - TSelf.One;
      var c = TSelf.One;
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
    public static bool IsPerfectIntegerRootN<TSelf>(TSelf y, TSelf n, TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => y == IntegerPow(x, n);

    /// <summary>Attempts to compute the (floor) <paramref name="n"/>th root of <paramref name="y"/> into the out parameter <paramref name="x"/>.</summary>
    public static bool TryIntegerRootN<TSelf>(TSelf y, TSelf n, out TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        x = IntegerRootN(y, n);

        return true;
      }
      catch
      {
        x = TSelf.Zero;

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
