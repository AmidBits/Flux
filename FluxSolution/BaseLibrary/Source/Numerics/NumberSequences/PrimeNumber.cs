#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a new sequence of ascending potential primes, greater-than-or-equal-to the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetAscendingPotentialPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.CreateChecked(2) is var two && startAt <= two)
        yield return two;

      if (TSelf.CreateChecked(3) is var three && startAt <= three)
        yield return three;

      if (TSelf.CreateChecked(5) is var five && startAt <= five)
        startAt = five;

      var six = TSelf.CreateChecked(6);

      var (quotient, remainder) = TSelf.DivRem(startAt, six);

      var multiple = (quotient + (remainder > TSelf.One ? TSelf.One : TSelf.Zero)) * six;

      if (remainder <= TSelf.One) // Or, either between two potential primes or on right of a % 6 number. E.g. 12 or 13.
      {
        yield return multiple + TSelf.One;

        multiple += six;
      }

      while (true)
      {
        yield return multiple - TSelf.One;
        yield return multiple + TSelf.One;

        multiple += six;
      }
    }

    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPotentialPrimes(System.Numerics.BigInteger startAt)
    //{
    //  var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

    //  var multiple = 6 * (quotient + (remainder > 1 ? 1 : 0));

    //  if (quotient == 0) // If startAt is less than 6.
    //  {
    //    if (startAt <= 2) yield return 2;
    //    if (startAt <= 3) yield return 3;

    //    multiple = 6;
    //  }
    //  else if (remainder <= 1) // Or, either between two potential primes or on right of a % 6 number. E.g. 12 or 13.
    //  {
    //    yield return multiple + 1;
    //    multiple += 6;
    //  }

    //  while (true)
    //  {
    //    yield return multiple - 1;
    //    yield return multiple + 1;

    //    multiple += 6;
    //  }
    //}

    /// <summary>Creates a new sequence ascending prime numbers, greater-than-or-equal-to the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetAscendingPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetAscendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

    /// <summary>Creates a new sequence of descending potential primes, less than the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetDescendingPotentialPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.CreateChecked(5) is var five && startAt >= five)
      {
        var six = TSelf.CreateChecked(6);

        var (quotient, remainder) = TSelf.DivRem(startAt, six);

        var multiple = (quotient + (remainder == five ? TSelf.One : TSelf.Zero)) * six;

        if (remainder == TSelf.Zero || remainder == five) // Or, either between two potential primes or on left of (startAt % 6). E.g. 11 or 12.
        {
          yield return multiple - TSelf.One;

          multiple -= six;
        }

        while (multiple >= six)
        {
          yield return multiple + TSelf.One;
          yield return multiple - TSelf.One;

          multiple -= six;
        }
      }

      if (TSelf.CreateChecked(3) is var three && startAt >= three)
        yield return three;

      if (TSelf.CreateChecked(2) is var two && startAt >= two)
        yield return two;
    }

    /// <summary>Creates a new sequence descending prime numbers, less-than-or-equal-to the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetDescendingPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetDescendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPotentialPrimes2(System.Numerics.BigInteger number)
    //{
    //  if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));

    //  var nm = GenericMath.NearestMultiple(number, 6, false, RoundingMode.HalfTowardZero, out var tz, out var afz);

    //  Flux.BoundaryRounding<System.Numerics.BigInteger, System.Numerics.BigInteger>.MeasureDistanceToBoundaries(number, tz, afz, out System.Numerics.BigInteger dtz, out System.Numerics.BigInteger dafz);

    //  if (number <= 2)
    //  {
    //    yield return 2;
    //    yield return 3;
    //    tz = -6;
    //  }
    //  else if (number <= 3)
    //  {
    //    yield return 3;
    //    yield return 2;
    //    tz = -6;
    //  }

    //  if (nm == afz)
    //  {
    //    while (true)
    //    {
    //      //if (tz < 0) break;
    //      yield return afz - 1;
    //      if (tz >= 6) yield return tz + 1;
    //      else if (tz == 0) yield return 3;
    //      yield return afz + 1;
    //      if (tz >= 6) yield return tz - 1;
    //      else if (tz == 0) yield return 2;
    //      afz += 6;
    //      tz -= 6;
    //    }
    //  }
    //  else // Assumption here.
    //  {
    //    while (true)
    //    {
    //      //if (tz < 0) break;
    //      if (tz > 0) yield return tz + 1;
    //      else if (tz == 0) yield return 3;
    //      yield return afz - 1;
    //      if (tz > 0) yield return tz - 1;
    //      else if (tz == 0) yield return 2;
    //      yield return afz + 1;
    //      tz -= 6;
    //      afz += 6;
    //    }
    //  }

    //  while (true)
    //  {
    //    yield return afz - 1;
    //    yield return afz + 1;
    //    afz += 6;
    //  }
    //}

    /// <summary>Finds the nearest potential prime multiple (i.e. a multiple of 6) of <paramref name="number"/>, and if needed, apply the specified rounding <paramref name="mode"/>. Also returns the <paramref name="nearestPotentialPrimeMultipleOffset"/> as an output parameter.</summary>
    /// <param name="number">The target number.</param>
    /// <param name="mode">The <see cref="RoundingMode"/> to use if exactly between two multiples.</param>
    /// <param name="nearestPotentialPrimeMultipleOffset">The offset direction from the returned potential prime multiple, or the multiple itself: +1 = nearer TZ, -1 = nearer AFZ, 0 = exactly halfway between TZ and AFZ, or <paramref name="number"/> if it is a potential prime multiple.</param>
    /// <returns></returns>
    //public static System.Numerics.BigInteger GetNearestPotentialPrimeMultiple(System.Numerics.BigInteger number, RoundingMode mode, out System.Numerics.BigInteger nearestPotentialPrimeMultipleOffset)
    //{
    //  number.RoundToMultipleOf(6, false, RoundingMode.HalfTowardZero, out var multipleTowardsZero, out var multipleAwayFromZero);

    //  var nm = ((double)number).RoundToBoundaries(mode, (double)multipleTowardsZero, (double)multipleAwayFromZero);

    //  var binm = new System.Numerics.BigInteger(nm);
    //  var binmtz = multipleTowardsZero;
    //  var binmafz = multipleAwayFromZero;

    //  nearestPotentialPrimeMultipleOffset = binmtz == binmafz ? number // If TZ and AFZ are equal (i.e. NearestMultiple(), above, returns number for both TZ and AFZ, if number is a multiple, and 'proper' is false), so number is a multiple.
    //    : number - binmtz <= 3 ? +1 // The difference between TZ and number is less than 3, therefor closer to TZ.
    //    : binmafz - number <= 3 ? -1 // The difference between AFZ and number is less than 3, therefor closer to AFZ.
    //    : 0; // The difference between either TZ/AFZ and number is exactly 3, therefor number is exactly halfway between TZ and AFZ.

    //  return binm;
    //}

    public static System.Collections.Generic.IEnumerable<TSelf> GetClosestPotentialPrimes<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var three = TSelf.CreateChecked(3);
      var four = TSelf.CreateChecked(4);
      var five = TSelf.CreateChecked(5);
      var six = TSelf.CreateChecked(6);

      var (quotient, remainder) = TSelf.DivRem(number, six);

      var lo = quotient * six;
      var hi = lo + six;

      if (number <= two)
      {
        yield return two;
        yield return three;
        lo = -six;
      }
      else if (number <= three)
      {
        yield return three;
        yield return two;
        lo = -six;
      }

      if (remainder >= three)
      {
        while (true)
        {
          yield return hi - TSelf.One;
          if (lo >= six) yield return lo + TSelf.One;
          else if (TSelf.IsZero(lo)) yield return three;
          yield return hi + TSelf.One;
          if (lo >= six) yield return lo - TSelf.One;
          else if (TSelf.IsZero(lo)) yield return two;
          hi += six;
          lo -= six;
        }
      }
      else
      {
        while (true)
        {
          if (lo > TSelf.Zero) yield return lo + TSelf.One;
          else if (TSelf.IsZero(lo)) yield return three;
          yield return hi - TSelf.One;
          if (lo > TSelf.Zero) yield return lo - TSelf.One;
          else if (TSelf.IsZero(lo)) yield return two;
          yield return hi + TSelf.One;
          lo -= six;
          hi += six;
        }
      }
    }

    public static System.Collections.Generic.IEnumerable<TSelf> GetClosestPrimes<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetClosestPotentialPrimes(number).AsParallel().AsOrdered().Where(IsPrimeNumber);

    /// <summary>Returns a sequence of cousine primes, each of which is a pair of primes that differ by four.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Cousin_prime"/>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetCousinePrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      foreach (var (leading, midling, trailing) in GetAscendingPrimes(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)))
      {
        if (midling - leading == TSelf.CreateChecked(4))
        {
          yield return (leading, midling);
        }
        else if (trailing - leading == TSelf.CreateChecked(4))
        {
          yield return (leading, trailing);
        }
      }
    }

    /// <summary>
    /// <para>Returns the infimum and supremum of <paramref name="number"/> (as the subset) of potential primes.</para>
    /// </summary>
    /// <param name="number">The target for locating the bounds.</param>
    /// <returns></returns>
    /// <remarks>Any number below 2, simply returns 2 for both infimum and supremum.</remarks>
    public static (TSelf Infimum, TSelf Supremum) GetInfimumAndSupremumOfPotentialPrimes<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.CreateChecked(2) is var two && number <= two // If the number is two, or less.
      ? (two, two)
      : TSelf.CreateChecked(3) is var three && number == three // If the number is three.
      ? (three, three)
      : (number % TSelf.CreateChecked(6) is var r && r == TSelf.Zero) || number == TSelf.CreateChecked(4) // It's between two potential primes, e.g. the number = 3]4[5, the remainder = 5]6[7 or 11]12[13 (examples).
      ? (number - TSelf.One, number + TSelf.One)
      : TSelf.CreateChecked(5) is var five && (r == TSelf.One || r == five) // If the remainder is either a one or a five, i.e. a potential prime.
      ? (number, number)
      : (number - r + TSelf.One, number - r + five); // Otherwise locate the potential prime using the remainder.

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Factorization"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Wheel_factorization"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPrimeFactors2<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number < TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(number));

      var two = TSelf.CreateChecked(2);
      var three = TSelf.CreateChecked(3);
      var four = TSelf.CreateChecked(4);
      var five = TSelf.CreateChecked(5);
      var six = TSelf.CreateChecked(6);
      var seven = TSelf.CreateChecked(7);

      var m_primeFactorWheelIncrements = new TSelf[] { four, two, four, two, four, six, two, six };

      while (TSelf.IsZero(number % two))
      {
        yield return two;
        number /= two;
      }

      while (TSelf.IsZero(number % three))
      {
        yield return three;
        number /= three;
      }

      while (TSelf.IsZero(number % five))
      {
        yield return five;
        number /= five;
      }

      TSelf k = seven, k2 = k * k;
      var index = 0;

      while (k2 <= number)
      {
        if (TSelf.IsZero(number % k))
        {
          yield return k;
          number /= k;
        }
        else
        {
          k += m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length)
            index = 0;
        }
      }

      if (number > TSelf.One)
        yield return number;
    }

    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified number.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPrimeGaps<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetAscendingPrimes(startAt).PartitionTuple2(false, (leading, trailing, index) => trailing - leading);

    /// <summary>Returns a sequence of prime quadruplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8}.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet"/>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf, TSelf)> GetPrimeQuadruplets<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var six = TSelf.CreateChecked(6);
      var eight = TSelf.CreateChecked(8);

      var list = new System.Collections.Generic.List<TSelf>();

      foreach (var primeNumber in GetAscendingPrimes(startAt))
      {
        list.Add(primeNumber);

        if (list.Count == 4)
        {
          if (list[1] - list[0] == two && list[2] - list[0] == six && list[3] - list[0] == eight)
            yield return (list[0], list[1], list[2], list[3]);

          list.RemoveAt(0);
        }
      }
    }

    /// <summary>Returns a sequence of prime quintuplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8} and {p-4 or p+12}.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_quintuplets"/>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf, TSelf, TSelf)> GetPrimeQuintuplets<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var four = TSelf.CreateChecked(4);
      var six = TSelf.CreateChecked(6);
      var eight = TSelf.CreateChecked(8);
      var twelve = TSelf.CreateChecked(12);

      var list = new System.Collections.Generic.List<TSelf>();

      foreach (var primeNumber in GetAscendingPrimes(startAt))
      {
        list.Add(primeNumber);

        if (list.Count == 5)
        {
          if (list[1] - list[0] == four && list[2] - list[1] == two && list[3] - list[1] == six && list[4] - list[1] == eight)
            yield return (list[0], list[1], list[2], list[3], list[4]);
          else if (list[1] - list[0] == two && list[2] - list[0] == six && list[3] - list[0] == eight && list[4] - list[0] == twelve)
            yield return (list[0], list[1], list[2], list[3], list[4]);

          list.RemoveAt(0);
        }
      }
    }

    /// <summary>Returns a sequence of prime sextuplets, each of which is a set of six primes of the form {p-4, p, p+2, p+6, p+8, p+12}.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_sextuplets"/>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf, TSelf, TSelf, TSelf)> GetPrimeSextuplets<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var four = TSelf.CreateChecked(4);
      var six = TSelf.CreateChecked(6);
      var eight = TSelf.CreateChecked(8);
      var twelve = TSelf.CreateChecked(12);

      var list = new System.Collections.Generic.List<TSelf>();

      foreach (var primeNumber in GetAscendingPrimes(startAt))
      {
        list.Add(primeNumber);

        if (list.Count == 6)
        {
          if (list[1] - list[0] == four && list[2] - list[1] == two && list[3] - list[1] == six && list[4] - list[1] == eight && list[5] - list[1] == twelve)
            yield return (list[0], list[1], list[2], list[3], list[4], list[5]);

          list.RemoveAt(0);
        }
      }
    }

    /// <summary>Returns a sequence of prime triplets, each of which is a set of three prime numbers of the form (p, p + 2, p + 6) or (p, p + 4, p + 6).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Prime_triplet"/>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf)> GetPrimeTriplets<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => 0 is var index ? GetAscendingPrimes(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)).Where((t) => t.trailing - t.leading is var gap3to1 && gap3to1 == TSelf.CreateChecked(6) && t.midling - t.leading is var gap2to1 && (gap2to1 == TSelf.CreateChecked(2) || gap2to1 == TSelf.CreateChecked(4))) : throw new System.Exception();

    /// <summary>Returns a sequence of super-primes, which is a subsequence of prime numbers that occupy prime-numbered positions within the sequence of all prime numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Super-prime"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetSuperPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetAscendingPrimes(startAt).Where((p, i) => IsPrimeNumber(i + 1));

    /// <summary>Returns a sequence of teim primes, each of which is a pair of primes that differ by two.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Twin_prime"/>
    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetTwinPrimes<TSelf>(TSelf startAt)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetAscendingPrimes(startAt).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Where((t) => t.trailing - t.leading == TSelf.CreateChecked(2));

    /// <summary>Indicates whether the prime number is also an additive prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see href="https://en.wikipedia.org/wiki/List_of_prime_numbers#Additive_primes"/>
    public static bool IsAlsoAdditivePrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber(Quantities.Radix.DigitSum(primeNumber, 10));

    /// <summary>Indicates whether the prime number is also a congruent modulo prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    public static bool IsAlsoCongruentModuloPrime(System.Numerics.BigInteger primeNumber, System.Numerics.BigInteger a, System.Numerics.BigInteger d)
      => (primeNumber % a == d);

    /// <summary>Indicates whether the prime number is also an Eisenstein prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see href="https://en.wikipedia.org/wiki/Eisenstein_prime"/>
    public static bool IsAlsoEisensteinPrime(System.Numerics.BigInteger primeNumber)
      => IsAlsoCongruentModuloPrime(primeNumber, 3, 2);

    /// <summary>Indicates whether the prime number is also a Gaussian prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see href="https://en.wikipedia.org/wiki/Gaussian_integer#Gaussian_primes"/>
    public static bool IsAlsoGaussianPrime(System.Numerics.BigInteger primeNumber)
      => IsAlsoCongruentModuloPrime(primeNumber, 4, 3);

    /// <summary>Indicates whether the prime number is also a left truncatable prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see href="https://en.wikipedia.org/wiki/Truncatable_prime"/>
    public static bool IsAlsoLeftTruncatablePrime(System.Numerics.BigInteger primeNumber)
    {
      var text = primeNumber.ToString(System.Globalization.CultureInfo.InvariantCulture);

      if (text.IndexOf('0', System.StringComparison.Ordinal) > -1)
      {
        return false;
      }

      while (text.Length > 0 && System.Numerics.BigInteger.TryParse(text, out var result))
      {
        if (!IsPrimeNumber(result))
        {
          return false;
        }

        text = text[1..];
      }

      return true;
    }

    /// <summary>Indicates whether the prime number is also a Pythagorean prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see href="https://en.wikipedia.org/wiki/Pythagorean_prime"/>
    public static bool IsAlsoPythagoreanPrime(System.Numerics.BigInteger primeNumber)
      => IsAlsoCongruentModuloPrime(primeNumber, 4, 1);

    /// <summary>Indicates whether the prime number is also a safe prime prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see href="https://en.wikipedia.org/wiki/Safe_prime"/>
    public static bool IsAlsoSafePrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber((primeNumber - 1) / 2);

    /// <summary>Indicates whether the prime number is also a Sophie Germain prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see href="https://en.wikipedia.org/wiki/Sophie_Germain_prime"/>
    public static bool IsAlsoSophieGermainPrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber((primeNumber * 2) + 1);

    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Coprime_integers"/>
    public static bool IsCoprime<TSelf>(TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => Maths.GreatestCommonDivisor(a, b) == TSelf.One;

    ///// <summary>Indicates whether a specified number is a prime candidate.</summary>

    //public static bool IsPrimeCandidate(System.Numerics.BigInteger number)
    //  => number % 6 is var remainder && (remainder == 5 || remainder == 1);

    ///// <summary>Indicates whether a specified number is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
    //public static bool IsPrimeCandidate(System.Numerics.BigInteger number, out System.Numerics.BigInteger multiplier, out System.Numerics.BigInteger offset)
    //{
    //  multiplier = System.Numerics.BigInteger.DivRem(number, 6, out offset);

    //  if (offset == 5)
    //  {
    //    multiplier++;
    //    offset = -1;

    //    return true;
    //  }
    //  else return offset == 1;
    //}

    ///// <summary>Indicates whether a specified number is a prime.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    public static bool IsPrimeNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var three = TSelf.CreateChecked(3);

      if (number <= three)
        return number >= two;

      if (TSelf.IsZero(number % two) || TSelf.IsZero(number % three))
        return false;

      var limit = number.IntegerSqrt();

      var six = TSelf.CreateChecked(6);

      for (var k = TSelf.CreateChecked(5); k <= limit; k += six)
        if (TSelf.IsZero(number % k) || TSelf.IsZero(number % (k + two)))
          return false;

      return true;
    }

    ///// <summary>Indicates whether a specified number is a prime.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

    //public static bool IsPrimeNumber(System.Numerics.BigInteger number)
    //{
    //  if (number <= long.MaxValue)
    //    return IsPrimeNumber((long)number);

    //  if (number <= 3)
    //    return number >= 2;

    //  if (number % 2 == 0 || number % 3 == 0)
    //    return false;

    //  var limit = number.IntegerSqrt();

    //  for (var k = 5; k <= limit; k += 6)
    //    if ((number % k) == 0 || (number % (k + 2)) == 0)
    //      return false;

    //  return true;
    //}
    ///// <summary>Indicates whether a specified number is a prime.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

    //public static bool IsPrimeNumber(int source)
    //{
    //  if (source <= 3)
    //    return source >= 2;

    //  if (source % 2 == 0 || source % 3 == 0)
    //    return false;

    //  var limit = System.Math.Sqrt(source);

    //  for (var k = 5; k <= limit; k += 6)
    //    if ((source % k) == 0 || (source % (k + 2)) == 0)
    //      return false;

    //  return true;
    //}
    ///// <summary>Indicates whether a specified number is a prime.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

    //public static bool IsPrimeNumber(long source)
    //{
    //  if (source <= int.MaxValue)
    //    return IsPrimeNumber((int)source);

    //  if (source <= 3)
    //    return source >= 2;

    //  if (source % 2 == 0 || source % 3 == 0)
    //    return false;

    //  var limit = System.Math.Sqrt(source);

    //  for (var k = 5; k <= limit; k += 6)
    //    if ((source % k) == 0 || (source % (k + 2)) == 0)
    //      return false;

    //  return true;
    //}
  }
}
#endif

