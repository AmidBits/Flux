#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    public static (int, int) CountPrimesInSieveOfEratosthenes(this System.Collections.BitArray source)
    {
      var length = source.Length - 1;

      var count = source.Length > 3 ? 2 : source.Length > 2 ? 1 : 0;

      for (var index = 6; index < length; index += 6)
      {
        if (source[index - 1]) count++;
        if (index + 1 < source.Length && source[index + 1]) count++;
      }

      var edge = length % 6;
      if (edge == 0 && source[length - 1]) count++; // The next to last element can be a prime. E.g. case is 6, so 5 will be set by this.
      else if (edge == 5 && source[length]) count++; // The last element can be a prime. E.g. case is 5, so 5 will be set by this.

      return (count, length);
    }

    //public static System.Collections.BitArray CreateSieveOfEratosthenes(int length)
    //{
    //  if (length < 1) throw new System.ArgumentOutOfRangeException(nameof(length));

    //  var ba = new System.Collections.BitArray(length + 1, false); // We are asking for NUMBERS, not indices.

    //  if (length >= 2) ba[2] = true; // 2 is a prime.
    //  if (length >= 3) ba[3] = true; // 3 is a prime.

    //  for (var index = 6; index <= length; index += 6)
    //  {
    //    ba[index - 1] = true; // 6n-1 could be a prime.
    //    if (index + 1 <= length) ba[index + 1] = true; // 6n+1 could be a prime.
    //  }

    //  var edge = length % 6;
    //  if (edge == 0) ba[length - 1] = true; // The next to last element can be a prime. E.g. case is 6, so 5 will be set by this.
    //  else if (edge == 5) ba[length] = true; // The last element can be a prime. E.g. case is 5, so 5 will be set by this.

    //  var sqrt = (int)System.Math.Sqrt(length);

    //  for (var odd = 3; odd <= sqrt; odd += 2) // Since only odd numbers were set in the previous loop, there can only be odd numbers to clear.
    //    if (ba[odd]) // This is a prime number, so all multiples of its square cannot be prime numbers.
    //      for (int sqr = odd * odd, oddX2 = odd * 2; sqr <= length; sqr += oddX2)
    //        ba[sqr] = false;

    //  return ba;
    //}

    public static System.Collections.BitArray CreateSieveOfEratosthenes(int maxNumber)
    {
      if (maxNumber < 1) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

      var ba = new System.Collections.BitArray(maxNumber + 1, true);

      var sqrt = (int)System.Math.Sqrt(maxNumber);

      var factor = 3;

      while (factor <= sqrt)
      {
        for (var num = factor; num <= maxNumber; num += 2)
          if (ba[num])
          {
            factor = num;
            break;
          }

        for (var num = factor * factor; num <= maxNumber; num += factor * 2)
          ba[num] = false;

        factor += 2;
      }

      return ba;
    }
  }
}
#endif

//namespace Flux.NumberSequences
//{
//  /// <summary>The Sieve of Eratosthenes is a simple, ancient algorithm for finding all prime numbers up to any given limit.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes"/>
//  public sealed class SieveOfEratosthenes
//    : System.Collections.Generic.IEnumerable<int>
//  {
//    private System.Collections.BitArray m_sieve = new(0);
//    /// <summary>Holds the boolean values for each index. Each index represents a number (true means it is a prime number, and false that it is not).</summary>
//    public System.Collections.BitArray Sieve
//      => m_sieve;

//    public bool this[int number]
//    {
//      get
//      {
//        if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));

//        if (number > Length)
//          m_sieve = CreateBitArray(number);

//        return m_sieve[number];
//      }
//    }

//    public int Length
//      => m_sieve.Length;

//    public SieveOfEratosthenes(int maxNumber)
//      => m_sieve = CreateBitArray(maxNumber);
//    public SieveOfEratosthenes()
//    {
//    }

//    public System.Collections.Generic.IEnumerable<int> GetCompositeNumbers()
//      => m_sieve.Cast<bool>().SelectWhere((e, i) => !e, (e, i) => i);
//    public System.Collections.Generic.IEnumerable<int> GetPrimeNumbers()
//      => m_sieve.Cast<bool>().SelectWhere((e, i) => e, (e, i) => i);

//    #region Static methods

//    public static System.Collections.BitArray CreateBitArray(int length)
//    {
//      if (length < 1) throw new System.ArgumentOutOfRangeException(nameof(length));

//      var ba = new System.Collections.BitArray(length + 1, false);

//      if (length >= 2) ba[2] = true; // 2 is a prime number.
//      if (length >= 3) ba[3] = true; // 3 is a prime number.

//      for (var index = 7; index <= length; index += 6)
//      {
//        ba[index - 2] = true; // 6n-1 can be a prime number.
//        ba[index] = true; // 6n+1 can be a prime number.
//      }

//      var edge = length % 6;
//      if (edge == 0) ba[length - 1] = true; // The next to last element can be a prime. E.g. case is 6, so 5 will be set by this.
//      else if (edge == 5) ba[length] = true; // The last element can be a prime. E.g. case is 5, so 5 will be set by this.

//      var sqrt = (int)System.Math.Sqrt(length);

//      for (var odd = 3; odd <= sqrt; odd += 2) // Since only odd numbers were set in the previous loop, there can only be odd numbers to clear.
//        if (ba[odd]) // This is a prime number, so all multiples of its square cannot be prime numbers.
//          for (int sqr = odd * odd, oddX2 = odd * 2; sqr <= length; sqr += oddX2)
//            ba[sqr] = false;

//      return ba;
//    }
//    #endregion Static methods

//    // IEnumerable<int>
//    public System.Collections.Generic.IEnumerator<int> GetEnumerator()
//      => GetPrimeNumbers().GetEnumerator();
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//  }
//}
