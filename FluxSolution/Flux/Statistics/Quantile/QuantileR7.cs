//namespace Flux.Statistics.Quantile
//{
//  /// <summary>
//  /// <para>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
//  /// </summary>
//  /// <remarks>
//  /// <para>Equivalent to</para>
//  /// <list type="bullet">
//  /// <item><see href="https://en.wikipedia.org/wiki/Percentile#Second_variant,_C_=_1">Percentile - Second variant, C = 1</see> - Microsoft Excel PERCENTILE.INC function - Python's optional "inclusive" method - Noted as an alternative by NIST</item>
//  /// <item><see href="https://en.wikipedia.org/wiki/Quartile#Method_3">Quartile Method 3</see> - Microsoft Excel	QUARTILE.INC function</item>
//  /// </list>
//  /// </remarks>
//  public record class R7
//    : IQuantileEstimatable
//  {
//    public static IQuantileEstimatable Default => new R7();

//    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
//      where TCount : System.Numerics.IBinaryInteger<TCount>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => TPercent.CreateChecked(count - TCount.One) * Units.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p)) + TPercent.One;

//    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
//      where TValue : System.Numerics.INumber<TValue>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => Edf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
//  }
//}
