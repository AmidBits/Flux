namespace Flux.Percentiles
{
  /// <summary>
  /// <para>From Microsoft Excel (up to and including version 2013 by means of the PERCENTILE.INC function). Noted as an alternative by NIST.</para>
  /// <see href="https://en.wikipedia.org/wiki/Percentile#Second_variant,_C_=_1"/>
  /// </summary>
  public record class Variant2
    : IPercentileComputable
  {
    public TSelf ComputePercentile<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => PercentValue(sample, p);

    /// <summary>Excel '.EXC' percent rank. Primary variant recommended by NIST.</summary>
    public static TPercent PercentRank<TPercent, TCount>(TPercent percent, TCount count)
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => percent < TPercent.Zero || percent > TPercent.One
      ? throw new System.ArgumentOutOfRangeException(nameof(percent))
      : count < TCount.Zero
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percent * TPercent.CreateChecked(count - TCount.One) + TPercent.One;


    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/>
    /// </summary>
    public static TSelf PercentValue<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (sample is null) throw new System.ArgumentNullException(nameof(sample));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var x = PercentRank(p, sample.Count);
      var m = x % TSelf.One;

      var i = System.Convert.ToInt32(TSelf.Floor(x));

      var v3 = sample[System.Math.Clamp(i, 0, sample.Count - 1)];
      var v2 = sample[System.Math.Clamp(i - 1, 0, sample.Count - 1)];

      return v2 + m * (v3 - v2);
    }
  }
}
