namespace Flux.Matrices
{
  /// <summary>
  /// 
  /// </summary>
  /// <see cref="https://en.wikipedia.org/wiki/Smith%E2%80%93Waterman_algorithm"/>
  public class StandardDynamicProgramming<T>
  {
    public int LinearGapPenalty { get; init; } = 1;
    public System.Func<T, T, int> SubstitutionMatrix { get; init; } = (s, t) => 1;

    public int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var equalityComparer = System.Collections.Generic.EqualityComparer<T>.Default;

      var matrix = new int[source.Length + 1, target.Length + 1];

      for (var i = source.Length; i >= 0; i--)
        matrix[i, 0] = i * LinearGapPenalty;
      for (var j = target.Length; j >= 0; j--)
        matrix[0, j] = j * LinearGapPenalty;

      System.Console.WriteLine(matrix.ToConsoleBlock());

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

      System.Console.WriteLine(matrix.ToConsoleBlock());

      return matrix;
    }

    public void TraceBackPath(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var i = matrix.GetLength(0) - 1;
      var j = matrix.GetLength(1) - 1;

      System.Collections.Generic.List<T> s = new System.Collections.Generic.List<T>();
      System.Collections.Generic.List<T> t = new System.Collections.Generic.List<T>();

      while (i > 0 && j > 0)
      {
        if (matrix[i, j] - SubstitutionMatrix(source[i - 1], target[j - 1]) == matrix[i - 1, j - 1])
        {
          i -= 1;
          j -= 1;
          s.Insert(0, source[i]);
          t.Insert(0, target[j]);
        }
        else if (matrix[i, j] - LinearGapPenalty == matrix[i, j - 1])
        {
          j -= 1;
          s.Insert(0, target[j]);
          t.Insert(0, default!);
        }
        else if (matrix[i, j] - LinearGapPenalty == matrix[i - 1, j])
        {
          i -= 1;
          s.Insert(0, default!);
          t.Insert(0, source[i]);
        }
      }

      while (j > 0)
      {
        j -= 1;
        s.Insert(0, target[j]);
        t.Insert(0, default!);
      }

      while (i > 0)
      {
        i -= 1;
        s.Insert(0, default!);
        t.Insert(0, source[i]);
      }

    }
  }
}
