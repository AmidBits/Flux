//namespace Flux
//{
//  /// <summary>
//  /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits (insertions, deletions or substitutions) required to change one sequence into the other.</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Levenshtein_distance" /></para>
//  /// </summary>
//  /// <typeparam name="T"></typeparam>
//  public ref struct LevenshteinDistance<T>
//    : IEditDistance<T>
//  {
//    /// <summary>
//    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits (insertions, deletions or substitutions) required to change one sequence into the other.</para>
//    /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
//    /// </summary>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    /// <remarks>
//    /// <para>Implemented based on the Wiki article.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    public readonly int EditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

//      var sn = source.Length;
//      var tn = target.Length;

//      if (sn == 0) return tn;
//      else if (tn == 0) return sn;

//      var v0 = new int[tn + 1]; // Row of costs, one row back (previous row).
//      var v1 = new int[tn + 1]; // Row of costs, current row.

//      for (var j = tn - 1; j >= 0; j--)
//        v0[j] = j; // Initialize the 'previous' row.

//      for (var i = 0; i < sn; i++)
//      {
//        v0[0] = i + 1; // The first element is delete (i + 1) chars from source to match empty target.

//        for (var j = 0; j < tn; j++)
//        {
//          v1[j + 1] = int.Min(
//            v0[j + 1] + 1, // Deletion.
//            int.Min(
//              v1[j] + 1, // Insertion.
//              v0[j] + System.Convert.ToInt32(!equalityComparer.Equals(source[i], target[j])) // Substitution.
//            )
//          );
//        }

//        (v0, v1) = (v1, v0);
//      }

//      return v0[target.Length];

//      #region Optimized version with only one vector and variables for prior costs, not yet tested!
//      //var v = new int[target.Length]; // Current row of costs.

//      //for (var ti = 0; ti < target.Length; ti++)
//      //  v[ti] = ti + 1; // Initialize v1 (the previous row of costs) to an edit distance for an empty source, i.e. the the number of characters to delete from target.

//      //var current = 0;

//      //for (var si = 0; si < source.Length; si++)
//      //{
//      //  current = si;

//      //  for (int ti = 0, left = si; ti < target.Length; ti++)
//      //  {
//      //    var above = current;
//      //    current = left; // cost on diagonal (substitution)
//      //    left = v[ti];

//      //    if (!equalityComparer.Equals(source[si], target[ti]))
//      //    {
//      //      current = int.Min(above + 1, int.Min(left + 1, current + 1));
//      //    }

//      //    v[ti] = current;
//      //  }
//      //}

//      //return v[target.Length - 1];
//      #endregion Optimized version with only one vector and variables for prior costs, not yet tested!

//      #region Another optimized version with one vector and temp variables this time, not yet tested!
//      //var v = new int[target.Length + 1]; // Current row of costs.

//      //for (var ti = 0; ti < target.Length; ti++)
//      //  v[ti] = ti;

//      //for (var si = 0; si < source.Length; si++)
//      //{
//      //  v[0] = si; // initialize with zero cost

//      //  for (int ti = 0, lastDiagonal = si; ti < target.Length; ti++)
//      //  {
//      //    var substitute = lastDiagonal + (equalityComparer.Equals(source[si], target[ti]) ? 0 : 1); // last diagonal cost + possible inequality cost

//      //    lastDiagonal = v[ti + 1]; // remember the cost of the last diagonal stripe

//      //    var insert = v[ti] + 1;
//      //    var delete = lastDiagonal + 1; // reuse the recently cached last diagonal in order to reduce use of indexer

//      //    v[ti + 1] = int.Min(substitute, int.Min(insert, delete));
//      //  }
//      //}

//      //return v[target.Length];
//      #endregion Another optimized version with one vector and temp variables this time, not yet tested!
//    }

//    /// <summary>
//    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
//    /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
//    /// </summary>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    /// <remarks>
//    /// <para>Implemented based on the Wiki article.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    public readonly double Smc(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      => 1d - Smd(source, target, equalityComparer);

//    /// <summary>
//    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
//    /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
//    /// </summary>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    /// <remarks>
//    /// <para>Implemented based on the Wiki article.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    public readonly double Smd(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      => (double)EditDistance(source, target, equalityComparer) / (double)int.Max(source.Length, target.Length);

//#if RESEARCH

//      /// <summary>
//      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
//      /// <see href="https://en.wikipedia.org/wiki/Levenshtein_distance" />
//      /// </summary>
//      /// <param name="target"></param>
//      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//      /// <returns></returns>
//      /// <remarks>
//      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//      /// </remarks>
//      public int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      {
//        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//        var sn = source.Length;
//        var tn = target.Length;

