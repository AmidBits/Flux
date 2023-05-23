namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static double GetDamerauLevenshteinDistanceDerivedSmc<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
      => 1d - GetDamerauLevenshteinDistanceDerivedSmd(source, target, equalityComparer);
    public static double GetDamerauLevenshteinDistanceDerivedSmd<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
      => (double)GetDamerauLevenshteinDistanceMetric(source, target, equalityComparer) / (double)System.Math.Max(source.Length, target.Length);

    /// <summary>
    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
    /// </summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
    public static int[,] GetDamerauLevenshteinDistanceMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
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

          ldg[si + 1, ti + 1] = System.Math.Min(
            System.Math.Min(
              ldg[si, ti + 1] + 1, // Deletion.
              ldg[si + 1, ti] + 1 // Insertion
            ),
            System.Math.Min(
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
    public static int GetDamerauLevenshteinDistanceMetric<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      TrimCommonEnds(source, target, out source, out target, out var _, out var _, equalityComparer);

      if (source.Length == 0) return target.Length;
      else if (target.Length == 0) return source.Length;

      var matrix = GetDamerauLevenshteinDistanceMatrix(source, target, equalityComparer);

      return matrix[source.Length + 1, target.Length + 1];
    }

    /// <summary>
    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
    /// </summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
    public static double[,] GetCustomDamerauLevenshteinDistanceMatrix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
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

          ldg[si + 1, ti + 1] = System.Math.Min(
            System.Math.Min(
              ldg[si, ti + 1] + costOfDeletion,
              ldg[si + 1, ti] + costOfInsertion
            ),
            System.Math.Min(
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

    /// <summary>
    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
    /// </summary>
    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
    public static double GetCustomDamerauLevenshteinEditDistance<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      TrimCommonEnds(source, target, out source, out target, out var _, out var _, equalityComparer);

      if (source.Length == 0) return target.Length;
      else if (target.Length == 0) return source.Length;

      var matrix = GetCustomDamerauLevenshteinDistanceMatrix(source, target, costOfDeletion, costOfInsertion, costOfSubstitution, costOfTransposition, equalityComparer);

      return matrix[source.Length + 1, target.Length + 1];
    }
  }
}
