using System.Linq;

namespace Flux.Percentiles
{
  /// <summary>
  /// <para>Adopted by Microsoft Excel since 2010 by means of PERCENTIL.EXC function. The primary variant recommended by NIST.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Percentile#Third_variant,_C_=_0"/></para>
  /// </summary>
  public record class Variant3
    : IPercentileComputable
  {
    public TSelf ComputePercentile<TSelf>(System.Collections.Generic.IEnumerable<TSelf> distribution, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => PercentValue(distribution, p);

    /// <summary>Excel '.EXC' percent rank. Primary variant recommended by NIST.</summary>
    public static TPercent PercentRank<TPercent, TCount>(TPercent percent, TCount count)
      where TPercent : System.Numerics.IFloatingPoint<TPercent>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => percent < TPercent.Zero || percent > TPercent.One
      ? throw new System.ArgumentOutOfRangeException(nameof(percent))
      : count < TCount.Zero
      ? throw new System.ArgumentOutOfRangeException(nameof(count))
      : percent * TPercent.CreateChecked(count + TCount.One);


    /// <summary>
    /// <para>Inverse of empirical distribution function.</para>
    /// <see href="https://en.wikipedia.org/wiki/Percentile"/>
    /// </summary>
    public static TSelf PercentValue<TSelf>(System.Collections.Generic.IEnumerable<TSelf> distribution, TSelf p)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      if (distribution is null) throw new System.ArgumentNullException(nameof(distribution));
      if (p < TSelf.Zero || p > TSelf.One) throw new System.ArgumentOutOfRangeException(nameof(p));

      var sampleCount = distribution.Count();

      var x = PercentRank(p, sampleCount);
      var m = x % TSelf.One;

      var i = System.Convert.ToInt32(TSelf.Floor(x));

      var v3 = distribution.ElementAt(int.Clamp(i, 0, sampleCount - 1));
      var v2 = distribution.ElementAt(int.Clamp(i - 1, 0, sampleCount - 1));

      return v2 + m * (v3 - v2);
    }
  }
}