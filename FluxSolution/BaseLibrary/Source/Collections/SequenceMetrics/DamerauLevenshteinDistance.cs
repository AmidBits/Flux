using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</summary>
    public static int DamerauLevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      where T : notnull
      => new SequenceMetrics.DamerauLevenshteinDistance<T>(comparer).GetMetricDistance(source.ToArray(), target.ToArray());
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</summary>
    public static int DamerauLevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      where T : notnull
      => new SequenceMetrics.DamerauLevenshteinDistance<T>().GetMetricDistance(source.ToArray(), target.ToArray());

    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</summary>
    public static int DamerauLevenshteinDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      where T : notnull
      => new SequenceMetrics.DamerauLevenshteinDistance<T>(comparer).GetMetricDistance(source, target);
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</summary>
    public static int DamerauLevenshteinDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      where T : notnull
      => new SequenceMetrics.DamerauLevenshteinDistance<T>().GetMetricDistance(source, target);
  }

  namespace SequenceMetrics
  {
    /// <summary>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Triangle_inequality"/>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public class DamerauLevenshteinDistance<T>
      : SequenceMetric<T>, IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
      where T : notnull
    {
      public DamerauLevenshteinDistance()
        : base(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }
      public DamerauLevenshteinDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        : base(equalityComparer)
      {
      }

      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

        if (sourceCount == 0) return targetCount;
        else if (targetCount == 0) return sourceCount;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
        var matrix = new int[sourceCount + 2, targetCount + 2];
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

        int maxDistance = sourceCount + targetCount;

        matrix[0, 0] = maxDistance;

        for (int i = sourceCount + 1; i >= 1; i--)
        {
          matrix[i, 1] = i - 1;
          matrix[i, 0] = maxDistance;
        }
        for (int j = targetCount + 1; j >= 1; j--)
        {
          matrix[1, j] = j - 1;
          matrix[0, j] = maxDistance;
        }

        var dr = new System.Collections.Generic.Dictionary<T, int>(maxDistance, EqualityComparer); // Unique list of all items in both lists.
        for (var si = sourceCount - 1; si >= 0; si--) dr[source[si]] = 0; // Add items from source.
        for (var ti = targetCount - 1; ti >= 0; ti--) dr[target[ti]] = 0; // Add items from target.

        for (int si = 1; si <= sourceCount; si++)
        {
          var ltim = 0; // Last target index of target item matching source item.

          var sourceItem = source[si - 1];

          for (int ti = 1; ti <= targetCount; ti++)
          {
            var targetItem = target[ti - 1];

            var lsi = dr[targetItem]; // Last source index of source item.

            var cost = EqualityComparer.Equals(sourceItem, targetItem) ? 0 : 1; // Cost of substitution.

            matrix[si + 1, ti + 1] = Maths.Min(
              matrix[si, ti + 1] + 1, // Deletion.
              matrix[si + 1, ti] + 1, // Insertion
              matrix[si, ti] + cost, // Substitution.
              matrix[lsi, ltim] + (si - lsi - 1) + 1 + (ti - ltim - 1) // Transposition.
            );

            if (cost == 0) ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return matrix[sourceCount + 1, targetCount + 1];
      }

      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 1 - GetSimpleMatchingDistance(source, target);

      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => (double)GetMetricDistance(source, target) / (double)System.Math.Max(source.Length, target.Length);
    }
  }
}