//#if NET7_0_OR_GREATER
//namespace Flux.NumberSequences
//{
//  public sealed partial class PrimeNumber
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    #region Constants
//    /// <summary>Represents the largest prime number possible in a byte (unsigned).</summary>
//    public const byte LargestPrimeByte = 251;
//    /// <summary>Represents the largest prime number possible in a 16-bit integer.</summary>
//    public const short LargestPrimeInt16 = 32749;
//    /// <summary>Represents the largest prime number possible in a 32-bit integer.</summary>
//    public const int LargestPrimeInt32 = 2147483647;
//    /// <summary>Represents the largest prime number possible in a 64-bit integer.</summary>
//    public const long LargestPrimeInt64 = 9223372036854775783;
//    /// <summary>Represents the largest prime number possible in a signed byte.</summary>
//    [System.CLSCompliant(false)]
//    public const sbyte LargestPrimeSByte = 127;
//    /// <summary>Represents the largest prime number possible in a 16-bit unsigned integer.</summary>
//    [System.CLSCompliant(false)]
//    public const ushort LargestPrimeUInt16 = 65521;
//    /// <summary>Represents the largest prime number possible in a 32-bit unsigned integer.</summary>
//    [System.CLSCompliant(false)]
//    public const uint LargestPrimeUInt32 = 4294967291;
//    /// <summary>Represents the largest prime number possible in a 64-bit unsigned integer.</summary>
//    [System.CLSCompliant(false)]
//    public const ulong LargestPrimeUInt64 = 18446744073709551557;

