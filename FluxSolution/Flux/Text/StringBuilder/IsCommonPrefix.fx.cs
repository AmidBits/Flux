namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Indicates whether there are <paramref name="count"/> elements satisfying the <paramref name="predicate"/> at <paramref name="source"/>[<paramref name="offset"/>].</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool IsCommonPrefix(this System.Text.StringBuilder source, int offset, int count, System.Func<char, bool> predicate)
      => source.CommonPrefixLength(offset, predicate, count) == count;

    /// <summary>
    /// <para>Indicates whether there are <paramref name="count"/> elements equal to <paramref name="value"/> at <paramref name="source"/>[<paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonPrefix(this System.Text.StringBuilder source, int offset, int count, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonPrefixLength(offset, value, equalityComparer, count) == count;

    /// <summary>
    /// <para>Indicates whether <paramref name="value"/> exists at <paramref name="source"/>[offset]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonPrefix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonPrefixLength(offset, value, equalityComparer, value.Length) == value.Length;
  }
}
