namespace Flux.Statistics
{
  ///// <summary>
  ///// <para>In probability theory and statistics, the binomial distribution with parameters n and p is the discrete probability distribution of the number of successes in a sequence of n independent experiments, each asking a yes–no question, and each with its own Boolean-valued outcome: success (with probability p) or failure (with probability q=1-p).</para>
  ///// <example>Suppose a biased coin comes up heads with probability 0.3 when tossed. The probability of seeing exactly 4 heads in 6 tosses is Flux.GenericMath.ProbabilityMassFunctionBinomialDistribution(4, 6, 0.3); which results in: 0.005953499999999999.</example>
  ///// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  ///// <para><see href="https://en.wikipedia.org/wiki/Geometric_distribution"/></para>
  ///// </summary>
  public static class PoissonDistribution
  {
    /// <summary>With the expectation of <paramref name="a"/> events in a given interval, the probability of <paramref name="k"/> events in the same interval is.</summary>
    /// <param name="a">The number of trials [1, ..].</param>
    /// <param name="k">The success probability (0, 1].</param>
    public static double ProbabilityMassFunction(this double a, int k)
      => System.Math.Pow(a, k) * System.Math.Pow(System.Math.E, -a) / new Factorial<int>().ComputeFactorial(k);
  }
}
