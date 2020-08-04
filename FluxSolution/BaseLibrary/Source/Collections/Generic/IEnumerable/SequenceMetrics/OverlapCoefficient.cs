using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public static double OverlapCoefficient<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.OverlapCoefficient<T>().GetCoefficient(source.ToArray(), target.ToArray(), comparer);
    /// <summary>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    public static double OverlapCoefficient<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? target)
      => new SequenceMetrics.OverlapCoefficient<T>().GetCoefficient(source.ToArray(), target.ToArray());
  }
}