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
    public System.Collections.Generic.List<T> Backtrack(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int sourceIndex, int targetIndex)
    {
      if (sourceIndex == 0 | targetIndex == 0)
        return new System.Collections.Generic.List<T>();
      if (EqualityComparer.Equals(source[sourceIndex - 1], target[targetIndex - 1]))
      {
        var list = Backtrack(matrix, source, target, sourceIndex - 1, targetIndex - 1);
        list.Add(source[sourceIndex - 1]);
        return list;
      }
      if (matrix[sourceIndex, targetIndex - 1] > matrix[sourceIndex - 1, targetIndex])
        return Backtrack(matrix, source, target, sourceIndex, targetIndex - 1);
      return Backtrack(matrix, source, target, sourceIndex - 1, targetIndex);
    }

    /// <summary>This can be used to trim the start and end of a sequence.</summary>
    public void OptimizeEnds(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int sourceCount, out int targetCount, out int atStart, out int atEnd)
    {
      sourceSlice = source;
      targetSlice = target;

      atStart = sourceSlice.CountEqualAtStart(targetSlice, out var _);

      if (atStart > 0) // If equality exist in the beginning, adjust.
      {
        sourceSlice = sourceSlice[atStart..];
        targetSlice = targetSlice[atStart..];
      }

      atEnd = sourceSlice.CountEqualAtEnd(targetSlice, out var _);

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
