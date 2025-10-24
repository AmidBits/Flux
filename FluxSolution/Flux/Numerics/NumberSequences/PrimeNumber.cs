//namespace Flux
//{
//  public static partial class PrimeNumbers
//  {
//    ///// <summary>
//    ///// <para>A prime candidate is a number that is either -1 or +1 of a prime multiple, which is a multiple of 6. Obviously all prime candidates are not prime numbers, hence the name, but all prime numbers are prime candidates.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="mode"></param>
//    ///// <param name="primeCandidateTowardZero"></param>
//    ///// <param name="primeCandidateAwayFromZero"></param>
//    ///// <returns></returns>
//    //public static TInteger PrimeCandidate<TInteger>(this TInteger value, UniversalRounding mode, out TInteger primeCandidateTowardZero, out TInteger primeCandidateAwayFromZero)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //{
//    //  var absValue = TInteger.Abs(value);

//    //  if (absValue > TInteger.CreateChecked(5))
//    //  {
//    //    var pmn = PrimeMilestone(value, mode, out var primeMultipleTowardZero, out var primeMultipleAwayFromZero);

//    //    var copySignOne = TInteger.CopySign(TInteger.One, value);

//    //    if (primeMultipleTowardZero == primeMultipleAwayFromZero)
//    //    {
//    //      primeCandidateTowardZero = pmn - copySignOne;
//    //      primeCandidateAwayFromZero = pmn + copySignOne;
//    //    }
//    //    else
//    //    {
//    //      primeCandidateTowardZero = primeMultipleTowardZero + copySignOne;
//    //      primeCandidateAwayFromZero = primeMultipleAwayFromZero - copySignOne;
//    //    }
//    //  }
//    //  else if (absValue == TInteger.One) // Yields a 2-tuple : 1 = (-2, 2) or -1 = (2, -2).
//    //  {
//    //    primeCandidateAwayFromZero = TInteger.CopySign(TInteger.CreateChecked(2), value);
//    //    primeCandidateTowardZero = -primeCandidateAwayFromZero;
//    //  }
//    //  else if (absValue == TInteger.CreateChecked(4)) // Yields value = 2-tuple : 4 = (3, 5) or -4 = (-3, -5).
//    //  {
//    //    primeCandidateTowardZero = TInteger.CopySign(TInteger.CreateChecked(3), value);
//    //    primeCandidateAwayFromZero = TInteger.CopySign(TInteger.CreateChecked(5), value);
//    //  }
//    //  else // Yields 2-tuples of any values : 5 or -5, 3 or -3, 2 or -2 or 0.
//    //    return primeCandidateTowardZero = primeCandidateAwayFromZero = value;

//    //  return TInteger.CopySign(value.RoundToNearest(mode, primeCandidateTowardZero, primeCandidateAwayFromZero), value);
//    //}

//    ///// <summary>
//    ///// <para>A prime multiple (in this context) is a number that is a multiple of six (6) since all prime numbers (except for 2 and 3) are either a ('multiple of 6' - 1) or a ('multiple of 6' + 1). Four (4) is also an exception because technically it is also a prime multiple since 3 (4 - 1) and 5 (4 + 1) are prime numbers.</para>
//    ///// </summary>
//    ///// <typeparam name="TInteger"></typeparam>
//    ///// <param name="value"></param>
//    ///// <param name="mode"></param>
//    ///// <param name="primeMultipleTowardZero"></param>
//    ///// <param name="primeMultipleAwayFromZero"></param>
//    ///// <returns></returns>
//    //public static TInteger PrimeMilestone<TInteger>(this TInteger value, UniversalRounding mode, out TInteger primeMultipleTowardZero, out TInteger primeMultipleAwayFromZero)
//    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    //{
//    //  var rev = value.ReverseRemainderWithZero(TInteger.CreateChecked(6), out var rem);

//    //  primeMultipleTowardZero = value - rem;
//    //  primeMultipleAwayFromZero = value + rev;

//    //  return TInteger.CopySign(value.RoundToNearest(mode, primeMultipleTowardZero, primeMultipleAwayFromZero), value);
//    //}

//    /// <summary>
//    /// <para>Creates a new sequence of ascending potential primes, greater-than-or-equal-to the specified <paramref name="value"/>.</para>
//    /// </summary>
//    public static System.Collections.Generic.IEnumerable<TInteger> GetAscendingPrimeCandidates<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      value.NearestPrimeCandidate(HalfRounding.TowardZero, out var tz, out var afz);

//      if (TInteger.CreateChecked(2) is var two && value <= two)
//        yield return two;

//      if (TInteger.CreateChecked(3) is var three && value <= three)
//        yield return three;

//      if (TInteger.CreateChecked(5) is var five && value <= five)
//        value = five;

//      var six = TInteger.CreateChecked(6);

//      var (quotient, remainder) = value.EnvelopedDivRem(six);

//      var multiple = quotient * six;

//      if (remainder <= TInteger.One) // Or, either between two potential primes or on right of a % 6 value. E.g. 12 or 13.
//      {
//        yield return multiple + TInteger.One;

//        multiple += six;
//      }

//      while (true)
//      {
//        yield return multiple - TInteger.One;
//        yield return multiple + TInteger.One;

//        multiple += six;
//      }
//    }

//    /// <summary>
//    /// <para>Creates a new sequence ascending prime numbers, greater-than-or-equal-to the specified <paramref name="value"/>.</para>
//    /// </summary>
//    public static System.Collections.Generic.IEnumerable<TInteger> GetAscendingPrimesX<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => GetAscendingPrimeCandidates(value).AsParallel().AsOrdered().Where(IsPrimeNumber2);

//    /// <summary>Creates a new sequence of descending potential primes, less than the specified <paramref name="value"/>.</summary>
//    public static System.Collections.Generic.IEnumerable<TInteger> GetDescendingPrimeCandidates<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      if (TInteger.CreateChecked(5) is var five && value >= five)
//      {
//        var six = TInteger.CreateChecked(6);

