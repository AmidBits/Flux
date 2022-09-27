#if NET7_0_OR_GREATER
namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>PREVIEW! Get the two power-of-2 nearest to value.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="towardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="awayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value as out parameters.</returns>
    public static void GetNearestPow2<TSelf>(this TSelf value, bool proper, out TSelf towardsZero, out TSelf awayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsPow2(value))
      {
        if (proper)
        {
          awayFromZero = value << 1;
          towardsZero = value >> 1;
        }
        else
        {
          awayFromZero = value;
          towardsZero = value;
        }
      }
      else
      {
        awayFromZero = BitFoldRight(value - TSelf.One) + TSelf.One;
        towardsZero = awayFromZero >> 1;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="towardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="awayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TSelf RoundToNearestPow2<TSelf>(this TSelf value, bool proper, HalfwayRounding mode, out TSelf towardsZero, out TSelf awayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GetNearestPow2(value, proper, out towardsZero, out awayFromZero);

      return GenericMath.RoundToNearest(value, towardsZero, awayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="FullRounding"/> <paramref name="mode"/>, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the power-of-2 will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The full rounding mode to use.</param>
    /// <param name="towardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="awayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TSelf RoundToPow2<TSelf>(this TSelf value, bool proper, FullRounding mode, out TSelf towardsZero, out TSelf awayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GetNearestPow2(value, proper, out towardsZero, out awayFromZero);

      return GenericMath.RoundTo(value, towardsZero, awayFromZero, mode);
    }
  }
}
#endif