//        var dp = new int[sn + 1, tn + 1];

//        for (var i = 1; i <= sn; i++) dp[i, 0] = i;
//        for (var j = 1; j <= tn; j++) dp[0, j] = j;

//        for (var i = 1; i <= sn; i++)
//          for (var j = 1; j <= tn; j++)
//            dp[i, j] = int.Min(
//              dp[i - 1, j] + 1, // Deletion.
//              int.Min(
//                dp[i, j - 1] + 1, // Insertion.
//                dp[i - 1, j - 1] + System.Convert.ToInt32(!equalityComparer.Equals(source[i - 1], target[j - 1])) // Substitution.
//              )
//            );

//        return dp;
//      }

//#endif
//  }

//  public ref struct LevenshteinDistanceCustom<T>
//    : IEditDistanceCustom<T>
//  {
//    public double CostOfDeletion { get; init; } = 1;
//    public double CostOfInsertion { get; init; } = 1;
//    public double CostOfSubstitution { get; init; } = 1;

//    public LevenshteinDistanceCustom(double costOfDeletion, double costOfInsertion, double costOfSubstitution)
//    {
//      CostOfDeletion = costOfDeletion;
//      CostOfInsertion = costOfInsertion;
//      CostOfSubstitution = costOfSubstitution;
//    }

//    public LevenshteinDistanceCustom() : this(1, 1, 1) { }

//    /// <summary>
//    /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
//    /// <para>This implementation allows use of custom values for deletion, insertion and substitution.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Levenshtein_distance" /></para>
//    /// </summary>
//    /// <remarks>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs two alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    /// <param name="target"></param>
//    /// <param name="costOfDeletion"></param>
//    /// <param name="costOfInsertion"></param>
//    /// <param name="costOfSubstitution"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public double EditDistanceCustom(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

//      var sn = source.Length;
//      var tn = target.Length;

//      if (sn == 0) return tn;
//      else if (tn == 0) return sn;

//      var v0 = new double[tn + 1]; // Row of costs, previous row.
//      var v1 = new double[tn + 1]; // Row of costs, current row.

//      for (var j = tn - 1; j >= 0; j--)
//        v0[j] = j * CostOfInsertion; // Initialize the 'previous' (swapped to 'v1' in loop) row.

//      for (var i = 0; i < sn; i++)
//      {
//        v1[0] = i + CostOfDeletion; // The first element is delete (i + 1) chars from source to match empty target.

//        for (var j = 0; j < tn; j++)
//        {
//          v1[j + 1] = double.Min(
//            v0[j + 1] + CostOfDeletion, // Deletion.
//            double.Min(
//              v1[j] + CostOfInsertion, // Insertion.
//              v1[j] + (equalityComparer.Equals(source[i], target[j]) ? 0 : CostOfSubstitution) // Substitution.
//            )
//          );
//        }

//        (v0, v1) = (v1, v0);
//      }

//      return v0[target.Length];
//    }

//    #region GetMatrix

//#if RESEARCH

//      /// <summary>
//      /// <para>The Levenshtein distance between two sequences is the minimum number of single-element edits(insertions, deletions or substitutions) required to change one sequence into the other.</para>
//      /// <para>This implementation allows use of custom values for deletion, insertion and substitution.</para>
//      /// <para><see href="https://en.wikipedia.org/wiki/Levenshtein_distance" /></para>
//      /// </summary>
//      /// <remarks>
//      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//      /// </remarks>
//      /// <param name="target"></param>
//      /// <param name="costOfDeletion"></param>
//      /// <param name="costOfInsertion"></param>
//      /// <param name="costOfSubstitution"></param>
//      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//      /// <returns></returns>
//      public double[,] GetMatrix(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      {
//        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//        var sn = source.Length;
//        var tn = target.Length;

//        var dp = new double[sn + 1, tn + 1];

//        for (var i = sn - 1; i >= 0; i--)
//          dp[i, 0] = i * costOfInsertion;
//        for (var j = tn - 1; j >= 0; j--)
//          dp[0, j] = j * costOfInsertion;

//        for (var i = 1; i <= sn; i++)
//          for (var j = 1; j <= tn; j++)
//            dp[i, j] = double.Min(
//              dp[i - 1, j] + costOfDeletion,
//              double.Min(
//                dp[i, j - 1] + costOfInsertion,
//                dp[i - 1, j - 1] + (equalityComparer.Equals(source[i - 1], target[j - 1]) ? 0 : costOfSubstitution)
//              )
//            );

//        return dp;
//      }

//#endif

//    #endregion
//  }
//}
