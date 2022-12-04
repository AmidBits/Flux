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
    public static void LocateNearestPow<TValue, TRadix>(this TValue number, TRadix radix, bool proper, out TValue nearestTowardsZero, out TValue nearestAwayFromZero)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      if (TValue.IsNegative(number))
      {
        LocateNearestPow(TValue.Abs(number), radix, proper, out nearestTowardsZero, out nearestAwayFromZero);

        nearestAwayFromZero = TValue.CopySign(nearestAwayFromZero, number);
        nearestTowardsZero = TValue.CopySign(nearestTowardsZero, number);

        return;
      }

      AssertRadix(radix, out TValue tradix);

      nearestTowardsZero = IntegerPow(tradix, TValue.Abs(number).NearestIntegerLogTowardsZero(tradix, out TValue _)) * TValue.CreateChecked(TValue.Sign(number));
      nearestAwayFromZero = nearestTowardsZero * tradix;

      if (proper)
      {
        if (nearestTowardsZero == number)
          nearestTowardsZero /= tradix;
        if (nearestAwayFromZero == number)
          nearestAwayFromZero *= tradix;
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
    public static TSelf NearestPow<TSelf, TRadix>(this TSelf number, TRadix radix, bool proper, RoundingMode mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocateNearestPow(number, radix, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return BoundaryRounding<TSelf, TSelf>.Round(number, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>Find the next power of <paramref name="radix"/> away from zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TSelf NearestPowAwayFromZero<TSelf, TRadix>(this TSelf number, TRadix radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocateNearestPow(number, radix, proper, out var _, out var nearestAwayFromZero);

      return nearestAwayFromZero;
    }

    /// <summary>Find the next power of 2 towards zero.</summary>
    /// <param name="number">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="number"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TSelf NearestPowTowardZero<TSelf, TRadix>(this TSelf number, TRadix radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocateNearestPow(number, radix, proper, out var nearestTowardsZero, out var _);

      return nearestTowardsZero;
    }

    /// <summary>Attempt to get the two power-of-radix nearest to value.</summary>
    /// <param name="number">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestPow<TSelf, TRadix>(this TSelf number, TRadix radix, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      try
      {
        LocateNearestPow(number, radix, proper, out nearestTowardsZero, out nearestAwayFromZero);

        return true;
      }
      catch { }

      nearestTowardsZero = TSelf.Zero;
      nearestAwayFromZero = TSelf.Zero;

      return false;
    }
  }
}
