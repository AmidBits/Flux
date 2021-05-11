using System.Linq;

namespace Flux.Memory.Metrics
{
  public class OverlapCoefficient<T>
    : AMetrics<T>, ISimilarityCoefficient<T>
  {
    public OverlapCoefficient()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    {
    }
    public OverlapCoefficient(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    {
    }

    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => (double)source.ToArray().Intersect(target.ToArray(), EqualityComparer).Count() / (double)System.Math.Min(source.Length, target.Length);
  }
}
