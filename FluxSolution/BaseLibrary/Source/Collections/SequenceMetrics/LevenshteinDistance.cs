using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>The Levenshtein distance between two words is the minimum number of single-character edits(insertions, deletions or substitutions) required to change one word into the other. Uses the specified comparer, or default if null.</summary>
    public static int LevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LevenshteinDistance<T>(comparer).GetMetricDistance(source.ToArray(), target.ToArray());
    /// <summary>The Levenshtein distance between two words is the minimum number of single-character edits(insertions, deletions or substitutions) required to change one word into the other. Uses the specified comparer, or default if null.</summary>
    public static int LevenshteinDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => new SequenceMetrics.LevenshteinDistance<T>().GetMetricDistance(source.ToArray(), target.ToArray());

    /// <summary>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other. Uses the specified comparer.</summary>
    public static int LevenshteinDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
      => new SequenceMetrics.LevenshteinDistance<T>(comparer).GetMetricDistance(source, target);
    /// <summary>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other. Uses the default comparer.</summary>
    public static int LevenshteinDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => ((IMetricDistance<T>)new SequenceMetrics.LevenshteinDistance<T>()).GetMetricDistance(source, target);
  }

  namespace SequenceMetrics
  {
    /// <summary>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</summary>
    /// <see cref = "https://en.wikipedia.org/wiki/Levenshtein_distance" />
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public class LevenshteinDistance<T>
      : IMetricDistance<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
    {
      private System.Collections.Generic.IEqualityComparer<T> m_equalityComparer;

      public LevenshteinDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        => m_equalityComparer = equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default;
      public LevenshteinDistance()
        : this(System.Collections.Generic.EqualityComparer<T>.Default)
      {
      }

      public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      {
        Helper.OptimizeEnds(source, target, m_equalityComparer, out source, out target, out var sourceCount, out var targetCount, out var _, out var _);

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
              m_equalityComparer.Equals(source[i], target[j]) ? v1[j] : v1[j] + 1 // Substitution.
            );
          }
        }

        return v0[targetCount];

        #region Optimized version with only one vector and variables for prior costs, not thoroughly tested!
        //var v0 = new int[target.Length]; // Current row of costs.

        //for (var j = 0; j < target.Length; j++) v0[j] = j + 1; // Initialize v1 (the previous row of costs) to an edit distance for an empty source, i.e. the the number of characters to delete from target.

        //var current = 0;

        //for (var i = 0; i < source.Length; i++)
        //{
        //  current = i;

        //  for (int j = 0, left = i; j < target.Length; j++)
        //  {
        //    var above = current;
        //    current = left; // cost on diagonal (substitution)
        //    left = v0[j];

        //    if (!comparer.Equals(source[i], target[j]))
        //    {
        //      current = Math.Min(above + 1, left + 1, current + 1);
        //    }

        //    v0[j] = current;
        //  }
        //}

        //return v0[target.Length - 1];
        #endregion Optimized version with only one vector and variables for prior costs, not thoroughly tested!

        #region Another optimized version with one vector and temp variables this time, not thoroughly tested!
        //var v0 = new int[target.Length + 1]; // Current row of costs.
        //for (var i = 0; i < target.Length; v0[i] = i++) ;

        //for (var i = 0; i < source.Length; i++)
        //{
        //  v0[0] = i; // initialize with zero cost

        //  for (int j = 0, lastDiagonal = i; j < target.Length; j++)
        //  {
        //    var substitute = lastDiagonal + (comparer.Equals(source[i], target[j]) ? 0 : 1); // last diagonal cost + possible inequality cost

        //    lastDiagonal = v0[j + 1]; // remember the cost of the last diagonal stripe

        //    var insert = v0[j] + 1;
        //    var delete = lastDiagonal + 1; // reuse the recently cached last diagonal in order to reduce use of indexer

        //    v0[j + 1] = Math.Min(substitute, insert, delete);
        //  }
        //}

        //return v0[target.Length];
        #endregion Another optimized version with one vector and temp variables this time, not thoroughly tested!
      }

      public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => 1.0 - GetSimpleMatchingDistance(source, target);

      public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
        => (double)GetMetricDistance(source, target) / (double)System.Math.Max(source.Length, target.Length);
    }
  }
}
