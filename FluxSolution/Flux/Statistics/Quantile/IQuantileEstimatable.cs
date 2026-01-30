//namespace Flux.Statistics.Quantile
//{
//  /// <summary>
//  /// <para>quantiles are cut points dividing the range of a probability distribution into continuous intervals with equal probabilities, or dividing the observations in a sample in the same way. There is one fewer quantile than the number of groups created. Common quantiles have special names, such as quartiles (four groups), deciles (ten groups), and percentiles (100 groups). The groups created are termed halves, thirds, quarters, etc., though sometimes the terms for the quantile are used for the groups created, rather than for the cut points.</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Quantile"/></para>
//  /// </summary>
//  public interface IQuantileEstimatable
//  {
//    /// <summary>Computes a real valued index (referred to as 'h' on Wikipedia) according to the implementation.</summary>
//    TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
//      where TCount : System.Numerics.IBinaryInteger<TCount>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>;

//    /// <summary>Estimates a quantile value of <paramref name="p"/> in the <paramref name="ordered"/> data according to the implementation.</summary>
//    /// <returns>The estimated quantile of the probability.</returns>
//    TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
//      where TValue : System.Numerics.INumber<TValue>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>;
//  }
//}
