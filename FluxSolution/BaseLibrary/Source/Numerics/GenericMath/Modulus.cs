#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the Euclidean modulus of a and b, i.e. the remainder of modular division of a and b.</summary>
    public static TSelf Mod<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsZero(b)) throw new System.DivideByZeroException();

      if (b == -TSelf.One)
        return TSelf.Zero;

      var m = a % b;

      if (m < TSelf.Zero)
        return b < TSelf.Zero ? m - b : m + b;

      return m;
    }

    /// <summary>Modular multiplicative inverse.</summary>
    /// <returns>-1, if no inverse.</returns>
    /// <remarks>
    /// <para>var mi = ModInv(4, 7); // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
    /// <para>var mi = ModInv(8, 11); // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
    /// </remarks>
    public static TSelf ModInv<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (b < TSelf.Zero)
        b = -b;

      if (a < TSelf.Zero)
        a = b - (-a % b);

      var t = TSelf.Zero;
      var nt = TSelf.One;
      var r = b;
      var nr = a % b;

      while (!TSelf.IsZero(nr))
      {
        var q = r / nr;

        (nt, t) = (t - q * nt, nt); // var tmp1 = nt; nt = t - q * nt; t = tmp1;
        (nr, r) = (r - q * nr, nr); // var tmp2 = nr; nr = r - q * nr; r = tmp2;
      }

      if (r > TSelf.One)
        return -TSelf.One; // No inverse.

      if (t < TSelf.Zero)
        t += b;

      return t;
    }

    /// <summary>Modular multiplication.</summary>
    public static TSelf MulMod<TSelf>(this TSelf a, TSelf b, TSelf m)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (a < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(b));
      if (m < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(m));

      if (a >= m)
        a %= m;
      if (b >= m)
        b %= m;

      var x = a;
      var c = x * b / m;
      var r = (a * b - c * m) % m;

      return r < TSelf.Zero ? r + m : r;
    }

    /// <summary>Modular exponentiation.</summary>
    public static TSelf PowMod<TSelf>(this TSelf a, TSelf b, TSelf m)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (a < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(b));
      if (m < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(m));

      var r = m == TSelf.One ? TSelf.Zero : TSelf.One;

      while (b > TSelf.Zero)
      {
        if (TSelf.IsOddInteger(b)) // if ((b & TSelf.One) != TSelf.Zero)
          r = MulMod(r, a, m);

        b >>= 1;
        a = MulMod(a, a, m);
      }

      return r;
    }
  }
}
#endif
