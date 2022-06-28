namespace Flux
{
  /// <summary>The edit distance is a way of quantifying how dissimilar two sets (e.g., words) are to one another by counting the minimum number of operations required to transform one set into the other.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Edit_distance"/>
  /// <remarks>Can be derived from (a.Length + b.Length - 2 * IMetricLength).</remarks>
  public interface IEditDistanceBacktrackable<T>
  {
    /// <summary>The grid method is using a traditional implementation in order to generate the Wagner-Fisher table.</summary>
    /// <param name="source">The source set.</param>
    /// <param name="target">The target set.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Edit_distance"/>
    int[,] GetMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);

    /// <summary>This can be used to backtrack a dynamically programmed matrix.</summary>
    System.Collections.Generic.List<T> Backtrack(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int sourceIndex, int targetIndex, T placeholder, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (sourceIndex == 0 | targetIndex == 0)
        return new System.Collections.Generic.List<T>();

      // If source and target are equal, add item (no edit) and move down the center.
      if (equalityComparer.Equals(source[sourceIndex - 1], target[targetIndex - 1]))
      {
        var list = Backtrack(matrix, source, target, sourceIndex - 1, targetIndex - 1, placeholder, equalityComparer);
        list.Add(source[sourceIndex - 1]);
        return list;
      }

      // If the matrix target cost is less, move to target.
      if (matrix[sourceIndex, targetIndex - 1] < matrix[sourceIndex - 1, targetIndex])
        return Backtrack(matrix, source, target, sourceIndex, targetIndex - 1, placeholder, equalityComparer);
      // If the matrix source cost is less, move to source.
      if (matrix[sourceIndex, targetIndex - 1] > matrix[sourceIndex - 1, targetIndex])
        return Backtrack(matrix, source, target, sourceIndex - 1, targetIndex, placeholder, equalityComparer);

      // If the matrix has equal costs, add placeholder (an edit) and move down the center.
      var unequal = Backtrack(matrix, source, target, sourceIndex - 1, targetIndex - 1, placeholder, equalityComparer);
      unequal.Add(placeholder);
      return unequal;
    }
  }
}
