namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new read-only-span from <paramref name="source"/> with all matching prefix elements satisfying the <paramref name="predicate"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonPrefix<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
      => source[source.CommonPrefixLength(0, predicate)..];

    /// <summary>
    /// <para>Creates a new read-only-span from <paramref name="source"/> with the matching prefix elements from <paramref name="target"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonPrefix<T>(this System.ReadOnlySpan<T> source, T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      => source[source.CommonPrefixLength(0, target, equalityComparer)..];

    /// <summary>
    /// <para>Creates a new read-only-span from <paramref name="source"/> with the matching prefix elements from <paramref name="target"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonPrefix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      => source[source.CommonPrefixLength(0, target, equalityComparer)..];
  }
}
