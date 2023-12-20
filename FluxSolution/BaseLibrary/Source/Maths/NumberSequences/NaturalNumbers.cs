#if NET7_0_OR_GREATER
namespace Flux.NumberSequences
{
  /// <summary>Creates a new sequence.</summary>
  public record class NaturalNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    private System.Numerics.BigInteger m_startNumber;

    public NaturalNumber(System.Numerics.BigInteger startNumber) => m_startNumber = startNumber;
    public NaturalNumber() : this(0) { }

    public System.Numerics.BigInteger StartNumber { get => m_startNumber; set => m_startNumber = value; }

    #region Static methods

    public static System.Collections.Generic.IEnumerable<TSelf> GetNaturalNumbers<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var naturalNumber = startAt; ; naturalNumber++)
        yield return naturalNumber;
    }

    #endregion // Static methods

    #region Implemented interfaces
    // INumberSequence

    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence() => GetNaturalNumbers(m_startNumber);

    // IEnumerable<>
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion Implemented interfaces
  }
}
#endif
