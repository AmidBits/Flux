namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
      /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
      /// </summary>
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// </remarks>
      public double[,] LevenshteinDistanceCustomMatrix(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var ldg = new double[source.Length + 1, target.Length + 1];

        for (var si = 1; si <= source.Length; si++)
          ldg[si, 0] = si * costOfInsertion;
        for (var ti = 1; ti <= target.Length; ti++)
          ldg[0, ti] = ti * costOfInsertion;

        for (var si = 1; si <= source.Length; si++)
          for (var ti = 1; ti <= target.Length; ti++)
            ldg[si, ti] = double.Min(
              ldg[si - 1, ti] + costOfDeletion,
              double.Min(
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
      /// <remarks>
      /// <para>Implemented based on the Wiki article.</para>
      /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
      /// </remarks>
      public double LevenshteinDistanceCustomMetric(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

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
            v0[j + 1] = double.Min(
              v1[j + 1] + costOfDeletion, // Deletion.
              double.Min(
                v0[j] + costOfInsertion, // Insertion.
                equalityComparer.Equals(source[i], target[j]) ? v1[j] : v1[j] + costOfSubstitution // Substitution.
              )
            );
          }
        }

        return v0[target.Length];
      }
    }
  }
}
