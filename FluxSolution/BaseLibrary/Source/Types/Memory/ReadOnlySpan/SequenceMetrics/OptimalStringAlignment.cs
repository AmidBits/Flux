namespace Flux
{
  public static partial class XtendSequenceMetrics
  {
    /// <summary>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
    public static int OptimalStringAlignment<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.OptimalStringAlignment<T>().GetMetricDistance(source, target, comparer);
    /// <summary>Computes the optimal sequence alignment (OSA) using the default comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
    public static int OptimalStringAlignment<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => new SequenceMetrics.OptimalStringAlignment<T>().GetMetricDistance(source, target);
  }

  namespace SequenceMetrics
  {
    /// <summary>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Edit_distance"/>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public class OptimalStringAlignment<T>
      : IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    {
      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      {
        comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        Helper.OptimizeEnds(source, target, comparer, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

        if (sourceCount == 0) return targetCount;
        else if (targetCount == 0) return sourceCount;

        var v2 = new int[targetCount + 1]; // Row of costs, two rows back.
        var v1 = new int[targetCount + 1]; // Row of costs, one row back (previous).
        var v0 = new int[targetCount + 1]; // Row of costs, current row.

        for (int ti = 0; ti <= targetCount; ti++) v0[ti] = ti; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

        for (int si = 1; si <= sourceCount; si++)
        {
          var rotate = v2; v2 = v1; v1 = v0; v0 = rotate; // Rotate and reuse buffered rows of the cost matrix.

          v0[0] = si; // Edit distance is delete (i) chars from source to match empty target.

          var sourceItem = source[si - 1];

          for (int ti = 1; ti <= targetCount; ti++)
          {
            var targetItem = target[ti - 1];

            var cost = comparer.Equals(sourceItem, targetItem) ? 0 : 1;

            if (si > 1 && ti > 1 && comparer.Equals(sourceItem, target[ti - 2]) && comparer.Equals(source[si - 2], targetItem))
            {
              v0[ti] = Maths.Min(
                v1[ti] + 1, // Deletion.
                v0[ti - 1] + 1, // Insertion.
                v1[ti - 1] + cost, // Substitution.
                v2[ti - 2] + 1 // Transposition.
              );
            }
            else
            {
              v0[ti] = Maths.Min(
                v1[ti] + 1, // Deletion.
                v0[ti - 1] + 1, // Insertion.
                v1[ti - 1] + cost // Substitution.
              );
            }
          }
        }

        return v0[targetCount];
      }
      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetMetricDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => 1.0 - GetSimpleMatchingDistance(source, target, comparer);
      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetSimpleMatchingCoefficient(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
        => (double)GetMetricDistance(source, target, comparer) / (double)System.Math.Max(source.Length, target.Length);
      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => GetSimpleMatchingDistance(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
    }
  }
}