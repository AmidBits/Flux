namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Yields the number of characters that the source and the target have in common at the end.</para>
    /// </summary>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      var length = 0;
      for (var si = source.Length - 1 - offset; si >= 0 && length < maxLength && predicate(source[si]); si--)
        length++;
      return length;
    }

    /// <summary>
    /// <para>Yields the number of characters that the source and the target have in common at the end.</para>
    /// </summary>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var length = 0;
      for (var si = source.Length - 1 - offset; si >= 0 && length < maxLength && equalityComparer.Equals(source[si], value); si--)
        length++;
      return length;
    }

    /// <summary>
    /// <para>Yields the number of characters that the source and the target have in common at the end.</para>
    /// </summary>
    public static int CommonSuffixLength(this System.Text.StringBuilder source, int index, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (index < 0 || index >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var si = source.Length - index;
      var ti = value.Length;

      var length = 0;
      while (--si >= 0 && --ti >= 0 && length < maxLength && equalityComparer.Equals(source[si], value[ti]))
        length++;
      return length;
    }
  }
}