//        var (quotient, remainder) = TInteger.DivRem(value, six);

//        var multiple = (quotient + (remainder == five ? TInteger.One : TInteger.Zero)) * six;

//        if (remainder == TInteger.Zero || remainder == five) // Or, either between two potential primes or on left of (startAt % 6). E.g. 11 or 12.
//        {
//          yield return multiple - TInteger.One;

//          multiple -= six;
//        }

//        while (multiple >= six)
//        {
//          yield return multiple + TInteger.One;
//          yield return multiple - TInteger.One;

//          multiple -= six;
//        }
//      }

//      if (TInteger.CreateChecked(3) is var three && value >= three)
//        yield return three;

//      if (TInteger.CreateChecked(2) is var two && value >= two)
//        yield return two;
//    }

//    /// <summary>
//    /// <para>Creates a new sequence descending prime numbers, less-than-or-equal-to the specified <paramref name="value"/>.</para>
//    /// </summary>
//    public static System.Collections.Generic.IEnumerable<TInteger> GetDescendingPrimes<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => GetDescendingPrimeCandidates(value).AsParallel().AsOrdered().Where(IsPrimeNumber2);

//    public static System.Collections.Generic.IEnumerable<TInteger> GetClosestPrimeCandidates<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      var two = TInteger.CreateChecked(2);
//      var three = TInteger.CreateChecked(3);
//      //var four = TValue.CreateChecked(4);
//      //var five = TValue.CreateChecked(5);
//      var six = TInteger.CreateChecked(6);

//      var (quotient, remainder) = TInteger.DivRem(value, six);

//      var lo = quotient * six;
//      var hi = lo + six;

//      if (value <= two)
//      {
//        yield return two;
//        yield return three;
//        lo = -six;
//      }
//      else if (value <= three)
//      {
//        yield return three;
//        yield return two;
//        lo = -six;
//      }

//      if (remainder >= three)
//      {
//        while (true)
//        {
//          yield return hi - TInteger.One;
//          if (lo >= six) yield return lo + TInteger.One;
//          else if (TInteger.IsZero(lo)) yield return three;
//          yield return hi + TInteger.One;
//          if (lo >= six) yield return lo - TInteger.One;
//          else if (TInteger.IsZero(lo)) yield return two;
//          hi += six;
//          lo -= six;
//        }
//      }
//      else
//      {
//        while (true)
//        {
//          if (lo > TInteger.Zero) yield return lo + TInteger.One;
//          else if (TInteger.IsZero(lo)) yield return three;
//          yield return hi - TInteger.One;
//          if (lo > TInteger.Zero) yield return lo - TInteger.One;
//          else if (TInteger.IsZero(lo)) yield return two;
//          yield return hi + TInteger.One;
//          lo -= six;
//          hi += six;
//        }
//      }
//    }

//    public static System.Collections.Generic.IEnumerable<TInteger> GetClosestPrimes<TInteger>(TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => GetClosestPrimeCandidates(value).AsParallel().AsOrdered().Where(IsPrimeNumber2);

//    /// <summary>Returns a sequence of cousine primes, each of which is a pair of primes that differ by four.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Cousin_prime"/>
//    public static System.Collections.Generic.IEnumerable<(TInteger, TInteger)> GetCousinePrimes<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      foreach (var (leading, midling, trailing) in GetAscendingPrimesX(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)))
//      {
//        if (midling - leading == TInteger.CreateChecked(4))
//        {
//          yield return (leading, midling);
//        }
//        else if (trailing - leading == TInteger.CreateChecked(4))
//        {
//          yield return (leading, trailing);
//        }
//      }
//    }

//    /// <summary>
//    /// <para>Returns the infimum and supremum of <paramref name="value"/> (as the subset) of potential primes.</para>
//    /// </summary>
//    /// <param name="value">The target for locating the bounds.</param>
//    /// <returns></returns>
//    /// <remarks>Any value below 2, simply returns 2 for both infimum and supremum.</remarks>
//    public static (TInteger Infimum, TInteger Supremum) GetPrimeCandidatesInfimumAndSupremum<TInteger>(TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => TInteger.CreateChecked(2) is var two && value <= two // If the value is two-or-less.
//      ? (two, two)
//      : TInteger.CreateChecked(3) is var three && value == three // If the value is three.
//      ? (three, three)
//      : value == TInteger.CreateChecked(4) || (value % TInteger.CreateChecked(6) is var r && TInteger.IsZero(r))  // It's between two potential primes, e.g. the value = 3]4[5, the remainder = 5]6[7 or 11]12[13 (examples).
//      ? (value - TInteger.One, value + TInteger.One)
//      : TInteger.CreateChecked(5) is var five && (r == TInteger.One || r == five) // If the remainder is either a one or a five, i.e. a potential prime.
//      ? (value, value)
//      : (value - r + TInteger.One, value - r + five); // Otherwise locate the potential prime using the remainder.

//    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified value.</summary>
//    public static System.Collections.Generic.IEnumerable<TInteger> GetPrimeGaps<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => GetAscendingPrimesX(startAt).PartitionTuple2(false, (leading, trailing, index) => trailing - leading);

//    /// <summary>Returns a sequence of prime quadruplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8}.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet"/>
//    public static System.Collections.Generic.IEnumerable<(TInteger, TInteger, TInteger, TInteger)> GetPrimeQuadruplets<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      var two = TInteger.CreateChecked(2);
//      var six = TInteger.CreateChecked(6);
//      var eight = TInteger.CreateChecked(8);

//      var list = new System.Collections.Generic.List<TInteger>();

//      foreach (var primeNumber in GetAscendingPrimesX(startAt))
//      {
//        list.Add(primeNumber);

