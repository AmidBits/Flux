using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Computes the optimal string alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    public static int OptimalStringAlignmentDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.OptimalStringAlignment<T>().GetMetricDistance(source.ToArray(), target.ToArray(), comparer);
    /// <summary>Computes the optimal string alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    public static int OptimalStringAlignmentDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.OptimalStringAlignment<T>().GetMetricDistance(source.ToArray(), target.ToArray(), System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
