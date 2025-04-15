namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Reports the last index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index]))
          return index;

      return -1;
    }

    /// <summary>
    /// <para>Returns the last index of <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = source.Length - 1; index >= 0; index--)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }

    /// <summary>
    /// <para>Returns the last index of <paramref name="target"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = source.Length - target.Length; index >= 0; index--)
        if (source[index..].IsCommonPrefix(target, equalityComparer))
          return index;

      return -1;
    }
  }
}
