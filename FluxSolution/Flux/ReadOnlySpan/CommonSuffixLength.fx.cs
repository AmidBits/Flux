namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, int maxLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var sourceMaxIndex = source.Length - 1;

      maxLength = int.Min(maxLength, sourceMaxIndex);

      var length = 0;
      while (length < maxLength && predicate(source[sourceMaxIndex - length]))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceMaxIndex = source.Length - 1;

      maxLength = int.Min(maxLength, sourceMaxIndex);

      var length = 0;
      while (length < maxLength && equalityComparer.Equals(source[sourceMaxIndex - length], value))
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
    public static int CommonSuffixLength<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxLength);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceMaxIndex = source.Length - 1;
      var targetMaxIndex = value.Length - 1;

      maxLength = int.Min(maxLength, int.Min(sourceMaxIndex, targetMaxIndex));

      var length = 0;
      while (length < maxLength && equalityComparer.Equals(source[sourceMaxIndex - length], value[targetMaxIndex - length]))
        length++;
      return length;
    }
  }
}
