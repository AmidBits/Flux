namespace Flux.Algorithms
{
  /// <summary>
  /// 
  /// </summary>
  /// <see cref="https://en.wikipedia.org/wiki/Needleman%E2%80%93Wunsch_algorithm"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Hirschberg%27s_algorithm"/>
  /// <seealso cref="http://www.biorecipes.com/DynProgBasic/code.html"/>
  public class NeedlemanWunschAlgorithm<T>
  {
    public System.Collections.Generic.EqualityComparer<T> EqualityComparer { get; init; }

    public int LinearGapPenalty { get; init; }
    public System.Func<T, T, int> SubstitutionMatrix { get; init; }

    public T GapPlaceholder { get; init; } = default!;

    public NeedlemanWunschAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix, System.Collections.Generic.EqualityComparer<T> equalityComparer)
    {
      EqualityComparer = equalityComparer;
      LinearGapPenalty = linearGapPenalty;
      SubstitutionMatrix = substitutionMatrix;
    }
    public NeedlemanWunschAlgorithm(int linearGapPenalty, System.Func<T, T, int> substitutionMatrix)
    {
      EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
      LinearGapPenalty = linearGapPenalty;
      SubstitutionMatrix = substitutionMatrix;
    }
    public NeedlemanWunschAlgorithm()
    {
      EqualityComparer = System.Collections.Generic.EqualityComparer<T>.Default;
      LinearGapPenalty = -1;
      SubstitutionMatrix = (s, t) => EqualityComparer.Equals(s, t) ? 1 : -1;
    }

    public int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {

      var matrix = new int[source.Length + 1, target.Length + 1];

      matrix[0, 0] = 0;

      for (var j = 1; j <= target.Length; j++)
        matrix[0, j] = matrix[0, j - 1] + LinearGapPenalty;

      for (var i = 1; i <= source.Length; i++)
      {
        var x = source[i - 1];

        matrix[i, 0] = matrix[i - 1, 0] + LinearGapPenalty;

        for (var j = 1; j <= target.Length; j++)
        {
          var y = target[j - 1];

          var scoreSub = matrix[i - 1, j - 1] + SubstitutionMatrix(x, y);// (equalityComparer.Equals(x, y) ? SubstitutionCosts.Item1 : SubstitutionCosts.Item2);
          var scoreDel = matrix[i - 1, j] + LinearGapPenalty;
          var scoreIns = matrix[i, j - 1] + LinearGapPenalty;

          matrix[i, j] = Maths.Max(scoreSub, scoreDel, scoreIns);
        }
      }

      return matrix;
    }

    public (System.Collections.Generic.List<T> source, System.Collections.Generic.List<T> target) TracebackPath(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var i = matrix.GetLength(0) - 1;
      var j = matrix.GetLength(1) - 1;

      var s = new System.Collections.Generic.List<T>();
      var t = new System.Collections.Generic.List<T>();

      while (i > 0 && j > 0)
      {
        if (matrix[i, j] == matrix[i - 1, j - 1] + SubstitutionMatrix(source[i - 1], target[j - 1]))
        {
          i -= 1;
          j -= 1;
          s.Insert(0, target[j]);
          t.Insert(0, source[i]);
        }
        else if (matrix[i, j] == matrix[i - 1, j] + LinearGapPenalty)
        {
          i -= 1;
          s.Insert(0, GapPlaceholder);
          t.Insert(0, source[i]);
        }
        else if (matrix[i, j] == matrix[i, j - 1] + LinearGapPenalty)
        {
          j -= 1;
          s.Insert(0, target[j]);
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
