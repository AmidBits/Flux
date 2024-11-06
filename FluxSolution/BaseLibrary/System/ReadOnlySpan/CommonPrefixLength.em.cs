namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/>, optionally skipping <paramref name="offset"/> elements.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="predicate"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, int offset, System.Func<T, bool> predicate, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      var length = 0;
      while (offset < source.Length && length < maxLength && predicate(source[offset++]))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, int offset, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var length = 0;
      while (offset < source.Length && length < maxLength && equalityComparer.Equals(source[offset++], value))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="maxLength"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength<T>(this System.ReadOnlySpan<T> source, int offset, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      if (source.Length == 0 || value.Length == 0)
        return 0;

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var minLength = int.Min(source.Length, value.Length);

      var length = 0;
      while (length < minLength && offset < source.Length && length < maxLength && equalityComparer.Equals(source[offset++], value[length]))
        length++;
      return length;
    }
  }
}