//        if (list.Count == 4)
//        {
//          if (list[1] - list[0] == two && list[2] - list[0] == six && list[3] - list[0] == eight)
//            yield return (list[0], list[1], list[2], list[3]);

//          list.RemoveAt(0);
//        }
//      }
//    }

//    /// <summary>Returns a sequence of prime quintuplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8} and {p-4 or p+12}.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_quintuplets"/>
//    public static System.Collections.Generic.IEnumerable<(TInteger, TInteger, TInteger, TInteger, TInteger)> GetPrimeQuintuplets<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      var two = TInteger.CreateChecked(2);
//      var four = TInteger.CreateChecked(4);
//      var six = TInteger.CreateChecked(6);
//      var eight = TInteger.CreateChecked(8);
//      var twelve = TInteger.CreateChecked(12);

//      var list = new System.Collections.Generic.List<TInteger>();

//      foreach (var primeNumber in GetAscendingPrimesX(startAt))
//      {
//        list.Add(primeNumber);

//        if (list.Count == 5)
//        {
//          if (list[1] - list[0] == four && list[2] - list[1] == two && list[3] - list[1] == six && list[4] - list[1] == eight)
//            yield return (list[0], list[1], list[2], list[3], list[4]);
//          else if (list[1] - list[0] == two && list[2] - list[0] == six && list[3] - list[0] == eight && list[4] - list[0] == twelve)
//            yield return (list[0], list[1], list[2], list[3], list[4]);

//          list.RemoveAt(0);
//        }
//      }
//    }

//    /// <summary>Returns a sequence of prime sextuplets, each of which is a set of six primes of the form {p-4, p, p+2, p+6, p+8, p+12}.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_sextuplets"/>
//    public static System.Collections.Generic.IEnumerable<(TInteger, TInteger, TInteger, TInteger, TInteger, TInteger)> GetPrimeSextuplets<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      var two = TInteger.CreateChecked(2);
//      var four = TInteger.CreateChecked(4);
//      var six = TInteger.CreateChecked(6);
//      var eight = TInteger.CreateChecked(8);
//      var twelve = TInteger.CreateChecked(12);

//      var list = new System.Collections.Generic.List<TInteger>();

//      foreach (var primeNumber in GetAscendingPrimesX(startAt))
//      {
//        list.Add(primeNumber);

//        if (list.Count == 6)
//        {
//          if (list[1] - list[0] == four && list[2] - list[1] == two && list[3] - list[1] == six && list[4] - list[1] == eight && list[5] - list[1] == twelve)
//            yield return (list[0], list[1], list[2], list[3], list[4], list[5]);

//          list.RemoveAt(0);
//        }
//      }
//    }

//    /// <summary>Returns a sequence of prime triplets, each of which is a set of three prime numbers of the form (p, p + 2, p + 6) or (p, p + 4, p + 6).</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Prime_triplet"/>
//    public static System.Collections.Generic.IEnumerable<(TInteger, TInteger, TInteger)> GetPrimeTriplets<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => 0 is var index ? GetAscendingPrimesX(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)).Where((t) => t.trailing - t.leading is var gap3to1 && gap3to1 == TInteger.CreateChecked(6) && t.midling - t.leading is var gap2to1 && (gap2to1 == TInteger.CreateChecked(2) || gap2to1 == TInteger.CreateChecked(4))) : throw new System.Exception();

//    /// <summary>Returns a sequence of super-primes, which is a subsequence of prime numbers that occupy prime-numbered positions within the sequence of all prime numbers.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Super-prime"/>
//    public static System.Collections.Generic.IEnumerable<TInteger> GetSuperPrimes<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => GetAscendingPrimesX(startAt).Where((p, i) => IsPrimeNumber2(i + 1));

//    /// <summary>Returns a sequence of teim primes, each of which is a pair of primes that differ by two.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Twin_prime"/>
//    public static System.Collections.Generic.IEnumerable<(TInteger, TInteger)> GetTwinPrimes<TInteger>(TInteger startAt)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => GetAscendingPrimesX(startAt).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Where((t) => t.trailing - t.leading == TInteger.CreateChecked(2));

//    /// <summary>Indicates whether the prime value is also an additive prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/List_of_prime_numbers#Additive_primes"/>
//    public static bool IsAlsoAdditivePrime<TInteger>(TInteger primeNumber)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => IsPrimeNumber2(primeNumber.DigitSum(10));

//    /// <summary>Indicates whether the prime value is also a congruent modulo prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    public static bool IsAlsoCongruentModuloPrime<TInteger>(TInteger primeNumber, TInteger a, TInteger d)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => primeNumber % a == d;

//    /// <summary>Indicates whether the prime value is also an Eisenstein prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Eisenstein_prime"/>
//    public static bool IsAlsoEisensteinPrime<TInteger>(TInteger primeNumber)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => IsAlsoCongruentModuloPrime(primeNumber, TInteger.CreateChecked(3), TInteger.CreateChecked(2));

//    /// <summary>Indicates whether the prime value is also a Gaussian prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Gaussian_integer#Gaussian_primes"/>
//    public static bool IsAlsoGaussianPrime<TInteger>(TInteger primeNumber)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => IsAlsoCongruentModuloPrime(primeNumber, TInteger.CreateChecked(4), TInteger.CreateChecked(3));

//    /// <summary>Indicates whether the prime value is also a left truncatable prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Truncatable_prime"/>
//    public static bool IsAlsoLeftTruncatablePrime<TInteger>(TInteger primeNumber)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      var text = primeNumber.ToString(null, System.Globalization.CultureInfo.InvariantCulture);

//      if (text.IndexOf('0', System.StringComparison.Ordinal) > -1)
//      {
//        return false;
//      }

//      while (text.Length > 0 && System.Numerics.BigInteger.TryParse(text, out var result))
//      {
//        if (!IsPrimeNumber2(result))
//        {
//          return false;
//        }

//        text = text[1..];
//      }

//      return true;
//    }

