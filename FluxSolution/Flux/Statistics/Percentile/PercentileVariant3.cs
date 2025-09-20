namespace Flux.Statistics.Percentile
{
  /// <summary>
  /// <para>Adopted by Microsoft Excel since 2010 by means of PERCENTIL.EXC function. The primary variant recommended by NIST.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Percentile#Third_variant,_C_=_0"/></para>
  /// </summary>
  public record class Variant3
    : IPercentileComputable
  {
    /// <summary>Excel '.EXC' percent rank. Primary variant recommended by NIST.</summary>
    public static TPercent PercentileRank<TCount, TPercent>(TCount count, TPercent percent)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(count);

      Interval.AssertMember(percent, TPercent.Zero, TPercent.One, IntervalNotation.Closed);

      return percent * TPercent.CreateChecked(count + TCount.One);
    }

    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Percentile"/>
    /// </summary>
    public static TPercent PercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      System.ArgumentNullException.ThrowIfNull(distribution);

      var sampleCount = distribution.Count();
      var x = PercentileRank(sampleCount, Units.UnitInterval.AssertMember(p, IntervalNotation.Closed, nameof(p)));
      var m = x % TPercent.One;

      var i = System.Convert.ToInt32(TPercent.Floor(x));

      var v3 = distribution.ElementAt(int.Clamp(i, 0, sampleCount - 1));
      var v2 = distribution.ElementAt(int.Clamp(i - 1, 0, sampleCount - 1));

      return TPercent.CreateChecked(v2) + m * TPercent.CreateChecked(v3 - v2);
    }

    public TPercent ComputePercentileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileRank(count, p);

    public TPercent ComputePercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileScore(distribution, p);
  }
}
