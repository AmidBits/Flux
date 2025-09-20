namespace Flux
{
  public static partial class ReadOnlySpans
  {
    #region TrimCommonPrefix

    /// <summary>
    /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of matching prefix elements satisfying the <paramref name="predicate"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonPrefix<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
      => source[source.CommonPrefixLength(predicate, maxTrimLength)..];

    /// <summary>
    /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of prefix elements matching <paramref name="value"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonPrefix<T>(this System.ReadOnlySpan<T> source, T value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source[source.CommonPrefixLength(value, maxTrimLength, equalityComparer)..];

    /// <summary>
    /// <para>Slice the <paramref name="source"/> with the matching prefix elements from <paramref name="value"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonPrefix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source[source.CommonPrefixLength(value, maxTrimLength, equalityComparer)..];

    #endregion // TrimCommonPrefix
  }
}