//    /// <summary>Indicates whether the prime value is also a Pythagorean prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Pythagorean_prime"/>
//    public static bool IsAlsoPythagoreanPrime<TInteger>(TInteger primeNumber)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => IsAlsoCongruentModuloPrime(primeNumber, TInteger.CreateChecked(4), TInteger.CreateChecked(1));

//    /// <summary>Indicates whether the prime value is also a safe prime prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Safe_prime"/>
//    public static bool IsAlsoSafePrime<TInteger>(TInteger primeNumber)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => IsPrimeNumber2((primeNumber - TInteger.One) / TInteger.CreateChecked(2));

//    /// <summary>Indicates whether the prime value is also a Sophie Germain prime.</summary>
//    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Sophie_Germain_prime"/>
//    public static bool IsAlsoSophieGermainPrime<TInteger>(TInteger primeNumber)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//      => IsPrimeNumber2((primeNumber * TInteger.CreateChecked(2)) + TInteger.One);

//    ///// <summary>Indicates whether a specified value is a prime candidate.</summary>

//    //public static bool IsPrimeCandidate(System.Numerics.BigInteger value)
//    //  => value % 6 is var remainder && (remainder == 5 || remainder == 1);

//    ///// <summary>Indicates whether a specified value is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
//    //public static bool IsPrimeCandidate(System.Numerics.BigInteger value, out System.Numerics.BigInteger multiplier, out System.Numerics.BigInteger offset)
//    //{
//    //  multiplier = System.Numerics.BigInteger.DivRem(value, 6, out offset);

//    //  if (offset == 5)
//    //  {
//    //    multiplier++;
//    //    offset = -1;

//    //    return true;
//    //  }
//    //  else return offset == 1;
//    //}

//    /// <summary>
//    /// <para>Indicates whether a <paramref name="value"/> is a prime.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Primality_test"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Prime_number"/></para>
//    /// </summary>
//    public static bool IsPrimeNumber2<TInteger>(this TInteger value)
//      where TInteger : System.Numerics.IBinaryInteger<TInteger>
//    {
//      var two = TInteger.CreateChecked(2);
//      var three = TInteger.CreateChecked(3);

//      if (value <= three)
//        return value >= two;

//      if (TInteger.IsZero(value % two) || TInteger.IsZero(value % three))
//        return false;

//      var limit = value.IntegerSqrt();

//      var six = TInteger.CreateChecked(6);

//      for (var k = TInteger.CreateChecked(5); k <= limit; k += six)
//        if (TInteger.IsZero(value % k) || TInteger.IsZero(value % (k + two)))
//          return false;

//      return true;
//    }

//    ///// <summary>Indicates whether a specified value is a prime.</summary>
//    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
//    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

//    //public static bool IsPrimeNumber(System.Numerics.BigInteger value)
//    //{
//    //  if (value <= long.MaxValue)
//    //    return IsPrimeNumber((long)value);

//    //  if (value <= 3)
//    //    return value >= 2;

//    //  if (value % 2 == 0 || value % 3 == 0)
//    //    return false;

//    //  var limit = value.IntegerSqrt();

//    //  for (var k = 5; k <= limit; k += 6)
//    //    if ((value % k) == 0 || (value % (k + 2)) == 0)
//    //      return false;

//    //  return true;
//    //}
//    ///// <summary>Indicates whether a specified value is a prime.</summary>
//    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
//    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

//    //public static bool IsPrimeNumber(int source)
//    //{
//    //  if (source <= 3)
//    //    return source >= 2;

//    //  if (source % 2 == 0 || source % 3 == 0)
//    //    return false;

//    //  var limit = System.Math.Sqrt(source);

//    //  for (var k = 5; k <= limit; k += 6)
//    //    if ((source % k) == 0 || (source % (k + 2)) == 0)
//    //      return false;

//    //  return true;
//    //}
//    ///// <summary>Indicates whether a specified value is a prime.</summary>
//    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
//    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

//    //public static bool IsPrimeNumber(long source)
//    //{
//    //  if (source <= int.MaxValue)
//    //    return IsPrimeNumber((int)source);

//    //  if (source <= 3)
//    //    return source >= 2;

//    //  if (source % 2 == 0 || source % 3 == 0)
//    //    return false;

//    //  var limit = System.Math.Sqrt(source);

//    //  for (var k = 5; k <= limit; k += 6)
//    //    if ((source % k) == 0 || (source % (k + 2)) == 0)
//    //      return false;

//    //  return true;
//    //}


//  }
//}

////#if NET7_0_OR_GREATER
////namespace Flux.NumberSequences
////{
////  public sealed partial class PrimeNumber
////    : INumericSequence<System.Numerics.BigInteger>
////  {
////    #region Constants
////    /// <summary>Represents the largest prime value possible in a byte (unsigned).</summary>
////    public const byte LargestPrimeByte = 251;
////    /// <summary>Represents the largest prime value possible in a 16-bit integer.</summary>
////    public const short LargestPrimeInt16 = 32749;
////    /// <summary>Represents the largest prime value possible in a 32-bit integer.</summary>
////    public const int LargestPrimeInt32 = 2147483647;
////    /// <summary>Represents the largest prime value possible in a 64-bit integer.</summary>
////    public const long LargestPrimeInt64 = 9223372036854775783;
////    /// <summary>Represents the largest prime value possible in a signed byte.</summary>
////    [System.CLSCompliant(false)]
////    public const sbyte LargestPrimeSByte = 127;
////    /// <summary>Represents the largest prime value possible in a 16-bit unsigned integer.</summary>
////    [System.CLSCompliant(false)]
////    public const ushort LargestPrimeUInt16 = 65521;
////    /// <summary>Represents the largest prime value possible in a 32-bit unsigned integer.</summary>
////    [System.CLSCompliant(false)]
////    public const uint LargestPrimeUInt32 = 4294967291;
////    /// <summary>Represents the largest prime value possible in a 64-bit unsigned integer.</summary>
////    [System.CLSCompliant(false)]
////    public const ulong LargestPrimeUInt64 = 18446744073709551557;

