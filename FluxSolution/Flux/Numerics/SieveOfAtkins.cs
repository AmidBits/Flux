
//namespace Flux
//{
//  public static partial class PrimeNumbers
//  {
//    ///// <summary>
//    ///// <para>Determines if the number is a prime candidate. If not, it's definitely a composite.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="n"></param>
//    ///// <returns></returns>
//    //public static bool IsPrimeCandidate<TInteger>(this TInteger n)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //  => TInteger.CreateChecked(6) is var six && n < six ? int.CreateChecked(n) is 2 or 3 or 5 : int.CreateChecked(n % six) is 1 or 5;

//    ///// <summary>
//    ///// 
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="n"></param>
//    ///// <returns></returns>
//    //public static (TInteger TowardZero, TInteger AwayFromZero) GetPrimeCandidates<TInteger>(this TInteger n)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //{
//    //  var n3 = TInteger.CreateChecked(3);
//    //  var n5 = TInteger.CreateChecked(5);

//    //  if (n >= n5)
//    //  {
//    //    var r = n % TInteger.CreateChecked(6);

//    //    if (r == TInteger.One || r == n5) // E.g. 11 or 13 (12 = mod 6)
//    //      return (n, n);

//    //    if (TInteger.IsZero(r)) // E.g. 12 (mod 6)
//    //      return (n - TInteger.One, n + TInteger.One);

//    //    return (n - r + TInteger.One, n + n5 - r); // For all others we can use a formula.
//    //  }
//    //  else if (n == TInteger.CreateChecked(4))
//    //    return (n3, n5);
//    //  else if (n == n3)
//    //    return (n3, n3);
//    //  else // Less-than-or-equal-to 2:
//    //  {
//    //    var n2 = TInteger.CreateChecked(2);

//    //    return (n2, n2);
//    //  }
//    //}

//    ///// <summary>
//    ///// <para>A prime candidate is a number that is either -1 or +1 of a prime multiple, which is a multiple of 6. Obviously all prime candidates are not prime numbers, hence the name, but all prime numbers are prime candidates.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="mode"></param>
//    ///// <param name="towardZero"></param>
//    ///// <param name="awayFromZero"></param>
//    ///// <returns></returns>
//    //public static TInteger NearestPrimeCandidate<TInteger>(this TInteger n, HalfRounding mode, out TInteger towardZero, out TInteger awayFromZero)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //{
//    //  (towardZero, awayFromZero) = GetPrimeCandidates(n);

//    //  return n.RoundToNearest(mode, towardZero, awayFromZero);
//    //}

//    //public static (TInteger TowardZero, TInteger AwayFromZero) GetPrimeMilestones<TInteger>(this TInteger n)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //{
//    //  var rev = n.ReverseRemainderWithZero(TInteger.CreateChecked(6), out var rem);

//    //  return (n - rem, n + rev);
//    //}

//    ///// <summary>
//    ///// <para>A prime multiple (in this context) is a number that is a multiple of six (6) since all prime numbers (except for 2 and 3) are either a ('multiple of 6' - 1) or a ('multiple of 6' + 1). Four (4) is also an exception because technically it is also a prime multiple since 3 (4 - 1) and 5 (4 + 1) are prime numbers.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="mode"></param>
//    ///// <param name="towardZero"></param>
//    ///// <param name="awayFromZero"></param>
//    ///// <returns></returns>
//    //public static TInteger NearestPrimeMilestone<TInteger>(this TInteger n, HalfRounding mode, out TInteger towardZero, out TInteger awayFromZero)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //{
//    //  (towardZero, awayFromZero) = GetPrimeMilestones(n);

//    //  return n.RoundToNearest(mode, towardZero, awayFromZero);
//    //}

//    ///// <summary>
//    ///// 
//    ///// </summary>
//    ///// <param name="limit">The max number of the sieve.</param>
//    ///// <returns></returns>
//    ///// <remarks>In .NET there is currently a maximum index limit for an array: 2,146,435,071 (0X7FEFFFFF). That number times 64 (137,371,844,544) is the practical limit of <paramref name="limit"/>.</remarks>
//    //public static Flux.DataStructures.BitArray64 SieveOfAtkins(long limit)
//    //{
//    //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(limit);
//    //  System.ArgumentOutOfRangeException.ThrowIfGreaterThan(limit, 64L * System.Array.MaxIndexArrayOfMultiByteStructures);

