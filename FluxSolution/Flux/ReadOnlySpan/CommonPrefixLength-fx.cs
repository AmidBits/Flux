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
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, int maxLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);
      System.ArgumentNullException.ThrowIfNull(predicate);

      maxLength = int.Min(maxLength, source.Length);

      var length = 0;
      while (length < maxLength && predicate(source[length]))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      maxLength = int.Min(maxLength, source.Length);

      var length = 0;
      while (length < maxLength && equalityComparer.Equals(source[length], value))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      maxLength = int.Min(maxLength, int.Min(source.Length, target.Length));

      var length = 0;
      while (length < maxLength && equalityComparer.Equals(source[length], target[length]))
        length++;
      return length;
    }

    #endregion // CommonPrefixLength
  }
}
