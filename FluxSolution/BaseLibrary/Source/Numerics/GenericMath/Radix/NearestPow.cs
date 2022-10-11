#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two power-of-radix nearest to value.</summary>
    /// <param name="x">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="b">The power of alignment.</param>
    /// <param name="properNearest">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void FindNearestPow<TSelf>(this TSelf x, TSelf b, bool properNearest, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(b);

      nearestTowardsZero = IntegerPow(b, TSelf.Abs(x).IntegerLogFloor(b)) * NumberSign(x);
      nearestAwayFromZero = nearestTowardsZero * b;

      if (properNearest)
      {
        if (nearestTowardsZero == x)
          nearestTowardsZero /= b;
        if (nearestAwayFromZero == x)
          nearestAwayFromZero /= b;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two power-of-radix, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-radix as out parameters.</summary>
    /// <param name="x">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="b">The power of to align to.</param>
    /// <param name="properNearest">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TSelf RoundToNearestPow<TSelf>(this TSelf x, TSelf b, bool properNearest, RoundingMode mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      FindNearestPow(x, b, properNearest, out nearestTowardsZero, out nearestAwayFromZero);

      return new BoundaryRounding<TSelf>(mode, nearestTowardsZero, nearestAwayFromZero).RoundNumber(x);
    }
  }
}
#endif
