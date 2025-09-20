namespace Flux
{
  public static class DamerauLevenshteinDistanceCustom
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
      public double DamerauLevenshteinDistanceCustomMetric(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

        if (source.Length == 0) return target.Length;
        else if (target.Length == 0) return source.Length;

        var matrix = DamerauLevenshteinDistanceCustomMatrix(source, target, costOfDeletion, costOfInsertion, costOfSubstitution, costOfTransposition, equalityComparer);

        return matrix[source.Length + 1, target.Length + 1];
      }

      /// <summary>
      /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
      /// </summary>
      /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
      public double[,] DamerauLevenshteinDistanceCustomMatrix(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var ldg = new double[source.Length + 2, target.Length + 2];

        var dr = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // Dictionary of items from both lists.
        for (var si = source.Length - 1; si >= 0; si--)
          if (!dr.ContainsKey(source[si]))
            dr[source[si]] = 0;
        for (var ti = target.Length - 1; ti >= 0; ti--)
          if (!dr.ContainsKey(target[ti]))
            dr[target[ti]] = 0;

        var maxDistance = (source.Length + target.Length) * costOfInsertion;

        ldg[0, 0] = maxDistance;

        for (var i = source.Length + 1; i >= 1; i--)
        {
          ldg[i, 1] = (i - 1) * costOfInsertion;
          ldg[i, 0] = maxDistance;
        }
        for (var j = target.Length + 1; j >= 1; j--)
        {
          ldg[1, j] = (j - 1) * costOfInsertion;
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

            var isEqual = equalityComparer.Equals(sourceItem, targetItem);

            ldg[si + 1, ti + 1] = double.Min(
              double.Min(
                ldg[si, ti + 1] + costOfDeletion,
                ldg[si + 1, ti] + costOfInsertion
              ),
              double.Min(
                isEqual ? ldg[si, ti] : ldg[si, ti] + costOfSubstitution,
                ldg[lsi, ltim] + (si - lsi - 1) + costOfTransposition + (ti - ltim - 1)
              )
            );

            if (isEqual)
              ltim = ti;
          }

          dr[sourceItem] = si;
        }

        return ldg;
      }
    }
  }
}
