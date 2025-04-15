namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para><see href="https://cp-algorithms.com/string/string-hashing.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="p">It is reasonable to make this a prime number roughly equal to the number of characters in the input "alphabet".</param>
    /// <param name="m">This is the limit of the hash-value created. It should be a large number since the probability of two random strings colliding is about (1/m).</param>
    /// <param name="valueSelector">This is used to create some numerical value for each "letter" in the "alphabet".</param>
    /// <returns></returns>
    public static long PolynomialRollingHash<T>(this System.ReadOnlySpan<T> source, long p, long m, System.Func<T, int, long> valueSelector)
    {
      var hash_value = 0L;
      var p_pow = 1L;

      for (var i = 0; i < source.Length; i++)
      {
        hash_value = (hash_value + (valueSelector(source[i], i) + 1) * p_pow) % m;

        p_pow = p_pow * p % m;
      }

      return hash_value;
    }
  }
}
