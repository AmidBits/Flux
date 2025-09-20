namespace Flux
{
  public static class DamerauLevenshteinDistance
  {
    extension<T>(System.ReadOnlySpan<T> source)
      where T : notnull
    {
      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      public int DamerauLevenshteinDistanceMetric(System.ReadOnlySpan<T> target, out int[,] matrix, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        matrix = new int[0, 0];

        if (source.Length == 0) return target.Length;
        else if (target.Length == 0) return source.Length;

        matrix = DamerauLevenshteinDistanceMatrix(source, target, equalityComparer);

        return matrix[source.Length + 1, target.Length + 1];
      }

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      public int[,] DamerauLevenshteinDistanceMatrix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var ldg = new int[source.Length + 2, target.Length + 2];

        var dr = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // Dictionary of items from both lists.
        for (var si = source.Length - 1; si >= 0; si--)
          if (!dr.ContainsKey(source[si]))
            dr[source[si]] = 0;
        for (var ti = target.Length - 1; ti >= 0; ti--)
          if (!dr.ContainsKey(target[ti]))
            dr[target[ti]] = 0;

        var maxDistance = source.Length + target.Length;

        ldg[0, 0] = maxDistance;

        for (var si = source.Length + 1; si >= 1; si--)
        {
          ldg[si, 1] = si - 1;
          ldg[si, 0] = maxDistance;
        }
        for (var ti = target.Length + 1; ti >= 1; ti--)
        {
          ldg[1, ti] = ti - 1;
          ldg[0, ti] = maxDistance;
        }

        for (var si = 1; si <= source.Length; si++)
        {
          var ltim = 0; // Last target index of target item matching source item.

          var sourceItem = source[si - 1];

          for (var ti = 1; ti <= target.Length; ti++)
          {
            var targetItem = target[ti - 1];

            var lsi = dr[targetItem]; // Last source index of source item.

            var isEqual = equalityComparer.Equals(sourceItem, targetItem);

            ldg[si + 1, ti + 1] = int.Min(
              int.Min(
                ldg[si, ti + 1] + 1, // Deletion.
                ldg[si + 1, ti] + 1 // Insertion
              ),
              int.Min(
                isEqual ? ldg[si, ti] : ldg[si, ti] + 1, // Substitution.
                ldg[lsi, ltim] + (si - lsi - 1) + 1 + (ti - ltim - 1) // Transposition.
              )
            );

            if (isEqual)
              ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return ldg;
      }

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      public double DamerauLevenshteinDistanceSmc(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => 1d - DamerauLevenshteinDistanceSmd(source, target, equalityComparer);

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      public double DamerauLevenshteinDistanceSmd(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)DamerauLevenshteinDistanceMetric(source, target, out var _, equalityComparer) / (double)int.Max(source.Length, target.Length);
    }
  }
}
