#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two multiples nearest to value.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static void GetNearestMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var offsetTowardsZero = value % multiple;

      nearestTowardsZero = value - offsetTowardsZero;
      nearestAwayFromZero = TSelf.IsNegative(value) ? nearestTowardsZero - multiple : nearestTowardsZero + multiple;

      if (proper)
      {
        if (nearestTowardsZero == value)
          nearestTowardsZero -= multiple;
        if (nearestAwayFromZero == value)
          nearestAwayFromZero -= multiple;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two multiples, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf RoundToNearestMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, HalfwayRounding mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      GetNearestMultipleOf(value, multiple, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundToNearest(value, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two multiples, using the specified <see cref="FullRounding"/> <paramref name="mode"/>, and also return both multiples as out parameters.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf RoundToMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, FullRounding mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      GetNearestMultipleOf(value, multiple, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundTo(value, nearestTowardsZero, nearestAwayFromZero, mode);
    }
  }
}
#endif
