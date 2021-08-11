namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Modular exponentiation.</summary>
    public static System.Numerics.BigInteger PowMod(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger m)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));
      if (m < 0) throw new System.ArgumentOutOfRangeException(nameof(m));

      var r = m == 1 ? System.Numerics.BigInteger.Zero : System.Numerics.BigInteger.One;

      while (b > 0)
      {
        if ((b & 1) != 0)
          r = MulMod(r, a, m);

        b >>= 1;
        a = MulMod(a, a, m);
      }

      return r;
    }

    ///// <summary>Modular exponentiation.</summary>
    //public static int PowMod(int a, int b, int m)
    //{
    //  if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
    //  if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));
    //  if (m < 0) throw new System.ArgumentOutOfRangeException(nameof(m));

    //  var r = m == 1 ? 0 : 1;

    //  while (b > 0)
    //  {
    //    if ((b & 1) != 0)
    //      r = MulMod(r, a, m);

    //    b >>= 1;
    //    a = MulMod(a, a, m);
    //  }

    //  return r;
    //}
    ///// <summary>Modular exponentiation.</summary>
    //public static long PowMod(long a, long b, long m)
    //{
    //  if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
    //  if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));
    //  if (m < 0) throw new System.ArgumentOutOfRangeException(nameof(m));

    //  var r = m == 1 ? 0L : 1L;

    //  while (b > 0)
    //  {
    //    if ((b & 1) != 0)
    //      r = MulMod(r, a, m);

    //    b >>= 1;
    //    a = MulMod(a, a, m);
    //  }

    //  return r;
    //}
  }
}
