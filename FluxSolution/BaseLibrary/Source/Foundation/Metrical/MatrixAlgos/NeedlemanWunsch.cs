namespace Flux.Matrices
{
  /// <summary>
  /// 
  /// </summary>
  /// <see cref="https://en.wikipedia.org/wiki/Needleman%E2%80%93Wunsch_algorithm"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Hirschberg%27s_algorithm"/>
  public class NeedlemanWunsch
  {
    public int InsertionCost { get; init; } = -2;
    public int DeletionCost { get; init; } = -2;
    public System.ValueTuple<int, int> SubstitutionCosts { get; init; } = (+2, -1);

    public int[,] GetFullMatrix<T>(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var equalityComparer = System.Collections.Generic.EqualityComparer<T>.Default;

      var matrix = new int[source.Length + 1, target.Length + 1];

      matrix[0, 0] = 0;

      for (var j = 1; j <= target.Length; j++)
        matrix[0, j] = matrix[0, j - 1] + InsertionCost;

      for (var i = 1; i <= source.Length; i++)
      {
        var x = source[i - 1];

        matrix[i, 0] = matrix[i - 1, 0] + DeletionCost;

        for (var j = 1; j <= target.Length; j++)
        {
          var y = target[j - 1];

          var scoreSub = matrix[i - 1, j - 1] + (equalityComparer.Equals(x, y) ? SubstitutionCosts.Item1 : SubstitutionCosts.Item2);
          var scoreDel = matrix[i - 1, j] + DeletionCost;
          var scoreIns = matrix[i, j - 1] + InsertionCost;

          matrix[i, j] = Maths.Max(scoreSub, scoreDel, scoreIns);
        }
      }

      return matrix;
    }
  }
}
