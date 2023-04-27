namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsIPowOf<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      if (value == radix) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (radix == (TSelf.One + TSelf.One)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(value);

      if (value > TSelf.One)
        while (TSelf.IsZero(value % radix))
          value /= radix;

      return value == TSelf.One;
    }

    /// <summary>Locate two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="powTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void LocateIPowOf<TSelf>(this TSelf value, TSelf radix, bool proper, out TSelf powTowardsZero, out TSelf powAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      if (TSelf.IsNegative(value))
      {
        LocateIPowOf(TSelf.Abs(value), radix, proper, out powTowardsZero, out powAwayFromZero);

        powAwayFromZero = -powAwayFromZero;
        powTowardsZero = -powTowardsZero;
      }
      else  // The value is greater than or equal to zero here.
      {
        powTowardsZero = IPow(radix, value.LocateILogTz(radix)) * TSelf.CreateChecked(TSelf.Sign(value));
        powAwayFromZero = powTowardsZero * radix;

        if (proper)
        {
          if (powTowardsZero == value)
            powTowardsZero /= radix;
          if (powAwayFromZero == value)
            powAwayFromZero *= radix;
        }
      }
    }

    /// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> away from zero, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static TSelf LocateIPowOfAfz<TSelf>(this TSelf value, TSelf radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateIPowOf(value, radix, proper, out var _, out var powAwayFromZero);

      return powAwayFromZero;
    }

    /// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> toward zero, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static TSelf LocateIPowOfTz<TSelf>(this TSelf value, TSelf radix, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateIPowOf(value, radix, proper, out var powTowardsZero, out var _);

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
    public static TSelf NearestIPowOf<TSelf>(this TSelf value, TSelf radix, bool proper, RoundingMode mode, out TSelf powTowardsZero, out TSelf powAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      LocateIPowOf(value, radix, proper, out powTowardsZero, out powAwayFromZero);

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
    public static bool TryNearestIPowOf<TSelf>(this TSelf value, TSelf radix, bool proper, RoundingMode mode, out TSelf powTowardsZero, out TSelf powAwayFromZero, out TSelf nearestPow)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        nearestPow = NearestIPowOf(value, radix, proper, mode, out powTowardsZero, out powAwayFromZero);

        return true;
      }
      catch { }

      powTowardsZero = TSelf.Zero;
      powAwayFromZero = TSelf.Zero;

      nearestPow = TSelf.Zero;

      return false;
    }

#else

    /// <summary>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsIPowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      if (value == radix) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      //if (radix == 2) // Special case for binary numbers, we can use dedicated IsPow2().
      //  return TSelf.IsPow2(value);

      if (value > System.Numerics.BigInteger.One)
        while ((value % radix).IsZero)
          value /= radix;

      return value == System.Numerics.BigInteger.One;
    }

    /// <summary>Locate two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="powTowardsZero">Outputs the power-of-radix that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-radix that is farther from zero.</param>
    /// <returns>The nearest two power-of-radix to value as out parameters.</returns>
    public static void LocateIPowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper, out System.Numerics.BigInteger powTowardsZero, out System.Numerics.BigInteger powAwayFromZero)
    {
      AssertRadix(radix);

      if (value < 0)
      {
        LocateIPowOf(System.Numerics.BigInteger.Abs(value), radix, proper, out powTowardsZero, out powAwayFromZero);

        powAwayFromZero = -powAwayFromZero;
        powTowardsZero = -powTowardsZero;
      }
      else  // The value is greater than or equal to zero here.
      {
        powTowardsZero = IPow(radix, value.LocateILogTz(radix)) * value.Sign;
        powAwayFromZero = powTowardsZero * radix;

        if (proper)
        {
          if (powTowardsZero == value)
            powTowardsZero /= radix;
          if (powAwayFromZero == value)
            powAwayFromZero *= radix;
        }
      }
    }

    /// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> away from zero, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 away from zero.</returns>
    public static System.Numerics.BigInteger LocateIPowOfAfz(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper)
    {
      LocateIPowOf(value, radix, proper, out var _, out var powAwayFromZero);

      return powAwayFromZero;
    }

    /// <summary>Find the power-of-<paramref name="radix"/> nearest <paramref name="value"/> toward zero, optionally <paramref name="proper"/>.</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">If true, then the result never the same as <paramref name="value"/>.</param>
    /// <returns>The the next power of 2 towards zero.</returns>
    public static System.Numerics.BigInteger LocateIPowOfTz(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper)
    {
      LocateIPowOf(value, radix, proper, out var powTowardsZero, out var _);

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
    public static System.Numerics.BigInteger NearestIPowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper, RoundingMode mode, out System.Numerics.BigInteger powTowardsZero, out System.Numerics.BigInteger powAwayFromZero)
    {
      LocateIPowOf(value, radix, proper, out powTowardsZero, out powAwayFromZero);

      return (System.Numerics.BigInteger)BoundaryRounding<double, System.Numerics.BigInteger>.Round((double)value, mode, (double)powTowardsZero, (double)powAwayFromZero);
    }

    /// <summary>Attempt to get two power-of-<paramref name="radix"/> numbers nearest to <paramref name="value"/>, optionally <paramref name="proper"/>, using the specified <see cref="RoundingMode"/> to resolve any halfway conflict, in out parameters <paramref name="powTowardsZero"/> and <paramref name="powAwayFromZero"/>.</summary>
    /// <param name="value">The value for which the nearest power-of-<paramref name="radix"/> will be found.</param>
    /// <param name="radix">The power of alignment.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a power-of-radix, i.e. the two power-of-radix will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode">The halfway <see cref="RoundingMode"/> to use, when halfway between two values.</param>
    /// <param name="powTowardsZero">Outputs the power-of-<paramref name="radix"/> that is closer to zero.</param>
    /// <param name="powAwayFromZero">Outputs the power-of-<paramref name="radix"/> that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestIPowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, bool proper, RoundingMode mode, out System.Numerics.BigInteger powTowardsZero, out System.Numerics.BigInteger powAwayFromZero, out System.Numerics.BigInteger nearestPow)
    {
      try
      {
        nearestPow = NearestIPowOf(value, radix, proper, mode, out powTowardsZero, out powAwayFromZero);

        return true;
      }
      catch { }

      powTowardsZero = System.Numerics.BigInteger.Zero;
      powAwayFromZero = System.Numerics.BigInteger.Zero;

      nearestPow = System.Numerics.BigInteger.Zero;

      return false;
    }

#endif
  }
}