//    /// <summary>Represents the smallest prime number.</summary>
//    public const int SmallestPrime = 2;
//    #endregion Constants

//    #region Static members

//    /// <summary>Creates a new sequence of ascending potential primes, greater-than-or-equal-to the specified number.</summary>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetAscendingPotentialPrimes<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (TSelf.CreateChecked(2) is var two && startAt <= two)
//        yield return two;

//      if (TSelf.CreateChecked(3) is var three && startAt <= three)
//        yield return three;

//      if (TSelf.CreateChecked(5) is var five && startAt <= five)
//        startAt = five;

//      var six = TSelf.CreateChecked(6);

//      var (quotient, remainder) = TSelf.DivRem(startAt, six);

//      var multiple = (quotient + (remainder > TSelf.One ? TSelf.One : TSelf.Zero)) * six;

//      if (remainder <= TSelf.One) // Or, either between two potential primes or on right of a % 6 number. E.g. 12 or 13.
//      {
//        yield return multiple + TSelf.One;

//        multiple += six;
//      }

//      while (true)
//      {
//        yield return multiple - TSelf.One;
//        yield return multiple + TSelf.One;

//        multiple += six;
//      }
//    }

//    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPotentialPrimes(System.Numerics.BigInteger startAt)
//    //{
//    //  var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

