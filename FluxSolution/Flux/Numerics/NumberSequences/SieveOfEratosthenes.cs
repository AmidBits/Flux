namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>This is a fast building sieve of Eratosthenes.</para>
    /// </summary>
    /// <param name="maxNumber">The max number of the sieve.</param>
    /// <returns></returns>
    /// <remarks>In .NET there is currently a maximum index limit for an array: 2,146,435,071 (0X7FEFFFFF). That number times 64 (137,371,844,544) is the practical limit of <paramref name="maxNumber"/>.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static Flux.DataStructures.BitArray64 CreateSieveOfEratosthenes(long maxNumber)
    {
      if (maxNumber < 1) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

      var ba = new Flux.DataStructures.BitArray64(maxNumber + 1, unchecked((long)0xAAAAAAAAAAAAAAAAUL)); // Bits represents the number line, so we start with all odd numbers being set to 1 and all even numbers set to 0.

      ba.Set(1, false); // One is not a prime so we set it to 0.
      ba.Set(2, true); // Two is the only even and the oddest prime so we set it to 1.

      var sqrt = (long)System.Math.Sqrt(maxNumber);

      var factor = 3L;

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

    //private static Flux.DataStructures.OrderedDictionary<int, int> DecimalPlaceValuePrimeCounts = new()
    //  {
    //    { 10, 4 },
    //    { 100, 25 },
    //    { 1000, 168 },
    //    { 10000, 1229 },
    //    { 100000, 9592 },
    //    { 1000000, 78498 },
    //    { 10000000, 664579 },
    //    { 100000000, 5761455 },
    //    //{ 1000000000, 50847534 }
    //  };

    //public static System.Collections.Generic.IEnumerable<(int primeNumber, int primeIndex)> EnumerateSieveOfEratosthenes(this System.Collections.BitArray source)
    //{
    //  if (source.Length >= 2)
    //    yield return (2, 0);

    //  if (source.Length >= 3)
    //    yield return (3, 1);

    //  var index = 2;

    //  for (var pn = 5; pn < source.Length; pn += 4)
    //  {
    //    if (source[pn]) yield return (pn, index);

    //    index++;

    //    pn += 2;

    //    if (pn < source.Length && source[pn]) yield return (pn, index);

    //    index++;
    //  }
    //}

    //public static System.Collections.Generic.IEnumerable<(int primeNumber, int primeIndex)> EnumerateSieveOfEratosthenes(this Flux.BitArray64 source)
    //{
    //  if (source.BitCount >= 2)
    //    yield return (2, 0);

    //  if (source.BitCount >= 3)
    //    yield return (3, 1);

    //  var index = 2;

    //  for (var pn = 5; pn < source.BitCount; pn += 4)
    //  {
    //    if (source.Get(pn)) yield return (pn, index);

    //    index++;

    //    pn += 2;

    //    if (pn < source.BitCount && source.Get(pn)) yield return (pn, index);

    //    index++;
    //  }
    //}

    //public static System.Collections.BitArray CreateSieveOfEratosthenes2(int length)
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

    ///// <summary>
    ///// <para>A very fast function that builds a map of prime numbers using a <see cref="System.Collections.BitArray"/> (booleans) up to <paramref name="maxNumber"/>. Optionally <paramref name="skipSettingEvenBitsToZero"/> can be used to optimize for speed.</para>
    ///// </summary>
    ///// <param name="maxNumber"></param>
    ///// <returns>A bit array with all primes set to 1. A side effect to this method for the sake of speed, depending on <paramref name="skipSettingEvenBitsToZero"/> it will either leave EVEN bits at 0, or set them to 1, so basically always disregard all even bits!</returns>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public static System.Collections.BitArray CreateSieveOfEratosthenes1(int maxNumber, bool skipSettingEvenBitsToZero = true)
    //{
    //  if (maxNumber < 1) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

    //  var ba = new System.Collections.BitArray(maxNumber + 1, true);

    //  var sqrt = (int)System.Math.Sqrt(maxNumber);

    //  var factor = 3;

    //  while (factor <= sqrt)
    //  {
    //    for (var num = factor; num <= maxNumber; num += 2)
    //      if (ba[num])
    //      {
    //        factor = num;
    //        break;
    //      }

    //    for (var num = factor * factor; num <= maxNumber; num += factor * 2)
    //      ba[num] = false;

    //    factor += 2;
    //  }

    //  if (skipSettingEvenBitsToZero)
    //    for (var num = 4; num <= maxNumber; num += 2)
    //      ba[num] = false;

    //  return ba;
    //}
  }
}

//namespace Flux.NumberSequences
//{
//  /// <summary>The Sieve of Eratosthenes is a simple, ancient algorithm for finding all prime numbers up to any given limit.</summary>
//  /// <see href="https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes"/>
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
