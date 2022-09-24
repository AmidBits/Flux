#if NET7_0_OR_GREATER
using Flux.AmbOps;

namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Radix
  {
    /// <summary>Computes the integer log of a number and the corresponding radix (base).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryGetIntegerLog<TSelf>(this TSelf value, TSelf radix, out TSelf logFloor, out TSelf logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Radix.AssertRadix(radix);

      logFloor = TSelf.Zero;
      logCeiling = TSelf.Zero;

      if (value.IsNonNegativeValue())
      {
        if (!TSelf.IsZero(value))
        {
          while (value >= radix)
          {
            value /= radix;

            logFloor++;
          }

          logCeiling = logFloor + TSelf.One;
        }

        return true;
      }
      else
        return false;
    }
  }
}
#endif
