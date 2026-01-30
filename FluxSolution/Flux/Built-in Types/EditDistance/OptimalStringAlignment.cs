//namespace Flux
//{
//  public ref struct OptimalStringAlignment<T>
//    : IEditDistance<T>
//  {
//    /// <summary>
//    /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
//    /// </summary>
//    /// <remarks>
//    /// <para>Implemented based on the Wiki article.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public readonly int EditDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

//      var sn = source.Length;
//      var tn = target.Length;

//      if (sn == 0) return tn;
//      else if (tn == 0) return sn;

//      var v2 = new int[tn + 1]; // Row of costs, two rows back.
//      var v1 = new int[tn + 1]; // Row of costs, one row back (previous).
//      var v0 = new int[tn + 1]; // Row of costs, current row.

//      for (int ti = 0; ti <= tn; ti++)
//        v0[ti] = ti; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

//      for (int si = 1; si <= sn; si++)
//      {
//        (v0, v1, v2) = (v2, v0, v1); // Rotate and reuse buffered rows of the cost matrix.

//        v0[0] = si; // Edit distance is delete (i) chars from source to match empty target.

//        var sourceItem = source[si - 1];

//        for (int ti = 1; ti <= tn; ti++)
//        {
//          var targetItem = target[ti - 1];

//          v0[ti] = int.Min(
//            int.Min(
//              v1[ti] + 1, // Deletion.
//              v0[ti - 1] + 1 // Insertion.
//            ),
//            int.Min(
//              v1[ti - 1] + System.Convert.ToInt32(!equalityComparer.Equals(sourceItem, targetItem)), // Substitution.
//              si > 1 && ti > 1 && equalityComparer.Equals(sourceItem, target[ti - 2]) && equalityComparer.Equals(source[si - 2], targetItem) ? v2[ti - 2] + 1 : int.MaxValue // Transposition.
//            )
//          );
//        }
//      }

//      return v0[tn];
//    }

//    /// <summary>
//    /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
//    /// </summary>
//    /// <remarks>
//    /// <para>Implemented based on the Wiki article.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public readonly double Smc(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      => 1d - Smd(source, target, equalityComparer);

//    /// <summary>
//    /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
//    /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
//    /// </summary>
//    /// <remarks>
//    /// <para>Implemented based on the Wiki article.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    /// <param name="target"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public readonly double Smd(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      => (double)EditDistance(source, target, equalityComparer) / (double)int.Max(source.Length, target.Length);

//    #region GetMatrix

//#if RESEARCH

//      /// <summary>
//      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
//      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
//      /// </summary>
//      /// <remarks>Implemented based on the Wiki article.</remarks>
//      /// <param name="target"></param>
//      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//      /// <returns></returns>
//      public int[,] GetMatrix(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//      {
//        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//        var sn = source.Length;
//        var tn = target.Length;

//        var dp = new int[sn + 1, tn + 1];

//        for (var i = sn - 1; i >= 0; i--)
//          dp[i, 0] = i;
//        for (var j = tn - 1; j >= 0; j--)
//          dp[0, j] = j;

//        for (var i = 1; i <= sn; i++)
//        {
//          var sourceItem = source[i - 1];

//          for (var j = 1; j <= tn; j++)
//          {
//            var targetItem = target[j - 1];

//            dp[i, j] = int.Min(
//              int.Min(
//                dp[i - 1, j] + 1, // Deletion.
//                dp[i, j - 1] + 1 // Insertion.
//              ),
//              int.Min(
//                dp[i - 1, j - 1] + System.Convert.ToInt32(!equalityComparer.Equals(sourceItem, targetItem)), // Substitution.
//                i > 1 && j > 1 && equalityComparer.Equals(sourceItem, target[j - 2]) && equalityComparer.Equals(source[i - 2], targetItem) ? dp[i - 2, j - 2] + 1 : int.MaxValue // Transposition.
//              )
//            );
//          }
//        }

//        return dp;
//      }

//#endif

//    #endregion
//  }

//  public ref struct OptimalStringAlignmentCustom<T>
//    : IEditDistanceCustom<T>
//  {
//    public double CostOfDeletion { get; init; } = 1;
//    public double CostOfInsertion { get; init; } = 1;
//    public double CostOfSubstitution { get; init; } = 1;
//    public double CostOfTransposition { get; init; } = 1;

//    public OptimalStringAlignmentCustom(double costOfDeletion, double costOfInsertion, double costOfSubstitution, double costOfTransposition)
//    {
//      CostOfDeletion = costOfDeletion;
//      CostOfInsertion = costOfInsertion;
//      CostOfSubstitution = costOfSubstitution;
//      CostOfTransposition = costOfTransposition;
//    }

