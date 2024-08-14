namespace Flux.Statistics
{
  ///// <summary>
  ///// <para>Poisson distribution is a discrete probability distribution that expresses the probability of a given number of events occurring in a fixed interval of time if these events occur with a known constant mean rate and independently of the time since the last event.</para>
  ///// <example>Consider a call center which receives, randomly, an average of a = 3 calls per minute at all times of day. If the calls are independent, receiving one does not change the probability of when the next one will arrive. Under these assumptions, the number k of calls received during any minute has a Poisson probability distribution. Receiving k = 1 to 4 calls then has a probability of about 0.77, while receiving 0 or at least 5 calls has a probability of about 0.23.</example>
  ///// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  ///// <para><see href="https://en.wikipedia.org/wiki/Poisson_distribution"/></para>
  ///// </summary>
  public static class PoissonDistribution
  {
    /// <summary>With the expectation of <paramref name="a"/> events in a given interval, the probability of <paramref name="k"/> events in the same interval is.</summary>
    /// <param name="a">The number of expected events [1, ..] in a given interval.</param>
    /// <param name="k">The number of events [1, ..] in the same interval.</param>
    public static double PoissonDistributionPmf(this double a, int k)
      => System.Math.Pow(a, k) * System.Math.Pow(System.Math.E, -a) / k.Factorial();
  }
}
