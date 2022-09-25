#if NET7_0_OR_GREATER
using Flux.AmbOps;

namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Radix
  {
    /// <summary>PREVIEW! Computes the integer log ceiling of an integer number in the specified radix (base).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogCeiling<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IntegerLogFloor(value, radix) + TSelf.One;

    /// <summary>PREVIEW! Computes the integer log floor of an integer number in the specified radix (base).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogFloor<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GenericMath.AssertNonNegativeValue(value);
      GenericMath.AssertRadix(radix);

      var logFloor = TSelf.Zero;

      if (!TSelf.IsZero(value))
        while (value >= radix)
        {
          value /= radix;

          logFloor++;
        }

      return logFloor;
    }

    /// <summary>PREVIEW! Tried to compute the integer log floor and ceiling of an integer number in the specified radix (base).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryGetIntegerLog<TSelf>(this TSelf value, TSelf radix, out TSelf logFloor, out TSelf logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        logFloor = IntegerLogFloor(value, radix);
        logCeiling = logFloor + TSelf.One;
        return true;
      }
      catch
      {
        logCeiling = logFloor = TSelf.Zero;
        return false;
      }
    }
  }
}
#endif
