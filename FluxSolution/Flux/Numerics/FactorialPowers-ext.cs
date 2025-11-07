namespace Flux
{
  public static partial class FactorialPowers
  {
    ///// <summary>
    ///// <para>A generic factorial power function.</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <param name="x"></param>
    ///// <param name="n"></param>
    ///// <param name="h"></param>
    ///// <returns></returns>
    //public static TNumber FactorialPower<TNumber>(this TNumber x, TNumber n, TNumber h)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(n);
    //  System.ArgumentOutOfRangeException.ThrowIfZero(h);

    //  TNumber result = TNumber.One;

    //  checked
    //  {
    //    for (var i = TNumber.Zero; i < n; i++) // Compute the falling factorial, decreasing x for each term.
    //      result *= x - i * h;
    //  }

    //  return result;
    //}

    /// <summary>
    /// <para>When n is a positive integer, the falling factorial, (x)_n, gives the number of n-permutations (sequences of distinct elements) from an n-element set.</para>
    /// <example>
    /// <para>The number (3) of different podiums (assignments of gold, silver, and bronze medals) possible in an eight-person race: <c>FallingFactorialPower(8, 3)</c></para>
    /// </example>
    /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
    /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <remarks>The count of permutations no repetitions.</remarks>
    public static TInteger FallingFactorial<TInteger>(this TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      TInteger result = TInteger.One;

      checked
      {
        for (var i = TInteger.Zero; i < n; i++) // Compute the falling factorial, decreasing x for each term.
          result *= x - i;
      }

      return result;
    }

    /// <summary>
    /// <para>The rising factorial, x^(n), gives the number of partitions of an n-element set into x ordered sequences (possibly empty).</para>
    /// <example>
    /// <para>The "the number of ways to arrange n flags on x flagpoles", where all flags must be used and each flagpole can have any number of flags.</para>
    /// <para>Equivalently, this is the number of ways to partition a set of size n (the flags) into x distinguishable parts (the poles), with a linear order on the elements assigned to each part (the order of the flags on a given pole).</para>
    /// </example>
    /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static TInteger RisingFactorial<TInteger>(this TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      var result = TInteger.One;

      checked
      {
        for (var i = TInteger.Zero; i < n; i++) // Calculate the product x * (x+1) * (x+2) * ... * (x+n-1).
          result *= x + i;
      }

      return result;
    }

    // Pockhammer is rather ambiguous and can represent either the rising or the falling power.
    //public static TInteger Pockhammer<TInteger>(this TInteger x, TInteger n)
    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //  => RisingFactorial(x, n);
  }
}
