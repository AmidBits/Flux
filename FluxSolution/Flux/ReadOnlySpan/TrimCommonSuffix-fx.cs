namespace Flux
{
  public static partial class ReadOnlySpans
  {
    public static System.ReadOnlySpan<T> TrimRight<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
      => source[..^source.CommonSuffixLength(predicate)];

    /// <summary>
    /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of matching suffix elements satisfying the <paramref name="predicate"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonSuffix<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
      => source[..^source.CommonSuffixLength(predicate, maxTrimLength)];

    /// <summary>
    /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of suffix elements matching <paramref name="value"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonSuffix<T>(this System.ReadOnlySpan<T> source, T value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source[..^source.CommonSuffixLength(value, maxTrimLength, equalityComparer)];

    /// <summary>
    /// <para>Slice the <paramref name="source"/> with <paramref name="maxTrimLength"/> of matching suffix elements from <paramref name="value"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonSuffix<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, int maxTrimLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => source[..^source.CommonSuffixLength(value, maxTrimLength, equalityComparer)];
  }
}
