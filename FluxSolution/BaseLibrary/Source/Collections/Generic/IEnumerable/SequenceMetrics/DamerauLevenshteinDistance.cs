using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences, using the specified comparer. Implemented based on the Wiki article.</summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or adjacent transpositions.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    public static int DamerauLevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      where T : notnull
      => new SequenceMetrics.DamerauLevenshteinDistance<T>().GetMetricDistance(source.ToArray(), target.ToArray(), comparer);
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences, using the specified comparer. Implemented based on the Wiki article.</summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or adjacent transpositions.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    public static int DamerauLevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      where T : notnull
      => new SequenceMetrics.DamerauLevenshteinDistance<T>().GetMetricDistance(source.ToArray(), target.ToArray(), System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
