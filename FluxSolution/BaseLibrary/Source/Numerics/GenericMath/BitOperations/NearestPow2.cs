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
    public static void LocateNearestPow2<TSelf, TResult>(this TSelf number, bool proper, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      if (TSelf.IsNegative(number))
      {
        LocateNearestPow2(TSelf.Abs(number), proper, out nearestTowardsZero, out nearestAwayFromZero);

        nearestTowardsZero = nearestTowardsZero.CopySign(number);
        nearestAwayFromZero = nearestAwayFromZero.CopySign(number);

        return;
      }

      var rnumber = TResult.CreateChecked(number.TruncMod(TSelf.One, out var r));

      if (TResult.IsPow2(rnumber))
      {
        if (proper)
        {
          nearestAwayFromZero = rnumber << 1;
          nearestTowardsZero = rnumber >> 1;
        }
        else
        {
          nearestAwayFromZero = rnumber;
          nearestTowardsZero = rnumber;
        }
      }
      else
      {
        nearestAwayFromZero = BitFoldRight(rnumber - TResult.One) + TResult.One;
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
    public static TResult NearestPow2<TSelf, TResult>(this TSelf number, bool proper, RoundingMode mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateNearestPow2(number, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return BoundaryRounding<TSelf, TResult>.Round(number, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>Find the next power of 2 away from zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TResult NearestPow2AwayFromZero<TSelf, TResult>(this TSelf number, bool proper, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateNearestPow2(number, proper, out var _, out nearestAwayFromZero);

      return nearestAwayFromZero;
    }

    /// <summary>Find the next power of 2 towards zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TResult NearestPow2TowardZero<TSelf, TResult>(this TSelf number, bool proper, out TResult nearestTowardsZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateNearestPow2(number, proper, out nearestTowardsZero, out var _);

      return nearestTowardsZero;
    }

    /// <summary>Attempt to get the two power-of-2 nearest to value.</summary>
    /// <param name="number">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestPow2<TSelf, TRadix, TResult>(this TSelf number, bool proper, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        LocateNearestPow2(number, proper, out nearestTowardsZero, out nearestAwayFromZero);

        return true;
      }
      catch { }

      nearestTowardsZero = TResult.Zero;
      nearestAwayFromZero = TResult.Zero;

      return false;
    }
  }
}
