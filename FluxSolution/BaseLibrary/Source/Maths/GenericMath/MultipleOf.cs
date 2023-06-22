namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Compute the <paramref name="multiple"/> of <paramref name="value"/>, less than or equal to <paramref name="value"/>. Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <returns>The <paramref name="multiple"/> equal to or less than <paramref name="value"/>.</returns>
    public static TSelf MultipleOf<TSelf>(this TSelf value, TSelf multiple)
      where TSelf : System.Numerics.INumber<TSelf>
      => value - (value % AssertNonNegative(multiple, nameof(multiple)));

    /// <summary>Get the two multiples nearest to value. Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include <paramref name="value"/> if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static TSelf MultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, RoundingMode mode, out TSelf multipleTowardsZero, out TSelf multipleAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      multipleAwayFromZero = multipleTowardsZero = MultipleOf(value, multiple);

      if (multipleAwayFromZero != value)
        multipleAwayFromZero += TSelf.CopySign(multiple, value);

      if (proper)
      {
        if (multipleTowardsZero == value)
          multipleTowardsZero -= TSelf.CopySign(multiple, value);
        if (multipleAwayFromZero == value)
          multipleAwayFromZero += TSelf.CopySign(multiple, value);
      }

      return value.RoundToBoundaries(mode, multipleTowardsZero, multipleAwayFromZero);
    }

    ///// <summary>Find the nearest (to <paramref name="value"/>) multiple away from zero (round-up). Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <returns>The nearest multiple to <paramref name="value"/> away from zero.</returns>
    //public static TSelf LocateMultipleOfAfz<TSelf>(this TSelf value, TSelf multiple, bool proper)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  LocateMultiplesOf(value, multiple, proper, out var _, out var multipleAwayFromZero);

    //  return multipleAwayFromZero;
    //}

    ///// <summary>Find the nearest (to <paramref name="value"/>) multiple towards zero (round-down). Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <returns>The nearest multiple to <paramref name="value"/> towards zero.</returns>
    //public static TSelf LocateMultipleOfTz<TSelf>(this TSelf value, TSelf multiple, bool proper)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  LocateMultiplesOf(value, multiple, proper, out var multipleTowardsZero, out var _);

    //  return multipleTowardsZero;
    //}

    ///// <summary>Find the nearest (to <paramref name="value"/>) of the two multiples <paramref name="multipleTowardsZero"/> and <paramref name="multipleAwayFromZero"/>, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters. Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <param name="mode"></param>
    ///// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    ///// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    ///// <returns>The nearest two multiples to value.</returns>
    //public static TSelf NearestMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, RoundingMode mode, out TSelf multipleTowardsZero, out TSelf multipleAwayFromZero)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  LocateMultiplesOf(value, multiple, proper, out multipleTowardsZero, out multipleAwayFromZero);

    //  return value.RoundToBoundaries(mode, multipleTowardsZero, multipleAwayFromZero);
    //}

    ///// <summary>Attempt to get the two multiples nearest to value. Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    ///// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    ///// <returns>Whether the operation was successful.</returns>
    //public static bool TryNearestMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, RoundingMode mode, out TSelf multipleTowardsZero, out TSelf multipleAwayFromZero, out TSelf nearestMultiple)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  try
    //  {
    //    nearestMultiple = NearestMultipleOf(value, multiple, proper, mode, out multipleTowardsZero, out multipleAwayFromZero);

    //    return true;
    //  }
    //  catch { }

    //  multipleTowardsZero = TSelf.Zero;
    //  multipleAwayFromZero = TSelf.Zero;

    //  nearestMultiple = TSelf.Zero;

    //  return false;
    //}

