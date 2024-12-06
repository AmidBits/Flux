namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> ends with <paramref name="count"/> elements that satisfy the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool IsCommonSuffix<T>(this System.ReadOnlySpan<T> source, int offset, int count, System.Func<T, bool> predicate)
      => source.CommonSuffixLength(offset, predicate, count) == count;

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> ends with <paramref name="count"/> occurences of the <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonSuffix<T>(this System.ReadOnlySpan<T> source, int offset, int count, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      => source.CommonSuffixLength(offset, value, equalityComparer, count) == count;

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> ends with the <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonSuffix<T>(this System.ReadOnlySpan<T> source, int offset, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      => source.CommonSuffixLength(offset, value, equalityComparer, value.Length) == value.Length;
  }
}
