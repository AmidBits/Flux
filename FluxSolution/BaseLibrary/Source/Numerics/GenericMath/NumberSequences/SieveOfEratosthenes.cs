#if NET7_0_OR_GREATER
namespace Flux.NumberSequences
{
  /// <summary>The Sieve of Eratosthenes is a simple, ancient algorithm for finding all prime numbers up to any given limit.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes"/>
  public sealed class SieveOfEratosthenes
    : System.Collections.Generic.IEnumerable<int>
  {
    private System.Collections.BitArray m_sieve = new(0);
    /// <summary>Holds the boolean values for each index. Each index represents a number (true means it is a prime number, and false that it is not).</summary>
    public System.Collections.BitArray Sieve
      => m_sieve;

    public bool this[int number]
    {
      get
      {
        if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));

        if (number > Length)
          m_sieve = CreateBitArray(number);

        return m_sieve[number];
      }
    }

    public int Length
      => m_sieve.Length;

    public SieveOfEratosthenes(int maxNumber)
      => m_sieve = CreateBitArray(maxNumber);
    public SieveOfEratosthenes()
    {
    }

    public System.Collections.Generic.IEnumerable<int> GetCompositeNumbers()
      => m_sieve.GetIndicesEqualToFalse();
    public System.Collections.Generic.IEnumerable<int> GetPrimeNumbers()
      => m_sieve.GetIndicesEqualToTrue();

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.BitArray CreateBitArray(int length)
    {
      if (length < 1) throw new System.ArgumentOutOfRangeException(nameof(length));

      var ba = new System.Collections.BitArray(length + 1, false);

      if (length >= 2) ba[2] = true; // 2 is a prime number.
      if (length >= 3) ba[3] = true; // 3 is a prime number.

      for (var index = 7; index <= length; index += 6)
      {
        ba[index - 2] = true; // 6n-1 can be a prime number.
        ba[index] = true; // 6n+1 can be a prime number.
      }

      var edge = length % 6;
      if (edge == 0) ba[length - 1] = true; // The next to last element can be a prime. E.g. case is 6, so 5 will be set by this.
      else if (edge == 5) ba[length] = true; // The last element can be a prime. E.g. case is 5, so 5 will be set by this.

      var sqrt = (int)System.Math.Sqrt(length);

      for (var odd = 3; odd <= sqrt; odd += 2) // Since only odd numbers were set in the previous loop, there can only be odd numbers to clear.
        if (ba[odd]) // This is a prime number, so all multiples of its square cannot be prime numbers.
          for (int sqr = odd * odd, oddX2 = odd * 2; sqr <= length; sqr += oddX2)
            ba[sqr] = false;

      return ba;
    }
    #endregion Static methods

    // IEnumerable<int>
    public System.Collections.Generic.IEnumerator<int> GetEnumerator()
      => GetPrimeNumbers().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }
}
#endif
