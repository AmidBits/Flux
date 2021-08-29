namespace Flux.Metrical
{
  public abstract class AMetrical<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public AMetrical(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));

    /// <summary>This can be used to backtrack a dynamically programmed matrix.</summary>
    public System.Collections.Generic.List<T> Backtrack(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int x, int y)
    {
      if (x == 0 | y == 0)
        return new System.Collections.Generic.List<T>();
      if (EqualityComparer.Equals(source[x - 1], target[y - 1]))
      {
        var list = Backtrack(matrix, source, target, x - 1, y - 1);
        list.Add(source[x - 1]);
        return list;
      }
      if (matrix[x, y - 1] > matrix[x - 1, y])
        return Backtrack(matrix, source, target, x, y - 1);
      return Backtrack(matrix, source, target, x - 1, y);
    }

    /// <summary>This can be used to trim the start and end of a sequence.</summary>
    public void OptimizeEnds(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int sourceCount, out int targetCount, out int atStart, out int atEnd)
    {
      sourceSlice = source;
      targetSlice = target;

      atStart = sourceSlice.CountEqualAtStart(targetSlice, out var _);

      if (atStart > 0) // If equality exist in the beginning, adjust.
      {
        sourceSlice = sourceSlice.Slice(atStart);
        targetSlice = targetSlice.Slice(atStart);
      }

      atEnd = sourceSlice.CountEqualAtEnd(targetSlice, out var _);

      if (atEnd > 0) // If equality exist at the end, adjust.
      {
        sourceSlice = sourceSlice.Slice(0, sourceSlice.Length - atEnd);
        targetSlice = targetSlice.Slice(0, targetSlice.Length - atEnd);
      }

      sourceCount = sourceSlice.Length;
      targetCount = targetSlice.Length;
    }
  }
}
