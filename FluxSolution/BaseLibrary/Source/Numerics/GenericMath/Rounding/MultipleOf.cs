#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two multiples nearest to value.</summary>
    /// <param name="x">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static void GetNearestMultipleOf<TSelf>(this TSelf x, TSelf multiple, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var offsetTowardsZero = x % multiple;

      nearestTowardsZero = x - offsetTowardsZero;
      nearestAwayFromZero = TSelf.IsNegative(x) ? nearestTowardsZero - multiple : nearestTowardsZero + multiple;

      if (proper)
      {
        if (nearestTowardsZero == x)
          nearestTowardsZero -= multiple;
        if (nearestAwayFromZero == x)
          nearestAwayFromZero -= multiple;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two multiples, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters.</summary>
    /// <param name="x">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf RoundToNearestMultipleOf<TSelf>(this TSelf x, TSelf multiple, bool proper, HalfwayRounding mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      GetNearestMultipleOf(x, multiple, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundToNearest(x, nearestTowardsZero, nearestAwayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two multiples, using the specified <see cref="IntegerRounding"/> <paramref name="mode"/>, and also return both multiples as out parameters.</summary>
    /// <param name="x">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf RoundToMultipleOf<TSelf>(this TSelf x, TSelf multiple, bool proper, IntegerRounding mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      GetNearestMultipleOf(x, multiple, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return RoundTo(x, nearestTowardsZero, nearestAwayFromZero, mode);
    }
  }
}
#endif
