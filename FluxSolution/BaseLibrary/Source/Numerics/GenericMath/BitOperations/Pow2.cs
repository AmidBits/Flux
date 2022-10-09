#if NET7_0_OR_GREATER
namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>PREVIEW! Get the two power-of-2 nearest to value.</summary>
    /// <param name="x">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="properNearest">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value as out parameters.</returns>
    public static void GetNearestPow2<TSelf>(this TSelf x, bool properNearest, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsPow2(x))
      {
        if (properNearest)
        {
          nearestAwayFromZero = x << 1;
          nearestTowardsZero = x >> 1;
        }
        else
        {
          nearestAwayFromZero = x;
          nearestTowardsZero = x;
        }
      }
      else
      {
        nearestAwayFromZero = BitFoldRight(x - TSelf.One) + TSelf.One;
        nearestTowardsZero = nearestAwayFromZero >> 1;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two power-of-2, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="x">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="properNearest">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TSelf RoundToNearestPow2<TSelf>(this TSelf x, bool properNearest, HalfwayRounding mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GetNearestPow2(x, properNearest, out nearestTowardsZero, out nearestAwayFromZero);

      return GenericMath.RoundToNearest(x, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two power-of-2, using the specified <see cref="IntegerRounding"/> <paramref name="mode"/>, and also return both power-of-2 as out parameters.</summary>
    /// <param name="x">The value for which the power-of-2 will be found.</param>
    /// <param name="properNearest">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode">The full rounding mode to use.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TSelf RoundToPow2<TSelf>(this TSelf x, bool properNearest, IntegerRounding mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      GetNearestPow2(x, properNearest, out nearestTowardsZero, out nearestAwayFromZero);

      return GenericMath.RoundTo(x, nearestTowardsZero, nearestAwayFromZero, mode);
    }
  }
}
#endif
