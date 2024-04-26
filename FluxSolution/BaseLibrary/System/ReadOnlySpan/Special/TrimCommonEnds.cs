namespace Flux
{
  public static partial class Fx
  {
    private static void TrimCommonEnds<T>(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int atStart, out int atEnd, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      sourceSlice = source;
      targetSlice = target;

      atStart = sourceSlice.CommonPrefixLength(targetSlice, equalityComparer);

      if (atStart > 0) // If equality exist in the beginning, adjust.
      {
        sourceSlice = sourceSlice[atStart..];
        targetSlice = targetSlice[atStart..];
      }

      atEnd = sourceSlice.CommonSuffixLength(targetSlice, equalityComparer);

      if (atEnd > 0) // If equality exist at the end, adjust.
      {
        sourceSlice = sourceSlice[..^atEnd];
        targetSlice = targetSlice[..^atEnd];
      }
    }
  }
}
