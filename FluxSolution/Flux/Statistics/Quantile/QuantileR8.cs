//namespace Flux.Statistics.Quantile
//{
//  /// <summary>
//  /// <para>Linear interpolation of the approximate medians for order statistics.</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
//  /// </summary>
//  public record class R8
//    : IQuantileEstimatable
//  {
//    public const double OneThird = 1 / 3;

//    public static IQuantileEstimatable Default => new R8();

//    public TPercent EstimateQuantileRank<TCount, TPercent>(TCount count, TPercent p)
//      where TCount : System.Numerics.IBinaryInteger<TCount>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => (TPercent.CreateChecked(count) + TPercent.CreateChecked(OneThird)) * Units.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p)) + TPercent.CreateChecked(OneThird);

//    public TPercent EstimateQuantileValue<TValue, TPercent>(System.Collections.Generic.IEnumerable<TValue> ordered, TPercent p)
//      where TValue : System.Numerics.INumber<TValue>
//      where TPercent : System.Numerics.IFloatingPoint<TPercent>
//      => Edf.Lerp(ordered, EstimateQuantileRank(ordered.Count(), p) - TPercent.One); // Adjust for 0-based indexing.
//  }
//}