////    /// <summary>Represents the smallest prime value.</summary>
////    public const int SmallestPrime = 2;
////    #endregion Constants

////    #region Static members

////    /// <summary>Creates a new sequence of ascending potential primes, greater-than-or-equal-to the specified value.</summary>
////    public static System.Collections.Generic.IEnumerable<TValue> GetAscendingPotentialPrimes<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      if (TValue.CreateChecked(2) is var two && startAt <= two)
////        yield return two;

////      if (TValue.CreateChecked(3) is var three && startAt <= three)
////        yield return three;

////      if (TValue.CreateChecked(5) is var five && startAt <= five)
////        startAt = five;

////      var six = TValue.CreateChecked(6);

////      var (quotient, remainder) = TValue.DivRem(startAt, six);

////      var multiple = (quotient + (remainder > TValue.One ? TValue.One : TValue.Zero)) * six;

////      if (remainder <= TValue.One) // Or, either between two potential primes or on right of a % 6 value. E.g. 12 or 13.
////      {
////        yield return multiple + TValue.One;

////        multiple += six;
////      }

////      while (true)
////      {
////        yield return multiple - TValue.One;
////        yield return multiple + TValue.One;

////        multiple += six;
////      }
////    }

////    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPotentialPrimes(System.Numerics.BigInteger startAt)
////    //{
////    //  var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

////    //  var multiple = 6 * (quotient + (remainder > 1 ? 1 : 0));

////    //  if (quotient == 0) // If startAt is less than 6.
////    //  {
////    //    if (startAt <= 2) yield return 2;
////    //    if (startAt <= 3) yield return 3;

////    //    multiple = 6;
////    //  }
////    //  else if (remainder <= 1) // Or, either between two potential primes or on right of a % 6 value. E.g. 12 or 13.
////    //  {
////    //    yield return multiple + 1;
////    //    multiple += 6;
////    //  }

////    //  while (true)
////    //  {
////    //    yield return multiple - 1;
////    //    yield return multiple + 1;

////    //    multiple += 6;
////    //  }
////    //}

////    /// <summary>Creates a new sequence ascending prime numbers, greater-than-or-equal-to the specified value.</summary>
////    public static System.Collections.Generic.IEnumerable<TValue> GetAscendingPrimes<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////      => GetAscendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

////    /// <summary>Creates a new sequence of descending potential primes, less than the specified value.</summary>
////    public static System.Collections.Generic.IEnumerable<TValue> GetDescendingPotentialPrimes<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      if (TValue.CreateChecked(5) is var five && startAt >= five)
////      {
////        var six = TValue.CreateChecked(6);

////        var (quotient, remainder) = TValue.DivRem(startAt, six);

////        var multiple = (quotient + (remainder == five ? TValue.One : TValue.Zero)) * six;

////        if (remainder == TValue.Zero || remainder == five) // Or, either between two potential primes or on left of (startAt % 6). E.g. 11 or 12.
////        {
////          yield return multiple - TValue.One;

////          multiple -= six;
////        }

////        while (multiple >= six)
////        {
////          yield return multiple + TValue.One;
////          yield return multiple - TValue.One;

////          multiple -= six;
////        }
////      }

////      if (TValue.CreateChecked(3) is var three && startAt >= three)
////        yield return three;

////      if (TValue.CreateChecked(2) is var two && startAt >= two)
////        yield return two;
////    }

////    /// <summary>Creates a new sequence descending prime numbers, less-than-or-equal-to the specified value.</summary>
////    public static System.Collections.Generic.IEnumerable<TValue> GetDescendingPrimes<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////      => GetDescendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

////    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPotentialPrimes2(System.Numerics.BigInteger value)
////    //{
////    //  if (value < 0) throw new System.ArgumentOutOfRangeException(nameof(value));

////    //  var nm = GenericMath.NearestMultiple(value, 6, false, RoundingMode.HalfTowardZero, out var tz, out var afz);

////    //  Flux.BoundaryRounding<System.Numerics.BigInteger, System.Numerics.BigInteger>.MeasureDistanceToBoundaries(value, tz, afz, out System.Numerics.BigInteger dtz, out System.Numerics.BigInteger dafz);

////    //  if (value <= 2)
////    //  {
////    //    yield return 2;
////    //    yield return 3;
////    //    tz = -6;
////    //  }
////    //  else if (value <= 3)
////    //  {
////    //    yield return 3;
////    //    yield return 2;
////    //    tz = -6;
////    //  }

////    //  if (nm == afz)
////    //  {
////    //    while (true)
////    //    {
////    //      //if (tz < 0) break;
////    //      yield return afz - 1;
////    //      if (tz >= 6) yield return tz + 1;
////    //      else if (tz == 0) yield return 3;
////    //      yield return afz + 1;
////    //      if (tz >= 6) yield return tz - 1;
////    //      else if (tz == 0) yield return 2;
////    //      afz += 6;
////    //      tz -= 6;
////    //    }
////    //  }
////    //  else // Assumption here.
////    //  {
////    //    while (true)
////    //    {
////    //      //if (tz < 0) break;
////    //      if (tz > 0) yield return tz + 1;
////    //      else if (tz == 0) yield return 3;
////    //      yield return afz - 1;
////    //      if (tz > 0) yield return tz - 1;
////    //      else if (tz == 0) yield return 2;
////    //      yield return afz + 1;
////    //      tz -= 6;
////    //      afz += 6;
////    //    }
////    //  }

////    //  while (true)
////    //  {
////    //    yield return afz - 1;
////    //    yield return afz + 1;
////    //    afz += 6;
////    //  }
////    //}

