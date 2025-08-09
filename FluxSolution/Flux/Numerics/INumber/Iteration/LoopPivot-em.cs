namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Loop toward or away-from and back-and-forth over <paramref name="mean"/>, in <paramref name="stepSize"/> for <paramref name="count"/> times.</para>
    /// <para>E.g. a direction = away-from, mean = 0, stepSize = -3 and count = 5, would yield the sequence [0, -3, 3, -6, 6].</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="source">The order of alternating numbers, either from mean to the outer limit, or from the outer limit to mean.</param>
    /// <param name="mean">This is the center of attention which the looping revolves around.</param>
    /// <param name="stepSize">The increasing (positive) and decreasing (negative) step size. Note, the min/max value of the loop inherits the same sign as step-size.</param>
    /// <param name="count">The number of numbers in the sequence.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopPivot<TNumber, TCount>(this TNumber source, CoordinateSystems.ReferenceRelativeOrientationTAf center, TNumber stepSize, TCount count)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
    {
      System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

      switch (center)
      {
        case CoordinateSystems.ReferenceRelativeOrientationTAf.AwayFrom:
          if (TCount.IsOddInteger(count)) stepSize = -stepSize;

          for (var index = TCount.One; index <= count; index++)
          {
            yield return source;

            source += stepSize * TNumber.CreateChecked(index);
            stepSize = -stepSize;
          }
          break;
        case CoordinateSystems.ReferenceRelativeOrientationTAf.Toward:
          source += stepSize * TNumber.CreateChecked(count).TruncRem(TNumber.One + TNumber.One).TruncatedQuotient; // Setup the inital outer edge value for inward iteration.

          for (var index = count - TCount.One; index >= TCount.Zero; index--)
          {
            yield return source;

            source -= stepSize * TNumber.CreateChecked(index);
            stepSize = -stepSize;
          }
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(center));
      }
    }
  }
}
