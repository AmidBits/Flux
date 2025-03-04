namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new read-only-span from <paramref name="source"/> with all matching suffix elements satisfying the <paramref name="predicate"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonSuffix<T>(this System.ReadOnlySpan<T> source, int offset, System.Func<T, bool> predicate, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, predicate, maxTrimLength);

      return source[^length..];
    }

    /// <summary>
    /// <para>Creates a new read-only-span from <paramref name="source"/> with the matching suffix elements from <paramref name="value"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonSuffix<T>(this System.ReadOnlySpan<T> source, int offset, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, value, equalityComparer, maxTrimLength);

      return source[^length..];
    }

    /// <summary>
    /// <para>Creates a new read-only-span from <paramref name="source"/> with the matching suffix elements from <paramref name="value"/> removed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> TrimCommonSuffix<T>(this System.ReadOnlySpan<T> source, int offset, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, value, equalityComparer, maxTrimLength);

      return source[^length..];
    }
  }
}
