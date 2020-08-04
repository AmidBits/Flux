using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    public class SieveOfEratosthenesSegmented
    {
      private const int m_segmentSize = int.MaxValue - 1;

      private readonly SieveOfEratosthenes m_sieveOfEratosthenes;

      private readonly long[] m_primeNumbersOfSegment0;

      public SieveOfEratosthenesSegmented()
      {
        m_sieveOfEratosthenes = new SieveOfEratosthenes(m_segmentSize);

        m_primeNumbersOfSegment0 = m_sieveOfEratosthenes.Select(i => (long)i).ToArray();
      }

      /// <summary>Yields a sequence of prime numbers up to the specified max number.</summary>
      public System.Collections.Generic.IEnumerable<long> GetPrimeNumbers(long maxNumber) => GetSegments(0, (int)System.Math.DivRem(maxNumber, int.MaxValue, out var remainder) + (remainder > 0 ? 1 : 0));

      private System.Collections.Generic.IEnumerable<long> GetSegment(int segmentIndex)
      {
        if (segmentIndex == 0)
        {
          foreach (var primeNumber in m_primeNumbersOfSegment0)
          {
            yield return primeNumber;
          }
        }
        else if (segmentIndex > 0)
        {
          long lo = segmentIndex * m_segmentSize, hi = lo + m_segmentSize;

          var bits = new System.Collections.BitArray(m_segmentSize + 1, true);

          for (var i = 0; i < m_primeNumbersOfSegment0.Length; i++)
          {
            var primeNumber = m_primeNumbersOfSegment0[i];

            var loLimit = lo / primeNumber * primeNumber;

            if (loLimit < lo)
            {
              loLimit += primeNumber;
            }

            for (var j = loLimit; j < hi; j += primeNumber)
            {
              bits[(int)(j - lo)] = false;
            }
          }

          for (var i = lo; i < hi; i++)
          {
            if (bits[(int)(i - lo)])
            {
              yield return i;
            }
          }
        }
        else throw new System.ArgumentOutOfRangeException(nameof(segmentIndex));
      }

      private System.Collections.Generic.IEnumerable<long> GetSegments(int segmentStart, int segmentCount) => System.Linq.ParallelEnumerable.Range(segmentStart, segmentCount).SelectMany(segment => GetSegment(segment));

      /// <summary></summary>
      /// <see cref="https://www.geeksforgeeks.org/segmented-sieve/"/>
      public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimes(System.Numerics.BigInteger number, int segmentSize)
      {
        var primes = new SieveOfEratosthenes(segmentSize).ToList();

        foreach (var p in primes)
        {
          yield return p;
        }

        System.Numerics.BigInteger lo = segmentSize;
        System.Numerics.BigInteger hi = segmentSize << 1;

        var bits = new System.Collections.BitArray(segmentSize + 1, true);

        while (lo < number)
        {
          if (hi >= number)
            hi = number;

          bits.SetAll(true);

          for (var i = 0; i < primes.Count; i++)
          {
            var loLimit = lo / primes[i] * primes[i];
            if (loLimit < lo)
              loLimit += (int)primes[i];

            for (var j = loLimit; j < hi; j += (int)primes[i])
              bits[(int)(j - lo)] = false;
          }

          for (var i = lo; i < hi; i++)
            if (bits[(int)(i - lo)] == true)
              yield return i;

          lo += segmentSize;
          hi += segmentSize;
        }
      }

      /// <summary></summary>
      /// <see cref="https://www.geeksforgeeks.org/segmented-sieve/"/>
      public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPrimesParallel(System.Numerics.BigInteger number, int segmentSize)
      {
        var primes = new SieveOfEratosthenes(segmentSize).Select(i => i.ToBigInteger()).ToList();

        var segmentCount = (int)System.Numerics.BigInteger.DivRem(number, segmentSize, out var remainder) + (remainder > 0 ? 0 : -1);

        return segmentCount > 0 ? primes.Concat(System.Linq.ParallelEnumerable.Range(1, segmentCount).SelectMany(segment => GetSegmentPrimes(segment))) : primes;

        System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSegmentPrimes(System.Numerics.BigInteger segment)
        {
          if (primes is null) throw new System.Exception();

          System.Numerics.BigInteger lo = segment * segmentSize, hi = lo + segmentSize;

          var bits = new System.Collections.BitArray(segmentSize + 1, true);

          if (hi >= number)
            hi = number;

          bits.SetAll(true);

          for (var i = 0; i < primes.Count; i++)
          {
            var loLimit = lo / primes[i] * primes[i];
            if (loLimit < lo)
              loLimit += (int)primes[i];

            for (var j = loLimit; j < hi; j += (int)primes[i])
              bits[(int)(j - lo)] = false;
          }

          for (var i = lo; i < hi; i++)
            if (bits[(int)(i - lo)])
              yield return i;
        }
      }
    }
  }
}
