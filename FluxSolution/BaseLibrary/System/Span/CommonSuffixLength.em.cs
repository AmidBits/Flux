namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer (null for default).</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public static int CommonSuffixLength<T>(this System.Span<T> source, System.Span<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => CommonSuffixLength((System.ReadOnlySpan<T>)source, target, equalityComparer);
  }
}
