namespace Flux
{
  public static partial class Fx
  {
    #region IsCommonPrefix

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="count"/> elements that satisfies the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool IsCommonPrefix<T>(this System.ReadOnlySpan<T> source, int count, System.Func<T, bool> predicate)
      => source.CommonPrefixLength(predicate, count) == count;

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="count"/> occurences of the <paramref name="value"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonPrefix<T>(this System.ReadOnlySpan<T> source, int count, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source.CommonPrefixLength(value, equalityComparer, count) == count;

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with <paramref name="length"/> elements from the <paramref name="target"/> span. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="length"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonPrefix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source.CommonPrefixLength(target, equalityComparer, length) == length;

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> starts with the <paramref name="target"/> span. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonPrefix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source.IsCommonPrefix(target, target.Length, equalityComparer);

    #endregion // IsCommonPrefix
  }
}
