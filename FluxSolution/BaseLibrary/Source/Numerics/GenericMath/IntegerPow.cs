#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns x raised to the power of n. Exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPow<TSelf, TExponent>(this TSelf x, TExponent n)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      if (n < TExponent.Zero) throw new System.ArgumentOutOfRangeException(nameof(n));

      if (TExponent.IsZero(n))
        return TSelf.One;

      var y = TSelf.One;

      while (n > TExponent.One)
        checked
        {
          if (TExponent.IsOddInteger(n))
            y *= x;

          x *= x;
          n >>= 1;
        }

      return x * y;
    }
  }
}
#endif