////    /// <summary>Finds the nearest potential prime multiple (i.e. a multiple of 6) of <paramref name="value"/>, and if needed, apply the specified rounding <paramref name="mode"/>. Also returns the <paramref name="nearestPotentialPrimeMultipleOffset"/> as an output parameter.</summary>
////    /// <param name="value">The target value.</param>
////    /// <param name="mode">The <see cref="RoundingMode"/> to use if exactly between two multiples.</param>
////    /// <param name="nearestPotentialPrimeMultipleOffset">The offset direction from the returned potential prime multiple, or the multiple itself: +1 = nearer TZ, -1 = nearer AFZ, 0 = exactly halfway between TZ and AFZ, or <paramref name="value"/> if it is a potential prime multiple.</param>
////    /// <returns></returns>
////    //public static System.Numerics.BigInteger GetNearestPotentialPrimeMultiple(System.Numerics.BigInteger value, RoundingMode mode, out System.Numerics.BigInteger nearestPotentialPrimeMultipleOffset)
////    //{
////    //  value.RoundToMultipleOf(6, false, RoundingMode.HalfTowardZero, out var multipleTowardsZero, out var multipleAwayFromZero);

////    //  var nm = ((double)value).RoundToBoundaries(mode, (double)multipleTowardsZero, (double)multipleAwayFromZero);

////    //  var binm = new System.Numerics.BigInteger(nm);
////    //  var binmtz = multipleTowardsZero;
////    //  var binmafz = multipleAwayFromZero;

////    //  nearestPotentialPrimeMultipleOffset = binmtz == binmafz ? value // If TZ and AFZ are equal (i.e. NearestMultiple(), above, returns value for both TZ and AFZ, if value is a multiple, and 'proper' is false), so value is a multiple.
////    //    : value - binmtz <= 3 ? +1 // The difference between TZ and value is less than 3, therefor closer to TZ.
////    //    : binmafz - value <= 3 ? -1 // The difference between AFZ and value is less than 3, therefor closer to AFZ.
////    //    : 0; // The difference between either TZ/AFZ and value is exactly 3, therefor value is exactly halfway between TZ and AFZ.

////    //  return binm;
////    //}

////    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPotentialPrimes(System.Numerics.BigInteger value)
////    {
////      var quotient = System.Numerics.BigInteger.DivRem(value, 6, out var remainder);

////      var lo = quotient * 6;
////      var hi = lo + 6;

////      if (value <= 2)
////      {
////        yield return 2;
////        yield return 3;
////        lo = -6;
////      }
////      else if (value <= 3)
////      {
////        yield return 3;
////        yield return 2;
////        lo = -6;
////      }

////      if (remainder >= 3)
////      {
////        while (true)
////        {
////          yield return hi - 1;
////          if (lo >= 6) yield return lo + 1;
////          else if (lo == 0) yield return 3;
////          yield return hi + 1;
////          if (lo >= 6) yield return lo - 1;
////          else if (lo == 0) yield return 2;
////          hi += 6;
////          lo -= 6;
////        }
////      }
////      else
////      {
////        while (true)
////        {
////          if (lo > 0) yield return lo + 1;
////          else if (lo == 0) yield return 3;
////          yield return hi - 1;
////          if (lo > 0) yield return lo - 1;
////          else if (lo == 0) yield return 2;
////          yield return hi + 1;
////          lo -= 6;
////          hi += 6;
////        }
////      }
////    }

////    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPrimes(System.Numerics.BigInteger value)
////      => GetClosestPotentialPrimes(value).AsParallel().AsOrdered().Where(IsPrimeNumber);

////    /// <summary>Returns a sequence of cousine primes, each of which is a pair of primes that differ by four.</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Cousin_prime"/>
////    public static System.Collections.Generic.IEnumerable<(TValue, TValue)> GetCousinePrimes<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      foreach (var (leading, midling, trailing) in GetAscendingPrimes(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)))
////      {
////        if (midling - leading == TValue.CreateChecked(4))
////        {
////          yield return (leading, midling);
////        }
////        else if (trailing - leading == TValue.CreateChecked(4))
////        {
////          yield return (leading, trailing);
////        }
////      }
////    }

////    /// <summary></summary>
////    /// <see href="https://en.wikipedia.org/wiki/Factorization"/>
////    /// <seealso cref="https://en.wikipedia.org/wiki/Wheel_factorization"/>
////    public static System.Collections.Generic.IEnumerable<TValue> GetPrimeFactors<TValue>(TValue value)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      if (value < TValue.One) throw new System.ArgumentOutOfRangeException(nameof(value));

////      var two = TValue.CreateChecked(2);
////      var three = TValue.CreateChecked(3);
////      var four = TValue.CreateChecked(4);
////      var five = TValue.CreateChecked(5);
////      var six = TValue.CreateChecked(6);
////      var seven = TValue.CreateChecked(7);

////      var m_primeFactorWheelIncrements = new TValue[] { four, two, four, two, four, six, two, six };

////      while (TValue.IsZero(value % two))
////      {
////        yield return two;
////        value /= two;
////      }

////      while (TValue.IsZero(value % three))
////      {
////        yield return three;
////        value /= three;
////      }

////      while (TValue.IsZero(value % five))
////      {
////        yield return five;
////        value /= five;
////      }

////      TValue k = seven, k2 = k * k;
////      var index = 0;

////      while (k2 <= value)
////      {
////        if (TValue.IsZero(value % k))
////        {
////          yield return k;
////          value /= k;
////        }
////        else
////        {
////          k += m_primeFactorWheelIncrements[index++];
////          k2 = k * k;

////          if (index >= m_primeFactorWheelIncrements.Length)
////            index = 0;
////        }
////      }

////      if (value > TValue.One)
////        yield return value;
////    }

////    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified value.</summary>
////    public static System.Collections.Generic.IEnumerable<TValue> GetPrimeGaps<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////      => GetAscendingPrimes(startAt).PartitionTuple2(false, (leading, trailing, index) => trailing - leading);

