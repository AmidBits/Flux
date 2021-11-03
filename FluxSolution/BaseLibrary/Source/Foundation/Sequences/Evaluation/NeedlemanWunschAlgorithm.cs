namespace Flux.Metrical
{
  /// <summary>A general dynamic programming algorithm for comparing sequences.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Needleman%E2%80%93Wunsch_algorithm"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Hirschberg%27s_algorithm"/>
  /// <seealso cref="http://www.biorecipes.com/DynProgBasic/code.html"/>
  public class NeedlemanWunschAlgorithm<T>
    : IMatrixDp<T>
  {
    public int LinearGapPenalty { get; init; }
    public System.Func<T, T, int> SubstitutionMatrix { get; init; }

    public T GapPlaceholder { get; init; } = default!;

    public System.Collections.Generic.EqualityComparer<T> EqualityComparer { get; init; }

    public NeedlemanWunschAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix, System.Collections.Generic.EqualityComparer<T> equalityComparer)
    {
      LinearGapPenalty = linearGapPenalty;
      SubstitutionMatrix = substitutionMatrix;
      EqualityComparer = equalityComparer;
    }
    public NeedlemanWunschAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix)
      : this(linearGapPenalty, substitutionMatrix, System.Collections.Generic.EqualityComparer<T>.Default)
    { }
    public NeedlemanWunschAlgorithm()
    {
      EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
      SubstitutionMatrix = (s, t) => EqualityComparer.Equals(s, t) ? 1 : -1;
      LinearGapPenalty = -1;
    }

    public int[,] GetDpMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var matrix = new int[source.Length + 1, target.Length + 1];

      matrix[0, 0] = 0;

      for (var ti = 1; ti <= target.Length; ti++)
        matrix[0, ti] = matrix[0, ti - 1] + LinearGapPenalty;

      for (var si = 1; si <= source.Length; si++)
      {
        var se = source[si - 1];

        matrix[si, 0] = matrix[si - 1, 0] + LinearGapPenalty;

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
        if (matrix[si, ti] == matrix[si - 1, ti - 1] + SubstitutionMatrix(source[si - 1], target[ti - 1]))
        {
          si -= 1;
          ti -= 1;
          s.Insert(0, target[ti]);
          t.Insert(0, source[si]);
        }
        else if (matrix[si, ti] == matrix[si - 1, ti] + LinearGapPenalty)
        {
          si -= 1;
          s.Insert(0, GapPlaceholder);
          t.Insert(0, source[si]);
        }
        else if (matrix[si, ti] == matrix[si, ti - 1] + LinearGapPenalty)
        {
          ti -= 1;
          s.Insert(0, target[ti]);
          t.Insert(0, GapPlaceholder);
        }
      }

      return (s, t);
    }
  }
}

/*
  var sm = new int[,]
  {
    { 2, -1, 1, -1 },
    { -1, 2, -1, 1 },
    { 1, -1, 2, -1 },
    { -1, 1, -1, 2 },
  };

  static int LetterToInteger(char letter)
    => letter == 'A' ? 0 : letter == 'C' ? 1 : letter == 'G' ? 2 : letter == 'T' ? 3 : throw new System.ArgumentOutOfRangeException(nameof(letter));

  var sdp = new Flux.Matrices.NeedlemanWunschAlgorithm<char>() { GapPlaceholder = '-' };

  var y = "GCATGCU";
  var x = "GATTACA";

  var z = sdp.GetFullMatrix(x, y);
  System.Console.WriteLine(z.ToConsoleBlock());
  System.Console.WriteLine();

  var a = sdp.TracebackPath(z, x, y);
  System.Console.WriteLine(string.Concat(a.source));
  System.Console.WriteLine();
  System.Console.WriteLine(string.Concat(a.target));
  System.Console.WriteLine();
*/
