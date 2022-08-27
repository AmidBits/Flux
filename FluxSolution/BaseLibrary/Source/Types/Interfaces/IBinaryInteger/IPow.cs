#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns x raised to the power of n. Exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IPow<TSelf>(this TSelf x, TSelf n)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (n < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(n));

      var y = TSelf.One;

      while (n > TSelf.One)
        checked
        {
          if ((n & TSelf.One) == TSelf.One)
            y *= x;

          x *= x;

          n >>= 1;
        }

      return x * y;
    }
  }
}
#endif
