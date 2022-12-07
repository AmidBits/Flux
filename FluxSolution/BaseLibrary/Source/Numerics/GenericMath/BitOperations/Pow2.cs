namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>Get the two power-of-2 nearest to value.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the two power-of-2 will be LT/GT instead of LTE/GTE.</param>
    /// <param name="pow2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="pow2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value as out parameters.</returns>
    public static void LocatePow2<TSelf, TResult>(this TSelf value, bool proper, out TResult pow2TowardsZero, out TResult pow2AwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      if (TSelf.IsZero(value))
      {
        pow2TowardsZero = TResult.Zero;
        pow2AwayFromZero = TResult.One;
      }
      else if (TSelf.IsNegative(value))
      {
        LocatePow2(TSelf.Abs(value), proper, out pow2TowardsZero, out pow2AwayFromZero);

        pow2TowardsZero = -pow2TowardsZero;
        pow2AwayFromZero = -pow2AwayFromZero;
      }
      else // The value is greater than zero here.
      {
        var quotient = TResult.CreateChecked(value.TruncMod(TSelf.One, out var remainder));

        if (TResult.IsPow2(quotient))
        {
          if (proper)
          {
            pow2TowardsZero = TSelf.IsZero(remainder) ? quotient >> 1 : quotient;
            pow2AwayFromZero = quotient << 1;
          }
          else
          {
            pow2TowardsZero = quotient;
            pow2AwayFromZero = TSelf.IsZero(remainder) ? quotient : quotient << 1;
          }
        }
        else
        {
          pow2TowardsZero = MostSignificant1Bit(quotient);
          pow2AwayFromZero = pow2TowardsZero << 1;
        }
      }
    }

    /// <summary>Find the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="nearestPow2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="pow2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static TResult NearestPow2<TSelf, TResult>(this TSelf value, bool proper, RoundingMode mode, out TResult nearestPow2TowardsZero, out TResult pow2AwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocatePow2(value, proper, out nearestPow2TowardsZero, out pow2AwayFromZero);

      return BoundaryRounding<TSelf, TResult>.Round(value, nearestPow2TowardsZero, pow2AwayFromZero, mode);
    }

    /// <summary>Find the next power of 2 away from zero.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TResult Pow2AwayFromZero<TSelf, TResult>(this TSelf value, bool proper, out TResult pow2AwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocatePow2(value, proper, out var _, out pow2AwayFromZero);

      return pow2AwayFromZero;
    }

    /// <summary>Find the next power of 2 towards zero.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TResult Pow2TowardZero<TSelf, TResult>(this TSelf value, bool proper, out TResult pow2TowardsZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocatePow2(value, proper, out pow2TowardsZero, out var _);

      return pow2TowardsZero;
    }

    /// <summary>Attempt to get the nearest (to <paramref name="value"/>) of two power-of-2, using the specified <see cref="RoundingMode"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-2 as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-2 will be found.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="pow2TowardsZero">Outputs the power-of-2 that is closer to zero.</param>
    /// <param name="pow2AwayFromZero">Outputs the power-of-2 that is farther from zero.</param>
    /// <returns>The nearest two power-of-2 to value.</returns>
    public static bool TryNearestPow2<TSelf, TRadix>(this TSelf value, bool proper, RoundingMode mode, out TSelf pow2TowardsZero, out TSelf pow2AwayFromZero, out TSelf nearestPow2)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      try
      {
        nearestPow2 = NearestPow2(value, proper, mode, out pow2TowardsZero, out pow2AwayFromZero);

        return true;
      }
      catch { }

      pow2TowardsZero = TSelf.Zero;
      pow2AwayFromZero = TSelf.Zero;

      nearestPow2 = TSelf.Zero;

      return false;
    }
  }
}
