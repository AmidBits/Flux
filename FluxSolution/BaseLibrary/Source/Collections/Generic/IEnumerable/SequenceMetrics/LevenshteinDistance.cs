using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>The Levenshtein distance between two words is the minimum number of single-character edits(insertions, deletions or substitutions) required to change one word into the other. Uses the specified comparer, or default if null.</summary>
    /// <see cref = "https://en.wikipedia.org/wiki/Levenshtein_distance" />
    public static int LevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LevenshteinDistance<T>().GetMetricDistance(source.ToArray(), target.ToArray(), comparer);
    /// <summary>The Levenshtein distance between two words is the minimum number of single-character edits(insertions, deletions or substitutions) required to change one word into the other. Uses the specified comparer, or default if null.</summary>
    /// <see cref = "https://en.wikipedia.org/wiki/Levenshtein_distance" />
    public static int LevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.LevenshteinDistance<T>().GetMetricDistance(source.ToArray(), target.ToArray(), System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
