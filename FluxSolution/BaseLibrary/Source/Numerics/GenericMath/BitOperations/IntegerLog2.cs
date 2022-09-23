#if NET7_0_OR_GREATER
using Flux.AmbOps;

namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    ///// <summary>PREVIEW! Computes the floor or ceiling (depending on the <paramref name="ceiling"/> argument) of the base 2 log of the value.</summary>
    ///// <typeparam name="TSelf"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="ceiling"></param>
    ///// <returns></returns>
    //public static int ILog2<TSelf>(this TSelf value, bool ceiling = false)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //  => value < TSelf.Zero ? throw new System.ArgumentOutOfRangeException(nameof(value), "Non-negative value required.")
    //  : TSelf.IsZero(value) ? 0
    //  : ((IsPowerOf2(value) || !ceiling) ? value.GetShortestBitLength() - 1 : value.GetShortestBitLength());

    /// <summary>PREVIEW! Computes the ceiling of the base 2 log of the value.</summary>
    public static int GetIntegerLog2Ceiling
      <TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      // No zero check is necessary because it's not a power of two.
      => TSelf.IsPow2(value.AssertNonNegativeValue()) ? value.GetShortestBitLength() - 1 : value.GetShortestBitLength();

    /// <summary>PREVIEW! Computes the floor of the base 2 log of the value. This is the common log function.</summary>
    public static int GetIntegerLog2Floor<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? 0 : value.AssertNonNegativeValue().GetShortestBitLength() - 1;

    /// <summary>PREVIEW! Computes the floor and ceiling of the base 2 log of the value, using the .NET try paradigm.</summary>
    public static bool TryGetIntegerLog2<TSelf>(this TSelf value, out int log2Floor, out int log2Ceiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      log2Floor = 0;
      log2Ceiling = 0;

      if (value.IsNonNegativeValue())
      {
        if (!TSelf.IsZero(value))
        {
          log2Floor = value.GetShortestBitLength() - 1;
          log2Ceiling = TSelf.IsPow2(value) ? log2Floor : value.GetShortestBitLength();
        }

        return true;
      }
      else
        return false;
    }
  }
}
#endif
