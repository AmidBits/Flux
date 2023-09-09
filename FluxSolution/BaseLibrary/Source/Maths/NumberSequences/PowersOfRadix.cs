namespace Flux.NumberSequences
{
  /// <summary>Creates a new sequence with powers-of-radix values.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/PowersOfRadix"/>
  public record class PowersOfRadix
    : INumericSequence<System.Numerics.BigInteger>
  {
    public int Radix { get; set; }

    public PowersOfRadix(int radix) => Radix = radix;

    #region Static methods

    /// <summary>Creates a sequence of powers-of-radix values.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPowersOfRadixSequence(System.Numerics.BigInteger radix)
    {
      for (var index = 0; ; index++)
        yield return System.Numerics.BigInteger.Pow(radix, index);
    }

    #endregion Static methods

    #region Implemented interfaces

    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence() => GetPowersOfRadixSequence(Radix);

    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion Implemented interfaces
  }
}
