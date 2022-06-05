using System.Linq;

namespace Flux.Numerics
{
  public record class PrimeNumber
    : INumberSequenceable<System.Numerics.BigInteger>
  {
    #region Constants
    /// <summary>Represents the largest prime number possible in a byte (unsigned).</summary>
    public const byte LargestPrimeByte = 251;
    /// <summary>Represents the largest prime number possible in a 16-bit integer.</summary>
    public const short LargestPrimeInt16 = 32749;
    /// <summary>Represents the largest prime number possible in a 32-bit integer.</summary>
    public const int LargestPrimeInt32 = 2147483647;
    /// <summary>Represents the largest prime number possible in a 64-bit integer.</summary>
    public const long LargestPrimeInt64 = 9223372036854775783;
    /// <summary>Represents the largest prime number possible in a signed byte.</summary>
    [System.CLSCompliant(false)]
    public const sbyte LargestPrimeSByte = 127;
    /// <summary>Represents the largest prime number possible in a 16-bit unsigned integer.</summary>
    [System.CLSCompliant(false)]
    public const ushort LargestPrimeUInt16 = 65521;
    /// <summary>Represents the largest prime number possible in a 32-bit unsigned integer.</summary>
    [System.CLSCompliant(false)]
    public const uint LargestPrimeUInt32 = 4294967291;
    /// <summary>Represents the largest prime number possible in a 64-bit unsigned integer.</summary>
    [System.CLSCompliant(false)]
    public const ulong LargestPrimeUInt64 = 18446744073709551557;

    /// <summary>Represents the smallest prime number.</summary>
    public const int SmallestPrime = 2;
    #endregion Constants

    #region Statics
    /// <summary>Creates a new sequence of ascending potential primes, greater than the specified number.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPotentialPrimes(System.Numerics.BigInteger startAt)
    {
      if (startAt <= 2) yield return 2;
      if (startAt <= 3) yield return 3;

      var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

      if (remainder == 5) // On a potential prime before an ascending % 6 number. E.g. 11.
        yield return 6 * ++quotient + 1;
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
        yield return 6 * quotient++ + 1;

      for (var index = 6 * (quotient + (remainder > 0 ? 1 : 0)); true; index += 6)
      {
        yield return index - 1;
        yield return index + 1;
      }
    }
    /// <summary>Creates a new sequence ascending prime numbers, greater than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetAscendingPrimes(System.Numerics.BigInteger startAt)
      => GetAscendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPotentialPrimes(System.Numerics.BigInteger number)
    {
      var quotient = System.Numerics.BigInteger.DivRem(number, 6, out var remainder);

      var lo = quotient * 6;
      var hi = lo + 6;

      if (number <= 2)
      {
        yield return 2;
        yield return 3;
        lo = -6;
      }
      else if (number <= 3)
      {
        yield return 3;
        yield return 2;
        lo = -6;
      }

      if (remainder >= 3)
      {
        while (true)
        {
          yield return hi - 1;
          if (lo >= 6) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi + 1;
          if (lo >= 6) yield return lo - 1;
          else if (lo == 0) yield return 2;
          hi += 6;
          lo -= 6;
        }
      }
      else
      {
        while (true)
        {
          if (lo > 0) yield return lo + 1;
          else if (lo == 0) yield return 3;
          yield return hi - 1;
          if (lo > 0) yield return lo - 1;
          else if (lo == 0) yield return 2;
          yield return hi + 1;
          lo -= 6;
          hi += 6;
        }
      }
    }
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetClosestPrimes(System.Numerics.BigInteger number)
      => GetClosestPotentialPrimes(number).AsParallel().AsOrdered().Where(IsPrimeNumber);

    /// <summary>Returns a sequence of cousine primes, each of which is a pair of primes that differ by four.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cousin_prime"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, int Index)> GetCousinePrimes()
    {
      var counter = 0;

      foreach (var (leading, midling, trailing) in GetAscendingPrimes(SmallestPrime).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)))
      {
        if (midling - leading == 4)
        {
          yield return (leading, midling, counter);
        }
        else if (trailing - leading == 4)
        {
          yield return (leading, trailing, counter);
        }

        counter++;
      }
    }

    /// <summary>Creates a new sequence of descending potential primes, less than the specified number.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDescendingPotentialPrimes(System.Numerics.BigInteger startAt)
    {
      var quotient = System.Numerics.BigInteger.DivRem(startAt, 6, out var remainder);

      if (remainder == 1) // On a potential prime before a descending % 6 number. E.g. 13.
        yield return 6 * quotient-- - 1;
      else if (remainder == 0) // Between two potential primes on a % 6 number. E.g. 12.
        yield return 6 * quotient-- - 1;

      for (var index = 6 * quotient; index > 0; index -= 6)
      {
        yield return index + 1;
        yield return index - 1;
      }

      yield return 3;
      yield return 2;
    }
    /// <summary>Creates a new sequence descending prime numbers, less than the specified number.</summary>
    /// <see cref="https://math.stackexchange.com/questions/164767/prime-number-generator-how-to-make"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDescendingPrimes(System.Numerics.BigInteger startAt)
      => GetDescendingPotentialPrimes(startAt).AsParallel().AsOrdered().Where(IsPrimeNumber);

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Factorization"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Wheel_factorization"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimeFactors(System.Numerics.BigInteger number)
    {
      if (number < 1) throw new System.ArgumentOutOfRangeException(nameof(number));

      var m_primeFactorWheelIncrements = new int[] { 4, 2, 4, 2, 4, 6, 2, 6 };

      while (number % 2 == 0)
      {
        yield return 2;
        number /= 2;
      }

      while (number % 3 == 0)
      {
        yield return 3;
        number /= 3;
      }

      while (number % 5 == 0)
      {
        yield return 5;
        number /= 5;
      }

      System.Numerics.BigInteger k = 7, k2 = k * k;
      var index = 0;

      while (k2 <= number)
      {
        if (number % k == 0)
        {
          yield return k;
          number /= k;
        }
        else
        {
          k += (uint)m_primeFactorWheelIncrements[index++];
          k2 = k * k;

          if (index >= m_primeFactorWheelIncrements.Length) index = 0;
        }
      }

      if (number > 1) yield return number;
    }

    /// <summary>Results in an ascending sequence of gaps between prime numbers starting with the specified number.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimeGaps(System.Numerics.BigInteger startAt)
      => GetAscendingPrimes(startAt).PartitionTuple2(false, (leading, trailing, index) => trailing - leading);

    /// <summary>Returns a sequence of prime quadruplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8}.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Prime_quadruplet"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, int Index)> GetPrimeQuadruplets()
    {
      var index = 0;

      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      foreach (var primeNumber in GetAscendingPrimes(SmallestPrime))
      {
        list.Add(primeNumber);

        if (list.Count == 4)
        {
          if (list[1] - list[0] == 2 && list[2] - list[0] == 6 && list[3] - list[0] == 8)
            yield return (list[0], list[1], list[2], list[3], index++);

          list.RemoveAt(0);
        }
      }
    }

    /// <summary>Returns a sequence of prime quintuplets, each of which is a set of four primes of the form {p, p+2, p+6, p+8} and {p-4 or p+12}.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_quintuplets"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, int Index)> GetPrimeQuintuplets()
    {
      var index = 0;

      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      foreach (var primeNumber in GetAscendingPrimes(SmallestPrime))
      {
        list.Add(primeNumber);

        if (list.Count == 5)
        {
          if (list[1] - list[0] == 4 && list[2] - list[1] == 2 && list[3] - list[1] == 6 && list[4] - list[1] == 8)
            yield return (list[0], list[1], list[2], list[3], list[4], index++);
          else if (list[1] - list[0] == 2 && list[2] - list[0] == 6 && list[3] - list[0] == 8 && list[4] - list[0] == 12)
            yield return (list[0], list[1], list[2], list[3], list[4], index++);

          list.RemoveAt(0);
        }
      }
    }

    /// <summary>Returns a sequence of prime sextuplets, each of which is a set of six primes of the form {p-4, p, p+2, p+6, p+8, p+12}.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Prime_quadruplet#Prime_sextuplets"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger, int Index)> GetPrimeSextuplets()
    {
      var index = 0;

      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      foreach (var primeNumber in GetAscendingPrimes(SmallestPrime))
      {
        list.Add(primeNumber);

        if (list.Count == 6)
        {
          if (list[1] - list[0] == 4 && list[2] - list[1] == 2 && list[3] - list[1] == 6 && list[4] - list[1] == 8 && list[5] - list[1] == 12)
            yield return (list[0], list[1], list[2], list[3], list[4], list[5], index++);

          list.RemoveAt(0);
        }
      }
    }

    /// <summary>Returns a sequence of prime triplets, each of which is a set of three prime numbers of the form (p, p + 2, p + 6) or (p, p + 4, p + 6).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Prime_triplet"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger, System.Numerics.BigInteger)> GetPrimeTriplets()
      => 0 is var index ? GetAscendingPrimes(SmallestPrime.ToBigInteger()).PartitionTuple3(0, (leading, midling, trailing, index) => (leading, midling, trailing)).Where((t) => t.trailing - t.leading is var gap3to1 && gap3to1 == 6 && t.midling - t.leading is var gap2to1 && (gap2to1 == 2 || gap2to1 == 4)) : throw new System.Exception();

    /// <summary>Returns a sequence of super-primes, which is a subsequence of prime numbers that occupy prime-numbered positions within the sequence of all prime numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Super-prime"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSuperPrimes()
      => GetAscendingPrimes(SmallestPrime.ToBigInteger()).Where((p, i) => IsPrimeNumber(((System.Numerics.BigInteger)i + System.Numerics.BigInteger.One)));

    /// <summary>Returns a sequence of teim primes, each of which is a pair of primes that differ by two.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Twin_prime"/>
    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger, System.Numerics.BigInteger)> GetTwinPrimes()
      => GetAscendingPrimes(SmallestPrime.ToBigInteger()).PartitionTuple2(false, (leading, trailing, index) => (leading, trailing)).Where((t) => t.trailing - t.leading == 2);

    /// <summary>Indicates whether the prime number is also an additive prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/List_of_prime_numbers#Additive_primes"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAlsoAdditivePrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber(Maths.DigitSum(primeNumber, 10));
    /// <summary>Indicates whether the prime number is also a congruent modulo prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAlsoCongruentModuloPrime(System.Numerics.BigInteger primeNumber, System.Numerics.BigInteger a, System.Numerics.BigInteger d)
      => (primeNumber % a == d);
    /// <summary>Indicates whether the prime number is also an Eisenstein prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Eisenstein_prime"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAlsoEisensteinPrime(System.Numerics.BigInteger primeNumber)
      => IsAlsoCongruentModuloPrime(primeNumber, 3, 2);
    /// <summary>Indicates whether the prime number is also a Gaussian prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Gaussian_integer#Gaussian_primes"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAlsoGaussianPrime(System.Numerics.BigInteger primeNumber)
      => IsAlsoCongruentModuloPrime(primeNumber, 4, 3);
    /// <summary>Indicates whether the prime number is also a left truncatable prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Truncatable_prime"/>
    [System.Diagnostics.Contracts.Pure]
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
    /// <see cref="https://en.wikipedia.org/wiki/Pythagorean_prime"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAlsoPythagoreanPrime(System.Numerics.BigInteger primeNumber)
      => IsAlsoCongruentModuloPrime(primeNumber, 4, 1);
    /// <summary>Indicates whether the prime number is also a safe prime prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Safe_prime"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAlsoSafePrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber((primeNumber - 1) / 2);
    /// <summary>Indicates whether the prime number is also a Sophie Germain prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Sophie_Germain_prime"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsAlsoSophieGermainPrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber((primeNumber * 2) + 1);

    /// <summary>Indicates whether a specified number is a prime candidate.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsPrimeCandidate(System.Numerics.BigInteger number)
      => number % 6 is var remainder && (remainder == 5 || remainder == 1);
    /// <summary>Indicates whether a specified number is a prime candidate, and also returns the properties of "6n-1"/"6n+1".</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsPrimeCandidate(System.Numerics.BigInteger number, out System.Numerics.BigInteger multiplier, out System.Numerics.BigInteger offset)
    {
      multiplier = System.Numerics.BigInteger.DivRem(number, 6, out offset);

      if (offset == 5)
      {
        multiplier++;
        offset = -1;

        return true;
      }
      else return offset == 1;
    }

    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsPrimeNumber(System.Numerics.BigInteger number)
    {
      if (number <= long.MaxValue)
        return IsPrimeNumber((long)number);

      if (number <= 3)
        return number >= 2;

      if (number % 2 == 0 || number % 3 == 0)
        return false;

      var limit = Maths.ISqrt(number);

      for (var k = 5; k <= limit; k += 6)
        if ((number % k) == 0 || (number % (k + 2)) == 0)
          return false;

      return true;
    }
    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsPrimeNumber(int source)
    {
      if (source <= 3)
        return source >= 2;

      if (source % 2 == 0 || source % 3 == 0)
        return false;

      var limit = System.Math.Sqrt(source);

      for (var k = 5; k <= limit; k += 6)
        if ((source % k) == 0 || (source % (k + 2)) == 0)
          return false;

      return true;
    }
    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsPrimeNumber(long source)
    {
      if (source <= int.MaxValue)
        return IsPrimeNumber((int)source);

      if (source <= 3)
        return source >= 2;

      if (source % 2 == 0 || source % 3 == 0)
        return false;

      var limit = System.Math.Sqrt(source);

      for (var k = 5; k <= limit; k += 6)
        if ((source % k) == 0 || (source % (k + 2)) == 0)
          return false;

      return true;
    }
    #endregion Statics

    #region Implemented interfaces
    // INumberSequence
    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetAscendingPrimes(2);

    [System.Diagnostics.Contracts.Pure]
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    [System.Diagnostics.Contracts.Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
    #endregion Implemented interfaces
  }

  public sealed class PrimeNumberReverse
    : INumberSequenceable<System.Numerics.BigInteger>
  {
    public System.Numerics.BigInteger StartAt { get; }

    public PrimeNumberReverse(System.Numerics.BigInteger startAt)
      => StartAt = startAt;

    // INumberSequence
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => PrimeNumber.GetDescendingPrimes(StartAt);
    // IEnumerable
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetNumberSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();
  }

  //public static partial class Maths
  //{
  //	/// <summary>Yields a Bell number of the specified value.</summary>
  //	/// <see cref="https://en.wikipedia.org/wiki/Bell_number"/>
  //	/// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
  //	public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetBellNumbers()
  //	{
  //		var current = new System.Numerics.BigInteger[1] { 1 };

  //		while (true)
  //		{
  //			yield return current[0];

  //			var previous = current;
  //			current = new System.Numerics.BigInteger[previous.Length + 1];
  //			current[0] = previous[previous.Length - 1];
  //			for (var i = 1; i <= previous.Length; i++)
  //				current[i] = previous[i - 1] + current[i - 1];
  //		}
  //	}

  //	/// <summary>Creates a new sequence with each element being an array (i.e. row) of Bell numbers in a Bell triangle.</summary>
  //	/// <see cref="https://en.wikipedia.org/wiki/Bell_number"/>
  //	/// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
  //	public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetBellTriangle()
  //	{
  //		var current = new System.Numerics.BigInteger[] { 1 };

  //		while (true)
  //		{
  //			yield return current;

  //			var previous = current;
  //			current = new System.Numerics.BigInteger[previous.Length + 1];
  //			current[0] = previous[previous.Length - 1];
  //			for (var i = 1; i <= previous.Length; i++)
  //				current[i] = previous[i - 1] + current[i - 1];
  //		}
  //	}

  //	/// <summary>Creates a new sequence with each element being an array (i.e. row) of Bell numbers in an augmented Bell triangle.</summary>
  //	/// <see cref="https://en.wikipedia.org/wiki/Bell_number"/>
  //	/// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
  //	public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger[]> GetBellTriangleAugmented()
  //	{
  //		var current = new System.Numerics.BigInteger[] { 1 };

  //		while (true)
  //		{
  //			yield return current;

  //			var previous = current;
  //			current = new System.Numerics.BigInteger[previous.Length + 1];
  //			current[0] = (current[1] = previous[previous.Length - 1]) - previous[0];
  //			for (var i = 2; i <= previous.Length; i++)
  //				current[i] = previous[i - 1] + current[i - 1];
  //		}
  //	}
  //}
}
