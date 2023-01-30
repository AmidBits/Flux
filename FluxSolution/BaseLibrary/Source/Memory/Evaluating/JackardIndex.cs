namespace Flux.Metrical
{
  /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
  public sealed class JackardIndex<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public JackardIndex(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public JackardIndex()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    /// <summary>The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    public double MeasureMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => 1 - MeasureSimilarity(source, target);

    /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    public double MeasureSimilarity(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => GetSimilarityCoefficient(source.ToArray(), target.ToArray(), EqualityComparer);

    /// <summary>The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    public static double GetDistance(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => 1 - GetSimilarityCoefficient(source, target, equalityComparer);

    /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    public static double GetSimilarityCoefficient(System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => (double)System.Linq.Enumerable.Count(System.Linq.Enumerable.Intersect(source, target, equalityComparer)) / (double)System.Linq.Enumerable.Count(System.Linq.Enumerable.Union(source, target, equalityComparer));
  }
}
