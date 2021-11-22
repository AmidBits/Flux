namespace Flux.Numerics
{
  public sealed class LeonardoNumber
    : ASequencedNumbers<System.Numerics.BigInteger>
  {
    /// <summary>This is the first number in the sequence (L0).</summary>
    public System.Numerics.BigInteger FirstNumber { get; init; } = 1;
    /// <summary>This is the second number in the sequence (L1).</summary>
    public System.Numerics.BigInteger SecondNumber { get; init; } = 1;

    /// <summary>This is size of increase between each iteration.</summary>
    public System.Numerics.BigInteger StepSize { get; init; } = 1;

    // INumberSequence
    /// <summary>Creates a new sequence with Leonardo numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Leonardo_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetLeonardoNumbers(FirstNumber, SecondNumber, StepSize);

    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetLeonardoNumbers(System.Numerics.BigInteger first, System.Numerics.BigInteger second, System.Numerics.BigInteger step)
    {
      while (true)
      {
        yield return first;

        (first, second) = (second, first + second + step);
      }
    }
  }
}
