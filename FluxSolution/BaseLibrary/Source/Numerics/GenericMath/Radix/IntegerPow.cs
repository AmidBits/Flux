namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IntegerPow<TSelf, TExponent>(this TSelf radix, TExponent exponent)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      AssertNonNegative(exponent, nameof(exponent));

      if (TExponent.IsZero(exponent))
        return TSelf.One;

      var result = TSelf.One;

      while (exponent != TExponent.One)
        checked
        {
          if (TExponent.IsOddInteger(exponent)) // Only act on set bits in exponent.
            result *= radix; // Multiply by the current corresponding power-of-radix (adjusted in radix below for each iteration).

          radix *= radix; // Compute power-of-radix for the next iteration.
          exponent >>= 1; // Half the exponent for the next iteration.
        }

      return result * radix;
    }

    /// <summary>Returns <paramref name="radix"/> raised to the power of Abs(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="System.Math.Pow"/>.</summary>
    public static TSelf IntegerPowRec<TSelf, TExponent, TReciprocal>(this TSelf radix, TExponent exponent, out TReciprocal reciprocal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      where TReciprocal : System.Numerics.IFloatingPoint<TReciprocal>
    {
      var ipow = IntegerPow(radix, TExponent.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }

    /// <summary>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsIntegerPow<TSelf, TRadix>(this TSelf value, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      AssertNonNegative(value);

      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      if (value == rdx) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (radix == (TRadix.One + TRadix.One)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(value);

      if (value > TSelf.One)
        while (TSelf.IsZero(value % rdx))
          value /= rdx;

      return value == TSelf.One;
    }

    /// <summary>Locate two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="powTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void LocateIntegerPow<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper, out TSelf powTowardsZero, out TSelf powAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      if (TSelf.IsNegative(value))
      {
        LocateIntegerPow(TSelf.Abs(value), radix, proper, out powTowardsZero, out powAwayFromZero);

        powAwayFromZero = -powAwayFromZero;
        powTowardsZero = -powTowardsZero;
      }
      else  // The value is greater than or equal to zero here.
      {
        powTowardsZero = IntegerPow(rdx, value.LocateIntegerLogTz(rdx, out TSelf _)) * TSelf.CreateChecked(TSelf.Sign(value));
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

    /// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> away from zero, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TSelf LocateIntegerPowAfz<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocateIntegerPow(value, radix, proper, out var _, out var powAwayFromZero);

      return powAwayFromZero;
    }

    /// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> toward zero, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TSelf LocateIntegerPowTz<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocateIntegerPow(value, radix, proper, out var powTowardsZero, out var _);

      return powTowardsZero;
    }

    /// <summary>Get the two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>, using the specified <see cref="RoundingMode"/> to resolve any halfway conflict, and also out parameters <paramref name="powTowardsZero"/> and <paramref name="powAwayFromZero"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of to align to.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway <see cref="RoundingMode"/> to use, when halfway between two values.</param>
    /// <param name="powTowardsZero">Outputs the power-of-<paramref name="radix"/> that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-<paramref name="radix"/> that is farther from zero.</param>
    /// <returns>The nearest of two power-of-<paramref name="radix"/> to <paramref name="value"/>, optionally <paramref name="proper"/>.</returns>
    public static TSelf NearestIntegerPow<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper, RoundingMode mode, out TSelf powTowardsZero, out TSelf powAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      LocateIntegerPow(value, radix, proper, out powTowardsZero, out powAwayFromZero);

      return BoundaryRounding<TSelf, TSelf>.Round(value, mode, powTowardsZero, powAwayFromZero);
    }

    /// <summary>Attempt to get two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>, using the specified <see cref="RoundingMode"/> to resolve any halfway conflict, in out parameters <paramref name="powTowardsZero"/> and <paramref name="powAwayFromZero"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-<paramref name="radix"/> will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway <see cref="RoundingMode"/> to use, when halfway between two values.</param>
    /// <param name="powTowardsZero">Outputs the power-of-<paramref name="radix"/> that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-<paramref name="radix"/> that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestIntegerPow<TSelf, TRadix>(this TSelf value, TRadix radix, bool proper, RoundingMode mode, out TSelf powTowardsZero, out TSelf powAwayFromZero, out TSelf nearestPow)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      try
      {
        nearestPow = NearestIntegerPow(value, radix, proper, mode, out powTowardsZero, out powAwayFromZero);

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
