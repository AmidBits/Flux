//namespace Flux.SequenceMetrics
//{
//  public static partial class Helper
//  {
//    public static void OptimizeEnds<T>(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int sourceCount, out int targetCount, out int atStart, out int atEnd)
//    {
//      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      sourceSlice = source;
//      targetSlice = target;

//      atStart = 0;
//      while (sourceSlice.Length > atStart && targetSlice.Length > atStart && comparer.Equals(sourceSlice[atStart], targetSlice[atStart]))
//        atStart++;
//      if (atStart > 0)
//      {
//        sourceSlice = sourceSlice.Slice(atStart);
//        targetSlice = targetSlice.Slice(atStart);
//      }

//      atEnd = 0;
//      for (int sourceEnd = sourceSlice.Length - 1, targetEnd = targetSlice.Length - 1; sourceEnd >= 0 && targetEnd >= 0 && comparer.Equals(sourceSlice[sourceEnd], targetSlice[targetEnd]); sourceEnd--, targetEnd--)
//        atEnd++;
//      if (atEnd > 0)
//      {
//        sourceSlice = sourceSlice.Slice(0, sourceSlice.Length - atEnd);
//        targetSlice = targetSlice.Slice(0, targetSlice.Length - atEnd);
//      }

//      sourceCount = sourceSlice.Length;
//      targetCount = targetSlice.Length;
//    }
//  }
//}
