namespace Flux.Numerics
{
  /// <summary>
  /// <para>From Microsoft Excel (up to and including version 2013 by means of the PERCENTILE.INC function). Noted as an alternative by NIST.</para>
  /// <see href="https://en.wikipedia.org/wiki/Percentile#Second_variant,_C_=_1"/>
  /// </summary>
  public record class PercentileVariant2
    : IPercentileComputable
  {
    public TPercent ComputePercentileRank<TCount, TPercent>(TCount count, TPercent p)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileRank(count, p);

    public TPercent ComputePercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => PercentileScore(distribution, p);

    /// <summary>Excel '.EXC' percent rank. Primary variant recommended by NIST.</summary>
    public static TPercent PercentileRank<TCount, TPercent>(TCount count, TPercent percent)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      => percent < TPercent.Zero || percent > TPercent.One
      ? throw new System.ArgumentOutOfRangeException(nameof(percent))
      : count < TCount.Zero
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percent * TPercent.CreateChecked(count - TCount.One) + TPercent.One;

    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    /// </summary>
    public static TPercent PercentileScore<TScore, TPercent>(System.Collections.Generic.IEnumerable<TScore> distribution, TPercent p)
      where TScore : System.Numerics.INumber<TScore>
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
    {
      if (distribution is null) throw new System.ArgumentNullException(nameof(distribution));
      if (p < TPercent.Zero || p > TPercent.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var sampleCount = distribution.Count();

      var x = PercentileRank(sampleCount, p);
      var m = x % TPercent.One;

      var i = System.Convert.ToInt32(TPercent.Floor(x));

      var v3 = distribution.ElementAt(int.Clamp(i, 0, sampleCount - 1));
      var v2 = distribution.ElementAt(int.Clamp(i - 1, 0, sampleCount - 1));

      return TPercent.CreateChecked(v2) + m * TPercent.CreateChecked(v3 - v2);
    }
  }
}