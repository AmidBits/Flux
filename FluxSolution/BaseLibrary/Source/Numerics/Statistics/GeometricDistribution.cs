namespace Flux.Numerics
{
  ///// <summary>
  ///// <para>In probability theory and statistics, the binomial distribution with parameters n and p is the discrete probability distribution of the number of successes in a sequence of n independent experiments, each asking a yes–no question, and each with its own Boolean-valued outcome: success (with probability p) or failure (with probability q=1-p).</para>
  ///// <example>Suppose a biased coin comes up heads with probability 0.3 when tossed. The probability of seeing exactly 4 heads in 6 tosses is Flux.GenericMath.ProbabilityMassFunctionBinomialDistribution(4, 6, 0.3); which results in: 0.005953499999999999.</example>
  ///// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  ///// <para><see href="https://en.wikipedia.org/wiki/Geometric_distribution"/></para>
  ///// </summary>
  public static class GeometricDistribution
  {
    /// <summary>The probability that the first occurrence of success requires <paramref name="k"/> independent trials, each with success probability <paramref name="p"/>.</summary>
    /// <param name="k">The number of trials [1, ..].</param>
    /// <param name="p">The success probability (0, 1].</param>
    public static TSelf ProbabilityMassFunction1<TSelf>(this TSelf k, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.Pow(TSelf.One - p, k - TSelf.One) * p;

    /// <summary>The probability of <paramref name="k"/> of failures until the first success, each with success probability <paramref name="p"/>.</summary>
    /// <param name="k">The number of failures [0, ..].</param>
    /// <param name="p">The success probability (0, 1].</param>
    public static TSelf ProbabilityMassFunction2<TSelf>(this TSelf k, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.Pow(TSelf.One - p, k) * p;

    public static TSelf CumulativeDistributionFunction1<TSelf>(this TSelf x, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => x >= TSelf.One ? TSelf.One - TSelf.Pow(TSelf.One - p, TSelf.Floor(x)) : TSelf.Zero;

    public static TSelf CumulativeDistributionFunction2<TSelf>(this TSelf x, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => TSelf.IsNegative(x) ? TSelf.Zero : TSelf.One - TSelf.Pow(TSelf.One - p, TSelf.Floor(x) + TSelf.One);
  }
}