//    //  var multiple = 6 * (quotient + (remainder > 1 ? 1 : 0));

//    //  if (quotient == 0) // If startAt is less than 6.
//    //  {
//    //    if (startAt <= 2) yield return 2;
//    //    if (startAt <= 3) yield return 3;

//    //    multiple = 6;
//    //  }
//    //  else if (remainder <= 1) // Or, either between two potential primes or on right of a % 6 number. E.g. 12 or 13.
//    //  {
//    //    yield return multiple + 1;
//    //    multiple += 6;
//    //  }

//    //  while (true)
//    //  {
//    //    yield return multiple - 1;
//    //    yield return multiple + 1;

//    //    multiple += 6;
//    //  }
//    //}

//    /// <summary>Creates a new sequence ascending prime numbers, greater-than-or-equal-to the specified number.</summary>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetAscendingPrimes<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => GetAscendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

//    /// <summary>Creates a new sequence of descending potential primes, less than the specified number.</summary>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetDescendingPotentialPrimes<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (TSelf.CreateChecked(5) is var five && startAt >= five)
//      {
//        var six = TSelf.CreateChecked(6);

//        var (quotient, remainder) = TSelf.DivRem(startAt, six);

//        var multiple = (quotient + (remainder == five ? TSelf.One : TSelf.Zero)) * six;

