using System.Linq;

namespace Flux.Metrics
{
  /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
  public class JackardIndex<T>
    : AMetrics<T>, ISimilarityCoefficient<T>
  {
    public JackardIndex()
      : base(System.Collections.Generic.EqualityComparer<T>.Default)
    { }
    public JackardIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      : base(equalityComparer)
    { }

    /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var sourceArray = source.ToArray();
      var targetArray = target.ToArray();

      return (double)sourceArray.Intersect(targetArray, EqualityComparer).Count() / (double)sourceArray.Union(targetArray, EqualityComparer).Count();
    }

    /// <summary>The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public double GetDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 1 - GetCoefficient(source, target);
  }
}