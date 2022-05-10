namespace Flux.Metrical
{
  public abstract class AMetrical<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public AMetrical(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
    public AMetrical()
      : this(System.Collections.Generic.EqualityComparer<T>.Default)
    { }

    /// <summary>This can be used to backtrack a dynamically programmed matrix.</summary>
    public System.Collections.Generic.List<T> Backtrack(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int sourceIndex, int targetIndex, T placeholder)
    {
      if (sourceIndex == 0 | targetIndex == 0)
        return new System.Collections.Generic.List<T>();

      // If source and target are equal, add item (no edit) and move down the center.
      if (EqualityComparer.Equals(source[sourceIndex - 1], target[targetIndex - 1]))
      {
        var list = Backtrack(matrix, source, target, sourceIndex - 1, targetIndex - 1, placeholder);
        list.Add(source[sourceIndex - 1]);
        return list;
      }

      // If the matrix target cost is less, move to target.
      if (matrix[sourceIndex, targetIndex - 1] < matrix[sourceIndex - 1, targetIndex])
        return Backtrack(matrix, source, target, sourceIndex, targetIndex - 1, placeholder);
      // If the matrix source cost is less, move to source.
      if (matrix[sourceIndex, targetIndex - 1] > matrix[sourceIndex - 1, targetIndex])
        return Backtrack(matrix, source, target, sourceIndex - 1, targetIndex, placeholder);

      // If the matrix has equal costs, add placeholder (an edit) and move down the center.
      var unequal = Backtrack(matrix, source, target, sourceIndex - 1, targetIndex - 1, placeholder);
      unequal.Add(placeholder);
      return unequal;
    }

    /// <summary>This can be used to trim the start and end of a sequence.</summary>
    public void OptimizeEnds(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int sourceCount, out int targetCount, out int atStart, out int atEnd)
    {
      sourceSlice = source;
      targetSlice = target;

      atStart = sourceSlice.CountEqualAtStart(targetSlice, EqualityComparer);

      if (atStart > 0) // If equality exist in the beginning, adjust.
      {
        sourceSlice = sourceSlice[atStart..];
        targetSlice = targetSlice[atStart..];
      }

      atEnd = sourceSlice.CountEqualAtEnd(targetSlice, EqualityComparer);

      if (atEnd > 0) // If equality exist at the end, adjust.
      {
        sourceSlice = sourceSlice[..^atEnd];
        targetSlice = targetSlice[..^atEnd];
      }

      sourceCount = sourceSlice.Length;
      targetCount = targetSlice.Length;
    }
  }
}
