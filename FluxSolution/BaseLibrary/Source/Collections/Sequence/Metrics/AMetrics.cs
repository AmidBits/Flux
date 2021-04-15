namespace Flux.Sequence.Metrics
{
  public abstract class AMetrics<T>
  {
    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

    public AMetrics(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));

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
