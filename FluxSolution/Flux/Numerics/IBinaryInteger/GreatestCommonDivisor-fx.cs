namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Returns the greatest common divisor of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TInteger Gcd<TInteger>(this TInteger a, params TInteger[] other)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

      return a.GreatestCommonDivisor(other.Aggregate(GreatestCommonDivisor));
    }

    /// <summary>Returns the greatest common divisor of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TInteger GreatestCommonDivisor<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      while (b != TInteger.Zero)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return TInteger.Abs(a);
    }

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of <paramref name="a"/> and <paramref name="b"/>, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static TInteger GreatestCommonDivisorEx<TInteger>(this TInteger a, TInteger b, out TInteger x, out TInteger y)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(a);
      System.ArgumentOutOfRangeException.ThrowIfNegative(b);

      x = TInteger.One;
      y = TInteger.Zero;

      var u = TInteger.Zero;
      var v = TInteger.One;

      while (!TInteger.IsZero(b))
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