////    /// <summary>Returns a sequence of prime quadruplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8}.</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet"/>
////    public static System.Collections.Generic.IEnumerable<(TValue, TValue, TValue, TValue)> GetPrimeQuadruplets<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      var two = TValue.CreateChecked(2);
////      var six = TValue.CreateChecked(6);
////      var eight = TValue.CreateChecked(8);

////      var list = new System.Collections.Generic.List<TValue>();

////      foreach (var primeNumber in GetAscendingPrimes(startAt))
////      {
////        list.Add(primeNumber);

////        if (list.Count == 4)
////        {
////          if (list[1] - list[0] == two && list[2] - list[0] == six && list[3] - list[0] == eight)
////            yield return (list[0], list[1], list[2], list[3]);

////          list.RemoveAt(0);
////        }
////      }
////    }

////    /// <summary>Returns a sequence of prime quintuplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8} and {p-4 or p+12}.</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_quintuplets"/>
////    public static System.Collections.Generic.IEnumerable<(TValue, TValue, TValue, TValue, TValue)> GetPrimeQuintuplets<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      var two = TValue.CreateChecked(2);
////      var four = TValue.CreateChecked(4);
////      var six = TValue.CreateChecked(6);
////      var eight = TValue.CreateChecked(8);
////      var twelve = TValue.CreateChecked(12);

////      var list = new System.Collections.Generic.List<TValue>();

////      foreach (var primeNumber in GetAscendingPrimes(startAt))
////      {
////        list.Add(primeNumber);

////        if (list.Count == 5)
////        {
////          if (list[1] - list[0] == four && list[2] - list[1] == two && list[3] - list[1] == six && list[4] - list[1] == eight)
////            yield return (list[0], list[1], list[2], list[3], list[4]);
////          else if (list[1] - list[0] == two && list[2] - list[0] == six && list[3] - list[0] == eight && list[4] - list[0] == twelve)
////            yield return (list[0], list[1], list[2], list[3], list[4]);

////          list.RemoveAt(0);
////        }
////      }
////    }

////    /// <summary>Returns a sequence of prime sextuplets, each of which is a set of six primes of the form {p-4, p, p+2, p+6, p+8, p+12}.</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_sextuplets"/>
////    public static System.Collections.Generic.IEnumerable<(TValue, TValue, TValue, TValue, TValue, TValue)> GetPrimeSextuplets<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      var two = TValue.CreateChecked(2);
////      var four = TValue.CreateChecked(4);
////      var six = TValue.CreateChecked(6);
////      var eight = TValue.CreateChecked(8);
////      var twelve = TValue.CreateChecked(12);

////      var list = new System.Collections.Generic.List<TValue>();

////      foreach (var primeNumber in GetAscendingPrimes(startAt))
////      {
////        list.Add(primeNumber);

////        if (list.Count == 6)
////        {
////          if (list[1] - list[0] == four && list[2] - list[1] == two && list[3] - list[1] == six && list[4] - list[1] == eight && list[5] - list[1] == twelve)
////            yield return (list[0], list[1], list[2], list[3], list[4], list[5]);

////          list.RemoveAt(0);
////        }
////      }
////    }

////    /// <summary>Returns a sequence of prime triplets, each of which is a set of three prime numbers of the form (p, p + 2, p + 6) or (p, p + 4, p + 6).</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Prime_triplet"/>
////    public static System.Collections.Generic.IEnumerable<(TValue, TValue, TValue)> GetPrimeTriplets<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////      => 0 is var index ? GetAscendingPrimes(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)).Where((t) => t.trailing - t.leading is var gap3to1 && gap3to1 == TValue.CreateChecked(6) && t.midling - t.leading is var gap2to1 && (gap2to1 == TValue.CreateChecked(2) || gap2to1 == TValue.CreateChecked(4))) : throw new System.Exception();

////    /// <summary>Returns a sequence of super-primes, which is a subsequence of prime numbers that occupy prime-numbered positions within the sequence of all prime numbers.</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Super-prime"/>
////    public static System.Collections.Generic.IEnumerable<TValue> GetSuperPrimes<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////      => GetAscendingPrimes(startAt).Where((p, i) => IsPrimeNumber(i + 1));

////    /// <summary>Returns a sequence of teim primes, each of which is a pair of primes that differ by two.</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Twin_prime"/>
////    public static System.Collections.Generic.IEnumerable<(TValue, TValue)> GetTwinPrimes<TValue>(TValue startAt)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////      => GetAscendingPrimes(startAt).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Where((t) => t.trailing - t.leading == TValue.CreateChecked(2));

////    /// <summary>Indicates whether the prime value is also an additive prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    /// <see href="https://en.wikipedia.org/wiki/List_of_prime_numbers#Additive_primes"/>
////    public static bool IsAlsoAdditivePrime(System.Numerics.BigInteger primeNumber)
////      => IsPrimeNumber(Maths.DigitSum(primeNumber, 10));

////    /// <summary>Indicates whether the prime value is also a congruent modulo prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    public static bool IsAlsoCongruentModuloPrime(System.Numerics.BigInteger primeNumber, System.Numerics.BigInteger a, System.Numerics.BigInteger d)
////      => (primeNumber % a == d);

////    /// <summary>Indicates whether the prime value is also an Eisenstein prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    /// <see href="https://en.wikipedia.org/wiki/Eisenstein_prime"/>
////    public static bool IsAlsoEisensteinPrime(System.Numerics.BigInteger primeNumber)
////      => IsAlsoCongruentModuloPrime(primeNumber, 3, 2);

////    /// <summary>Indicates whether the prime value is also a Gaussian prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    /// <see href="https://en.wikipedia.org/wiki/Gaussian_integer#Gaussian_primes"/>
////    public static bool IsAlsoGaussianPrime(System.Numerics.BigInteger primeNumber)
////      => IsAlsoCongruentModuloPrime(primeNumber, 4, 3);

