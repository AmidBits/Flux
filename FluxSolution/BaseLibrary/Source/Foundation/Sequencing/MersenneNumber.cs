using System.Linq;

namespace Flux.Numerics
{
  public sealed class MersenneNumber
  : ASequencedNumbers<System.Numerics.BigInteger>
  {
    // INumberSequence
    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public override System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNumberSequence()
      => GetMersenneNumbers();

    #region Static methods
    /// <summary>Results in the mersenne number for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static System.Numerics.BigInteger GetMersenneNumber(int number)
      => System.Numerics.BigInteger.Pow(2, number) - 1;
    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetMersenneNumbers()
    {
      for (var number = 1; number <= int.MaxValue; number++)
        yield return GetMersenneNumber(number);
    }
    /// <summary>Results in a sequence of mersenne primes.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_prime"/>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetMersennePrimes()
      => GetMersenneNumbers().Where(mn => PrimeNumber.IsPrimeNumber(mn));
    #endregion Static methods
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