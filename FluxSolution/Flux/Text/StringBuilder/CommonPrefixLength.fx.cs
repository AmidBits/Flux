namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and the <paramref name="predicate"/>, optionally skipping <paramref name="offset"/> elements.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="predicate"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);
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
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength(this System.Text.StringBuilder source, int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var length = 0;
      while (offset < source.Length && length < maxLength && equalityComparer.Equals(source[offset++], value))
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common prefix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonPrefixLength(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var minLength = int.Min(source.Length, value.Length);

      var length = 0;
      while (length < minLength && offset < source.Length && length < maxLength && equalityComparer.Equals(source[offset++], value[length]))
        length++;
      return length;
    }
  }
}
