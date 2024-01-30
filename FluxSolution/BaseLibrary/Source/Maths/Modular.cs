#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    /// <remarks>This function is equivalent to the DivRem() function, only provided for here for all INumber<> types.</remarks>
    public static TSelf DivMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }

    /// <summary>Returns the full quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) and integer (i.e. truncated/floor) quotient <paramref name="truncatedQuotient"/> as output parameters.</summary>
    public static TSelf DivModTrunc<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder, out TSelf truncatedQuotient)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      truncatedQuotient = TruncMod(dividend, divisor, out remainder);

      return dividend / divisor;
    }

    /// <summary>Returns the remainder of the Euclidean division of <paramref name="dividend"/> and <paramref name="divisor"/>, i.e. the remainder of modular division of a and b.</summary>
    public static TSelf Mod<TSelf>(this TSelf dividend, TSelf divisor)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsZero(divisor)) throw new System.DivideByZeroException();

      if (divisor == -TSelf.One)
        return TSelf.Zero;

      var m = dividend % divisor;

      if (TSelf.IsNegative(m))
        return TSelf.IsNegative(divisor) ? m - divisor : m + divisor;

      return m;
    }

    /// <summary>Modular multiplicative inverse of an integer <paramref name="a"/> and the modulus <paramref name="modulus"/>.</summary>
    /// <returns>-1, if no inverse.</returns>
    /// <remarks>
    /// <para>var mi = ModInv(4, 7); // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
    /// <para>var mi = ModInv(8, 11); // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
    /// </remarks>
    public static TSelf ModInv<TSelf>(this TSelf a, TSelf modulus)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(modulus))
        modulus = -modulus;

      if (TSelf.IsNegative(a))
        a = modulus - (-a % modulus);

      var t = TSelf.Zero;
      var nt = TSelf.One;
      var r = modulus;
      var nr = a % modulus;

      while (!TSelf.IsZero(nr))
      {
        var q = r / nr;

        (nt, t) = (t - q * nt, nt); // var tmp1 = nt; nt = t - q * nt; t = tmp1;
        (nr, r) = (r - q * nr, nr); // var tmp2 = nr; nr = r - q * nr; r = tmp2;
      }

      if (r > TSelf.One)
        return -TSelf.One; // No inverse.

      if (TSelf.IsNegative(t))
        t += modulus;

      return t;
    }

    /// <summary>
    /// <para>Modular multiplication of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TSelf MulMod<TSelf>(this TSelf dividend, TSelf divisor, TSelf modulus)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(dividend, nameof(dividend));
      AssertNonNegative(divisor, nameof(divisor));
      AssertNonNegative(modulus, nameof(modulus));

      if (dividend >= modulus) dividend %= modulus;
      if (divisor >= modulus) divisor %= modulus;

      var x = dividend;
      var c = x * divisor / modulus;
      var r = (dividend * divisor - c * modulus) % modulus;

      return TSelf.IsNegative(r) ? r + modulus : r;
    }

    /// <summary>
    /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
    /// <see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/>
    /// </summary>
    public static TSelf PowMod<TSelf>(this TSelf dividend, TSelf divisor, TSelf modulus)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (dividend < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(dividend));
      if (divisor < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(divisor));
      if (modulus < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(modulus));

      var r = modulus == TSelf.One ? TSelf.Zero : TSelf.One;

      while (divisor > TSelf.Zero)
      {
        if (TSelf.IsOddInteger(divisor)) // if ((b & TSelf.One) != TSelf.Zero)
          r = MulMod(r, dividend, modulus);

        divisor >>= 1;
        dividend = MulMod(dividend, dividend, modulus);
      }

      return r;
    }

    /// <summary>
    /// <para>Returns the remainder in reverse, i.e. instead of [0, divisor), we get (divisor, 0], a reversal of the modulo/remainder output image (range). The function also returns the normal modulo/remainder.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <param name="remainder"></param>
    /// <returns></returns>
    public static TSelf RevMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      remainder = dividend % divisor;

      return TSelf.IsZero(remainder) ? remainder : -remainder + divisor;
    }

    /// <summary>Computes the integer (i.e. truncated/floor) quotient.</summary>
    public static TSelf TruncMod<TSelf>(this TSelf dividend, TSelf divisor)
      where TSelf : System.Numerics.INumber<TSelf>
      => (dividend - (dividend % divisor)) / divisor;

    /// <summary>Computes the integer (i.e. truncated/floor) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    public static TSelf TruncMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }
  }
}
#endif
