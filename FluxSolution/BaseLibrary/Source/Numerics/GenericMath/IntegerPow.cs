#if NET7_0_OR_GREATER
using System.Numerics;

namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns x raised to the power of n. Exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPow<TSelf, TPower>(this TSelf number, TPower power)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TPower : System.Numerics.IBinaryInteger<TPower>
    {
      if (power < TPower.Zero) throw new System.ArgumentOutOfRangeException(nameof(power));

      var y = TSelf.One;

      while (power > TPower.One)
        checked
        {
          if (TPower.IsOddInteger(power))
            y *= number;

          number *= number;

          power >>= 1;
        }

      return number * y;
    }
  }
}
#endif
