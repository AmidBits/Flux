namespace Flux
{
  public static class ModularArithmetic
  {
    extension<TInteger>(TInteger a)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Modular addition.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public TInteger ModAdd(TInteger b, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        return (a + b) % m;
      }

      /// <summary>
      /// <para>Modular division.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public TInteger ModDiv(TInteger b, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        a %= m;

        var inv = ModInv(b, m);

        return (a * inv) % m;
      }

      /// <summary>
      /// <para>Modular multiplicative inverse of an integer <paramref name="a"/> and the modulus <paramref name="m"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_multiplicative_inverse"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/></para>
      /// </summary>
      /// <remarks>
      /// <para>A modular multiplicative inverse may not exists for the specified parameters. In that case an arithmetic exception is thrown.</para>
      /// <para><c>var mi = ModInv(4, 7);</c> // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
      /// <para><c>var mi = ModInv(8, 11);</c> // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
      /// </remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      public TInteger ModInv(TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(m, TInteger.One);

        var t = TInteger.Zero;
        var newt = TInteger.One;
        var r = m;
        var newr = a;

        while (!TInteger.IsZero(newr))
        {
          var q = r / newr;
          (t, newt) = (newt, t - q * newt);
          (r, newr) = (newr, r - q * newr);
        }

        if (r > TInteger.One)
          throw new System.ArithmeticException();

        if (TInteger.IsNegative(t))
          t += m;

        return t;
      }

      /// <summary>
      /// <para>Modular multiplication.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public TInteger ModMul(TInteger b, TInteger m)
        => ((a % m) * (b % m)) % m;

      /// <summary>
      /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_exponentiation"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="b"></param>
      /// <param name="e"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public TInteger ModPow(TInteger e, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        if (m == TInteger.One)
          return TInteger.Zero;

        var r = TInteger.One;

        a = a % m;

        while (e > TInteger.Zero)
        {
          if ((e % TInteger.CreateChecked(2)) == TInteger.One)
            r = (r * a) % m;

          a = (a * a) % m;

          e >>= 1;
        }

        return r;
      }

      /// <summary>
      /// <para>Modular subtraction.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public TInteger ModSub(TInteger b, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        return (a - b) % m;
      }
    }

    //public static TInteger Redc<TInteger>(TInteger thi, TInteger tlo, TInteger n, TInteger invn)
    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //{
    //  if (!TInteger.IsOddInteger(n)) throw new System.ArgumentOutOfRangeException(nameof(n), "Requires odd modulus.");
    //  if (thi < n) throw new System.ArgumentException("Requires thi < n.");

    //  var m = tlo * invn;
    //  var mN = m * n;
    //  var mN_hi = mN >> 64;
    //  var tmp = thi + n;
    //  tmp -= mN_hi;
    //  var result = thi - mN_hi;
    //  if (thi < mN_hi)
    //    result = tmp;

    //  if (result < n) throw new System.ArgumentException("Requires result < n.");

    //  return result;
    //}
  }
}
