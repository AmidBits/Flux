namespace Flux
{
  public static partial class Fx
  {
    public static int IndexOf(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var sourceLength = source.Length;

      for (; offset < sourceLength; offset++)
        if (predicate(source[offset]))
          return offset;

      return -1;
    }

    /// <summary>Reports the first index of the specified char in the string builder, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;

      for (; offset < sourceLength; offset++)
        if (equalityComparer.Equals(source[offset], value))
          return offset;

      return -1;
    }

    /// <summary>Returns the first index of the specified string in the string builder, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var maxIndex = source.Length - 1 - value.Length;

      for (; offset <= maxIndex; offset++)
        if (source.IsCommonPrefix(offset, value, equalityComparer))
          return offset;

      return -1;
    }
  }
}
