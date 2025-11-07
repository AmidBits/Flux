namespace Flux
{
  public static partial class BinomialTheorem
  {
    //extension<TInteger>(TInteger n)
    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //{
    /// <summary>
    /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
    /// <para>This implementation can easily overflow, use larger storage types when possible.</para>
    /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
    /// <para><seealso href="https://cp-algorithms.com/combinatorics/binomial-coefficients.html"/></para>
    /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</para>
    /// <para>(k &lt; 0 or k > n) = 0</para>
    /// <para>(k = 0 or k = n) = 1</para>
    /// </remarks>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static TInteger BinomialCoefficient<TInteger>(this TInteger n, TInteger k)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      //if (TInteger.IsNegative(k) || (k > n))
      //  return TInteger.Zero;

      k = TInteger.Min(k, n - k); // Optimization.

      if (TInteger.IsNegative(k))
        return TInteger.Zero;

      //if (TInteger.IsZero(k)) // Because of the optimization above, only half of "(TInteger.IsZero(k) || k == n)" is needed.
      //  return TInteger.One;

      return FactorialPowers.FallingFactorial(n, k) / k.Factorial();
    }
    // Alternative:
    //{
    //  if (TInteger.IsNegative(k) || k > n)
    //    return TInteger.Zero; // (k < 0 || k > n) = 0
    //  else if (TInteger.IsZero(k) || k == n)
    //    return TInteger.One; // (k == 0 || k == n) = 1

    //  checked
    //  {
    //    k = TInteger.Min(k, n - k); // Optimize for the loop below.

    //    var c = TInteger.One;

    //    for (var i = TInteger.One; i <= k; i++)
    //      c = c * (n - k + i) / i;

    //    return c;
    //  }
    //}
    //}

    /// <summary>
    /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
    /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
    /// <para><seealso href="https://cp-algorithms.com/combinatorics/binomial-coefficients.html"/></para>
    /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</para>
    /// <para>(k &lt; 0 or k > n) = 0</para>
    /// <para>(k = 0 or k = n) = 1</para>
    /// </remarks>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static long BinomialCoefficient(this long n, long k)
    {
      var c = 1L;
      for (var i = 1; i <= k; i++)
        c = c * (n - k + i) / i;
      return c;
    }
  }
}
