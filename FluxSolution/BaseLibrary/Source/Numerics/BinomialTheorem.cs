namespace Flux.Numerics
{
  /// <summary>
  /// <para>In probability theory and statistics, the binomial distribution with parameters n and p is the discrete probability distribution of the number of successes in a sequence of n independent experiments, each asking a yes–no question, and each with its own Boolean-valued outcome: success (with probability p) or failure (with probability q=1-p).</para>
  /// <example>Suppose a biased coin comes up heads with probability 0.3 when tossed. The probability of seeing exactly 4 heads in 6 tosses is Flux.GenericMath.ProbabilityMassFunctionBinomialDistribution(4, 6, 0.3); which results in: 0.005953499999999999.</example>
  /// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Binomial_distribution"/></para>
  /// </summary>
  public static class BinomialTheorem
  {
    /// <summary>
    /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
    /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
    /// </summary>
    /// <remarks>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="n">Greater than or equal to <paramref name="k"/>.</param>
    /// <param name="k">Greater than or equal to 0.</param>
    /// <returns></returns>
    public static TSelf BinomialCoefficient<TSelf>(this TSelf n, TSelf k)
       where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(k) || k > n)
        return TSelf.Zero;

      if (TSelf.IsZero(k) || k == n)
        return TSelf.One;

      k = TSelf.Min(k, n - k);

      var c = TSelf.One;

      for (var d = TSelf.One; d <= k; d++)
      {
        c *= n--;
        c /= d;
      }

      #region Alternative loop (not verified)
      //for (var i = TSelf.Zero; i < k; i++)
      //  c = c * (n - i) / (i + TSelf.One);
      #endregion

      return c;
    }

    /// <summary>The probability of getting exactly <paramref name="k"/> successes in <paramref name="n"/> independent Bernoulli trials (each with success probability <paramref name="p"/>).</summary>
    /// <param name="k">The exact number of successes of an outcome inquired.</param>
    /// <param name="n">The number of trials.</param>
    /// <param name="p">The success probability for each trial.</param>
    public static TSelf ProbabilityMassFunction<TSelf>(this TSelf k, TSelf n, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => (n / k) * TSelf.Pow(p, k) * TSelf.Pow(TSelf.One - p, n - k);
  }
}
