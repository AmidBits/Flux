namespace Flux.NumberSequences
{
  /// <summary>Creates a new sequence with Catalan numbers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
  /// <remarks>This function runs indefinitely, if allowed.</remarks>
  public sealed class CatalanNumber
  : INumericSequence<System.Numerics.BigInteger>
  {
    #region Static methods
    /// <summary>Returns the Catalan number for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>

    public static System.Numerics.BigInteger GetCatalanNumber(System.Numerics.BigInteger number)
      => (number * 2).ComputeFactorial(FactorialFunction.SplitFactorial) / ((number + 1).ComputeFactorial(FactorialFunction.SplitFactorial) * (number).ComputeFactorial(FactorialFunction.SplitFactorial));

    /// <summary>Creates a new sequence with Catalan numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Catalan_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>

    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetCatalanSequence()
    {
      for (var number = System.Numerics.BigInteger.Zero; ; number++)
        yield return GetCatalanNumber(number);
    }
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => GetCatalanSequence();


    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