//        if (remainder == TSelf.Zero || remainder == five) // Or, either between two potential primes or on left of (startAt % 6). E.g. 11 or 12.
//        {
//          yield return multiple - TSelf.One;

//          multiple -= six;
//        }

//        while (multiple >= six)
//        {
//          yield return multiple + TSelf.One;
//          yield return multiple - TSelf.One;

//          multiple -= six;
//        }
//      }

//      if (TSelf.CreateChecked(3) is var three && startAt >= three)
//        yield return three;

//      if (TSelf.CreateChecked(2) is var two && startAt >= two)
//        yield return two;
//    }

//    /// <summary>Creates a new sequence descending prime numbers, less-than-or-equal-to the specified number.</summary>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetDescendingPrimes<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => GetDescendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

//    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPotentialPrimes2(System.Numerics.BigInteger number)
//    //{
//    //  if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));

//    //  var nm = GenericMath.NearestMultiple(number, 6, false, RoundingMode.HalfTowardZero, out var tz, out var afz);

//    //  Flux.BoundaryRounding<System.Numerics.BigInteger, System.Numerics.BigInteger>.MeasureDistanceToBoundaries(number, tz, afz, out System.Numerics.BigInteger dtz, out System.Numerics.BigInteger dafz);

//    //  if (number <= 2)
//    //  {
//    //    yield return 2;
//    //    yield return 3;
//    //    tz = -6;
//    //  }
//    //  else if (number <= 3)
//    //  {
//    //    yield return 3;
//    //    yield return 2;
//    //    tz = -6;
//    //  }

