namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Loop toward or away-from and back-and-forth over <paramref name="mean"/>, in <paramref name="stepSize"/> for <paramref name="count"/> times.</para>
    /// <para>E.g. a direction = away-from, mean = 0, stepSize = -3 and count = 5, would yield the sequence [0, -3, 3, -6, 6].</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="source"></param>
    /// <param name="mean">This is the center of attention which the looping revolves around.</param>
    /// <param name="stepSize"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> Loop<TNumber, TCount>(this AlternatingLoopDirection source, TNumber mean, TNumber stepSize, TCount count)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
    {
      System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

      switch (source)
      {
        case AlternatingLoopDirection.AwayFromCenter:
          for (var index = TCount.One; index <= count; index++)
          {
            yield return mean;

            mean += stepSize * TNumber.CreateChecked(index);
            stepSize = -stepSize;
          }
          break;
        case AlternatingLoopDirection.TowardsCenter:
          mean += stepSize * TNumber.CreateChecked(count).TruncRem(TNumber.One + TNumber.One).TruncatedQuotient; // Setup the inital outer edge value for inward iteration.

          for (var index = count - TCount.One; index >= TCount.Zero; index--)
          {
            yield return mean;

            mean -= stepSize * TNumber.CreateChecked(index);
            stepSize = -stepSize;
          }
          break;
      }
    }
  }
}
