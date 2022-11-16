namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of numbers starting with at the specified mean, how many numbers and step size, with every other number above/below the mean.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> LoopRange<TSelf>(this TSelf start, TSelf count, TSelf step)
      where TSelf : System.Numerics.INumber<TSelf>
      => new Loops.Range<TSelf>(start, count, step).GetSequence();
  }

  namespace Loops
  {
    /// <summary>Creates a new sequence.</summary>
    public record class Range<TNumber>
    : INumericSequence<TNumber>
    where TNumber : System.Numerics.INumber<TNumber>
    {
      private TNumber m_start;
      private TNumber m_count;
      private TNumber m_step;

      public Range(TNumber start, TNumber count, TNumber step)
      {
        m_start = start;
        m_count = count;
        m_step = step;
      }

      public static Range<TNumber> CreateBetween(TNumber source, TNumber target, TNumber step)
        => new Range<TNumber>(source, TNumber.Abs(target - source) / step + TNumber.One, TNumber.Abs(step) is var absStep && source <= target ? absStep : -absStep);
      public static Range<TNumber> CreateBetween(TNumber source, TNumber target)
        => CreateBetween(source, target, TNumber.One);

      // INumberSequence
      [System.Diagnostics.Contracts.Pure]
      public System.Collections.Generic.IEnumerable<TNumber> GetSequence()
      {
        for (TNumber n = m_start, c = m_count - TNumber.One; c >= TNumber.Zero; n += m_step, c--)
          yield return n;
      }

      // IEnumerable<>
      [System.Diagnostics.Contracts.Pure] public System.Collections.Generic.IEnumerator<TNumber> GetEnumerator() => GetSequence().GetEnumerator();
      [System.Diagnostics.Contracts.Pure] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
  }
}
