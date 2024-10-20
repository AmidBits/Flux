namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Yields the number of characters that the source and the target have in common from the start.</para>
    /// </summary>
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
    /// <para>Yields the number of characters that the <paramref name="source"/> contains of <paramref name="value"/> the start.</para>
    /// </summary>
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
    /// <para>Yields the number of characters that the source and the target have in common from the start.</para>
    /// </summary>
    public static int CommonPrefixLength(this System.Text.StringBuilder source, int index, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (index < 0 || index >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var minLength = System.Math.Min(source.Length, value.Length);

      var length = 0;
      while (length < minLength && index < source.Length && length < maxLength && equalityComparer.Equals(source[index++], value[length]))
        length++;
      return length;
    }
  }
}
