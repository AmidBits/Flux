namespace Flux
{
  public static partial class ReadOnlySpans
  {
    #region CommonSuffixLength

    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTestLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate, int maxTestLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var sourceMaxIndex = source.Length - 1;

      maxTestLength = int.Min(maxTestLength, sourceMaxIndex);

      var length = 0;
      while (length < maxTestLength && predicate(source[sourceMaxIndex - length], length))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTestLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, int maxTestLength = int.MaxValue)
      => CommonSuffixLength(source, (e, i) => predicate(e), maxTestLength);

    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="maxTestLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, T value, int maxTestLength = int.MaxValue, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxTestLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceMaxIndex = source.Length - 1;

      maxTestLength = int.Min(maxTestLength, sourceMaxIndex);

      var length = 0;
      while (length < maxTestLength && equalityComparer.Equals(source[sourceMaxIndex - length], value))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <remarks>
    /// <para>There is a built-in method for <see cref="System.MemoryExtensions.CommonPrefixLength{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, IEqualityComparer{T}?)"/> but not one for common-suffix-length.</para>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int maxLength, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceMaxIndex = source.Length - 1;
      var targetMaxIndex = target.Length - 1;

      maxLength = int.Min(maxLength, int.Min(sourceMaxIndex, targetMaxIndex));

      var length = 0;
      while (length < maxLength && equalityComparer.Equals(source[sourceMaxIndex - length], target[targetMaxIndex - length]))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Simulating a built-in version of a CommonSuffixLength extension (akin to the actual built-in CommonPrefixLength extension).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => CommonSuffixLength(source, target, int.MaxValue, equalityComparer);

    #endregion
  }
}
