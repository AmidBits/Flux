//namespace Flux.Metrical
//{
//  /// <summary>A general dynamic programming algorithm for comparing sequences.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Smith%E2%80%93Waterman_algorithm"/>
//  public sealed class SmithWatermanAlgorithm<T>
//    where T : System.Numerics.INumber<T>
//  {
//    public int LinearGapPenalty { get; init; }
//    public System.Func<T, T, int> SubstitutionMatrix { get; init; }

//    public T GapPlaceholder { get; init; } = default!;

//    public System.Collections.Generic.EqualityComparer<T> EqualityComparer { get; }

//    public SmithWatermanAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix, System.Collections.Generic.EqualityComparer<T> equalityComparer)
//    {
//      EqualityComparer = equalityComparer;
//      LinearGapPenalty = linearGapPenalty;
//      SubstitutionMatrix = substitutionMatrix;
//    }
//    public SmithWatermanAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix)
//      : this(linearGapPenalty, substitutionMatrix, System.Collections.Generic.EqualityComparer<T>.Default)
//    { }
//    public SmithWatermanAlgorithm(int linearGapPenalty, int substitutionMatrixEqual, int substitutionMatrixUnequal)
//    {
//      EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
//      SubstitutionMatrix = (s, t) => EqualityComparer.Equals(s, t) ? substitutionMatrixEqual : substitutionMatrixUnequal;
//      LinearGapPenalty = linearGapPenalty;
//    }

//    public int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
//    {
//      var matrix = new int[source.Length + 1, target.Length + 1];

//      System.Array.Clear(matrix);

//      for (var si = 1; si <= source.Length; si++)
//      {
//        var se = source[si - 1];

//        for (var ti = 1; ti <= target.Length; ti++)
//        {
//          var te = target[ti - 1];

//          matrix[si, ti] = int.Max(
//            int.Max(
//              matrix[si - 1, ti - 1] + SubstitutionMatrix(se, te), // Match.
//              matrix[si - 1, ti] + LinearGapPenalty // Delete.
//            ),
//            int.Max(
//              matrix[si, ti - 1] + LinearGapPenalty, // Insert.
//              0 // Minimum max cost.
//            )
//          );
//        }
//      }

//      return matrix;
//    }

//    public (System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target) TracebackPath(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
//    {
//      var best = 0;
//      var si = 0;
//      var ti = 0;

//      for (var msi = matrix.GetLength(0) - 1; msi >= 0; msi--)
//        for (var mti = matrix.GetLength(1) - 1; mti >= 0; mti--)
//          if (matrix[msi, mti] > best)
//          {
//            best = matrix[msi, mti];
//            si = msi;
//            ti = mti;
//          }

//      var s = new System.Collections.Generic.List<T>();
//      var t = new System.Collections.Generic.List<T>();

//      while (matrix[si, ti] > 0)
//      {
//        if (matrix[si, ti] - SubstitutionMatrix(source[si - 1], target[ti - 1]) == matrix[si - 1, ti - 1])
//        {
//          s.Insert(0, target[ti -= 1]);
//          t.Insert(0, source[si -= 1]);
//        }
//        else if (matrix[si, ti] - LinearGapPenalty == matrix[si - 1, ti])
//        {
//          s.Insert(0, GapPlaceholder);
//          t.Insert(0, source[si -= 1]);
//        }
//        else if (matrix[si, ti] - LinearGapPenalty == matrix[si, ti - 1])
//        {
//          s.Insert(0, target[ti -= 1]);
//          t.Insert(0, GapPlaceholder);
//        }
//      }

//      return (s, t);
//    }
//  }
//}
