namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Get the two multiples nearest to value. Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static void LocateMultiple<TSelf>(this TSelf value, TSelf multiple, bool proper, out TSelf multipleTowardsZero, out TSelf multipleAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      AssertNonNegative(multiple, nameof(multiple));

      multipleTowardsZero = value - (value % multiple);
      multipleAwayFromZero = multipleTowardsZero == value ? multipleTowardsZero : multipleTowardsZero + TSelf.CopySign(multiple, value);

      if (proper)
      {
        if (multipleTowardsZero == value)
          multipleTowardsZero -= TSelf.CopySign(multiple, value);
        if (multipleAwayFromZero == value)
          multipleAwayFromZero += TSelf.CopySign(multiple, value);
      }
    }

    /// <summary>Find the nearest (to <paramref name="value"/>) of the two multiples <paramref name="multipleTowardsZero"/> and <paramref name="multipleAwayFromZero"/>, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters. Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf NearestMultiple<TSelf>(this TSelf value, TSelf multiple, bool proper, RoundingMode mode, out TSelf multipleTowardsZero, out TSelf multipleAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateMultiple(value, multiple, proper, out multipleTowardsZero, out multipleAwayFromZero);

      return BoundaryRounding<TSelf, TSelf>.Round(value, mode, multipleTowardsZero, multipleAwayFromZero);
    }

    /// <summary>Find the nearest (to <paramref name="value"/>) multiple away from zero (round-up). Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <returns>The nearest multiple to <paramref name="value"/> away from zero.</returns>
    public static TSelf MultipleAwayFromZero<TSelf>(this TSelf value, TSelf multiple, bool proper)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateMultiple(value, multiple, proper, out var _, out var multipleAwayFromZero);

      return multipleAwayFromZero;
    }


    /// <summary>Find the nearest (to <paramref name="value"/>) multiple towards zero (round-down). Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <returns>The nearest multiple to <paramref name="value"/> towards zero.</returns>
    public static TSelf MultipleTowardsZero<TSelf>(this TSelf value, TSelf multiple, bool proper)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateMultiple(value, multiple, proper, out var multipleTowardsZero, out var _);

      return multipleTowardsZero;
    }

    /// <summary>Attempt to get the two multiples nearest to value. Negative <paramref name="value"/> resilient.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="multipleTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryNearestMultiple<TSelf>(this TSelf value, TSelf multiple, bool proper, RoundingMode mode, out TSelf multipleTowardsZero, out TSelf multipleAwayFromZero, out TSelf nearestMultiple)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      try
      {
        nearestMultiple = NearestMultiple(value, multiple, proper, mode, out multipleTowardsZero, out multipleAwayFromZero);

        return true;
      }
      catch { }

      multipleTowardsZero = TSelf.Zero;
      multipleAwayFromZero = TSelf.Zero;

      nearestMultiple = TSelf.Zero;

      return false;
    }
  }
}
