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

    public void TraceMatrix(int[,] D, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var i = D.GetLength(0)-1 ;
      var j = D.GetLength(1) -1;

      System.Collections.Generic.List<T> s = new System.Collections.Generic.List<T>();
      System.Collections.Generic.List<T> t = new System.Collections.Generic.List<T>();

      while (i > 1 && j > 1)
      {
        if (D[i, j] - SubstitutionMatrix(source[i - 1], target[j - 1]) == D[i - 1, j - 1])
        {
          t.Add(target[j - 1]);
          s.Add(source[i- 1]);
          i = i - 1;
          j = j - 1;
        }
        else if (D[i, j] - LinearGapPenalty == D[i, j - 1])
        {
          s.Add(source[i - 1]);
          t.Add(default!);
          j = j - 1;
        }
        else if (D[i, j] - LinearGapPenalty == D[i - 1, j])
        {
          s.Add(default!);
          t.Add(target[j - 1]);
          i = i - 1;
        }
        else
          throw new System.Exception("should not happen");
      }
    }
  }
}
