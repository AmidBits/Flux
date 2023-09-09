namespace Flux.NumberSequences
{
  /// <summary>Creates a new sequence with Leonardo numbers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Leonardo_number"/>
  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  public record class LeonardoNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    /// <summary>This is the first number in the sequence (L0).</summary>
    public System.Numerics.BigInteger FirstNumber { get; init; } = 1;
    /// <summary>This is the second number in the sequence (L1).</summary>
    public System.Numerics.BigInteger SecondNumber { get; init; } = 1;

    /// <summary>This is size of increase between each iteration.</summary>
    public System.Numerics.BigInteger StepSize { get; init; } = 1;

    #region Static methods
    /// <summary>Creates a new sequence with Leonardo numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Leonardo_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>

    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetLeonardoNumbers(System.Numerics.BigInteger first, System.Numerics.BigInteger second, System.Numerics.BigInteger step)
    {
      while (true)
      {
        yield return first;

        (first, second) = (second, first + second + step);
      }
    }
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => GetLeonardoNumbers(FirstNumber, SecondNumber, StepSize);

    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
