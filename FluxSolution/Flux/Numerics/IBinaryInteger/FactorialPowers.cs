namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>When x is a positive integer, the falling factorial gives the number of n-permutations (sequences of distinct elements) from an x-element set.</para>
    /// <example>
    /// <para>The number (3) of different podiums (assignments of gold, silver, and bronze medals) possible in an eight-person race: <c>FallingFactorialPower(8, 3)</c></para>
    /// </example>
    /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="exponent"></param>
    /// <returns></returns>
    /// <remarks>The count of permutations no repetitions.</remarks>
    public static TInteger FallingFactorial<TInteger>(this TInteger value, TInteger exponent)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);
      System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

      return checked(value.Factorial() / (value - exponent).Factorial());
    }

    /// <summary>
    /// <para>The rising factorial gives the number of partitions of an n-element set into x ordered sequences (possibly empty).</para>
    /// <example>
    /// <para>The "the number of ways to arrange n flags on x flagpoles", where all flags must be used and each flagpole can have any number of flags.</para>
    /// <para>Equivalently, this is the number of ways to partition a set of size n (the flags) into x distinguishable parts (the poles), with a linear order on the elements assigned to each part (the order of the flags on a given pole).</para>
    /// </example>
    /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="exponent"></param>
    /// <returns></returns>
    public static TInteger RisingFactorial<TInteger>(this TInteger value, TInteger exponent)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);
      System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

      return checked((value + exponent - TInteger.One).Factorial() / (value - TInteger.One).Factorial());
    }
  }
}
