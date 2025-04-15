namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static double OverlapCoefficient<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => (double)source.Intersect(target, equalityComparer).Count / (double)int.Min(source.Length, target.Length);
  }
}
