namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the remainder of the Euclidean division of <paramref name="dividend"/> and <paramref name="divisor"/>, i.e. the remainder of modular division of a and b.</summary>
    public static TValue Mod<TValue>(this TValue dividend, TValue divisor)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (TValue.IsZero(divisor)) throw new System.DivideByZeroException();

      if (divisor == -TValue.One)
        return TValue.Zero;

      var m = dividend % divisor;

      if (TValue.IsNegative(m))
        return TValue.IsNegative(divisor) ? m - divisor : m + divisor;

      return m;
    }

    /// <summary>Modular multiplicative inverse of an integer <paramref name="a"/> and the modulus <paramref name="modulus"/>.</summary>
    /// <returns>-1, if no inverse.</returns>
    /// <remarks>
    /// <para>var mi = ModInv(4, 7); // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
    /// <para>var mi = ModInv(8, 11); // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
    /// </remarks>
    public static TValue ModInv<TValue>(this TValue a, TValue modulus)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (TValue.IsNegative(modulus))
        modulus = -modulus;

      if (TValue.IsNegative(a))
        a = modulus - (-a % modulus);

      var t = TValue.Zero;
      var nt = TValue.One;
      var r = modulus;
      var nr = a % modulus;

      while (!TValue.IsZero(nr))
      {
        var q = r / nr;

        (nt, t) = (t - q * nt, nt); // var tmp1 = nt; nt = t - q * nt; t = tmp1;
        (nr, r) = (r - q * nr, nr); // var tmp2 = nr; nr = r - q * nr; r = tmp2;
      }

      if (r > TValue.One)
        return -TValue.One; // No inverse.

      if (TValue.IsNegative(t))
        t += modulus;

      return t;
    }

    /// <summary>
    /// <para>Modular multiplication of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TValue MulMod<TValue>(this TValue dividend, TValue divisor, TValue modulus)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      dividend.AssertNonNegativeRealNumber(nameof(dividend));
      divisor.AssertNonNegativeRealNumber(nameof(divisor));
      modulus.AssertNonNegativeRealNumber(nameof(modulus));

      if (dividend >= modulus) dividend %= modulus;
      if (divisor >= modulus) divisor %= modulus;

      var x = dividend;
      var c = x * divisor / modulus;
      var r = (dividend * divisor - c * modulus) % modulus;

      return TValue.IsNegative(r) ? r + modulus : r;
    }

    /// <summary>
    /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TValue PowMod<TValue>(this TValue dividend, TValue divisor, TValue modulus)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      dividend.AssertNonNegativeRealNumber(nameof(dividend));
      divisor.AssertNonNegativeRealNumber(nameof(divisor));
      modulus.AssertNonNegativeRealNumber(nameof(modulus));

      var r = modulus == TValue.One ? TValue.Zero : TValue.One;

      while (divisor > TValue.Zero)
      {
        if (TValue.IsOddInteger(divisor)) // if ((b & TValue.One) != TValue.Zero)
          r = MulMod(r, dividend, modulus);

        divisor >>= 1;
        dividend = MulMod(dividend, dividend, modulus);
      }

      return r;
    }
  }
}
