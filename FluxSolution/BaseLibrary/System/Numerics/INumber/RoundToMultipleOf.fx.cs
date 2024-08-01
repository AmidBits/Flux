namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the closest <paramref name="multiple"/> (i.e. of <paramref name="multipleOfTowardsZero"/> and <paramref name="multipleOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="value"/> resilient.</para>
    /// </summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="unequal">Proper means nearest but not <paramref name="value"/> if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="multipleOfTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleOfAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static TSelf RoundToMultipleOf<TSelf>(this TSelf value, TSelf multiple, bool unequal, RoundingMode mode, out TSelf multipleOfTowardsZero, out TSelf multipleOfAwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      multipleOfTowardsZero = MultipleOfTowardZero(value, multiple, unequal);
      multipleOfAwayFromZero = MultipleOfAwayFromZero(value, multiple, unequal);

      return value.RoundToBoundaries(mode, multipleOfTowardsZero, multipleOfAwayFromZero);
    }
  }
}
