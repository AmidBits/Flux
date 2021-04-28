namespace Flux.Numerics
{
  public class MersenneNumber
  : INumberSequence<System.Numerics.BigInteger>
  {
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static System.Numerics.BigInteger GetNumber(int number)
      => System.Numerics.BigInteger.Pow(2, number) - 1;

    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
    {
      for (var number = 1; number <= int.MaxValue; number++)
        yield return GetNumber(number);
    }

    /// <summary>Results in a sequence of mersenne primes.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_prime"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetMersennePrimes()
    {
      for (var number = 1; number <= int.MaxValue; number++)
        if (GetNumber(number) is var potentialMersennePrime && PrimeNumber.IsPrimeNumber(potentialMersennePrime))
          yield return potentialMersennePrime;
    }
  }
}

//namespace Flux
//{
//	public static partial class Maths
//	{
//		/// <summary>Results in a sequence of mersenne numbers.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
//		public static System.Numerics.BigInteger GetMersenneNumber(int value)
//			=> System.Numerics.BigInteger.Pow(2, value) - 1;

//		/// <summary>Results in a sequence of mersenne numbers.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
//		public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetMersenneNumbers()
//		{
//			for (var number = 1; number <= int.MaxValue; number++)
//				yield return GetMersenneNumber(number);
//		}

//		/// <summary>Results in a sequence of mersenne primes.</summary>
//		/// <see cref="https://en.wikipedia.org/wiki/Mersenne_prime"/>
//		public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetMersennePrimes()
//			=> GetMersenneNumbers().Where(bi => IsPrimeNumber(bi));
//	}
//}
