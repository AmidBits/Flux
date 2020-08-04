using System.Linq;

namespace Flux
{
  public static partial class XtensionsDouble
  {
    /// <summary>Computes the percentile of the specified percentile rank within the ordered source distribution.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile"/>
    public static double Percentile(this IOrderedEnumerable<double> source, double percentileRank)
      => source.ToArray() is var scores && scores.Length >= 1 ? (percentileRank * (scores.Length - 1) / 100.0) is var index && index >= 0 && index < scores.Length ? scores[(int)index] : throw new System.ArgumentOutOfRangeException(nameof(percentileRank)) : throw new System.ArgumentException(@"At least one element required.", nameof(source));
    /// <summary>Computes the percentile of the specified percentile rank within the source distribution.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile"/>
    public static double Percentile(this System.Collections.Generic.IEnumerable<double> source, double percentileRank)
      => source.OrderBy(k => k).Percentile(percentileRank);
    /// <summary>Computes the percentile of the percentile rank within the specified source distribution.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile"/>
    public static double Percentile(this double source, params double[] distribution)
      => distribution.Percentile(source);
  }
}
