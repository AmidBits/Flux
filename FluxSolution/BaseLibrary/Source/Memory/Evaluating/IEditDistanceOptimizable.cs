//namespace Flux
//{
//  public interface IEditDistanceOptimizable<T>
//  {
//    System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

//    void OptimizeEnds(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int sourceCount, out int targetCount, out int atStart, out int atEnd)
//    {
//      sourceSlice = source;
//      targetSlice = target;

//      atStart = sourceSlice.CountEqualAtStart(targetSlice, EqualityComparer);

//      if (atStart > 0) // If equality exist in the beginning, adjust.
//      {
//        sourceSlice = sourceSlice[atStart..];
//        targetSlice = targetSlice[atStart..];
//      }

//      atEnd = sourceSlice.CountEqualAtEnd(targetSlice, EqualityComparer);

//      if (atEnd > 0) // If equality exist at the end, adjust.
//      {
//        sourceSlice = sourceSlice[..^atEnd];
//        targetSlice = targetSlice[..^atEnd];
//      }

//      sourceCount = sourceSlice.Length;
//      targetCount = targetSlice.Length;
//    }

//    static void OptimizeEndsEx(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int sourceCount, out int targetCount, out int atStart, out int atEnd, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
//    {
//      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//      sourceSlice = source;
//      targetSlice = target;

//      atStart = sourceSlice.CountEqualAtStart(targetSlice, equalityComparer);

//      if (atStart > 0) // If equality exist in the beginning, adjust.
//      {
//        sourceSlice = sourceSlice[atStart..];
//        targetSlice = targetSlice[atStart..];
//      }

//      atEnd = sourceSlice.CountEqualAtEnd(targetSlice, equalityComparer);

//      if (atEnd > 0) // If equality exist at the end, adjust.
//      {
//        sourceSlice = sourceSlice[..^atEnd];
//        targetSlice = targetSlice[..^atEnd];
//      }

//      sourceCount = sourceSlice.Length;
//      targetCount = targetSlice.Length;
//    }
//  }
//}
