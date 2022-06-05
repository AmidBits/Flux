namespace Flux.Numerics
{
  /// <summary>Creates a new sequence with Moser/DeBruijn numbers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
  public record class MoserDeBruijnSequence
    : INumberSequenceable<System.Numerics.BigInteger>
  {
    public int MaxNumber { get; set; }

    public MoserDeBruijnSequence(int maxNumber)
      => MaxNumber = maxNumber;

    #region Static methods
    /// <summary>Creates a sequence of Moser/DeBruijn numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
    /// <seealso cref="https://www.geeksforgeeks.org/moser-de-bruijn-sequence/"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.List<System.Numerics.BigInteger> GetMoserDeBruijnSequence(System.Numerics.BigInteger maxNumber)
    {
      if (maxNumber < 0) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

      var sequence = new System.Collections.Generic.List<System.Numerics.BigInteger>() { 0 };

      if (maxNumber > 0)
        sequence.Add(1);

      for (var i = 2; i <= maxNumber; i++)
      {
        if (i % 2 == 0) // S(2 * n) = 4 * S(n)
          sequence.Add(4 * sequence[i / 2]);
        else // S(2 * n + 1) = 4 * S(n) + 1
          sequence.Add(4 * sequence[i / 2] + 1);
      }

      return sequence;
    }
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetMoserDeBruijnSequence(MaxNumber);

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
