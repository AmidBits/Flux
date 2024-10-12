namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="count"/> elements that satisfies the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, int count, System.Func<T, bool> predicate)
      => source.StartMatchLength(predicate) == count;

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="count"/> occurences of the <paramref name="target"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, int count, T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      => source.StartMatchLength(target, equalityComparer) == count;

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with the <paramref name="target"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool StartsWith<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      => source.StartMatchLength(target, equalityComparer) == target.Length;
  }
}
