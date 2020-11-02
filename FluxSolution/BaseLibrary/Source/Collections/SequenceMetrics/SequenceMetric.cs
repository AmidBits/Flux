namespace Flux
{
  namespace SequenceMetrics
  {
    public abstract class SequenceMetric<T>
    {
      public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

      public SequenceMetric(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
        => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));

      public void OptimizeEnds(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int sourceCount, out int targetCount, out int atStart, out int atEnd)
      {
        sourceSlice = source;
        targetSlice = target;

        atStart = sourceSlice.CountEqualAtStart(targetSlice);
        //while (sourceSlice.Length > atStart && targetSlice.Length > atStart && EqualityComparer.Equals(sourceSlice[atStart], targetSlice[atStart]))
        //  atStart++;
        if (atStart > 0)
        {
          sourceSlice = sourceSlice.Slice(atStart);
          targetSlice = targetSlice.Slice(atStart);
        }

        atEnd = sourceSlice.CountEqualAtEnd(targetSlice);
        //for (int sourceEnd = sourceSlice.Length - 1, targetEnd = targetSlice.Length - 1; sourceEnd >= 0 && targetEnd >= 0 && EqualityComparer.Equals(sourceSlice[sourceEnd], targetSlice[targetEnd]); sourceEnd--, targetEnd--)
        //  atEnd++;
        if (atEnd > 0)
        {
          sourceSlice = sourceSlice.Slice(0, sourceSlice.Length - atEnd);
          targetSlice = targetSlice.Slice(0, targetSlice.Length - atEnd);
        }

        sourceCount = sourceSlice.Length;
        targetCount = targetSlice.Length;
      }
    }
  }
}
