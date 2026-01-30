//namespace Flux.Statistics.Quantile
//{
//  /// <summary>
//  /// <para>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</para>
//  /// <para></para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
//  /// </summary>
//  /// <remarks>
//  /// <list type="bullet">
//  /// <item><see href="https://en.wikipedia.org/wiki/Percentile#Third_variant,_C_=_0">Percentile, Third variant C = 0</see> - Microsoft Excel PERCENTILE.EXC function - Python's default "exclusive" method - Primary variant recommended by NIST.</item>
//  /// <item><see href="https://en.wikipedia.org/wiki/Quartile#Method_4">Quartile, Method 4</see> - Microsoft Excel QUARTILE.EXC function</item>
//  /// </list>
//  /// </remarks>
//  public record class R6
//    : IQuantileEstimatable
//  {
//    public static IQuantileEstimatable Default => new R6();

//    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
//      where TCount : System.Numerics.IBinaryInteger<TCount>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => TPercent.CreateChecked(count + TCount.One) * Units.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p));

//    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
//      where TValue : System.Numerics.INumber<TValue>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => Edf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
//  }
//}
