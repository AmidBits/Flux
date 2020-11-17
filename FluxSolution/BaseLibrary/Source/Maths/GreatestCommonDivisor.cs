using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Numerics.BigInteger Gcd(params System.Numerics.BigInteger[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], System.Numerics.BigInteger.GreatestCommonDivisor) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static int Gcd(params int[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], GreatestCommonDivisor) : throw new System.ArgumentOutOfRangeException(nameof(values));
    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static long Gcd(params long[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], GreatestCommonDivisor) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    [System.CLSCompliant(false)]
    public static uint Gcd(params uint[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], GreatestCommonDivisor) : throw new System.ArgumentOutOfRangeException(nameof(values));
    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    [System.CLSCompliant(false)]
    public static ulong Gcd(params ulong[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], GreatestCommonDivisor) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Int32 GreatestCommonDivisor(System.Int32 a, System.Int32 b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      while (a != 0 && b != 0)
      {
        if (a > b)
          a %= b;
        else
          b %= a;
      }

      return a == 0 ? b : a;
    }

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Int64 GreatestCommonDivisor(System.Int64 a, System.Int64 b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      while (a != 0 && b != 0)
      {
        if (a > b)
          a %= b;
        else
          b %= a;
      }

      return a == 0 ? b : a;
    }

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    [System.CLSCompliant(false)]
    public static uint GreatestCommonDivisor(uint a, uint b)
    {
      while (a != 0 && b != 0)
      {
        if (a > b)
          a %= b;
        else
          b %= a;
      }

      return a == 0 ? b : a;
    }
    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    [System.CLSCompliant(false)]
    public static ulong GreatestCommonDivisor(ulong a, ulong b)
    {
      while (a != 0 && b != 0)
      {
        if (a > b)
          a %= b;
        else
          b %= a;
      }

      return a == 0 ? b : a;
    }

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static System.Numerics.BigInteger GreatestCommonDivisorExtended(System.Numerics.BigInteger a, System.Numerics.BigInteger b, out System.Numerics.BigInteger x, out System.Numerics.BigInteger y)
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
    public static int GreatestCommonDivisorExtended(int a, int b, out int x, out int y)
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
    public static long GreatestCommonDivisorExtended(long a, long b, out long x, out long y)
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
    public static uint GreatestCommonDivisorExtended(uint a, uint b, out uint x, out uint y)
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
    public static ulong GreatestCommonDivisorExtended(ulong a, ulong b, out ulong x, out ulong y)
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
