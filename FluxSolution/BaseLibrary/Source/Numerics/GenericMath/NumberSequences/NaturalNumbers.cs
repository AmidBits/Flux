#if NET7_0_OR_GREATER
namespace Flux.NumberSequences
{
  /// <summary>Creates a new sequence.</summary>
  public record class NaturalNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    private bool m_includeZero;

    public NaturalNumber(bool includeZero) => m_includeZero = includeZero;
    public NaturalNumber() : this(true) { }

    public bool IncludeZero { get => m_includeZero; set => m_includeZero = value; }

    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNaturalNumbers(bool includeZero = true)
    {
      for (var naturalNumber = includeZero ? System.Numerics.BigInteger.Zero : System.Numerics.BigInteger.One; ; naturalNumber++)
        yield return naturalNumber;
    }

    #region Implemented interfaces
    // INumberSequence
    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence() => GetNaturalNumbers(m_includeZero);

    // IEnumerable<>
    [System.Diagnostics.Contracts.Pure] public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion Implemented interfaces
  }
}
#endif
