namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Reports the first index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>
    /// <para>Reports the first index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
      => source.IndexOf((e, i) => predicate(e));

    /// <summary>
    /// <para>Reports the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Length; index++)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }

    /// <summary>
    /// <para>Returns the first index of the specified <paramref name="target"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var maxIndex = source.Length - target.Length;

      for (var index = 0; index <= maxIndex; index++)
        if (source[index..].IsCommonPrefix(target, equalityComparer))
          return index;

      return -1;
    }
  }
}
