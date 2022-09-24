#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Loop
  {
    /// <summary>Creates a new sequence of numbers starting with at the specified mean, how many numbers and step size, with every other number above/below the mean.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> Alternating<TSelf>(TSelf mean, TSelf count, TSelf step, AlternatingDirection direction)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (count < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(count));

      switch (direction)
      {
        case AlternatingDirection.AwayFromMean:
          for (var index = TSelf.One; index <= count; index++)
          {
            yield return mean;

            mean += step * index;
            step = -step;
          }
          break;
        case AlternatingDirection.TowardsMean:
          // Setup the inital outer edge value for inward iteration.
          mean += step * GenericMath.TruncDivRem(count, TSelf.One + TSelf.One, out var _);

          for (var index = count - TSelf.One; index >= TSelf.Zero; index--)
          {
            yield return mean;

            mean -= step * index;
            step = -step;
          }
          break;
      }
    }
  }
}
#endif
