namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the remainder of the Euclidean division of <paramref name="dividend"/> and <paramref name="divisor"/>, i.e. the remainder of modular division of a and b.</summary>
    public static TNumber Mod<TNumber>(this TNumber dividend, TNumber divisor)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (TNumber.IsZero(divisor)) throw new System.DivideByZeroException();

      if (divisor == -TNumber.One)
        return TNumber.Zero;

      var m = dividend % divisor;

      if (TNumber.IsNegative(m))
        return TNumber.IsNegative(divisor) ? m - divisor : m + divisor;

      return m;
    }

    /// <summary>Modular multiplicative inverse of an integer <paramref name="a"/> and the modulus <paramref name="modulus"/>.</summary>
    /// <returns>-1, if no inverse.</returns>
    /// <remarks>
    /// <para>var mi = ModInv(4, 7); // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
    /// <para>var mi = ModInv(8, 11); // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
    /// </remarks>
    public static TNumber ModInv<TNumber>(this TNumber a, TNumber modulus)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (TNumber.IsNegative(modulus))
        modulus = -modulus;

      if (TNumber.IsNegative(a))
        a = modulus - (-a % modulus);

      var t = TNumber.Zero;
      var nt = TNumber.One;
      var r = modulus;
      var nr = a % modulus;

      while (!TNumber.IsZero(nr))
      {
        var q = r / nr;

        (nt, t) = (t - q * nt, nt); // var tmp1 = nt; nt = t - q * nt; t = tmp1;
        (nr, r) = (r - q * nr, nr); // var tmp2 = nr; nr = r - q * nr; r = tmp2;
      }

      if (r > TNumber.One)
        return -TNumber.One; // No inverse.

      if (TNumber.IsNegative(t))
        t += modulus;

      return t;
    }

    /// <summary>
    /// <para>Modular multiplication of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TNumber MulMod<TNumber>(this TNumber dividend, TNumber divisor, TNumber modulus)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      dividend.AssertNonNegativeNumber(nameof(dividend));
      divisor.AssertNonNegativeNumber(nameof(divisor));
      modulus.AssertNonNegativeNumber(nameof(modulus));

      if (dividend >= modulus) dividend %= modulus;
      if (divisor >= modulus) divisor %= modulus;

      var x = dividend;
      var c = x * divisor / modulus;
      var r = (dividend * divisor - c * modulus) % modulus;

      return TNumber.IsNegative(r) ? r + modulus : r;
    }

    /// <summary>
    /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TNumber PowMod<TNumber>(this TNumber dividend, TNumber divisor, TNumber modulus)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      dividend.AssertNonNegativeNumber(nameof(dividend));
      divisor.AssertNonNegativeNumber(nameof(divisor));
      modulus.AssertNonNegativeNumber(nameof(modulus));

      var r = modulus == TNumber.One ? TNumber.Zero : TNumber.One;

      while (divisor > TNumber.Zero)
      {
        if (TNumber.IsOddInteger(divisor)) // if ((b & TValue.One) != TValue.Zero)
          r = MulMod(r, dividend, modulus);

        divisor >>= 1;
        dividend = MulMod(dividend, dividend, modulus);
      }

      return r;
    }
  }
}
