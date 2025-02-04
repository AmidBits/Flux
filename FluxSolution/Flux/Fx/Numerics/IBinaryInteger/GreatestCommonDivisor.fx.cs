namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the greatest common divisor of all values.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TNumber Gcd<TNumber>(this TNumber a, params TNumber[] other)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => other.Length >= 1
      ? a.GreatestCommonDivisor(other.Aggregate((b, c) => b.GreatestCommonDivisor(c)))
      : throw new System.ArgumentOutOfRangeException(nameof(other));

    /// <summary>Returns the greatest common divisor of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TNumber GreatestCommonDivisor<TNumber>(this TNumber a, TNumber b)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      while (b != TNumber.Zero)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return TNumber.Abs(a);
    }

    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of <paramref name="a"/> and <paramref name="b"/>, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
    public static TNumber GreatestCommonDivisorEx<TNumber>(this TNumber a, TNumber b, out TNumber x, out TNumber y)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      if (a < TNumber.Zero) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < TNumber.Zero) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = TNumber.One;
      y = TNumber.Zero;

      var u = TNumber.Zero;
      var v = TNumber.One;

      while (!TNumber.IsZero(b))
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
