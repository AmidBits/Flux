using System.Linq;

namespace Flux.SequenceMetrics
{
  public class JackardIndex<T>
  {
    /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      var sourceArray = source.ToArray();
      var targetArray = target.ToArray();

      return (double)sourceArray.Intersect(targetArray, comparer).Count() / (double)sourceArray.Union(targetArray, comparer).Count();
    }
    /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public double GetCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public double GetDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => 1 - GetCoefficient(source, target, comparer);
    /// <summary>The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public double GetDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
