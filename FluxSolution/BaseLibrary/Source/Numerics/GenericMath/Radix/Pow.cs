using Flux.Percentiles;

namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Get the two power-of-radix nearest to value.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="powTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void LocatePow<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper, out TSelf powTowardsZero, out TSelf powAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      if (TSelf.IsNegative(value))
      {
        LocatePow(TSelf.Abs(value), radix, proper, out powTowardsZero, out powAwayFromZero);

        powAwayFromZero = -powAwayFromZero;
        powTowardsZero = -powTowardsZero;
      }
      else  // The value is greater than or equal to zero here.
      {
        powTowardsZero = IntegerPow(rdx, value.ILogTowardsZero(rdx, out TSelf _)) * TSelf.CreateChecked(TSelf.Sign(value));
        powAwayFromZero = powTowardsZero * rdx;

        if (proper)
        {
          if (powTowardsZero == value)
            powTowardsZero /= rdx;
          if (powAwayFromZero == value)
            powAwayFromZero *= rdx;
        }
      }
    }

    /// <summary>Find the nearest (to <paramref name="value"/>) of two power-of-radix, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both power-of-radix as out parameters.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway rounding mode to use, when halfway between two values.</param>
    /// <param name="powTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value.</returns>
    public static TSelf NearestPow<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper, RoundingMode mode, out TSelf powTowardsZero, out TSelf powAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocatePow(value, radix, proper, out powTowardsZero, out powAwayFromZero);

      return BoundaryRounding<TSelf, TSelf>.Round(value, powTowardsZero, powAwayFromZero, mode);
    }

    /// <summary>Find the next power of <paramref name="radix"/> away from zero.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TSelf PowAwayFromZero<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocatePow(value, radix, proper, out var _, out var powAwayFromZero);

      return powAwayFromZero;
    }

    /// <summary>Find the next power of 2 towards zero.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TSelf PowTowardZero<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocatePow(value, radix, proper, out var powTowardsZero, out var _);

      return powTowardsZero;
    }

    /// <summary>Attempt to get the two power-of-radix nearest to value.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="powTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestPow<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper, RoundingMode mode, out TSelf powTowardsZero, out TSelf powAwayFromZero, out TSelf nearestPow)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      try
      {
        nearestPow = NearestPow(value, radix, proper, mode, out powTowardsZero, out powAwayFromZero);

        return true;
      }
      catch { }

      powTowardsZero = TSelf.Zero;
      powAwayFromZero = TSelf.Zero;

      nearestPow = TSelf.Zero;

      return false;
    }
  }
}
