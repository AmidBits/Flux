namespace Flux.Statistics
{
  ///// <summary>
  ///// <para>The geometric distribution gives the probability that the first occurrence of success requires k independent trials, each with success probability p.</para>
  ///// <example>Suppose an ordinary die is thrown repeatedly until the first time a "1" appears. The probability distribution of the number of times it is thrown is supported on the infinite set {1,2,3,...} and is a geometric distribution with p = 1/6.</example>
  ///// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  ///// <para><see href="https://en.wikipedia.org/wiki/Geometric_distribution"/></para>
  ///// </summary>
  public static class GeometricDistribution
  {
    public static System.Collections.Generic.IEnumerable<TSelf> GeometricSequence<TSelf>(this TSelf a, TSelf r)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if(a == 0) throw new System.ArgumentExceptionOutOfRange(nameof(a));
      if(r == 0) throw new System.ArgumentExceptionOutOfRange(nameof(r));

      while(true)
      {
        yield return a;

        a *= r;
      }
    }
    
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
      => x < TSelf.Zero ? TSelf.Zero : TSelf.One - TSelf.Pow(TSelf.One - p, TSelf.Floor(x) + TSelf.One);
  }
}
