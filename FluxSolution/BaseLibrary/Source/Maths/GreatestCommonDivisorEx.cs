namespace Flux
{
  public static partial class Maths
  {
    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static System.Numerics.BigInteger GreatestCommonDivisorEx(System.Numerics.BigInteger a, System.Numerics.BigInteger b, out System.Numerics.BigInteger x, out System.Numerics.BigInteger y)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = 1;
      y = 0;

      var u = System.Numerics.BigInteger.Zero;
      var v = System.Numerics.BigInteger.One;

      while (b != 0)
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

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static int GreatestCommonDivisorEx(int a, int b, out int x, out int y)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = 1;
      y = 0;

      var u = 0;
      var v = 1;

      while (b != 0)
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
    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static long GreatestCommonDivisorEx(long a, long b, out long x, out long y)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = 1;
      y = 0;

      var u = 0L;
      var v = 1L;

      while (b != 0)
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

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    [System.CLSCompliant(false)]
    public static uint GreatestCommonDivisorEx(uint a, uint b, out uint x, out uint y)
    {
      x = 1;
      y = 0;

      var u = 0U;
      var v = 1U;

      checked
      {
        while (b != 0)
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
      }

      return a;
    }
    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    [System.CLSCompliant(false)]
    public static ulong GreatestCommonDivisorEx(ulong a, ulong b, out ulong x, out ulong y)
    {
      x = 1;
      y = 0;

      ulong u = 0;
      ulong v = 1;

      checked
      {
        while (b != 0)
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
      }

      return a;
    }
  }
}
