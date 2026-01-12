//namespace Flux
//{
//  public ref struct DamerauLevenshteinDistance<T>
//    : IEditDistance<T>
//    where T : notnull
//  {
//    /// <summary>
//    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
//    /// </summary>
//    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
//    /// <param name="target"></param>
//    /// <param name="matrix"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public int EditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

//      if (source.Length == 0) return target.Length;
//      else if (target.Length == 0) return source.Length;

//      var matrix = GetMatrix(source, target, equalityComparer);

//      return matrix[source.Length + 1, target.Length + 1];
//    }

//    /// <summary>
//    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
//    /// </summary>
//    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public double Smc(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      => 1d - Smd(source, target, equalityComparer);

//    /// <summary>
//    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
//    /// </summary>
//    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public double Smd(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      => (double)EditDistance(source, target, equalityComparer) / (double)int.Max(source.Length, target.Length);


//    /// <summary>
//    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
//    /// </summary>
//    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      var dp = new int[source.Length + 2, target.Length + 2];

//      var dr = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // Dictionary of items from both lists.

//      for (var si = source.Length - 1; si >= 0; si--)
//        if (!dr.ContainsKey(source[si]))
//          dr[source[si]] = 0;
//      for (var ti = target.Length - 1; ti >= 0; ti--)
//        if (!dr.ContainsKey(target[ti]))
//          dr[target[ti]] = 0;

//      var maxDistance = source.Length + target.Length;

//      dp[0, 0] = maxDistance;

//      for (var si = source.Length + 1; si >= 1; si--)
//      {
//        dp[si, 1] = si - 1;
//        dp[si, 0] = maxDistance;
//      }
//      for (var ti = target.Length + 1; ti >= 1; ti--)
//      {
//        dp[1, ti] = ti - 1;
//        dp[0, ti] = maxDistance;
//      }

//      for (var si = 1; si <= source.Length; si++)
//      {
//        var ltim = 0; // Last target index of target item matching source item.

//        var sourceItem = source[si - 1];

//        for (var ti = 1; ti <= target.Length; ti++)
//        {
//          var targetItem = target[ti - 1];

//          var lsi = dr[targetItem]; // Last source index of source item.

//          var isEqual = equalityComparer.Equals(sourceItem, targetItem);

//          dp[si + 1, ti + 1] = int.Min(
//            int.Min(
//              dp[si, ti + 1] + 1, // Deletion.
//              dp[si + 1, ti] + 1 // Insertion
//            ),
//            int.Min(
//              isEqual ? dp[si, ti] : dp[si, ti] + 1, // Substitution.
//              dp[lsi, ltim] + (si - lsi - 1) + 1 + (ti - ltim - 1) // Transposition.
//            )
//          );

//          if (isEqual)
//            ltim = ti;
//        }

//        dr[sourceItem] = si;
//      }

//      return dp;
//    }
//  }

//  public ref struct DamerauLevenshteinDistanceCustom<T>
//    : IEditDistanceCustom<T>
//    where T : notnull
//  {
//    public double CostOfDeletion { get; init; } = 1;
//    public double CostOfInsertion { get; init; } = 1;
//    public double CostOfSubstitution { get; init; } = 1;
//    public double CostOfTransposition { get; init; } = 1;

//    public DamerauLevenshteinDistanceCustom(double costOfDeletion, double costOfInsertion, double costOfSubstitution, double costOfTransposition)
//    {
//      CostOfDeletion = costOfDeletion;
//      CostOfInsertion = costOfInsertion;
//      CostOfSubstitution = costOfSubstitution;
//      CostOfTransposition = costOfTransposition;
//    }

//    public DamerauLevenshteinDistanceCustom() : this(1, 1, 1, 1) { }

//    /// <summary>
//    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
//    /// </summary>
//    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
//    /// <param name="target"></param>
//    /// <param name="costOfDeletion"></param>
//    /// <param name="costOfInsertion"></param>
//    /// <param name="costOfSubstitution"></param>
//    /// <param name="costOfTransposition"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public double EditDistanceCustom(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

//      if (source.Length == 0) return target.Length;
//      else if (target.Length == 0) return source.Length;

//      var matrix = GetMatrixCustom(source, target, equalityComparer);

//      return matrix[source.Length + 1, target.Length + 1];
//    }

//    /// <summary>
//    /// <para>Computes the true Damerau–Levenshtein distance with adjacent transpositions, between two sequences.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Triangle_inequality"/></para>
//    /// </summary>
//    /// <remarks>Takes into account: insertions, deletions, substitutions, or transpositions, using a dictionary. Implemented based on the Wiki article.</remarks>
//    /// <param name="target"></param>
//    /// <param name="costOfDeletion"></param>
//    /// <param name="costOfInsertion"></param>
//    /// <param name="costOfSubstitution"></param>
//    /// <param name="costOfTransposition"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public double[,] GetMatrixCustom(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      var ldg = new double[source.Length + 2, target.Length + 2];

//      var dr = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // Dictionary of items from both lists.
//      for (var si = source.Length - 1; si >= 0; si--)
//        if (!dr.ContainsKey(source[si]))
//          dr[source[si]] = 0;
//      for (var ti = target.Length - 1; ti >= 0; ti--)
//        if (!dr.ContainsKey(target[ti]))
//          dr[target[ti]] = 0;

//      var maxDistance = (source.Length + target.Length) * CostOfInsertion;

//      ldg[0, 0] = maxDistance;

//      for (var i = source.Length + 1; i >= 1; i--)
//      {
//        ldg[i, 1] = (i - 1) * CostOfInsertion;
//        ldg[i, 0] = maxDistance;
//      }
//      for (var j = target.Length + 1; j >= 1; j--)
//      {
//        ldg[1, j] = (j - 1) * CostOfInsertion;
//        ldg[0, j] = maxDistance;
//      }

//      for (var si = 1; si <= source.Length; si++)
//      {
//        var ltim = 0; // Last target index of target item matching source item.

//        var sourceItem = source[si - 1];

//        for (var ti = 1; ti <= target.Length; ti++)
//        {
//          var targetItem = target[ti - 1];

//          var lsi = dr[targetItem]; // Last source index of source item.

//          var isEqual = equalityComparer.Equals(sourceItem, targetItem);

//          ldg[si + 1, ti + 1] = double.Min(
//            double.Min(
//              ldg[si, ti + 1] + CostOfDeletion,
//              ldg[si + 1, ti] + CostOfInsertion
//            ),
//            double.Min(
//              isEqual ? ldg[si, ti] : ldg[si, ti] + CostOfSubstitution,
//              ldg[lsi, ltim] + (si - lsi - 1) + CostOfTransposition + (ti - ltim - 1)
//            )
//          );

//          if (isEqual)
//            ltim = ti;
//        }

//        dr[sourceItem] = si;
//      }

//      return ldg;
//    }
//  }
//}
