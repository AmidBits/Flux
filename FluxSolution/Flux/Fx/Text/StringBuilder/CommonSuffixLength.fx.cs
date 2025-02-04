namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and the <paramref name="predicate"/>, optionally skipping <paramref name="offset"/> elements.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="predicate"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (source.Length == 0 || maxLength <= 0)
        return 0;

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      var length = 0;
      for (var si = source.Length - 1 - offset; si >= 0 && length < maxLength && predicate(source[si]); si--)
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Length == 0 || maxLength <= 0)
        return 0;

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var length = 0;
      for (var si = source.Length - 1 - offset; si >= 0 && length < maxLength && equalityComparer.Equals(source[si], value); si--)
        length++;
      return length;
    }

    /// <summary>
    /// <para>Finds the length of any common suffix shared between <paramref name="source"/> and <paramref name="value"/>, optionally skipping <paramref name="offset"/> elements. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Length == 0 || value.Length == 0 || maxLength <= 0)
        return 0;

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var si = source.Length - offset;
      var ti = value.Length;

      var length = 0;
      while (--si >= 0 && --ti >= 0 && length < maxLength && equalityComparer.Equals(source[si], value[ti]))
        length++;
      return length;
    }
  }
}
