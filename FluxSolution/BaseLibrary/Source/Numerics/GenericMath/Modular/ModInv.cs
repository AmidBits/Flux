namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Modular multiplicative inverse of an integer <paramref name="a"/> and the modulus <paramref name="modulus"/>.</summary>
    /// <returns>-1, if no inverse.</returns>
    /// <remarks>
    /// <para>var mi = ModInv(4, 7); // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
    /// <para>var mi = ModInv(8, 11); // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
    /// </remarks>
    public static TSelf ModInv<TSelf>(this TSelf a, TSelf modulus)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (modulus < TSelf.Zero)
        modulus = -modulus;

      if (a < TSelf.Zero)
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

      if (t < TSelf.Zero)
        t += modulus;

      return t;
    }

    //public static (TSelf LeftFactor, TSelf RightFactor, TSelf Gcd) ExtendedGcd<TSelf>(this TSelf left, TSelf right)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //{
    //  TSelf leftFactor = TSelf.Zero;
    //  TSelf rightFactor = TSelf.One;

    //  TSelf u = TSelf.One;
    //  TSelf v = TSelf.Zero;
    //  TSelf gcd = TSelf.Zero;

    //  while (!TSelf.IsZero(left))
    //  {
    //    var q = right / left;
    //    var r = right % left;

    //    var m = leftFactor - u * q;
    //    var n = rightFactor - v * q;

    //    right = left;
    //    left = r;
    //    leftFactor = u;
    //    rightFactor = v;

    //    u = m;
    //    v = n;

    //    gcd = right;
    //  }

    //  return (leftFactor, rightFactor, gcd);
    //}

    //public static TSelf ModInversion<TSelf>(this TSelf value, TSelf modulo)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //{
    //  var egcd = ExtendedGcd(value, modulo);

    //  if (egcd.Gcd != TSelf.One)
    //    throw new System.ArgumentOutOfRangeException(nameof(modulo));

    //  var result = egcd.LeftFactor;

    //  if (result < TSelf.Zero)
    //    result += modulo;

    //  return result % modulo;
    //}
  }
}
