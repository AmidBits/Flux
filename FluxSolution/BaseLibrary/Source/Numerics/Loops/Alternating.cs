namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of numbers starting with at the specified mean, how many numbers and step size, with every other number above/below the mean.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> LoopAlternating<TSelf>(this TSelf mean, TSelf count, TSelf step, Loops.AlternatingDirection direction)
      where TSelf : System.Numerics.INumber<TSelf>
      => new Loops.Alternating<TSelf>(mean, count, step, direction).GetSequence();
  }

  namespace Loops
  {
    /// <summary>Creates a new sequence.</summary>
    public record class Alternating<TNumber>
      : NumberSequences.INumericSequence<TNumber>
      where TNumber : System.Numerics.INumber<TNumber>
    {
      private TNumber m_mean;
      private TNumber m_count;
      private TNumber m_step;
      private AlternatingDirection m_direction;

      public Alternating(TNumber mean, TNumber count, TNumber step, AlternatingDirection direction)
      {
        m_mean = mean;
        m_count = count;
        m_step = step;
        m_direction = direction;
      }

      public static Alternating<TNumber> CreateBetween(TNumber source, TNumber target, TNumber step, AlternatingDirection direction)
        => new Alternating<TNumber>(source, TNumber.Abs(target - source) / step + TNumber.One, TNumber.Abs(step) is var absStep && source <= target ? absStep : -absStep, direction);
      public static Alternating<TNumber> CreateBetween(TNumber source, TNumber target, AlternatingDirection direction)
        => CreateBetween(source, target, TNumber.One, direction);

      // INumberSequence

      public System.Collections.Generic.IEnumerable<TNumber> GetSequence()
      {
        var mean = m_mean;
        var step = m_step;

        switch (m_direction)
        {
          case AlternatingDirection.AwayFromMean:
            for (var index = TNumber.One; index <= m_count; index++)
            {
              yield return mean;

              mean += step * index;
              step = -step;
            }
            break;
          case AlternatingDirection.TowardsMean:
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
    }
  }
}
