namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the greatest common divisor of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TValue Gcd<TValue>(this TValue a, params TValue[] other)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => other.Length >= 1
      ? a.GreatestCommonDivisor(other.Aggregate((b, c) => b.GreatestCommonDivisor(c)))
      : throw new System.ArgumentOutOfRangeException(nameof(other));

    /// <summary>Returns the greatest common divisor of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TValue GreatestCommonDivisor<TValue>(this TValue a, TValue b)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      while (b != TValue.Zero)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return TValue.Abs(a);
    }

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of <paramref name="a"/> and <paramref name="b"/>, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static TValue GreatestCommonDivisorEx<TValue>(this TValue a, TValue b, out TValue x, out TValue y)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (a < TValue.Zero) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < TValue.Zero) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = TValue.One;
      y = TValue.Zero;

      var u = TValue.Zero;
      var v = TValue.One;

      while (!TValue.IsZero(b))
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
