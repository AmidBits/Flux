using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>The Levenshtein distance between two words is the minimum number of single-character edits(insertions, deletions or substitutions) required to change one word into the other. Uses the specified comparer, or default if null.</summary>
    public static int LevenshteinDistance<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new IndexedMetrics.LevenshteinDistance<T>(comparer).GetMetricDistance((T[])source, (T[])target);
    /// <summary>The Levenshtein distance between two words is the minimum number of single-character edits(insertions, deletions or substitutions) required to change one word into the other. Uses the specified comparer, or default if null.</summary>
    public static int LevenshteinDistance<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
      => new IndexedMetrics.LevenshteinDistance<T>().GetMetricDistance((T[])source, (T[])target);

    /// <summary>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other. Uses the specified comparer.</summary>
    public static int LevenshteinDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new IndexedMetrics.LevenshteinDistance<T>(comparer).GetMetricDistance(source, target);
    /// <summary>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other. Uses the default comparer.</summary>
    public static int LevenshteinDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => ((IMetricDistance<T>)new IndexedMetrics.LevenshteinDistance<T>()).GetMetricDistance(source, target);
  }

  namespace IndexedMetrics
  {
    /// <summary>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</summary>
    /// <see cref = "https://en.wikipedia.org/wiki/Levenshtein_distance" />
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public class LevenshteinDistance<T>
      : AIndexedMetrics<T>, IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    {
      public LevenshteinDistance()
        : base(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }
      public LevenshteinDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        : base(equalityComparer)
      {
      }

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
      /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
      public int[,] GetGrid(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        var ldg = new int[source.Length + 1, target.Length + 1];

        for (var si = 1; si <= source.Length; si++)
          ldg[si, 0] = si;
        for (var ti = 1; ti <= target.Length; ti++)
          ldg[0, ti] = ti;

        for (var si = 1; si <= source.Length; si++)
          for (var ti = 1; ti <= target.Length; ti++)
            ldg[si, ti] = Maths.Min(
              ldg[si - 1, ti] + 1, // Deletion.
              ldg[si, ti - 1] + 1, // Insertion.
              EqualityComparer.Equals(source[si - 1], target[ti - 1]) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + 1 // Substitution.
            );

        return ldg;
      }

      /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
      public double[,] GetGrid(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion, double costOfInsertion, double costOfSubstitution)
      {
        var ldg = new double[source.Length + 1, target.Length + 1];

        for (var si = 1; si <= source.Length; si++)
          ldg[si, 0] = si * costOfInsertion;
        for (var ti = 1; ti <= target.Length; ti++)
          ldg[0, ti] = ti * costOfInsertion;

        for (var si = 1; si <= source.Length; si++)
          for (var ti = 1; ti <= target.Length; ti++)
            ldg[si, ti] = Maths.Min(
              ldg[si - 1, ti] + costOfDeletion,
              ldg[si, ti - 1] + costOfInsertion,
              EqualityComparer.Equals(source[si - 1], target[ti - 1]) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + costOfSubstitution
            );

        return ldg;
      }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

        if (sourceCount == 0) return targetCount;
        else if (targetCount == 0) return sourceCount;

        var v1 = new int[targetCount + 1]; // Row of costs, one row back (previous row).
        var v0 = new int[targetCount + 1]; // Row of costs, current row.

        for (var j = 0; j <= targetCount; j++)
          v0[j] = j; // Initialize the 'previous' (swapped to 'v1' in loop) row.

        for (var i = 0; i < sourceCount; i++)
        {
          var rotate = v1; v1 = v0; v0 = rotate;

          v0[0] = i + 1; // The first element is delete (i + 1) chars from source to match empty target.

          for (var j = 0; j < targetCount; j++)
          {
            v0[j + 1] = Maths.Min(
              v1[j + 1] + 1, // Deletion.
              v0[j] + 1, // Insertion.
              EqualityComparer.Equals(source[i], target[j]) ? v1[j] : v1[j] + 1 // Substitution.
            );
          }
        }

        return v0[targetCount];

        #region Optimized version with only one vector and variables for prior costs, not yet tested!
        //var v = new int[target.Length]; // Current row of costs.

        //for (var ti = 0; ti < target.Length; ti++) 
        //  v[ti] = ti + 1; // Initialize v1 (the previous row of costs) to an edit distance for an empty source, i.e. the the number of characters to delete from target.

        //var current = 0;

        //for (var si = 0; si < source.Length; si++)
        //{
        //  current = si;

        //  for (int ti = 0, left = si; ti < target.Length; ti++)
        //  {
        //    var above = current;
        //    current = left; // cost on diagonal (substitution)
        //    left = v[ti];

        //    if (!EqualityComparer.Equals(source[si], target[ti]))
        //    {
        //      current = Maths.Min(above + 1, left + 1, current + 1);
        //    }

        //    v[ti] = current;
        //  }
        //}

        //return v[target.Length - 1];
        #endregion Optimized version with only one vector and variables for prior costs, not yet tested!

        #region Another optimized version with one vector and temp variables this time, not yet tested!
        //var v = new int[target.Length + 1]; // Current row of costs.

        //for (var ti = 0; ti < target.Length; ti++)
        //  v[ti] = ti;

        //for (var si = 0; si < source.Length; si++)
        //{
        //  v[0] = si; // initialize with zero cost

        //  for (int ti = 0, lastDiagonal = si; ti < target.Length; ti++)
        //  {
        //    var substitute = lastDiagonal + (EqualityComparer.Equals(source[si], target[ti]) ? 0 : 1); // last diagonal cost + possible inequality cost

        //    lastDiagonal = v[ti + 1]; // remember the cost of the last diagonal stripe

        //    var insert = v[ti] + 1;
        //    var delete = lastDiagonal + 1; // reuse the recently cached last diagonal in order to reduce use of indexer

        //    v[ti + 1] = Maths.Min(substitute, insert, delete);
        //  }
        //}

        //return v[target.Length];
        #endregion Another optimized version with one vector and temp variables this time, not yet tested!
      }

      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 1.0 - GetSimpleMatchingDistance(source, target);

      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => (double)GetMetricDistance(source, target) / (double)System.Math.Max(source.Length, target.Length);
    }
  }
}
