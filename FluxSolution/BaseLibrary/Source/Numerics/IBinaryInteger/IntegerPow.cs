#if NET7_0_OR_GREATER
using System.Numerics;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns x raised to the power of n. Exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPow<TSelf, TOther>(this TSelf number, TOther power)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TOther : System.Numerics.IBinaryInteger<TOther>
    {
      if (power < TOther.Zero) throw new System.ArgumentOutOfRangeException(nameof(power));

      var y = TSelf.One;

      while (power > TOther.One)
        checked
        {
          if ((power & TOther.One) == TOther.One)
            y *= number;

          number *= number;

          power >>= 1;
        }

      return number * y;
    }
  }
}
#endif
