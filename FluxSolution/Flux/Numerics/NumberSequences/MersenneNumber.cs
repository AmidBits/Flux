namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Computes the Mersenne number for the specified <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TValue GetMersenneNumber<TValue>(TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => checked((TValue.One << int.CreateChecked(value)) - TValue.One);

    /// <summary>`
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
