using System.Linq;

namespace Flux.SequenceMetrics
{
  public class OverlapCoefficient<T>
  {
    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => (double)source.ToArray().Intersect(target.ToArray(), comparer).Count() / System.Math.Min(source.Length, target.Length);
    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
