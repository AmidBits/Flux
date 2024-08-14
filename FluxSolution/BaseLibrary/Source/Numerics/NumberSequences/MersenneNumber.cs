namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Computes the Mersenne number for the specified <paramref name="number"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static TSelf GetMersenneNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (TSelf.One + TSelf.One).IntegerPow(number) - TSelf.One;

    /// <summary>
    /// <para>Creates a new sequence of Mersenne numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetMersenneNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var number = TSelf.One; ; number++)
        yield return GetMersenneNumber(number);
    }

    /// <summary>
    /// <para>Creates a new sequence of Mersenne primes.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetMersennePrimes<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetMersenneNumbers<TSelf>().Where(IsPrimeNumber);
  }
}
