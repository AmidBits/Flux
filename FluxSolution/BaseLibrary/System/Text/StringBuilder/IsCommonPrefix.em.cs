namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether there are <paramref name="count"/> elements satisfying the <paramref name="predicate"/> at <paramref name="source"/>[<paramref name="offset"/>].</summary>
    public static bool IsCommonPrefix(this System.Text.StringBuilder source, int offset, int count, System.Func<char, bool> predicate)
      => source.CommonPrefixLength(offset, predicate, count) == count;

    /// <summary>Indicates whether there are <paramref name="count"/> elements equal to <paramref name="value"/> at <paramref name="source"/>[<paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool IsCommonPrefix(this System.Text.StringBuilder source, int offset, int count, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonPrefixLength(offset, value, equalityComparer, count) == count;

    /// <summary>Indicates whether <paramref name="value"/> exists at <paramref name="source"/>[offset]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool IsCommonPrefix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonPrefixLength(offset, value, equalityComparer) == value.Length;
  }
}
