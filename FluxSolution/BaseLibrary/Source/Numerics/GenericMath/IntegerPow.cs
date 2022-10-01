#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns x raised to the power of n. Exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPow<TSelf, TExponent>(this TSelf value, TExponent exponent)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      if (exponent < TExponent.Zero) throw new System.ArgumentOutOfRangeException(nameof(exponent));

      var y = TSelf.One;

      while (exponent > TExponent.One)
        checked
        {
          if (TExponent.IsOddInteger(exponent))
            y *= value;

          value *= value;

          exponent >>= 1;
        }

      return value * y;
    }
  }
}
#endif
