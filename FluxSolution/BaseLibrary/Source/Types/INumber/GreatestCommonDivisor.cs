#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns the greatest common divisor of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TSelf Gcd<TSelf>(this TSelf[] values)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Linq.Enumerable.Aggregate(values, (a, b) => a.GreatestCommonDivisor(b));

    /// <summary>PREVIEW! Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TSelf GreatestCommonDivisor<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      while (b != TSelf.Zero)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return TSelf.Abs(a);
    }

    /// <summary>PREVIEW! The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of B�zout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/B�zout%27s_identity"/>
    public static TSelf GreatestCommonDivisorEx<TSelf>(this TSelf a, TSelf b, out TSelf x, out TSelf y)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (a < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(b));

      x = TSelf.One;
      y = TSelf.Zero;

      var u = TSelf.Zero;
      var v = TSelf.One;

      while (b != TSelf.Zero)
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
#endif