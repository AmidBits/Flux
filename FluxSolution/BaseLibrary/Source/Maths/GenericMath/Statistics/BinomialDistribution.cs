namespace Flux.Statistics
{
  /// <summary>
  /// <para>In probability theory and statistics, the binomial distribution with parameters n and p is the discrete probability distribution of the number of successes in a sequence of n independent experiments, each asking a yes–no question, and each with its own Boolean-valued outcome: success (with probability p) or failure (with probability q=1-p).</para>
  /// <example>Suppose a biased coin comes up heads with probability 0.3 when tossed. The probability of seeing exactly 4 heads in 6 tosses is Flux.GenericMath.ProbabilityMassFunctionBinomialDistribution(4, 6, 0.3); which results in: 0.005953499999999999.</example>
  /// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Binomial_distribution"/></para>
  /// </summary>
  public static class BinomialDistribution
  {
#if NET7_0_OR_GREATER

    /// <summary>The probability of getting exactly <paramref name="k"/> successes in <paramref name="n"/> independent Bernoulli trials (each with success probability <paramref name="p"/>).</summary>
    /// <param name="k">The exact number of successes of an outcome inquired.</param>
    /// <param name="n">The number of trials.</param>
    /// <param name="p">The success probability for each trial.</param>
    public static TSelf ProbabilityMassFunction<TSelf>(this TSelf k, TSelf n, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => (n / k) * TSelf.Pow(p, k) * TSelf.Pow(TSelf.One - p, n - k);

#else

    /// <summary>The probability of getting exactly <paramref name="k"/> successes in <paramref name="n"/> independent Bernoulli trials (each with success probability <paramref name="p"/>).</summary>
    /// <param name="k">The exact number of successes of an outcome inquired.</param>
    /// <param name="n">The number of trials.</param>
    /// <param name="p">The success probability for each trial.</param>
    public static double ProbabilityMassFunction(this double k, double n, double p)
      => (n / k) * System.Math.Pow(p, k) * System.Math.Pow(1 - p, n - k);

#endif
  }
}
