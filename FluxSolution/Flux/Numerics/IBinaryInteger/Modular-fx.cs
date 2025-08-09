namespace Flux
{
  public static partial class BinaryInteger
  {
    //public static TNumber Rem<TNumber>(TNumber x, TNumber y)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
    //{
    //  if (TNumber.IsZero(y)) throw new System.DivideByZeroException();
    //  if (y == -1) return 0;
    //  return x % y;
    //}

    //public static TNumber Mod<TNumber>(TNumber x, TNumber y)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>
    //{
    //  if (TNumber.IsZero(y)) throw new System.DivideByZeroException();
    //  if (y == -TNumber.One) return TNumber.Zero;
    //  return x - y * (x / y - (!TNumber.IsZero(x % y) && x.FastIntegerPow(y, UniversalRounding.HalfTowardZero, out var _) < 0));
    //}

    //public static TNumber Euc1<TNumber>(TNumber x, TNumber y)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>, System.Numerics.IMinMaxValue<TNumber>
    //{
    //  if (TNumber.IsZero(y)) throw new System.DivideByZeroException();
    //  if (y == -TNumber.One) return TNumber.Zero;
    //  if (y == TNumber.MinValue) return x >= TNumber.Zero ? x : TNumber.MaxValue + x + TNumber.One;
    //  y = TNumber.Abs(y);
    //  return x - y * (x / y - (TNumber.IsNegative(x) && !TNumber.IsZero(x % y) ? TNumber.Zero : TNumber.One));
    //}

    //public static TNumber Euc2<TNumber>(TNumber x, TNumber y)
    //  where TNumber : System.Numerics.IBinaryInteger<TNumber>, System.Numerics.IMinMaxValue<TNumber>
    //{
    //  if (TNumber.IsZero(y)) throw new System.DivideByZeroException();
    //  if (y == -TNumber.One) return TNumber.Zero;
    //  if (y != TNumber.MinValue) y = TNumber.Abs(y);
    //  var tmp = x / y - (TNumber.IsNegative(x) && !TNumber.IsZero(x % y) ? TNumber.Zero : TNumber.One);
    //  tmp = y * tmp; // __builtin_mul_overflow(y, tmp, &tmp);
    //  tmp = x * tmp; // __builtin_sub_overflow(x, tmp, &tmp);
    //  return tmp;
    //}

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
