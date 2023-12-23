#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Results in the mersenne number for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static TSelf GetMersenneNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (TSelf.One + TSelf.One).IntegerPow(number) - TSelf.One;

    /// <summary>Results in a sequence of mersenne numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetMersenneNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var number = TSelf.One; ; number++)
        yield return GetMersenneNumber(number);
    }

    /// <summary>Results in a sequence of mersenne primes.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_prime"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetMersennePrimes<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Linq.Enumerable.Where(GetMersenneNumbers<TSelf>(), IsPrimeNumber);
  }
}
#endif

//#if NET7_0_OR_GREATER
//namespace Flux.NumberSequences
//{
//  /// <summary>Results in a sequence of mersenne numbers.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
//  public sealed class MersenneNumber
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    #region Static methods

//    /// <summary>Results in the mersenne number for the specified number.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
//    public static TSelf GetMersenneNumber<TSelf>(TSelf number)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => (TSelf.One + TSelf.One).IntegerPow(number) - TSelf.One;

//    /// <summary>Results in a sequence of mersenne numbers.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_number"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetMersenneNumbers<TSelf>()
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      for (var number = TSelf.One; ; number++)
//        yield return GetMersenneNumber(number);
//    }
//    /// <summary>Results in a sequence of mersenne primes.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Mersenne_prime"/>
//    public static System.Collections.Generic.IEnumerable<TSelf> GetMersennePrimes<TSelf>()
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//      => System.Linq.Enumerable.Where(GetMersenneNumbers<TSelf>(), PrimeNumber.IsPrimeNumber);

//    #endregion Static methods

//    #region Implemented interfaces
//    // INumberSequence
//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
//      => GetMersenneNumbers<System.Numerics.BigInteger>();


//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
//      => GetSequence().GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//    #endregion Implemented interfaces
//  }
//}
//#endif
