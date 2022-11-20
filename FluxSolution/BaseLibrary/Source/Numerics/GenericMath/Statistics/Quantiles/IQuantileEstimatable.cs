namespace Flux
{
  /// <summary>
  /// <para>quantiles are cut points dividing the range of a probability distribution into continuous intervals with equal probabilities, or dividing the observations in a sample in the same way. There is one fewer quantile than the number of groups created. Common quantiles have special names, such as quartiles (four groups), deciles (ten groups), and percentiles (100 groups). The groups created are termed halves, thirds, quarters, etc., though sometimes the terms for the quantile are used for the groups created, rather than for the cut points.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Quantile"/></para>
  /// </summary>
  public interface IQuantileEstimatable
  {
    /// <summary>Computes an estimated quantile of the probability according to the implementation.</summary>
    /// <returns>The estimated quantile of the probability.</returns>
    TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> sample, TSelf probability)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>;
  }
}
