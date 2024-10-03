namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Yields the number of values that the <paramref name="source"/> and the <paramref name="target"/> have in common from the specified <paramref name="sourceStartIndex"/> and <paramref name="targetStartIndex"/> respectively. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static int CommonLengthAt<T>(this System.Span<T> source, int sourceStartIndex, System.Span<T> target, int targetStartIndex, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => CommonLengthAt((System.ReadOnlySpan<T>)source, sourceStartIndex, target, targetStartIndex, equalityComparer);
  }
}
