namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="sourceSlice"></param>
    /// <param name="targetSlice"></param>
    /// <param name="atStart"></param>
    /// <param name="atEnd"></param>
    /// <param name="equalityComparer"></param>
    private static void TrimCommonEnds<T>(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out System.ReadOnlySpan<T> sourceSlice, out System.ReadOnlySpan<T> targetSlice, out int atStart, out int atEnd, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      sourceSlice = source;
      targetSlice = target;

      atStart = sourceSlice.StartMatchLength(targetSlice, equalityComparer);

      if (atStart > 0) // If equality exist in the beginning, adjust.
      {
        sourceSlice = sourceSlice[atStart..];
        targetSlice = targetSlice[atStart..];
      }

      atEnd = sourceSlice.EndMatchLength(targetSlice, equalityComparer);

      if (atEnd > 0) // If equality exist at the end, adjust.
      {
        sourceSlice = sourceSlice[..^atEnd];
        targetSlice = targetSlice[..^atEnd];
      }
    }
  }
}
