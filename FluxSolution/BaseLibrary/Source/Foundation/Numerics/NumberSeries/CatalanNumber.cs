namespace Flux.Numerics
{
  /// <summary>Creates a new sequence with Catalan numbers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  public record class CatalanNumber
  : INumberSequenceable<System.Numerics.BigInteger>
  {
    #region Static methods
    /// <summary>Returns the Catalan number for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Numerics.BigInteger GetCatalanNumber(System.Numerics.BigInteger number)
      => Maths.Factorial(number * 2) / (Maths.Factorial(number + 1) * Maths.Factorial(number));

    /// <summary>Creates a new sequence with Catalan numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetCatalanSequence()
    {
      for (var number = System.Numerics.BigInteger.Zero; ; number++)
        yield return GetCatalanNumber(number);
    }
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetCatalanSequence();

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
