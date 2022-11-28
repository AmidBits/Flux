namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>Get the two integer log-2 nearest to value.</summary>
    /// <param name="number">The value for which the nearest integer log-2 will be found.</param>
    /// <param name="nearestTowardsZero">Outputs the integer log-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the integer log-2 that is farther from zero.</param>
    /// <returns>The nearest two integer log-2 to value as out parameters.</returns>
    public static void LocateNearestIntegerLog2<TSelf, TResult>(this TSelf number, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      if (TSelf.IsNegative(number))
      {
        LocateNearestIntegerLog2(TSelf.Abs(number), out nearestTowardsZero, out nearestAwayFromZero);

        nearestTowardsZero = -nearestTowardsZero;
        nearestAwayFromZero = -nearestAwayFromZero;

        return;
      }

      nearestTowardsZero = TResult.CreateChecked(TSelf.Log2(number));
      nearestAwayFromZero = TSelf.IsPow2(number) || TSelf.IsZero(number) ? nearestTowardsZero : nearestTowardsZero + TResult.One;
    }

    /// <summary>Find the nearest (to <paramref name="number"/>) of two integer log-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both log-2 as out parameters.</summary>
    /// <param name="number">The value for which the nearest integer log-2 will be found.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestTowardsZero">Outputs the integer log-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the integer log-2 that is farther from zero.</param>
    /// <returns>The nearest (to <paramref name="number"/>) integer log-2, and also the nearest two integer log-2 to value as out parameters.</returns>
    public static TSelf NearestIntegerLog2<TSelf, TResult>(this TSelf number, RoundingMode mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateNearestIntegerLog2(number, out nearestTowardsZero, out nearestAwayFromZero);

      return BoundaryRounding<TSelf>.Round(number, TSelf.CreateChecked(nearestTowardsZero), TSelf.CreateChecked(nearestAwayFromZero), mode);
    }

    /// <summary>Computes the integer base 2 log away from zero (ceiling) of <paramref name="number"/>.</summary>
    public static TResult NearestIntegerLog2AwayFromZero<TSelf, TResult>(this TSelf number, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateNearestIntegerLog2(number, out _, out nearestAwayFromZero);

      return nearestAwayFromZero;
    }

    /// <summary>Computes the integer base 2 log towards zero (floor) of <paramref name="number"/>.</summary>
    public static TResult NearestIntegerLog2TowardsZero<TSelf, TResult>(this TSelf number, out TResult nearestTowardsZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateNearestIntegerLog2(number, out nearestTowardsZero, out _);

      return nearestTowardsZero;
    }

    /// <summary>Attempt to get the two integer log-2 nearest to value.</summary>
    /// <param name="number">The value for which the nearest integer log-2 will be found.</param>
    /// <param name="nearestTowardsZero">Outputs the integer log-2 that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the integer log-2 that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestIntegerLog2<TSelf, TResult>(this TSelf number, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        LocateNearestIntegerLog2(number, out nearestTowardsZero, out nearestAwayFromZero);

        return true;
      }
      catch { }

      nearestTowardsZero = TResult.Zero;
      nearestAwayFromZero = TResult.Zero;

      return false;
    }
  }
}
