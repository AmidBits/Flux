namespace Flux.Statistics
{
  ///// <summary>
  ///// <para>In probability theory and statistics, the Bernoulli distribution, is the discrete probability distribution of a random variable which takes the value 1 with probability p and the value 0 with probability q = 1 - p. Less formally, it can be thought of as a model for the set of possible outcomes of any single experiment that asks a yes–no question.</para>
  ///// <example>It can be used to represent a (possibly biased) coin toss where 1 and 0 would represent "heads" and "tails", respectively, and p would be the probability of the coin landing on heads (or vice versa where 1 would represent tails and p would be the probability of tails). In particular, unfair coins would have p != 1/2.</example>
  ///// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  ///// <para><see href="https://en.wikipedia.org/wiki/Bernoulli_distribution"/></para>
  ///// </summary>
  public static class BernoulliDistribution
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="p">The probability.</param>
    /// <param name="k">A value of 0 or 1.</param>
    /// <returns></returns>
    public static double ProbabilityMassFunction(this double p, int k)
      => p * k + (1 - p) * (1 - k);
  }
}
