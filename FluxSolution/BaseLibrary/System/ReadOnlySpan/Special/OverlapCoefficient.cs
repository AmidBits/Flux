namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The overlap coefficient is a similarity measure that measures the overlap between two finite sets. It is related to the Jaccard index and is defined as the size of the intersection divided by the smaller of the size of the two sets. The overlap coefficient will iterate each sequence multiple times, so if that is an issue opt to buffer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Overlap_coefficient"/>
    /// </summary>
    public static double OverlapCoefficient<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => (double)source.Intersect(target, equalityComparer).Count / (double)System.Math.Min(source.Length, target.Length);
  }
}