//    //  if (nm == afz)
//    //  {
//    //    while (true)
//    //    {
//    //      //if (tz < 0) break;
//    //      yield return afz - 1;
//    //      if (tz >= 6) yield return tz + 1;
//    //      else if (tz == 0) yield return 3;
//    //      yield return afz + 1;
//    //      if (tz >= 6) yield return tz - 1;
//    //      else if (tz == 0) yield return 2;
//    //      afz += 6;
//    //      tz -= 6;
//    //    }
//    //  }
//    //  else // Assumption here.
//    //  {
//    //    while (true)
//    //    {
//    //      //if (tz < 0) break;
//    //      if (tz > 0) yield return tz + 1;
//    //      else if (tz == 0) yield return 3;
//    //      yield return afz - 1;
//    //      if (tz > 0) yield return tz - 1;
//    //      else if (tz == 0) yield return 2;
//    //      yield return afz + 1;
//    //      tz -= 6;
//    //      afz += 6;
//    //    }
//    //  }

//    //  while (true)
//    //  {
//    //    yield return afz - 1;
//    //    yield return afz + 1;
//    //    afz += 6;
//    //  }
//    //}

//    /// <summary>Finds the nearest potential prime multiple (i.e. a multiple of 6) of <paramref name="number"/>, and if needed, apply the specified rounding <paramref name="mode"/>. Also returns the <paramref name="nearestPotentialPrimeMultipleOffset"/> as an output parameter.</summary>
//    /// <param name="number">The target number.</param>
//    /// <param name="mode">The <see cref="RoundingMode"/> to use if exactly between two multiples.</param>
//    /// <param name="nearestPotentialPrimeMultipleOffset">The offset direction from the returned potential prime multiple, or the multiple itself: +1 = nearer TZ, -1 = nearer AFZ, 0 = exactly halfway between TZ and AFZ, or <paramref name="number"/> if it is a potential prime multiple.</param>
//    /// <returns></returns>
//    //public static System.Numerics.BigInteger GetNearestPotentialPrimeMultiple(System.Numerics.BigInteger number, RoundingMode mode, out System.Numerics.BigInteger nearestPotentialPrimeMultipleOffset)
//    //{
//    //  number.RoundToMultipleOf(6, false, RoundingMode.HalfTowardZero, out var multipleTowardsZero, out var multipleAwayFromZero);

//    //  var nm = ((double)number).RoundToBoundaries(mode, (double)multipleTowardsZero, (double)multipleAwayFromZero);

//    //  var binm = new System.Numerics.BigInteger(nm);
//    //  var binmtz = multipleTowardsZero;
//    //  var binmafz = multipleAwayFromZero;

//    //  nearestPotentialPrimeMultipleOffset = binmtz == binmafz ? number // If TZ and AFZ are equal (i.e. NearestMultiple(), above, returns number for both TZ and AFZ, if number is a multiple, and 'proper' is false), so number is a multiple.
//    //    : number - binmtz <= 3 ? +1 // The difference between TZ and number is less than 3, therefor closer to TZ.
//    //    : binmafz - number <= 3 ? -1 // The difference between AFZ and number is less than 3, therefor closer to AFZ.
//    //    : 0; // The difference between either TZ/AFZ and number is exactly 3, therefor number is exactly halfway between TZ and AFZ.

//    //  return binm;
//    //}

//    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPotentialPrimes(System.Numerics.BigInteger number)
//    {
//      var quotient = System.Numerics.BigInteger.DivRem(number, 6, out var remainder);

//      var lo = quotient * 6;
//      var hi = lo + 6;

//      if (number <= 2)
//      {
//        yield return 2;
//        yield return 3;
//        lo = -6;
//      }
//      else if (number <= 3)
//      {
//        yield return 3;
//        yield return 2;
//        lo = -6;
//      }

//      if (remainder >= 3)
//      {
//        while (true)
//        {
//          yield return hi - 1;
//          if (lo >= 6) yield return lo + 1;
//          else if (lo == 0) yield return 3;
//          yield return hi + 1;
//          if (lo >= 6) yield return lo - 1;
//          else if (lo == 0) yield return 2;
//          hi += 6;
//          lo -= 6;
//        }
//      }
//      else
//      {
//        while (true)
//        {
//          if (lo > 0) yield return lo + 1;
//          else if (lo == 0) yield return 3;
//          yield return hi - 1;
//          if (lo > 0) yield return lo - 1;
//          else if (lo == 0) yield return 2;
//          yield return hi + 1;
//          lo -= 6;
//          hi += 6;
//        }
//      }
//    }

//    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPrimes(System.Numerics.BigInteger number)
//      => GetClosestPotentialPrimes(number).AsParallel().AsOrdered().Where(IsPrimeNumber);

