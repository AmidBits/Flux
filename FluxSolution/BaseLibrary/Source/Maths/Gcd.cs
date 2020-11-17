

using System.Linq;

namespace Flux
{
  public static partial class Maths
  {

    
    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Numerics.BigInteger GcdX(params System.Numerics.BigInteger[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], GreatestCommonDivisorX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Numerics.BigInteger GreatestCommonDivisorX(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
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

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static System.Numerics.BigInteger GreatestCommonDivisorExtendedX(System.Numerics.BigInteger a, System.Numerics.BigInteger b, out System.Numerics.BigInteger x, out System.Numerics.BigInteger y)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = 1;
      y = 0;

      System.Numerics.BigInteger u = 0;
      System.Numerics.BigInteger v = 1;

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

    
    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Int32 GcdX(params System.Int32[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], GreatestCommonDivisorX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Int32 GreatestCommonDivisorX(System.Int32 a, System.Int32 b)
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

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static System.Int32 GreatestCommonDivisorExtendedX(System.Int32 a, System.Int32 b, out System.Int32 x, out System.Int32 y)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = 1;
      y = 0;

      System.Int32 u = 0;
      System.Int32 v = 1;

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

    
    /// <summary>Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Int64 GcdX(params System.Int64[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], GreatestCommonDivisorX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static System.Int64 GreatestCommonDivisorX(System.Int64 a, System.Int64 b)
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

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static System.Int64 GreatestCommonDivisorExtendedX(System.Int64 a, System.Int64 b, out System.Int64 x, out System.Int64 y)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = 1;
      y = 0;

      System.Int64 u = 0;
      System.Int64 v = 1;

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

    
  }
}
