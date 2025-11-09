namespace Flux
{
  /// <para><see href="https://en.wikipedia.org/wiki/Multiplicative_function"/></para>
  public static class EuclideanAlgorithm
  {
    extension<TInteger>(TInteger a)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region BinaryGcd

      public TInteger BinaryGcd(TInteger b)
      {
        if (TInteger.IsNegative(a) || TInteger.IsNegative(b))
          BinaryGcd(TInteger.Abs(a), TInteger.Abs(b));

        if (TInteger.IsZero(a))
          return b;

        if (TInteger.IsZero(b))
          return a;

        var i = a.GetTrailingZeroCount(); a >>= i;
        var j = b.GetTrailingZeroCount(); b >>= j;
        var k = int.Min(i, j);

        while (true)
        {
          if (a > b)
            (a, b) = (b, a);

          b -= a;

          if (TInteger.IsZero(b))
            return a << k;

          b >>>= b.GetTrailingZeroCount();
        }
      }

      #endregion

      #region EuclidGcd

      /// <summary>
      /// <para>This is the GCD function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Multiplicative_function"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Arithmetic_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Fundamental_theorem_of_arithmetic"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public TInteger EuclidGcd(TInteger b)
      {
        while (b != TInteger.Zero)
        {
          var t = b;
          b = a % b;
          a = t;
        }

        return TInteger.Abs(a);
      }

      /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of <paramref name="a"/> and <paramref name="b"/>, also the addition the coefficients of Bézout's identity.</summary>
      /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
      /// <see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
      /// <seealso href="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
      public TInteger EuclidGcdExt(TInteger b, out TInteger x, out TInteger y)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(a);
        System.ArgumentOutOfRangeException.ThrowIfNegative(b);

        x = TInteger.One;
        y = TInteger.Zero;

        var u = TInteger.Zero;
        var v = TInteger.One;

        while (!TInteger.IsZero(b))
        {
          a = b;
          b = a % b;

          var q = a / b;

          var u1 = x - q * u;
          var v1 = y - q * v;

          x = u;
          y = v;

          u = u1;
          v = v1;
        }

        return a;
      }

      /// <summary>
      /// <para>This is the LCM function.</para>
      /// <para>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Least_common_multiple"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public TInteger EuclidLcm(TInteger b) => a / EuclidGcd(a, b) * b;

      #endregion

      public TInteger Gcd(TInteger b)
      {
        (a, b) = (TInteger.Max(a, b), TInteger.Min(a, b)); // Ensure "a >= b".

        if (b.GetBitLength() < 47)
          return EuclidGcd(a, b);

        return LehmerGcd(a, b);
      }

      /// <summary>Returns the greatest common divisor of all values.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
      public TInteger GreatestCommonDivisor(params TInteger[] other)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

        return Gcd(a, other.Aggregate(Gcd));
      }

      public bool IsCoprime(TInteger b)
        => Gcd(a, b) == TInteger.One;

      public TInteger Lcm(TInteger b)
        => EuclidLcm(a, b);

      /// <summary>Returns the least common multiple of all values.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
      public TInteger LeastCommonMultiple(params TInteger[] other)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

        return Lcm(a, other.Aggregate(Lcm));
      }

      #region LehmerGcd

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Lehmer%27s_GCD_algorithm"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public TInteger LehmerGcd(TInteger b)
      {
        if (System.Numerics.BigInteger.CreateChecked(a) <= int.MaxValue || System.Numerics.BigInteger.CreateChecked(b) <= int.MaxValue)
          return Gcd(a, b);

        return LehmerGcdOuter(a, b);
      }

      #endregion
    }

    #region LehmerGcd helpers

    private static TInteger LehmerGcdOuter<TInteger>(TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TInteger.IsZero(b)) return a;

      var (x, y, u, v) = LehmerGcdInner(TInteger.One, TInteger.Zero, TInteger.Zero, TInteger.One, a >> (a.GetBitLength() - 32), b >> (b.GetBitLength() - 32));

      if (TInteger.IsZero(u))
        return LehmerGcdOuter(b, a % b);

      return LehmerGcdOuter(TInteger.Abs(x * a + y * b), TInteger.Abs(u * a + v * b));
    }

    private static (TInteger x, TInteger y, TInteger u, TInteger v) LehmerGcdInner<TInteger>(TInteger x, TInteger y, TInteger u, TInteger v, TInteger a1, TInteger b1)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (!TInteger.IsZero(b1 + v) && !TInteger.IsZero(b1 + y))
        if ((a1 + x * b1 + v) == (a1 + u * b1 + y))
        {
          var q = (a1 + x) / (b1 + v);

          return LehmerGcdInner(u, v, x - q * u, y - q * v, b1, a1 - q * b1);
        }

      return (x, y, u, v);
    }

    #endregion
  }
}