//    /// <summary>Returns a sequence of cousine primes, each of which is a pair of primes that differ by four.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Cousin_prime"/>
//    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetCousinePrimes<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      foreach (var (leading, midling, trailing) in GetAscendingPrimes(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)))
//      {
//        if (midling - leading == TSelf.CreateChecked(4))
//        {
//          yield return (leading, midling);
//        }
//        else if (trailing - leading == TSelf.CreateChecked(4))
//        {
//          yield return (leading, trailing);
//        }
//      }
//    }

//    /// <summary></summary>
//    /// <see href="https://en.wikipedia.org/wiki/Factorization"/>
//    /// <seealso cref="https://en.wikipedia.org/wiki/Wheel_factorization"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetPrimeFactors<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (number < TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(number));

//      var two = TSelf.CreateChecked(2);
//      var three = TSelf.CreateChecked(3);
//      var four = TSelf.CreateChecked(4);
//      var five = TSelf.CreateChecked(5);
//      var six = TSelf.CreateChecked(6);
//      var seven = TSelf.CreateChecked(7);

//      var m_primeFactorWheelIncrements = new TSelf[] { four, two, four, two, four, six, two, six };

//      while (TSelf.IsZero(number % two))
//      {
//        yield return two;
//        number /= two;
//      }

//      while (TSelf.IsZero(number % three))
//      {
//        yield return three;
//        number /= three;
//      }

//      while (TSelf.IsZero(number % five))
//      {
//        yield return five;
//        number /= five;
//      }

//      TSelf k = seven, k2 = k * k;
//      var index = 0;

//      while (k2 <= number)
//      {
//        if (TSelf.IsZero(number % k))
//        {
//          yield return k;
//          number /= k;
//        }
//        else
//        {
//          k += m_primeFactorWheelIncrements[index++];
//          k2 = k * k;

//          if (index >= m_primeFactorWheelIncrements.Length)
//            index = 0;
//        }
//      }

//      if (number > TSelf.One)
//        yield return number;
//    }

//    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified number.</summary>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetPrimeGaps<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => GetAscendingPrimes(startAt).PartitionTuple2(false, (leading, trailing, index) => trailing - leading);

//    /// <summary>Returns a sequence of prime quadruplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8}.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Prime_quadruplet"/>
//    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf, TSelf)> GetPrimeQuadruplets<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var two = TSelf.CreateChecked(2);
//      var six = TSelf.CreateChecked(6);
//      var eight = TSelf.CreateChecked(8);

//      var list = new System.Collections.Generic.List<TSelf>();

//      foreach (var primeNumber in GetAscendingPrimes(startAt))
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
//    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf, TSelf, TSelf)> GetPrimeQuintuplets<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var two = TSelf.CreateChecked(2);
//      var four = TSelf.CreateChecked(4);
//      var six = TSelf.CreateChecked(6);
//      var eight = TSelf.CreateChecked(8);
//      var twelve = TSelf.CreateChecked(12);

//      var list = new System.Collections.Generic.List<TSelf>();

//      foreach (var primeNumber in GetAscendingPrimes(startAt))
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
//    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf, TSelf, TSelf, TSelf)> GetPrimeSextuplets<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var two = TSelf.CreateChecked(2);
//      var four = TSelf.CreateChecked(4);
//      var six = TSelf.CreateChecked(6);
//      var eight = TSelf.CreateChecked(8);
//      var twelve = TSelf.CreateChecked(12);

//      var list = new System.Collections.Generic.List<TSelf>();

//      foreach (var primeNumber in GetAscendingPrimes(startAt))
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
//    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf, TSelf)> GetPrimeTriplets<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => 0 is var index ? GetAscendingPrimes(startAt).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)).Where((t) => t.trailing - t.leading is var gap3to1 && gap3to1 == TSelf.CreateChecked(6) && t.midling - t.leading is var gap2to1 && (gap2to1 == TSelf.CreateChecked(2) || gap2to1 == TSelf.CreateChecked(4))) : throw new System.Exception();

//    /// <summary>Returns a sequence of super-primes, which is a subsequence of prime numbers that occupy prime-numbered positions within the sequence of all prime numbers.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Super-prime"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetSuperPrimes<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => GetAscendingPrimes(startAt).Where((p, i) => IsPrimeNumber(i + 1));

//    /// <summary>Returns a sequence of teim primes, each of which is a pair of primes that differ by two.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Twin_prime"/>
//    public static System.Collections.Generic.IEnumerable<(TSelf, TSelf)> GetTwinPrimes<TSelf>(TSelf startAt)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => GetAscendingPrimes(startAt).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Where((t) => t.trailing - t.leading == TSelf.CreateChecked(2));

//    /// <summary>Indicates whether the prime number is also an additive prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/List_of_prime_numbers#Additive_primes"/>
//    public static bool IsAlsoAdditivePrime(System.Numerics.BigInteger primeNumber)
//      => IsPrimeNumber(Maths.DigitSum(primeNumber, 10));

//    /// <summary>Indicates whether the prime number is also a congruent modulo prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    public static bool IsAlsoCongruentModuloPrime(System.Numerics.BigInteger primeNumber, System.Numerics.BigInteger a, System.Numerics.BigInteger d)
//      => (primeNumber % a == d);

