namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a sequence of <paramref name="count"/> numbers alternating (as in larger and smaller) around <paramref name="source"/>, controlled by <paramref name="direction"/> (towards or away from <paramref name="source"/>) and <paramref name="stepSize"/> size.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="source">The mean around which the iteration takes place.</param>
    /// <param name="count">The number of iterations to execute.</param>
    /// <param name="stepSize">The stepping size between iterations. A positive number iterates from mean to extent, whereas a negative number iterates from extent to mean.</param>
    /// <param name="direction">Specified by <see cref="AlternatingLoopDirection"/>.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopAlternating<TNumber, TCount>(this TNumber source, TNumber stepSize, TCount count, AlternatingLoopDirection direction)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
    {
      if (TNumber.IsZero(stepSize)) throw new System.ArgumentOutOfRangeException(nameof(stepSize));
      if (TCount.IsNegative(count)) throw new System.ArgumentOutOfRangeException(nameof(count));

      switch (direction)
      {
        case AlternatingLoopDirection.AwayFromCenter:
          for (var index = TCount.One; index <= count; index++)
          {
            yield return source;

            source += stepSize * TNumber.CreateChecked(index);
            stepSize = -stepSize;
          }
          break;
        case AlternatingLoopDirection.TowardsCenter:
          source += stepSize * TNumber.CreateChecked(count).TruncRem(TNumber.One + TNumber.One, out TNumber _); // Setup the inital outer edge value for inward iteration.

          for (var index = count - TCount.One; index >= TCount.Zero; index--)
          {
            yield return source;

            source -= stepSize * TNumber.CreateChecked(index);
            stepSize = -stepSize;
          }
          break;
      }
    }
  }
}
