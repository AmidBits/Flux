namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Reports the first index in <paramref name="source"/> that satisfies the <paramref name="predicate"/>, or -1 if not found.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index]))
          return index;

      return -1;
    }

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

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }

    /// <summary>
    /// <para>Returns the first index of the specified <paramref name="value"/> in <paramref name="source"/>, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var maxIndex = source.Length - value.Length;

      for (var index = 0; index <= maxIndex; index++)
        if (source.IsCommonPrefix(index, value, equalityComparer))
          return index;

      return -1;
    }
  }
}
