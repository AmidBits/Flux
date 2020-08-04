namespace Flux
{
  public static partial class Math
  {
    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    public static bool IsPrimeNumber(System.Numerics.BigInteger source)
    {
      if (source <= long.MaxValue) return IsPrimeNumber((long)source);
      else if (source <= 3) return source >= 2;
      else if (source % 2 == 0 || source % 3 == 0) return false;

      var limit = source.Sqrt();

      for (var k = 5; k <= limit; k += 6)
      {
        if ((source % k) == 0 || (source % (k + 2)) == 0)
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    public static bool IsPrimeNumber(int source)
    {
      if (source <= 3) return source >= 2;
      else if (source % 2 == 0 || source % 3 == 0) return false;

      var limit = (int)System.Math.Sqrt(source);

      for (var k = 5; k <= limit; k += 6)
      {
        if ((source % k) == 0 || (source % (k + 2)) == 0)
        {
          return false;
        }
      }

      return true;
    }
    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    public static bool IsPrimeNumber(long source)
    {
      if (source <= int.MaxValue) return IsPrimeNumber((int)source);
      else if (source <= 3) return source >= 2;
      else if (source % 2 == 0 || source % 3 == 0) return false;

      var limit = (long)System.Math.Sqrt(source);

      for (var k = 5; k <= limit; k += 6)
      {
        if ((source % k) == 0 || (source % (k + 2)) == 0)
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>Indicates whether the prime number is also an additive prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/List_of_prime_numbers#Additive_primes"/>
    public static bool IsAdditivePrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber(DigitSum(primeNumber, 10));
    /// <summary>Indicates whether the prime number is also a congruent modulo prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    public static bool IsCongruentModuloPrime(System.Numerics.BigInteger primeNumber, System.Numerics.BigInteger a, System.Numerics.BigInteger d)
      => (primeNumber % a == d);
    /// <summary>Indicates whether the prime number is also an Eisenstein prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Eisenstein_prime"/>
    public static bool IsEisensteinPrime(System.Numerics.BigInteger primeNumber)
      => IsCongruentModuloPrime(primeNumber, 3, 2);
    /// <summary>Indicates whether the prime number is also a Gaussian prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Gaussian_integer#Gaussian_primes"/>
    public static bool IsGaussianPrime(System.Numerics.BigInteger primeNumber)
      => IsCongruentModuloPrime(primeNumber, 4, 3);
    /// <summary>Indicates whether the prime number is also a left truncatable prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Truncatable_prime"/>
    public static bool IsLeftTruncatablePrime(System.Numerics.BigInteger primeNumber)
    {
      var text = primeNumber.ToString();

      if (text.IndexOf('0') > -1)
      {
        return false;
      }

      while (text.Length > 0 && System.Numerics.BigInteger.TryParse(text, out var result))
      {
        if (!IsPrimeNumber(result))
        {
          return false;
        }

        text = text.Substring(1);
      }

      return true;
    }
    /// <summary>Indicates whether the prime number is also a Pythagorean prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Pythagorean_prime"/>
    public static bool IsPythagoreanPrime(System.Numerics.BigInteger primeNumber)
      => IsCongruentModuloPrime(primeNumber, 4, 1);
    /// <summary>Indicates whether the prime number is also a safe prime prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Safe_prime"/>
    public static bool IsSafePrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber((primeNumber - 1) / 2);
    /// <summary>Indicates whether the prime number is also a Sophie Germain prime.</summary>
    /// <param name="primeNumber">A prime number. If this number is not a prime number, the result is unpredictable.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Sophie_Germain_prime"/>
    public static bool IsSophieGermainPrime(System.Numerics.BigInteger primeNumber)
      => IsPrimeNumber((primeNumber * 2) + 1);
  }
}
