namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of numbers with the specified <paramref name="startNumber"/>, <paramref name="count"/> (iterations) and <paramref name="stepSize"/>.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> LoopRange<TSelf>(this TSelf startNumber, TSelf count, TSelf stepSize)
      where TSelf : System.Numerics.INumber<TSelf>
      => new Loops.Range<TSelf>(startNumber, count, stepSize).GetSequence();
  }

  namespace Loops
  {
    /// <summary>Creates a new sequence based on the range properties.</summary>
    public record class Range<TSelf>
      : NumberSequences.INumericSequence<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
    {
      private readonly TSelf m_startNumber;
      private readonly TSelf m_count;
      private readonly TSelf m_stepSize;

      public Range(TSelf startNumber, TSelf count, TSelf stepSize)
      {
        if (TSelf.IsNegative(count)) throw new System.ArgumentOutOfRangeException(nameof(count));

        m_startNumber = startNumber;
        m_count = count;
        m_stepSize = stepSize;
      }

      /// <summary>The first number in the sequence.</summary>
      public TSelf StartNumber { get => m_startNumber; init => m_startNumber = value; }
      /// <summary>How many numbers in the sequence.</summary>
      public TSelf Count { get => m_count; init => m_count = value; }
      /// <summary>The size between any two consecutive numbers, e.g. the first (StartNumber) and the second number, in the sequence.</summary>
      public TSelf StepSize { get => m_stepSize; init => m_stepSize = value; }

      #region Static methods
      public static Range<TSelf> CreateBetween(TSelf source, TSelf target, TSelf step)
        => new Range<TSelf>(source, TSelf.Abs(target - source) / step + TSelf.One, TSelf.Abs(step) is var absStep && source <= target ? absStep : -absStep);
      public static Range<TSelf> CreateBetween(TSelf source, TSelf target)
        => CreateBetween(source, target, TSelf.One);
      #endregion Static methods

      #region Implemented interfaces
      // INumberSequence

      public System.Collections.Generic.IEnumerable<TSelf> GetSequence()
      {
        for (TSelf n = m_startNumber, c = m_count - TSelf.One; c >= TSelf.Zero; n += m_stepSize, c--)
          yield return n;
      }

      // IEnumerable<>
      public System.Collections.Generic.IEnumerator<TSelf> GetEnumerator() => GetSequence().GetEnumerator();
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
      #endregion Implemented interfaces
    }
  }
}
