namespace Flux.NumberSequences
{
  public record class NsmoothNumber
    : INumericSequence<System.Numerics.BigInteger>
  {
    private readonly System.Collections.Generic.IReadOnlyList<System.Numerics.BigInteger> m_primeNumbers; // Needs to be converted to BitArray instead.

    public NsmoothNumber(System.Numerics.BigInteger n)
      => m_primeNumbers = System.Linq.Enumerable.ToList(System.Linq.Enumerable.TakeWhile(PrimeNumber.GetAscendingPrimes(System.Numerics.BigInteger.CreateChecked(2)), p => p <= n));

    /// <summary>Creates a new sequence of n-smooth numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Smooth_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>

    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNsmoothNumbers()
    {
      for (var number = System.Numerics.BigInteger.One; true; number++)
        if (IsNsmoothNumber(number))
          yield return number;
    }


    public bool IsNsmoothNumber(System.Numerics.BigInteger number)
    {
      if (number <= 1)
        return true;

      foreach (var p in m_primeNumbers)
        if (number % p == 0)
          return IsNsmoothNumber(number / p);

      return false;
    }

    #region Static methods
    /// <summary>Creates an 5-smooth number generator, a.k.a. regular numbers.</summary>
    public static NsmoothNumber RegularNumbers()
      => new(5);
    /// <summary>Creates an 7-smooth number generator, a.k.a. humble numbers.</summary>
    public static NsmoothNumber HumbleNumbers()
      => new(7);
    #endregion Static methods

    #region Implemented interfaces
    // INumberSequence

    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => GetNsmoothNumbers();


    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }
}
