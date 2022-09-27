#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Get the two multiples nearest to value.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="towardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="awayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static void GetNearestMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, out TSelf towardsZero, out TSelf awayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var offsetTowardsZero = value % multiple;

      towardsZero = value - offsetTowardsZero;
      awayFromZero = TSelf.IsNegative(value) ? towardsZero - multiple : towardsZero + multiple;

      if (proper)
      {
        if (towardsZero == value)
          towardsZero -= multiple;
        if (awayFromZero == value)
          awayFromZero -= multiple;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two multiples, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="towardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="awayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf RoundToNearestMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, HalfwayRounding mode, out TSelf towardsZero, out TSelf awayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      GetNearestMultipleOf(value, multiple, proper, out towardsZero, out awayFromZero);

      return RoundToNearest(value, towardsZero, awayFromZero, mode);
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="value"/>) of two multiples, using the specified <see cref="FullRounding"/> <paramref name="mode"/>, and also return both multiples as out parameters.</summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">If true, ensure the multiples are not equal to value, i.e. the two multiples will be LT/GT instead of LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="towardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="awayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf RoundToMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool proper, FullRounding mode, out TSelf towardsZero, out TSelf awayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      GetNearestMultipleOf(value, multiple, proper, out towardsZero, out awayFromZero);

      return RoundTo(value, towardsZero, awayFromZero, mode);
    }
  }
}
#endif