#else

    /// <summary>Get the two multiples nearest to value. Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static double MultipleOf(this double value, double multiple, bool proper, RoundingMode mode, out double multipleTowardsZero, out double multipleAwayFromZero)
    {
      if (multiple < 0) throw new System.ArgumentOutOfRangeException(nameof(multiple)); // AssertNonNegative(multiple, nameof(multiple));

      multipleTowardsZero = value - (value % multiple);
      multipleAwayFromZero = multipleTowardsZero == value ? multipleTowardsZero : multipleTowardsZero + System.Math.CopySign(multiple, value);

      if (proper)
      {
        if (multipleTowardsZero == value)
          multipleTowardsZero -= System.Math.CopySign(multiple, value);
        if (multipleAwayFromZero == value)
          multipleAwayFromZero += System.Math.CopySign(multiple, value);
      }

      return value.RoundToBoundaries(mode, multipleTowardsZero, multipleAwayFromZero);
    }

    /// <summary>Get the two multiples nearest to value. Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static System.Numerics.BigInteger MultipleOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger multiple, bool proper, RoundingMode mode, out System.Numerics.BigInteger multipleTowardsZero, out System.Numerics.BigInteger multipleAwayFromZero)
    {
      AssertNonNegative(multiple, nameof(multiple));

      multipleTowardsZero = value - (value % multiple);
      multipleAwayFromZero = multipleTowardsZero == value ? multipleTowardsZero : multipleTowardsZero + multiple * value.Sign;

      if (proper)
      {
        if (multipleTowardsZero == value)
          multipleTowardsZero -= multiple * value.Sign;
        if (multipleAwayFromZero == value)
          multipleAwayFromZero += multiple * value.Sign;
      }

      return value.RoundToBoundaries(mode, multipleTowardsZero, multipleAwayFromZero);
    }

    ///// <summary>Find the nearest (to <paramref name="value"/>) multiple away from zero (round-up). Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <returns>The nearest multiple to <paramref name="value"/> away from zero.</returns>
    //public static double LocateMultipleOfAfz<TSelf>(this double value, double multiple, bool proper)
    //{
    //  LocateMultipleOf(value, multiple, proper, out var _, out var multipleAwayFromZero);

    //  return multipleAwayFromZero;
    //}

    ///// <summary>Find the nearest (to <paramref name="value"/>) multiple towards zero (round-down). Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <returns>The nearest multiple to <paramref name="value"/> towards zero.</returns>
    //public static double LocateMultipleOfTz(this double value, double multiple, bool proper)
    //{
    //  LocateMultipleOf(value, multiple, proper, out var multipleTowardsZero, out var _);

    //  return multipleTowardsZero;
    //}

    ///// <summary>Find the nearest (to <paramref name="value"/>) of the two multiples <paramref name="multipleTowardsZero"/> and <paramref name="multipleAwayFromZero"/>, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters. Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <param name="mode"></param>
    ///// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    ///// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    ///// <returns>The nearest two multiples to value.</returns>
    //public static double NearestMultipleOf<Bogus>(this double value, double multiple, bool proper, RoundingMode mode, out double multipleTowardsZero, out double multipleAwayFromZero)
    //{
    //  LocateMultipleOf(value, multiple, proper, out multipleTowardsZero, out multipleAwayFromZero);

    //  return RoundToBoundary(value, mode, multipleTowardsZero, multipleAwayFromZero);
    //}

    ///// <summary>Attempt to get the two multiples nearest to value. Negative <paramref name="value"/> resilient.</summary>
    ///// <param name="value">The value for which the nearest multiples of will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    ///// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    ///// <returns>Whether the operation was successful.</returns>
    //public static bool TryNearestMultipleOf<Bogus>(this double value, double multiple, bool proper, RoundingMode mode, out double multipleTowardsZero, out double multipleAwayFromZero, out double nearestMultiple)
    //{
    //  try
    //  {
    //    nearestMultiple = NearestMultipleOf<Bogus>(value, multiple, proper, mode, out multipleTowardsZero, out multipleAwayFromZero);

    //    return true;
    //  }
    //  catch { }

    //  multipleTowardsZero = 0;
    //  multipleAwayFromZero = 0;

    //  nearestMultiple = 0;

    //  return false;
    //}

#endif
  }
}
