namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of numbers starting with at the specified mean, how many numbers and step size, with every other number above/below the mean.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> LoopAlternating<TSelf>(this TSelf mean, TSelf count, TSelf step, Loops.AlternatingLoopDirection direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => new Loops.AlternatingLoop<TSelf>(mean, count, step, direction).GetSequence();
  }

  namespace Loops
  {
    /// <summary>Creates a new sequence.</summary>
    public record class AlternatingLoop<TNumber>
      : NumberSequences.INumericSequence<TNumber>
      where TNumber : System.Numerics.INumber<TNumber>
    {
      private readonly TNumber m_mean;
      private readonly TNumber m_count;
      private readonly TNumber m_step;
      private readonly AlternatingLoopDirection m_direction;

      /// <summary></summary>
      /// <param name="mean">This is the either the start value (<paramref name="direction"/> = <see cref="AlternatingLoopDirection.AwayFromMean"/>) or the last value (<paramref name="direction"/> = <see cref="AlternatingLoopDirection.TowardsMean"/>.</param>
      /// <param name="count">How many values should be produced.</param>
      /// <param name="step">This has dual purpose; first the sign dictates the initial direction of the alternating values, and secondly the absolute value is the stepping size for each iteration. A negative step results in the lower value first, and a positive results in the higher value first.</param>
      /// <param name="direction">Determines whether the loop is stepping further from <paramref name="mean"/> or closer to <paramref name="mean"/>.</param>
      public AlternatingLoop(TNumber mean, TNumber count, TNumber step, AlternatingLoopDirection direction)
      {
        m_mean = mean;
        m_count = count;
        m_step = step;
        m_direction = direction;
      }

      /// <summary>This is the either the start value (<see cref="Direction"/> = <see cref="AlternatingLoopDirection.AwayFromMean"/>) or the last value (<see cref="Direction"/> = <see cref="AlternatingLoopDirection.TowardsMean"/>.</summary>
      public TNumber Mean { get => m_mean; init => m_mean = value; }
      /// <summary>How many values should be produced.</summary>
      public TNumber Count { get => m_count; init => m_count = value; }
      /// <summary>This has dual purpose; first the sign dictates the initial direction of the alternating values, and secondly the absolute value is the stepping size for each iteration. A negative step results in the lower value first, and a positive results in the higher value first.</summary>
      public TNumber Step { get => m_step; init => m_step = value; }
      /// <summary>Determines whether the loop is stepping further from <see cref="Mean"/> or closer to <see cref="Mean"/>.</summary>
      public AlternatingLoopDirection Direction { get => m_direction; init => m_direction = value; }

      public static AlternatingLoop<TNumber> CreateBetween(TNumber source, TNumber target, TNumber step, AlternatingLoopDirection direction)
        => new(source, TNumber.Abs(target - source) / step + TNumber.One, TNumber.Abs(step) is var absStep && source <= target ? absStep : -absStep, direction);
      public static AlternatingLoop<TNumber> CreateBetween(TNumber source, TNumber target, AlternatingLoopDirection direction)
        => CreateBetween(source, target, TNumber.One, direction);

      #region Implemented interfaces

      // INumberSequence
      public System.Collections.Generic.IEnumerable<TNumber> GetSequence()
      {
        var mean = m_mean;
        var step = m_step;

        switch (m_direction)
        {
          case AlternatingLoopDirection.AwayFromMean:
            for (var index = TNumber.One; index <= m_count; index++)
            {
              yield return mean;

              mean += step * index;
              step = -step;
            }
            break;
          case AlternatingLoopDirection.TowardsMean:
            // Setup the inital outer edge value for inward iteration.
            mean += step * GenericMath.TruncMod(m_count, TNumber.One + TNumber.One, out var _);

            for (var index = m_count - TNumber.One; index >= TNumber.Zero; index--)
            {
              yield return mean;

              mean -= step * index;
              step = -step;
            }
            break;
        }
      }

      // IEnumerable<>
      public System.Collections.Generic.IEnumerator<TNumber> GetEnumerator() => GetSequence().GetEnumerator();
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

      #endregion Implemented interfaces
    }
  }
}