namespace Flux
{
  public static partial class Fx
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

      return LastIndexOf(source, t => equalityComparer.Equals(t, value));
    }

    /// <summary>
    /// <para>Returns the last index of <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = source.Length - value.Length; index >= 0; index--)
        if (EqualsAt(source, index, value, 0, value.Length, equalityComparer))
          return index;

      return -1;
    }
  }
}
