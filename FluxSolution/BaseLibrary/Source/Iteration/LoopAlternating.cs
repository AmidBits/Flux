namespace Flux
{
  public static partial class Iteration
  {
    /// <summary>Creates a sequence of <paramref name="count"/> numbers alternating (as in larger and smaller) around <paramref name="source"/>, controlled by <paramref name="direction"/> (towards or away from <paramref name="source"/>) and <paramref name="step"/> size.</summary>
    /// <param name="source">The mean around which the iteration takes place.</param>
    /// <param name="count">The number of iterations to execute.</param>
    /// <param name="step">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="direction">Specified by <see cref="AlternatingLoopDirection"/>.</param>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopAlternating<TNumber, TCount>(this TNumber source, TNumber step, TCount count, AlternatingLoopDirection direction)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
    {
      if (TNumber.IsZero(step)) throw new System.ArgumentOutOfRangeException(nameof(step));
      if (TCount.IsNegative(count)) throw new System.ArgumentOutOfRangeException(nameof(count));

      switch (direction)
      {
        case AlternatingLoopDirection.AwayFromCenter:
          for (var index = TCount.One; index <= count; index++)
          {
            yield return source;

            source += step * TNumber.CreateChecked(index);
            step = -step;
          }
          break;
        case AlternatingLoopDirection.TowardsCenter:
          source += step * TNumber.CreateChecked(count).TruncMod(TNumber.One + TNumber.One, out TNumber _); // Setup the inital outer edge value for inward iteration.

          for (var index = count - TCount.One; index >= TCount.Zero; index--)
          {
            yield return source;

            source -= step * TNumber.CreateChecked(index);
            step = -step;
          }
          break;
      }
    }
  }
}
