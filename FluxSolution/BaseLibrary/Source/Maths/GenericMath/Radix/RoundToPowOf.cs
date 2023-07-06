namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Compute the (floor or toward-zero) power-of-<paramref name="radix"/> number of <paramref name="value"/>.</summary>
    /// <param name="value">The value for which the floor power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <returns>The floor power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
    /// <remarks>To compute the (away-from-zero/ceiling) power-of-<paramref name="radix"/> instead, multiply the return value from <see cref="PowOf"/> with <paramref name="radix"/>.</remarks>
    public static TSelf PowOf<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IntegerPow(radix, IntegerLog(value, radix));

    /// <summary>Locate two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="powOfTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powOfAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static TSelf RoundToPowOf<TSelf>(this TSelf value, TSelf radix, bool proper, RoundingMode mode, out TSelf powOfTowardsZero, out TSelf powOfAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(value))
      {
        var nearest = RoundToPowOf(TSelf.Abs(value), radix, proper, mode, out powOfTowardsZero, out powOfAwayFromZero);

        powOfAwayFromZero = -powOfAwayFromZero;
        powOfTowardsZero = -powOfTowardsZero;

        return -nearest;
      }
      else  // The value is greater than or equal to zero here.
      {
        powOfTowardsZero = IntegerPow(radix, IntegerLog(value, radix));
        powOfAwayFromZero = powOfTowardsZero == value ? powOfTowardsZero : powOfTowardsZero * radix;

        if (proper)
        {
          if (powOfTowardsZero == value) powOfTowardsZero /= radix;
          if (powOfAwayFromZero == value) powOfAwayFromZero *= radix;
        }
      }

      return value.RoundToBoundaries(mode, powOfTowardsZero, powOfAwayFromZero);
    }

#else

    /// <summary>Locate two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="powofTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powofAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static System.Numerics.BigInteger PowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper, RoundingMode mode, out System.Numerics.BigInteger powofTowardsZero, out System.Numerics.BigInteger powofAwayFromZero)
    {
      AssertRadix(radix);

      if (value < 0)
      {
        var nearest = PowOf(System.Numerics.BigInteger.Abs(value), radix, proper, mode, out powofTowardsZero, out powofAwayFromZero);

        powofAwayFromZero = -powofAwayFromZero;
        powofTowardsZero = -powofTowardsZero;

        return nearest;
      }
      else  // The value is greater than or equal to zero here.
      {
        powofAwayFromZero = (powofTowardsZero = PowOf(value, radix)) * radix;

        if (proper)
        {
          if (powofTowardsZero == value)
            powofTowardsZero /= radix;
          if (powofAwayFromZero == value)
            powofAwayFromZero *= radix;
        }
      }

      return value.RoundToBoundaries(mode, powofTowardsZero, powofAwayFromZero);
    }

    /// <summary>Compute the (floor or toward-zero) power-of-<paramref name="radix"/> number of <paramref name="value"/>.</summary>
    /// <param name="value">The value for which the floor power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <returns>The floor power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
    /// <remarks>To compute the (away-from-zero/ceiling) power-of-<paramref name="radix"/> instead, multiply the return value from <see cref="PowOf"/> with <paramref name="radix"/>.</remarks>
    public static System.Numerics.BigInteger PowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
       => IntegerPow(radix, IntegerLog(value, radix));

    ///// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> away from zero, optionally <paramref name="proper"/>.</summary>
    ///// <param name="value">The reference value.</param>
    ///// <param name="radix">The power of alignment.</param>
    ///// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    ///// <returns>The the next power of 2 away from zero.</returns>
    //public static System.Numerics.BigInteger LocatePowOfAfz(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper)
    //{
    //  LocatePowOf(value, radix, proper, out var _, out var powAwayFromZero);

    //  return powAwayFromZero;
    //}

    ///// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> toward zero, optionally <paramref name="proper"/>.</summary>
    ///// <param name="value">The reference value.</param>
    ///// <param name="radix">The power of alignment.</param>
    ///// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    ///// <returns>The the next power of 2 towards zero.</returns>
    //public static System.Numerics.BigInteger LocatePowOfTz(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper)
    //{
    //  LocatePowOf(value, radix, proper, out var powTowardsZero, out var _);

    //  return powTowardsZero;
    //}

    ///// <summary>Get the two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>, using the specified <see cref="RoundingMode"/> to resolve any halfway conflict, and also out parameters <paramref name="powTowardsZero"/> and <paramref name="powAwayFromZero"/>.</summary>
    ///// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    ///// <param name="radix">The power of to align to.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    ///// <param name="mode">The halfway <see cref="RoundingMode"/> to use, when halfway between two values.</param>
    ///// <param name="powTowardsZero">Outputs the power-of-<paramref name="radix"/> that is closer to zero.</param>
    ///// <param name="powAwayFromZero">Outputs the power-of-<paramref name="radix"/> that is farther from zero.</param>
    ///// <returns>The nearest of two power-of-<paramref name="radix"/> to <paramref name="value"/>, optionally <paramref name="proper"/>.</returns>
    //public static System.Numerics.BigInteger NearestPowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper, RoundingMode mode, out System.Numerics.BigInteger powTowardsZero, out System.Numerics.BigInteger powAwayFromZero)
    //{
    //  LocatePowOf(value, radix, proper, out powTowardsZero, out powAwayFromZero);

    //  return (System.Numerics.BigInteger)RoundToBoundaries((double)value, mode, (double)powTowardsZero, (double)powAwayFromZero);
    //}

    ///// <summary>Attempt to get two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>, using the specified <see cref="RoundingMode"/> to resolve any halfway conflict, in out parameters <paramref name="powTowardsZero"/> and <paramref name="powAwayFromZero"/>.</summary>
    ///// <param name="value">The value for which the nearest power-of-<paramref name="radix"/> will be found.</param>
    ///// <param name="radix">The power of alignment.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    ///// <param name="mode">The halfway <see cref="RoundingMode"/> to use, when halfway between two values.</param>
    ///// <param name="powTowardsZero">Outputs the power-of-<paramref name="radix"/> that is closer to zero.</param>
    ///// <param name="powAwayFromZero">Outputs the power-of-<paramref name="radix"/> that is farther from zero.</param>
    ///// <returns>Whether the operation was successful.</returns>
    //public static bool TryNearestPowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper, RoundingMode mode, out System.Numerics.BigInteger powTowardsZero, out System.Numerics.BigInteger powAwayFromZero, out System.Numerics.BigInteger nearestPow)
    //{
    //  try
    //  {
    //    nearestPow = NearestPowOf(value, radix, proper, mode, out powTowardsZero, out powAwayFromZero);

    //    return true;
    //  }
    //  catch { }

    //  powTowardsZero = System.Numerics.BigInteger.Zero;
    //  powAwayFromZero = System.Numerics.BigInteger.Zero;

    //  nearestPow = System.Numerics.BigInteger.Zero;

    //  return false;
    //}

#endif
  }
}
