namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>Get the two power-of-2 nearest to value.</summary>
    /// <param name="number">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value as out parameters.</returns>
    public static void LocateNearestPow2<TSelf>(this TSelf number, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(number))
      {
        LocateNearestPow2(TSelf.Abs(number), proper, out nearestTowardsZero, out nearestAwayFromZero);

        nearestAwayFromZero.CopySign(number, out nearestAwayFromZero);
        nearestTowardsZero.CopySign(number, out nearestTowardsZero);

        return;
      }

      if (TSelf.IsPow2(number))
      {
        if (proper)
        {
          nearestAwayFromZero = number << 1;
          nearestTowardsZero = number >> 1;
        }
        else
        {
          nearestAwayFromZero = number;
          nearestTowardsZero = number;
        }
      }
      else
      {
        nearestAwayFromZero = BitFoldRight(number - TSelf.One) + TSelf.One;
        nearestTowardsZero = nearestAwayFromZero >> 1;
      }
    }

    /// <summary>Find the nearest (to <paramref name="number"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="number">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TSelf NearestPow2<TSelf>(this TSelf number, bool proper, RoundingMode mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateNearestPow2(number, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return BoundaryRounding<TSelf>.Round(number, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>Find the next power of 2 away from zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TSelf NearestPow2AwayFromZero<TSelf>(this TSelf number, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateNearestPow2(number, proper, out var _, out var nearestAwayFromZero);

      return nearestAwayFromZero;
    }

    /// <summary>Find the next power of 2 towards zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TSelf NearestPow2TowardZero<TSelf>(this TSelf number, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateNearestPow2(number, proper, out var nearestTowardsZero, out var _);

      return nearestTowardsZero;
    }

    /// <summary>Attempt to get the two power-of-2 nearest to value.</summary>
    /// <param name="number">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestPow2<TSelf, TRadix>(this TSelf number, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        LocateNearestPow2(number, proper, out nearestTowardsZero, out nearestAwayFromZero);

        return true;
      }
      catch { }

      nearestTowardsZero = TSelf.Zero;
      nearestAwayFromZero = TSelf.Zero;

      return false;
    }
  }
}