//    /// <summary>Indicates whether the prime number is also an Eisenstein prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Eisenstein_prime"/>
//    public static bool IsAlsoEisensteinPrime(System.Numerics.BigInteger primeNumber)
//      => IsAlsoCongruentModuloPrime(primeNumber, 3, 2);

//    /// <summary>Indicates whether the prime number is also a Gaussian prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Gaussian_integer#Gaussian_primes"/>
//    public static bool IsAlsoGaussianPrime(System.Numerics.BigInteger primeNumber)
//      => IsAlsoCongruentModuloPrime(primeNumber, 4, 3);

//    /// <summary>Indicates whether the prime number is also a left truncatable prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Truncatable_prime"/>
//    public static bool IsAlsoLeftTruncatablePrime(System.Numerics.BigInteger primeNumber)
//    {
//      var text = primeNumber.ToString(System.Globalization.CultureInfo.InvariantCulture);

//      if (text.IndexOf('0', System.StringComparison.Ordinal) > -1)
//      {
//        return false;
//      }

//      while (text.Length > 0 && System.Numerics.BigInteger.TryParse(text, out var result))
//      {
//        if (!IsPrimeNumber(result))
//        {
//          return false;
//        }

//        text = text[1..];
//      }

//      return true;
//    }

//    /// <summary>Indicates whether the prime number is also a Pythagorean prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Pythagorean_prime"/>
//    public static bool IsAlsoPythagoreanPrime(System.Numerics.BigInteger primeNumber)
//      => IsAlsoCongruentModuloPrime(primeNumber, 4, 1);

//    /// <summary>Indicates whether the prime number is also a safe prime prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Safe_prime"/>
//    public static bool IsAlsoSafePrime(System.Numerics.BigInteger primeNumber)
//      => IsPrimeNumber((primeNumber - 1) / 2);

//    /// <summary>Indicates whether the prime number is also a Sophie Germain prime.</summary>
//    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
//    /// <see href="https://en.wikipedia.org/wiki/Sophie_Germain_prime"/>
//    public static bool IsAlsoSophieGermainPrime(System.Numerics.BigInteger primeNumber)
//      => IsPrimeNumber((primeNumber * 2) + 1);

//    /// <summary>Returns whether <paramref name="a"/> and <paramref name="b"/> are co-prime.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Coprime_integers"/>
//    public static bool IsCoprime<TSelf>(TSelf a, TSelf b)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => Maths.GreatestCommonDivisor(a, b) == TSelf.One;

//    ///// <summary>Indicates whether a specified number is a prime candidate.</summary>

//    //public static bool IsPrimeCandidate(System.Numerics.BigInteger number)
//    //  => number % 6 is var remainder && (remainder == 5 || remainder == 1);

//    ///// <summary>Indicates whether a specified number is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
//    //public static bool IsPrimeCandidate(System.Numerics.BigInteger number, out System.Numerics.BigInteger multiplier, out System.Numerics.BigInteger offset)
//    //{
//    //  multiplier = System.Numerics.BigInteger.DivRem(number, 6, out offset);

//    //  if (offset == 5)
//    //  {
//    //    multiplier++;
//    //    offset = -1;

//    //    return true;
//    //  }
//    //  else return offset == 1;
//    //}

//    ///// <summary>Indicates whether a specified number is a prime.</summary>
//    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
//    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
//    public static bool IsPrimeNumber<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      var two = TSelf.CreateChecked(2);
//      var three = TSelf.CreateChecked(3);

//      if (number <= three)
//        return number >= two;

//      if (TSelf.IsZero(number % two) || TSelf.IsZero(number % three))
//        return false;

//      var limit = number.IntegerSqrt();

//      var six = TSelf.CreateChecked(6);

//      for (var k = TSelf.CreateChecked(5); k <= limit; k += six)
//        if (TSelf.IsZero(number % k) || TSelf.IsZero(number % (k + two)))
//          return false;

//      return true;
//    }

//    ///// <summary>Indicates whether a specified number is a prime.</summary>
//    ///// <see href="https://en.wikipedia.org/wiki/Primality_test"/>
//    ///// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>

//    //public static bool IsPrimeNumber(System.Numerics.BigInteger number)
//    //{
//    //  if (number <= long.MaxValue)
//    //    return IsPrimeNumber((long)number);

//    //  if (number <= 3)
//    //    return number >= 2;

//    //  if (number % 2 == 0 || number % 3 == 0)
//    //    return false;

//    //  var limit = number.IntegerSqrt();

//    //  for (var k = 5; k <= limit; k += 6)
//    //    if ((number % k) == 0 || (number % (k + 2)) == 0)
//    //      return false;

//    //  return true;
//    //}
//    ///// <summary>Indicates whether a specified number is a prime.</summary>
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
//    ///// <summary>Indicates whether a specified number is a prime.</summary>
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

//    #endregion Static members

//    #region Implemented interfaces
//    // INumberSequence
//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence() => GetAscendingPrimes(System.Numerics.BigInteger.CreateChecked(SmallestPrime));

//    // IEnumerable<>
//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator() => GetSequence().GetEnumerator();
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
//    #endregion Implemented interfaces
//  }
//}
//#endif
