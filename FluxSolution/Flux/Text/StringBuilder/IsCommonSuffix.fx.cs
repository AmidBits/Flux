namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Indicates whether there are <paramref name="count"/> elements satisfying the <paramref name="predicate"/> that ends <paramref name="source"/>[end - <paramref name="offset"/>].</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool IsCommonSuffix(this System.Text.StringBuilder source, int offset, int count, System.Func<char, bool> predicate)
      => source.CommonSuffixLength(offset, predicate, count) == count;

    /// <summary>
    /// <para>Indicates whether there are <paramref name="count"/> elements equal to <paramref name="value"/> that ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonSuffix(this System.Text.StringBuilder source, int offset, int count, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonSuffixLength(offset, value, equalityComparer, count) == count;

    /// <summary>
    /// <para>Indicates whether the <paramref name="value"/> ends at <paramref name="source"/>[end - <paramref name="offset"/>]. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static bool IsCommonSuffix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.CommonSuffixLength(offset, value, equalityComparer, value.Length) == value.Length;
  }
}
