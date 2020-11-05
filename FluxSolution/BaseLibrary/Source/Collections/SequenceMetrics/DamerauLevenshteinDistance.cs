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
      : ASequenceMetric<T>, IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
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

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
      /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
      public int[,] GetGrid(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        var ldg = new int[source.Length + 2, target.Length + 2];

        var dr = new System.Collections.Generic.Dictionary<T, int>(EqualityComparer); // Dictionary of items from both lists.
        for (var si = source.Length - 1; si >= 0; si--)
          if (!dr.ContainsKey(source[si]))
            dr[source[si]] = 0;
        for (var ti = target.Length - 1; ti >= 0; ti--)
          if (!dr.ContainsKey(target[ti]))
            dr[target[ti]] = 0;

        var maxDistance = source.Length + target.Length;

        ldg[0, 0] = maxDistance;

        for (var i = source.Length + 1; i >= 1; i--)
        {
          ldg[i, 1] = i - 1;
          ldg[i, 0] = maxDistance;
        }
        for (var j = target.Length + 1; j >= 1; j--)
        {
          ldg[1, j] = j - 1;
          ldg[0, j] = maxDistance;
        }

        for (var si = 1; si <= source.Length; si++)
        {
          var ltim = 0; // Last target index of target item matching source item.

          var sourceItem = source[si - 1];

          for (var ti = 1; ti <= target.Length; ti++)
          {
            var targetItem = target[ti - 1];

            var lsi = dr[targetItem]; // Last source index of source item.

            var isEqual = EqualityComparer.Equals(sourceItem, targetItem);

            ldg[si + 1, ti + 1] = Maths.Min(
              ldg[si, ti + 1] + 1, // Deletion.
              ldg[si + 1, ti] + 1, // Insertion
              isEqual ? ldg[si, ti] : ldg[si, ti] + 1, // Substitution.
              ldg[lsi, ltim] + (si - lsi - 1) + 1 + (ti - ltim - 1) // Transposition.
            );

            if (isEqual)
              ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return ldg;
      }

      /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
      public double[,] GetGrid(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion, double costOfInsertion, double costOfSubstitution, double costOfTransposition)
      {
        var ldg = new double[source.Length + 2, target.Length + 2];

        var dr = new System.Collections.Generic.Dictionary<T, int>(EqualityComparer); // Dictionary of items from both lists.
        for (var si = source.Length - 1; si >= 0; si--)
          if (!dr.ContainsKey(source[si]))
            dr[source[si]] = 0;
        for (var ti = target.Length - 1; ti >= 0; ti--)
          if (!dr.ContainsKey(target[ti]))
            dr[target[ti]] = 0;

        var maxDistance = source.Length + target.Length;

        ldg[0, 0] = maxDistance * costOfInsertion;

        for (var i = source.Length + 1; i >= 1; i--)
        {
          ldg[i, 1] = (i - 1) * costOfInsertion;
          ldg[i, 0] = maxDistance * costOfInsertion;
        }
        for (var j = target.Length + 1; j >= 1; j--)
        {
          ldg[1, j] = (j - 1) * costOfInsertion;
          ldg[0, j] = maxDistance * costOfInsertion;
        }

        for (var si = 1; si <= source.Length; si++)
        {
          var ltim = 0; // Last target index of target item matching source item.

          var sourceItem = source[si - 1];

          for (var ti = 1; ti <= target.Length; ti++)
          {
            var targetItem = target[ti - 1];

            var lsi = dr[targetItem]; // Last source index of source item.

            var isEqual = EqualityComparer.Equals(sourceItem, targetItem);

            ldg[si + 1, ti + 1] = Maths.Min(
              ldg[si, ti + 1] + costOfDeletion,
              ldg[si + 1, ti] + costOfInsertion,
              isEqual ? ldg[si, ti] : ldg[si, ti] + costOfSubstitution,
              ldg[lsi, ltim] + (si - lsi - 1) + costOfTransposition + (ti - ltim - 1)
            );

            if (isEqual)
              ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return ldg;
      }

      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

        if (sourceCount == 0) return targetCount;
        else if (targetCount == 0) return sourceCount;

        var matrix = new int[sourceCount + 2, targetCount + 2];

        var dr = new System.Collections.Generic.Dictionary<T, int>(EqualityComparer); // Dictionary of items from both lists.
        for (var si = sourceCount - 1; si >= 0; si--)
          if (!dr.ContainsKey(source[si]))
            dr[source[si]] = 0;
        for (var ti = targetCount - 1; ti >= 0; ti--)
          if (!dr.ContainsKey(target[ti]))
            dr[target[ti]] = 0;

        var maxDistance = sourceCount + targetCount;

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

        for (int si = 1; si <= sourceCount; si++)
        {
          var ltim = 0; // Last target index of target item matching source item.

          var sourceItem = source[si - 1];

          for (int ti = 1; ti <= targetCount; ti++)
          {
            var targetItem = target[ti - 1];

            var lsi = dr[targetItem]; // Last source index of source item.

            var isEqual = EqualityComparer.Equals(sourceItem, targetItem);

            matrix[si + 1, ti + 1] = Maths.Min(
              matrix[si, ti + 1] + 1, // Deletion.
              matrix[si + 1, ti] + 1, // Insertion
              isEqual ? matrix[si, ti] : matrix[si, ti] + 1, // Substitution.
              matrix[lsi, ltim] + (si - lsi - 1) + 1 + (ti - ltim - 1) // Transposition.
            );

            if (isEqual)
              ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return matrix[sourceCount + 1, targetCount + 1];
      }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 1 - GetSimpleMatchingDistance(source, target);

      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => (double)GetMetricDistance(source, target) / (double)System.Math.Max(source.Length, target.Length);
    }
  }
}