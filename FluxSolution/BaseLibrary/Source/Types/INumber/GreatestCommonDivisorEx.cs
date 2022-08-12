#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of a and b, also the addition the coefficients of Bézout's identity.</summary>
    /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
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
