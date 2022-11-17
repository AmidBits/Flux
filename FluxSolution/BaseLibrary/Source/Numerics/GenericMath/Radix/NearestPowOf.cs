namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Get the two power-of-radix nearest to value.</summary>
    /// <param name="number">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void LocateNearestPowOf<TSelf>(this TSelf number, TSelf radix, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(number))
      {
        LocateNearestPowOf(TSelf.Abs(number), radix, proper, out nearestTowardsZero, out nearestAwayFromZero);

        nearestAwayFromZero.CopySign(number, out nearestAwayFromZero);
        nearestTowardsZero.CopySign(number, out nearestTowardsZero);

        return;
      }

      AssertRadix(radix);

      nearestTowardsZero = IntegerPow(radix, TSelf.Abs(number).IntegerLogFloor(radix)) * number.Signum();
      nearestAwayFromZero = nearestTowardsZero * radix;

      if (proper)
      {
        if (nearestTowardsZero == number)
          nearestTowardsZero /= radix;
        if (nearestAwayFromZero == number)
          nearestAwayFromZero /= radix;
      }
    }

    /// <summary>Find the nearest (to <paramref name="number"/>) of two power-of-radix, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-radix as out parameters.</summary>
    /// <param name="number">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TSelf NearestPowOf<TSelf>(this TSelf number, TSelf radix, bool proper, RoundingMode mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateNearestPowOf(number, radix, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return BoundaryRounding<TSelf>.Round(number, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>Find the next power of <paramref name="radix"/> away from zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TSelf NearestPowOfAwayFromZero<TSelf>(this TSelf number, TSelf radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateNearestPowOf(number, radix, proper, out var _, out var nearestAwayFromZero);

      return nearestAwayFromZero;
    }

    /// <summary>Find the next power of 2 towards zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TSelf NearestPowOfTowardZero<TSelf>(this TSelf number, TSelf radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateNearestPowOf(number, radix, proper, out var nearestTowardsZero, out var _);

      return nearestTowardsZero;
    }
  }
}
