namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Get the two multiples nearest to value. Negative <paramref name="number"/> resilient.</summary>
    /// <param name="number">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static void LocateNearestMultiple<TSelf>(this TSelf number, TSelf multiple, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (multiple <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(multiple));

      nearestTowardsZero = number - (number % multiple);
      nearestAwayFromZero = nearestTowardsZero == number ? nearestTowardsZero : nearestTowardsZero - multiple;

      if (proper)
      {
        if (nearestTowardsZero == number)
          nearestTowardsZero -= multiple.CopySign(number, out TSelf _);
        if (nearestAwayFromZero == number)
          nearestAwayFromZero += multiple.CopySign(number, out TSelf _);
      }
    }

    /// <summary>Find the nearest (to <paramref name="number"/>) of the two multiples <paramref name="nearestTowardsZero"/> and <paramref name="nearestAwayFromZero"/>, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters. Negative <paramref name="number"/> resilient.</summary>
    /// <param name="number">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf NearestMultiple<TSelf>(this TSelf number, TSelf multiple, bool proper, RoundingMode mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateNearestMultiple(number, multiple, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return BoundaryRounding<TSelf>.Round(number, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>Find the nearest (to <paramref name="number"/>) multiple away from zero (round-up). Negative <paramref name="number"/> resilient.</summary>
    /// <param name="number">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <returns>The nearest multiple to <paramref name="number"/> away from zero.</returns>
    public static TSelf NearestMultipleAwayFromZero<TSelf>(this TSelf number, TSelf multiple, bool proper)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateNearestMultiple(number, multiple, proper, out var _, out var nearestAwayFromZero);

      return nearestAwayFromZero;
    }


    /// <summary>Find the nearest (to <paramref name="number"/>) multiple towards zero (round-down). Negative <paramref name="number"/> resilient.</summary>
    /// <param name="number">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <returns>The nearest multiple to <paramref name="number"/> towards zero.</returns>
    public static TSelf NearestMultipleTowardsZero<TSelf>(this TSelf number, TSelf multiple, bool proper)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateNearestMultiple(number, multiple, proper, out var nearestTowardsZero, out var _);

      return nearestTowardsZero;
    }
  }
}
