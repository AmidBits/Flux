namespace Flux.Algorithms
{
  /// <summary>A general dynamic programming algorithm for comparing sequences.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Smith%E2%80%93Waterman_algorithm"/>
  public class SmithWatermanAlgorithm<T>
  {
    public System.Collections.Generic.EqualityComparer<T> EqualityComparer { get; init; }

    public int LinearGapPenalty { get; init; }
    public System.Func<T, T, int> SubstitutionMatrix { get; init; }

    public T GapPlaceholder { get; init; } = default!;

    public SmithWatermanAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix, System.Collections.Generic.EqualityComparer<T> equalityComparer)
    {
      EqualityComparer = equalityComparer;
      LinearGapPenalty = linearGapPenalty;
      SubstitutionMatrix = substitutionMatrix;
    }
    public SmithWatermanAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix)
    {
      EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
      LinearGapPenalty = linearGapPenalty;
      SubstitutionMatrix = substitutionMatrix;
    }
    public SmithWatermanAlgorithm()
    {
      EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
      LinearGapPenalty = -1;
      SubstitutionMatrix = (s, t) => EqualityComparer.Equals(s, t) ? 1 : -1;
    }

    public int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var matrix = new int[source.Length + 1, target.Length + 1];

      for (var i = source.Length; i >= 0; i--)
        matrix[i, 0] = i * LinearGapPenalty;
      for (var j = target.Length; j >= 0; j--)
        matrix[0, j] = j * LinearGapPenalty;

      for (var si = 1; si <= source.Length; si++)
      {
        var se = source[si - 1];

        for (var ti = 1; ti <= target.Length; ti++)
        {
          var te = target[ti - 1];

          var scoreSub = matrix[si - 1, ti - 1] + SubstitutionMatrix(se, te);
          var scoreDel = matrix[si - 1, ti] + LinearGapPenalty;
          var scoreIns = matrix[si, ti - 1] + LinearGapPenalty;

          matrix[si, ti] = Maths.Max(scoreSub, scoreDel, scoreIns);
        }
      }

      return matrix;
    }

    public (System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target) TracebackPath(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var si = matrix.GetLength(0) - 1;
      var ti = matrix.GetLength(1) - 1;

      var s = new System.Collections.Generic.List<T>();
      var t = new System.Collections.Generic.List<T>();

      while (si > 0 && ti > 0)
      {
        if (matrix[si, ti] - SubstitutionMatrix(source[si - 1], target[ti - 1]) == matrix[si - 1, ti - 1])
        {
          si -= 1;
          ti -= 1;
          s.Insert(0, target[ti]);
          t.Insert(0, source[si]);
        }
        else if (matrix[si, ti] - LinearGapPenalty == matrix[si, ti - 1])
        {
          ti -= 1;
          s.Insert(0, target[ti]);
          t.Insert(0, GapPlaceholder);
        }
        else if (matrix[si, ti] - LinearGapPenalty == matrix[si - 1, ti])
        {
          si -= 1;
          s.Insert(0, GapPlaceholder);
          t.Insert(0, source[si]);
        }
      }

      while (ti > 0)
      {
        ti -= 1;
        s.Insert(0, target[ti]);
        t.Insert(0, GapPlaceholder);
      }

      while (si > 0)
      {
        si -= 1;
        s.Insert(0, GapPlaceholder);
        t.Insert(0, source[si]);
      }

      return (s, t);
    }
  }
}