////    /// <summary>Indicates whether the prime value is also a left truncatable prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    /// <see href="https://en.wikipedia.org/wiki/Truncatable_prime"/>
////    public static bool IsAlsoLeftTruncatablePrime(System.Numerics.BigInteger primeNumber)
////    {
////      var text = primeNumber.ToString(System.Globalization.CultureInfo.InvariantCulture);

////      if (text.IndexOf('0', System.StringComparison.Ordinal) > -1)
////      {
////        return false;
////      }

////      while (text.Length > 0 && System.Numerics.BigInteger.TryParse(text, out var result))
////      {
////        if (!IsPrimeNumber(result))
////        {
////          return false;
////        }

////        text = text[1..];
////      }

////      return true;
////    }

////    /// <summary>Indicates whether the prime value is also a Pythagorean prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    /// <see href="https://en.wikipedia.org/wiki/Pythagorean_prime"/>
////    public static bool IsAlsoPythagoreanPrime(System.Numerics.BigInteger primeNumber)
////      => IsAlsoCongruentModuloPrime(primeNumber, 4, 1);

////    /// <summary>Indicates whether the prime value is also a safe prime prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    /// <see href="https://en.wikipedia.org/wiki/Safe_prime"/>
////    public static bool IsAlsoSafePrime(System.Numerics.BigInteger primeNumber)
////      => IsPrimeNumber((primeNumber - 1) / 2);

////    /// <summary>Indicates whether the prime value is also a Sophie Germain prime.</summary>
////    /// <param name="primeNumber">A prime value. If this value is not a prime value, the result is unpredictable.</param>
////    /// <see href="https://en.wikipedia.org/wiki/Sophie_Germain_prime"/>
////    public static bool IsAlsoSophieGermainPrime(System.Numerics.BigInteger primeNumber)
////      => IsPrimeNumber((primeNumber * 2) + 1);

////    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
////    /// <see href="https://en.wikipedia.org/wiki/Coprime_integers"/>
////    public static bool IsCoprime<TValue>(TValue a, TValue b)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////      => Maths.GreatestCommonDivisor(a, b) == TValue.One;

////    ///// <summary>Indicates whether a specified value is a prime candidate.</summary>

////    //public static bool IsPrimeCandidate(System.Numerics.BigInteger value)
////    //  => value % 6 is var remainder && (remainder == 5 || remainder == 1);

////    ///// <summary>Indicates whether a specified value is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
////    //public static bool IsPrimeCandidate(System.Numerics.BigInteger value, out System.Numerics.BigInteger multiplier, out System.Numerics.BigInteger offset)
////    //{
////    //  multiplier = System.Numerics.BigInteger.DivRem(value, 6, out offset);

////    //  if (offset == 5)
////    //  {
////    //    multiplier++;
////    //    offset = -1;

////    //    return true;
////    //  }
////    //  else return offset == 1;
////    //}

////    ///// <summary>Indicates whether a specified value is a prime.</summary>
////    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
////    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
////    public static bool IsPrimeNumber<TValue>(TValue value)
////      where TValue : System.Numerics.IBinaryInteger<TValue>
////    {
////      var two = TValue.CreateChecked(2);
////      var three = TValue.CreateChecked(3);

////      if (value <= three)
////        return value >= two;

////      if (TValue.IsZero(value % two) || TValue.IsZero(value % three))
////        return false;

////      var limit = value.IntegerSqrt();

////      var six = TValue.CreateChecked(6);

////      for (var k = TValue.CreateChecked(5); k <= limit; k += six)
////        if (TValue.IsZero(value % k) || TValue.IsZero(value % (k + two)))
////          return false;

////      return true;
////    }

////    ///// <summary>Indicates whether a specified value is a prime.</summary>
////    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
////    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

////    //public static bool IsPrimeNumber(System.Numerics.BigInteger value)
////    //{
////    //  if (value <= long.MaxValue)
////    //    return IsPrimeNumber((long)value);

////    //  if (value <= 3)
////    //    return value >= 2;

////    //  if (value % 2 == 0 || value % 3 == 0)
////    //    return false;

////    //  var limit = value.IntegerSqrt();

////    //  for (var k = 5; k <= limit; k += 6)
////    //    if ((value % k) == 0 || (value % (k + 2)) == 0)
////    //      return false;

////    //  return true;
////    //}
////    ///// <summary>Indicates whether a specified value is a prime.</summary>
////    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
////    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

////    //public static bool IsPrimeNumber(int source)
////    //{
////    //  if (source <= 3)
////    //    return source >= 2;

////    //  if (source % 2 == 0 || source % 3 == 0)
////    //    return false;

////    //  var limit = System.Math.Sqrt(source);

////    //  for (var k = 5; k <= limit; k += 6)
////    //    if ((source % k) == 0 || (source % (k + 2)) == 0)
////    //      return false;

////    //  return true;
////    //}
////    ///// <summary>Indicates whether a specified value is a prime.</summary>
////    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
////    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

////    //public static bool IsPrimeNumber(long source)
////    //{
////    //  if (source <= int.MaxValue)
////    //    return IsPrimeNumber((int)source);

////    //  if (source <= 3)
////    //    return source >= 2;

////    //  if (source % 2 == 0 || source % 3 == 0)
////    //    return false;

////    //  var limit = System.Math.Sqrt(source);

////    //  for (var k = 5; k <= limit; k += 6)
////    //    if ((source % k) == 0 || (source % (k + 2)) == 0)
////    //      return false;

////    //  return true;
////    //}

////    #endregion Static members

////    #region Implemented interfaces
////    // INumberSequence
////    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence() => GetAscendingPrimes(System.Numerics.BigInteger.CreateChecked(SmallestPrime));

////    // IEnumerable<>
////    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
////    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
////    #endregion Implemented interfaces
////  }
////}
////#endif
