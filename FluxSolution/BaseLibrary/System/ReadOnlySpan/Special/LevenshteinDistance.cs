namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
    /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
    /// </summary>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public static double[,] LevenshteinDistanceCustomMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var ldg = new double[source.Length + 1, target.Length + 1];

      for (var si = 1; si <= source.Length; si++)
        ldg[si, 0] = si * costOfInsertion;
      for (var ti = 1; ti <= target.Length; ti++)
        ldg[0, ti] = ti * costOfInsertion;

      for (var si = 1; si <= source.Length; si++)
        for (var ti = 1; ti <= target.Length; ti++)
          ldg[si, ti] = System.Math.Min(
            ldg[si - 1, ti] + costOfDeletion,
            System.Math.Min(
              ldg[si, ti - 1] + costOfInsertion,
              equalityComparer.Equals(source[si - 1], target[ti - 1]) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + costOfSubstitution
            )
          );

      return ldg;
    }

    /// <summary>
    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
    /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
    /// </summary>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public static double LevenshteinDistanceCustomMetric<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      TrimCommonEnds(source, target, out source, out target, out var _, out var _, equalityComparer);

      if (source.Length == 0) return target.Length;
      else if (target.Length == 0) return source.Length;

      var v1 = new double[target.Length + 1]; // Row of costs, one row back (previous row).
      var v0 = new double[target.Length + 1]; // Row of costs, current row.

      for (var j = 0; j <= target.Length; j++)
        v0[j] = j * costOfInsertion; // Initialize the 'previous' (swapped to 'v1' in loop) row.

      for (var i = 0; i < source.Length; i++)
      {
        (v0, v1) = (v1, v0);

        v0[0] = i + costOfDeletion; // The first element is delete (i + 1) chars from source to match empty target.

        for (var j = 0; j < target.Length; j++)
        {
          v0[j + 1] = System.Math.Min(
            v1[j + 1] + costOfDeletion, // Deletion.
            System.Math.Min(
              v0[j] + costOfInsertion, // Insertion.
              equalityComparer.Equals(source[i], target[j]) ? v1[j] : v1[j] + costOfSubstitution // Substitution.
            )
          );
        }
      }

      return v0[target.Length];
    }

    /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    public static int[,] LevenshteinDistanceMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var ldg = new int[source.Length + 1, target.Length + 1];

      for (var si = 1; si <= source.Length; si++)
        ldg[si, 0] = si;
      for (var ti = 1; ti <= target.Length; ti++)
        ldg[0, ti] = ti;

      for (var si = 1; si <= source.Length; si++)
        for (var ti = 1; ti <= target.Length; ti++)
          ldg[si, ti] = System.Math.Min(
            ldg[si - 1, ti] + 1, // Deletion.
            System.Math.Min(
              ldg[si, ti - 1] + 1, // Insertion.
              equalityComparer.Equals(source[si - 1], target[ti - 1]) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + 1 // Substitution.
            )
          );

      return ldg;
    }

    ///// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    //public double[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion, double costOfInsertion, double costOfSubstitution)
    //{
    //  var sourceLength = source.Length;
    //  var targetLength = target.Length;

    //  var ldg = new double[sourceLength + 1, targetLength + 1];

    //  for (var si = 1; si <= sourceLength; si++)
    //    ldg[si, 0] = si * costOfInsertion;
    //  for (var ti = 1; ti <= targetLength; ti++)
    //    ldg[0, ti] = ti * costOfInsertion;

    //  for (var si = 1; si <= sourceLength; si++)
    //    for (var ti = 1; ti <= targetLength; ti++)
    //      ldg[si, ti] = Maths.Min(
    //        ldg[si - 1, ti] + costOfDeletion,
    //        ldg[si, ti - 1] + costOfInsertion,
    //        EqualityComparer.Equals(source[si - 1], target[ti - 1]) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + costOfSubstitution
    //      );

    //  return ldg;
    //}
    public static int LevenshteinDistanceMetric<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      TrimCommonEnds(source, target, out source, out target, out var _, out var _, equalityComparer);

      if (source.Length == 0) return target.Length;
      else if (target.Length == 0) return source.Length;

      var v1 = new int[target.Length + 1]; // Row of costs, one row back (previous row).
      var v0 = new int[target.Length + 1]; // Row of costs, current row.

      for (var ti = 0; ti <= target.Length; ti++)
        v0[ti] = ti; // Initialize the 'previous' (swapped to 'v1' in loop) row.

      for (var si = 0; si < source.Length; si++)
      {
        (v0, v1) = (v1, v0);

        v0[0] = si + 1; // The first element is delete (i + 1) chars from source to match empty target.

        for (var ti = 0; ti < target.Length; ti++)
        {
          v0[ti + 1] = System.Math.Min(
            v1[ti + 1] + 1, // Deletion.
            System.Math.Min(
              v0[ti] + 1, // Insertion.
              equalityComparer.Equals(source[si], target[ti]) ? v1[ti] : v1[ti] + 1 // Substitution.
            )
          );
        }
      }

      return v0[target.Length];

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

      //    if (!equalityComparer.Equals(source[si], target[ti]))
      //    {
      //      current = int.Min(above + 1, int.Min(left + 1, current + 1));
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
      //    var substitute = lastDiagonal + (equalityComparer.Equals(source[si], target[ti]) ? 0 : 1); // last diagonal cost + possible inequality cost

      //    lastDiagonal = v[ti + 1]; // remember the cost of the last diagonal stripe

      //    var insert = v[ti] + 1;
      //    var delete = lastDiagonal + 1; // reuse the recently cached last diagonal in order to reduce use of indexer

      //    v[ti + 1] = int.Min(substitute, int.Min(insert, delete));
      //  }
      //}

      //return v[target.Length];
      #endregion Another optimized version with one vector and temp variables this time, not yet tested!
    }

    /// <summary>
    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
    /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
    /// </summary>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public static double LevenshteinDistanceSmc<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => 1d - LevenshteinDistanceSmd(source, target, equalityComparer);

    /// <summary>
    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
    /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
    /// </summary>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public static double LevenshteinDistanceSmd<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => (double)LevenshteinDistanceMetric(source, target, equalityComparer) / (double)System.Math.Max(source.Length, target.Length);
  }
}
