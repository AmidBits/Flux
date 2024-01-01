namespace Flux
{
  public static partial class Fx
  {
    public static double GetOptimalStringAlignmentDerivedSmc<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => 1d - GetOptimalStringAlignmentDerivedSmd(source, target, equalityComparer);
    public static double GetOptimalStringAlignmentDerivedSmd<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => (double)GetOptimalStringAlignmentMetric(source, target, equalityComparer) / (double)System.Math.Max(source.Length, target.Length);

    /// <summary>
    /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
    /// </summary>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public static int[,] GetOptimalStringAlignmentMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      var ldg = new int[sourceLength + 1, targetLength + 1];

      for (var si = 1; si <= sourceLength; si++)
        ldg[si, 0] = si;
      for (var ti = 1; ti <= targetLength; ti++)
        ldg[0, ti] = ti;

      for (int si = 1; si <= sourceLength; si++)
      {
        var sourceItem = source[si - 1];

        for (int ti = 1; ti <= targetLength; ti++)
        {
          var targetItem = target[ti - 1];

          ldg[si, ti] = ldg[si, ti] = System.Math.Min(
            System.Math.Min(
              ldg[si - 1, ti] + 1, // Deletion.
              ldg[si, ti - 1] + 1 // Insertion.
            ),
            System.Math.Min(
              equalityComparer.Equals(sourceItem, targetItem) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + 1, // Substitution.
              si > 1 && ti > 1 && equalityComparer.Equals(sourceItem, target[ti - 2]) && equalityComparer.Equals(source[si - 2], targetItem) ? ldg[si - 2, ti - 2] + 1 : int.MaxValue // Transposition.
            )
          );
        }
      }

      return ldg;
    }

    public static int GetOptimalStringAlignmentMetric<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      TrimCommonEnds(source, target, out source, out target, out var _, out var _, equalityComparer);

      if (source.Length == 0) return target.Length;
      else if (target.Length == 0) return source.Length;

      var v2 = new int[target.Length + 1]; // Row of costs, two rows back.
      var v1 = new int[target.Length + 1]; // Row of costs, one row back (previous).
      var v0 = new int[target.Length + 1]; // Row of costs, current row.

      for (int ti = 0; ti <= target.Length; ti++) v0[ti] = ti; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

      for (int si = 1; si <= source.Length; si++)
      {
        var rotate = v2; v2 = v1; v1 = v0; v0 = rotate; // Rotate and reuse buffered rows of the cost matrix.

        v0[0] = si; // Edit distance is delete (i) chars from source to match empty target.

        var sourceItem = source[si - 1];

        for (int ti = 1; ti <= target.Length; ti++)
        {
          var targetItem = target[ti - 1];

          v0[ti] = System.Math.Min(
            System.Math.Min(
              v1[ti] + 1, // Deletion.
              v0[ti - 1] + 1 // Insertion.
            ),
            System.Math.Min(
              equalityComparer.Equals(sourceItem, targetItem) ? v1[ti - 1] : v1[ti - 1] + 1, // Substitution.
              si > 1 && ti > 1 && equalityComparer.Equals(sourceItem, target[ti - 2]) && equalityComparer.Equals(source[si - 2], targetItem) ? v2[ti - 2] + 1 : int.MaxValue // Transposition.
            )
          );
        }
      }

      return v0[target.Length];
    }

    /// <summary>
    /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
    /// </summary>
    /// <remarks>Implemented based on the Wiki article.</remarks>
    public static double[,] GetCustomOptimalStringAlignmentMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      var ldg = new double[sourceLength + 1, targetLength + 1];

      for (var si = 1; si <= sourceLength; si++)
        ldg[si, 0] = si * costOfInsertion;
      for (var ti = 1; ti <= targetLength; ti++)
        ldg[0, ti] = ti * costOfInsertion;

      for (int si = 1; si <= sourceLength; si++)
      {
        var sourceItem = source[si - 1];

        for (int ti = 1; ti <= targetLength; ti++)
        {
          var targetItem = target[ti - 1];

          ldg[si, ti] = ldg[si, ti] = System.Math.Min(
            System.Math.Min(
              ldg[si - 1, ti] + costOfDeletion,
              ldg[si, ti - 1] + costOfInsertion
            ),
            System.Math.Min(
              equalityComparer.Equals(sourceItem, targetItem) ? ldg[si - 1, ti - 1] : ldg[si - 1, ti - 1] + costOfSubstitution,
              si > 1 && ti > 1 && equalityComparer.Equals(sourceItem, target[ti - 2]) && equalityComparer.Equals(source[si - 2], targetItem) ? ldg[si - 2, ti - 2] + costOfTransposition : double.MaxValue
            )
          );
        }
      }

      return ldg;
    }

    public static double GetCustomOptimalStringAlignmentMetric<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      TrimCommonEnds(source, target, out source, out target, out var _, out var _, equalityComparer);

      if (source.Length == 0) return target.Length;
      else if (target.Length == 0) return source.Length;

      var v2 = new double[target.Length + 1]; // Row of costs, two rows back.
      var v1 = new double[target.Length + 1]; // Row of costs, one row back (previous).
      var v0 = new double[target.Length + 1]; // Row of costs, current row.

      for (int ti = 0; ti <= target.Length; ti++) v0[ti] = ti * costOfInsertion; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

      for (int si = 1; si <= source.Length; si++)
      {
        var rotate = v2; v2 = v1; v1 = v0; v0 = rotate; // Rotate and reuse buffered rows of the cost matrix.

        v0[0] = si; // Edit distance is delete (i) chars from source to match empty target.

        var sourceItem = source[si - 1];

        for (int ti = 1; ti <= target.Length; ti++)
        {
          var targetItem = target[ti - 1];

          v0[ti] = System.Math.Min(
            System.Math.Min(
              v1[ti] + costOfDeletion, // Deletion.
              v0[ti - 1] + costOfInsertion // Insertion.
            ),
            System.Math.Min(
              equalityComparer.Equals(sourceItem, targetItem) ? v1[ti - 1] : v1[ti - 1] + costOfSubstitution, // Substitution.
              si > 1 && ti > 1 && equalityComparer.Equals(sourceItem, target[ti - 2]) && equalityComparer.Equals(source[si - 2], targetItem) ? v2[ti - 2] + costOfTransposition : double.MaxValue // Transposition.
            )
          );
        }
      }

      return v0[target.Length];
    }
  }
}