//    //  var ba = new Flux.DataStructures.BitArray64(limit + 1, 0);

//    //  if (limit > 2) ba.Set(2, true);
//    //  if (limit > 3) ba.Set(3, true);

//    //  for (var x = 1L; x * x <= limit; x++)
//    //  {
//    //    for (var y = 1L; y * y <= limit; y++)
//    //    {
//    //      var n = (4 * x * x) + (y * y);
//    //      if (n <= limit && (n % 12 == 1 || n % 12 == 5))
//    //        if (!ba.Get(n))
//    //          ba.Set(n, true);

//    //      n = (3 * x * x) + (y * y);
//    //      if (n <= limit && n % 12 == 7)
//    //        if (!ba.Get(n))
//    //          ba.Set(n, true);

//    //      n = (3 * x * x) - (y * y);
//    //      if (x > y && n <= limit && n % 12 == 11)
//    //        if (!ba.Get(n))
//    //          ba.Set(n, true);
//    //    }
//    //  }

//    //  for (var i = 5L; i * i <= limit; i++)
//    //  {
//    //    if (!ba.Get(i))
//    //      continue;

//    //    for (var j = i * i; j <= limit; j += i * i)
//    //      ba.Set(j, false);
//    //  }

//    //  List<int> primes = new List<int>();
//    //  for (int i = 2; i <= limit; i++)
//    //  {
//    //    if (ba.Get(i))
//    //    {
//    //      primes.Add(i);
//    //    }
//    //  }
//    //  System.Console.WriteLine(string.Join(", ", primes));

//    //  return ba;
//    //}

//    //private static Flux.DataStructures.OrderedDictionary<int, int> DecimalPlaceValuePrimeCounts = new()
//    //  {
//    //    { 10, 4 },
//    //    { 100, 25 },
//    //    { 1000, 168 },
//    //    { 10000, 1229 },
//    //    { 100000, 9592 },
//    //    { 1000000, 78498 },
//    //    { 10000000, 664579 },
//    //    { 100000000, 5761455 },
//    //    //{ 1000000000, 50847534 }
//    //  };

//    //public static System.Collections.BitArray CreateSieveOfEratosthenes2(int length)
//    //{
//    //  if (length < 1) throw new System.ArgumentOutOfRangeException(nameof(length));

//    //  var ba = new System.Collections.BitArray(length + 1, false); // We are asking for NUMBERS, not indices.

//    //  if (length >= 2) ba[2] = true; // 2 is a prime.
//    //  if (length >= 3) ba[3] = true; // 3 is a prime.

//    //  for (var index = 6; index <= length; index += 6)
//    //  {
//    //    ba[index - 1] = true; // 6n-1 could be a prime.
//    //    if (index + 1 <= length) ba[index + 1] = true; // 6n+1 could be a prime.
//    //  }

//    //  var edge = length % 6;
//    //  if (edge == 0) ba[length - 1] = true; // The next to last element can be a prime. E.g. case is 6, so 5 will be set by this.
//    //  else if (edge == 5) ba[length] = true; // The last element can be a prime. E.g. case is 5, so 5 will be set by this.

//    //  var sqrt = (int)System.Math.Sqrt(length);

//    //  for (var odd = 3; odd <= sqrt; odd += 2) // Since only odd numbers were set in the previous loop, there can only be odd numbers to clear.
//    //    if (ba[odd]) // This is a prime number, so all multiples of its square cannot be prime numbers.
//    //      for (int sqr = odd * odd, oddX2 = odd * 2; sqr <= length; sqr += oddX2)
//    //        ba[sqr] = false;

//    //  return ba;
//    //}