//    public OptimalStringAlignmentCustom() : this(1, 1, 1, 1) { }

//    /// <summary>
//    /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
//    /// </summary>
//    /// <remarks>
//    /// <para>Implemented based on the Wiki article.</para>
//    /// <para>This Levenshtein algorithm does not rely on a complete matrix. It only needs three alternating horizontal rows throughout the process.</para>
//    /// </remarks>
//    /// <param name="target"></param>
//    /// <param name="costOfDeletion"></param>
//    /// <param name="costOfInsertion"></param>
//    /// <param name="costOfSubstitution"></param>
//    /// <param name="costOfTransposition"></param>
//    /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//    /// <returns></returns>
//    public readonly double EditDistanceCustom(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      source.TrimCommonEnds(target, out source, out target, out var _, out var _, equalityComparer);

//      var sn = source.Length;
//      var tn = target.Length;

//      if (sn == 0) return tn;
//      else if (tn == 0) return sn;

//      var v2 = new double[tn + 1]; // Row of costs, two rows back.
//      var v1 = new double[tn + 1]; // Row of costs, one row back (previous).
//      var v0 = new double[tn + 1]; // Row of costs, current row.

//      for (var j = tn - 1; j >= 0; j--)
//        v0[j] = j * CostOfInsertion; // Initialize v1 (the previous row of costs) to an edit distance for empty source items, i.e. the the number of characters to delete from target.

//      for (var i = 1; i <= sn; i++)
//      {
//        (v0, v1, v2) = (v2, v0, v1); // Rotate and reuse buffered rows of the cost matrix.

//        v0[0] = i; // Edit distance is delete (i) chars from source to match empty target.

//        var sourceItem = source[i - 1];

//        for (var j = 1; j <= tn; j++)
//        {
//          var targetItem = target[j - 1];

//          v0[j] = double.Min(
//            double.Min(
//              v1[j] + CostOfDeletion, // Deletion.
//              v0[j - 1] + CostOfInsertion // Insertion.
//            ),
//            double.Min(
//              v1[j - 1] + (equalityComparer.Equals(sourceItem, targetItem) ? 0 : CostOfSubstitution), // Substitution.
//              i > 1 && j > 1 && equalityComparer.Equals(sourceItem, target[j - 2]) && equalityComparer.Equals(source[i - 2], targetItem) ? v2[j - 2] + CostOfTransposition : double.MaxValue // Transposition.
//            )
//          );
//        }
//      }

//      return v0[tn];
//    }

//    #region GetMatrix

//#if RESEARCH

//      /// <summary>
//      /// <para>Computes the optimal sequence alignment (OSA) using the specified comparer. OSA is basically an edit distance algorithm somewhere between Levenshtein and Damerau-Levenshtein, and is also referred to as 'restricted edit distance'.</para>
//      /// <para>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</para>
//      /// <para><seealso href="https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance"/></para>
//      /// <para><seealso href="https://en.wikipedia.org/wiki/Edit_distance"/></para>
//      /// </summary>
//      /// <remarks>Implemented based on the Wiki article.</remarks>
//      /// <param name="target"></param>
//      /// <param name="costOfDeletion"></param>
//      /// <param name="costOfInsertion"></param>
//      /// <param name="costOfSubstitution"></param>
//      /// <param name="costOfTransposition"></param>
//      /// <param name="equalityComparer">If null, then <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> is used.</param>
//      /// <returns></returns>
//      public double[,] GetMatrixCustom(System.ReadOnlySpan<T> target, double costOfDeletion = 1, double costOfInsertion = 1, double costOfSubstitution = 1, double costOfTransposition = 1, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
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
//        {
//          var sourceItem = source[i - 1];

//          for (var j = 1; j <= tn; j++)
//          {
//            var targetItem = target[j - 1];

//            dp[i, j] = double.Min(
//              double.Min(
//                dp[i - 1, j] + costOfDeletion,
//                dp[i, j - 1] + costOfInsertion
//              ),
//              double.Min(
//                dp[i - 1, j - 1] + (equalityComparer.Equals(sourceItem, targetItem) ? 0 : costOfSubstitution),
//                i > 1 && j > 1 && equalityComparer.Equals(sourceItem, target[j - 2]) && equalityComparer.Equals(source[i - 2], targetItem) ? dp[i - 2, j - 2] + costOfTransposition : double.MaxValue
//              )
//            );
//          }
//        }

//        return dp;
//      }

//#endif

//    #endregion
//  }
//}
