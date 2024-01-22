namespace Flux
{
  public static partial class Iteration
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source">The iteration starts with this value.</param>
    /// <param name="count">The process iterates this many times.</param>
    /// <param name="stepSize">The value is increased this much each iteration.</param>
    /// <returns></returns>
    /// <remarks>This version runs indefinitely.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> Iterate<TSelf>(this TSelf source, TSelf stepSize, TSelf count)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (TSelf.IsZero(stepSize)) throw new System.ArgumentOutOfRangeException(nameof(stepSize));
      if (count < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (TSelf n = source, c = count - TSelf.One; c >= TSelf.Zero; n += stepSize, c--)
        yield return n;
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source">The iteration starts with this value.</param>
    /// <param name="stepSize">The value is increased this much each iteration.</param>
    /// <returns></returns>
    /// <remarks>This version runs indefinitely.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> Iterate<TSelf>(this TSelf source, TSelf stepSize)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (TSelf.IsZero(stepSize)) throw new System.ArgumentOutOfRangeException(nameof(stepSize));

      for (var n = source; ; n += stepSize)
        yield return n;
    }

#else
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Range(System.Numerics.BigInteger initial, System.Numerics.BigInteger count, System.Numerics.BigInteger step)
    {
      for (System.Numerics.BigInteger n = initial, c = count - System.Numerics.BigInteger.One; c >= System.Numerics.BigInteger.Zero; n += step, c--)
        yield return n;
    }

#endif
  }

  namespace Loops
  {
#if NET7_0_OR_GREATER

    ///// <summary>Creates a new sequence based on the range properties.</summary>
    //public record class RangeLoop<TSelf>
    //  : NumberSequences.INumericSequence<TSelf>
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  private readonly TSelf m_startNumber;
    //  private readonly TSelf m_count;
    //  private readonly TSelf m_stepSize;

    //  public RangeLoop(TSelf startNumber, TSelf count, TSelf stepSize)
    //  {
    //    Maths.AssertNonNegative(count, nameof(count));

    //    m_startNumber = startNumber;
    //    m_count = count;
    //    m_stepSize = stepSize;
    //  }

    //  /// <summary>The first number in the sequence.</summary>
    //  public TSelf StartNumber { get => m_startNumber; init => m_startNumber = value; }
    //  /// <summary>How many numbers in the sequence.</summary>
    //  public TSelf Count { get => m_count; init => m_count = value; }
    //  /// <summary>The size between any two consecutive numbers, e.g. the first (StartNumber) and the second number, in the sequence.</summary>
    //  public TSelf StepSize { get => m_stepSize; init => m_stepSize = value; }

    //  #region Static methods
    //  public static RangeLoop<TSelf> CreateBetween(TSelf source, TSelf target, TSelf step)
    //    => new(source, TSelf.Abs(target - source) / step + TSelf.One, TSelf.Abs(step) is var absStep && source <= target ? absStep : -absStep);
    //  public static RangeLoop<TSelf> CreateBetween(TSelf source, TSelf target)
    //    => CreateBetween(source, target, TSelf.One);
    //  #endregion Static methods

    //  #region Implemented interfaces

    //  // INumberSequence
    //  public System.Collections.Generic.IEnumerable<TSelf> GetSequence()
    //  {
    //    for (TSelf n = m_startNumber, c = m_count - TSelf.One; c >= TSelf.Zero; n += m_stepSize, c--)
    //      yield return n;
    //  }

    //  // IEnumerable<>
    //  public System.Collections.Generic.IEnumerator<TSelf> GetEnumerator() => GetSequence().GetEnumerator();
    //  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    //  #endregion Implemented interfaces
    //}

#else

    ///// <summary>Creates a new sequence based on the range properties.</summary>
    //public record class RangeLoop<Bogus>
    //  : NumberSequences.INumericSequence<System.Numerics.BigInteger>
    //{
    //  private readonly System.Numerics.BigInteger m_startNumber;
    //  private readonly System.Numerics.BigInteger m_count;
    //  private readonly System.Numerics.BigInteger m_stepSize;

    //  public RangeLoop(System.Numerics.BigInteger startNumber, System.Numerics.BigInteger count, System.Numerics.BigInteger stepSize)
    //  {
    //    if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

    //    m_startNumber = startNumber;
    //    m_count = count;
    //    m_stepSize = stepSize;
    //  }

    //  /// <summary>The first number in the sequence.</summary>
    //  public System.Numerics.BigInteger StartNumber { get => m_startNumber; init => m_startNumber = value; }
    //  /// <summary>How many numbers in the sequence.</summary>
    //  public System.Numerics.BigInteger Count { get => m_count; init => m_count = value; }
    //  /// <summary>The size between any two consecutive numbers, e.g. the first (StartNumber) and the second number, in the sequence.</summary>
    //  public System.Numerics.BigInteger StepSize { get => m_stepSize; init => m_stepSize = value; }

    //  #region Static methods
    //  public static RangeLoop<Bogus> CreateBetween(System.Numerics.BigInteger source, System.Numerics.BigInteger target, System.Numerics.BigInteger step)
    //    => new(source, System.Numerics.BigInteger.Abs(target - source) / step + System.Numerics.BigInteger.One, System.Numerics.BigInteger.Abs(step) is var absStep && source <= target ? absStep : -absStep);
    //  public static RangeLoop<Bogus> CreateBetween(System.Numerics.BigInteger source, System.Numerics.BigInteger target)
    //    => CreateBetween(source, target, System.Numerics.BigInteger.One);
    //  #endregion Static methods

    //  #region Implemented interfaces

    //  // INumberSequence
    //  public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
    //  {
    //    for (System.Numerics.BigInteger n = m_startNumber, c = m_count - System.Numerics.BigInteger.One; c >= System.Numerics.BigInteger.Zero; n += m_stepSize, c--)
    //      yield return n;
    //  }

    //  // IEnumerable<>
    //  public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
    //  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    //  #endregion Implemented interfaces
    //}

#endif
  }
}
