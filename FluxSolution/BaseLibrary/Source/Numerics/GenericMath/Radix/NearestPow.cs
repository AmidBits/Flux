#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two power-of-radix nearest to value.</summary>
    /// <param name="number">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="properNearest">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void FindNearestPow<TSelf>(this TSelf number, TSelf radix, bool properNearest, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      nearestTowardsZero = IntegerPow(radix, TSelf.Abs(number).IntegerLogFloor(radix)) * NumberSign(number);
      nearestAwayFromZero = nearestTowardsZero * radix;

      if (properNearest)
      {
        if (nearestTowardsZero == number)
          nearestTowardsZero /= radix;
        if (nearestAwayFromZero == number)
          nearestAwayFromZero /= radix;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="number"/>) of two power-of-radix, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-radix as out parameters.</summary>
    /// <param name="number">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="properNearest">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TSelf RoundToNearestPow<TSelf>(this TSelf number, TSelf radix, bool properNearest, RoundingMode mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      FindNearestPow(number, radix, properNearest, out nearestTowardsZero, out nearestAwayFromZero);

      return new BoundaryRounding<TSelf>(mode, nearestTowardsZero, nearestAwayFromZero).RoundNumber(number);
    }
  }
}
#endif
