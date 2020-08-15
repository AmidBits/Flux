using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public static double JackardCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.JackardIndex<T>().GetCoefficient(source.ToArray(), target.ToArray(), comparer);
    /// <summary>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Jaccard_index"/>
    public static double JackardCoefficient<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.JackardIndex<T>().GetCoefficient(source.ToArray(), target.ToArray(), System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
