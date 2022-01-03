using System.Linq;

namespace Flux.Metrical
{
  /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
  public sealed class OverlapCoefficient<T>
    : ISimilarityCoefficient<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public OverlapCoefficient(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public OverlapCoefficient()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    public double GetSimilarityCoefficient(T[] source, T[] target)
      => (double)source.Intersect(target, EqualityComparer).Count() / (double)System.Math.Min(source.Length, target.Length);
    public double GetSimilarityCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetSimilarityCoefficient(source.ToArray(), target.ToArray());
  }
}
