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
    public System.Collections.Generic.List<T> Backtrack(int[,] matrix, System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int si, int ti)
    {
      if (si == 0 | ti == 0)
        return new System.Collections.Generic.List<T>();
      if (EqualityComparer.Equals(source[si - 1], target[ti - 1]))
      {
        var list = Backtrack(matrix, source, target, si - 1, ti - 1);
        list.Add(source[si - 1]);
        return list;
      }
      if (matrix[si, ti - 1] > matrix[si - 1, ti])
        return Backtrack(matrix, source, target, si, ti - 1);
      return Backtrack(matrix, source, target, si - 1, ti);
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
