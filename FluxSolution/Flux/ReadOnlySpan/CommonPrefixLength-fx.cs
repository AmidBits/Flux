namespace Flux
{
  public static partial class ReadOnlySpans
  {
    #region CommonPrefixLength

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTestLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate, int maxTestLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);
      System.ArgumentNullException.ThrowIfNull(predicate);

      maxTestLength = int.Min(maxTestLength, source.Length);

      var length = 0;
      while (length < maxTestLength && predicate(source[length], length))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTestLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, int maxTestLength = int.MaxValue)
      => CommonPrefixLength(source, (e, i) => predicate(e), maxTestLength);

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and a <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="maxTestLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, T value, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      maxTestLength = int.Min(maxTestLength, source.Length);

      var length = 0;
      while (length < maxTestLength && equalityComparer.Equals(source[length], value))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="maxTestLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      maxTestLength = int.Min(maxTestLength, int.Min(source.Length, target.Length));

      var length = 0;
      while (length < maxTestLength && equalityComparer.Equals(source[length], target[length]))
        length++;
      return length;
    }

    #endregion // CommonPrefixLength
  }
}