//    ///// <summary>
//    ///// <para>A very fast function that builds a map of prime numbers using a <see cref="System.Collections.BitArray"/> (booleans) up to <paramref name="maxNumber"/>. Optionally <paramref name="skipSettingEvenBitsToZero"/> can be used to optimize for speed.</para>
//    ///// </summary>
//    ///// <param name="maxNumber"></param>
//    ///// <returns>A bit array with all primes set to 1. A side effect to this method for the sake of speed, depending on <paramref name="skipSettingEvenBitsToZero"/> it will either leave EVEN bits at 0, or set them to 1, so basically always disregard all even bits!</returns>
//    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    //public static System.Collections.BitArray SieveOfEratosthenesN(int maxNumber, bool skipSettingEvenBitsToZero = true)
//    //{
//    //  if (maxNumber < 1) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

//    //  var ba = new System.Collections.BitArray(maxNumber + 1, true);

//    //  var sqrt = (int)System.Math.Sqrt(maxNumber);

//    //  var factor = 3;

//    //  while (factor <= sqrt)
//    //  {
//    //    for (var num = factor; num <= maxNumber; num += 2)
//    //      if (ba[num])
//    //      {
//    //        factor = num;
//    //        break;
//    //      }

//    //    for (var num = factor * factor; num <= maxNumber; num += factor * 2)
//    //      ba[num] = false;

//    //    factor += 2;
//    //  }

//    //  if (skipSettingEvenBitsToZero)
//    //    for (var num = 4; num <= maxNumber; num += 2)
//    //      ba[num] = false;

//    //  return ba;
//    //}
//  }
//}

////namespace Flux.NumberSequences
////{
////  /// <summary>The Sieve of Eratosthenes is a simple, ancient algorithm for finding all prime numbers up to any given limit.</summary>
////  /// <see href="https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes"/>
////  public sealed class SieveOfEratosthenes
////    : System.Collections.Generic.IEnumerable<int>
////  {
////    private System.Collections.BitArray m_sieve = new(0);
////    /// <summary>Holds the boolean values for each index. Each index represents a number (true means it is a prime number, and false that it is not).</summary>
////    public System.Collections.BitArray Sieve
////      => m_sieve;

////    public bool this[int number]
////    {
////      get
////      {
////        if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));

////        if (number > Length)
////          m_sieve = CreateBitArray(number);

////        return m_sieve[number];
////      }
////    }

////    public int Length
////      => m_sieve.Length;

////    public SieveOfEratosthenes(int maxNumber)
////      => m_sieve = CreateBitArray(maxNumber);
////    public SieveOfEratosthenes()
////    {
////    }

////    public System.Collections.Generic.IEnumerable<int> GetCompositeNumbers()
////      => m_sieve.Cast<bool>().SelectWhere((e, i) => !e, (e, i) => i);
////    public System.Collections.Generic.IEnumerable<int> GetPrimeNumbers()
////      => m_sieve.Cast<bool>().SelectWhere((e, i) => e, (e, i) => i);

////    #region Static methods

////    public static System.Collections.BitArray CreateBitArray(int length)
////    {
////      if (length < 1) throw new System.ArgumentOutOfRangeException(nameof(length));

////      var ba = new System.Collections.BitArray(length + 1, false);

////      if (length >= 2) ba[2] = true; // 2 is a prime number.
////      if (length >= 3) ba[3] = true; // 3 is a prime number.

////      for (var index = 7; index <= length; index += 6)
////      {
////        ba[index - 2] = true; // 6n-1 can be a prime number.
////        ba[index] = true; // 6n+1 can be a prime number.
////      }

////      var edge = length % 6;
////      if (edge == 0) ba[length - 1] = true; // The next to last element can be a prime. E.g. case is 6, so 5 will be set by this.
////      else if (edge == 5) ba[length] = true; // The last element can be a prime. E.g. case is 5, so 5 will be set by this.

////      var sqrt = (int)System.Math.Sqrt(length);

////      for (var odd = 3; odd <= sqrt; odd += 2) // Since only odd numbers were set in the previous loop, there can only be odd numbers to clear.
////        if (ba[odd]) // This is a prime number, so all multiples of its square cannot be prime numbers.
////          for (int sqr = odd * odd, oddX2 = odd * 2; sqr <= length; sqr += oddX2)
////            ba[sqr] = false;

////      return ba;
////    }
////    #endregion Static methods

////    // IEnumerable<int>
////    public System.Collections.Generic.IEnumerator<int> GetEnumerator()
////      => GetPrimeNumbers().GetEnumerator();
////    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
////      => GetEnumerator();
////  }
////}
