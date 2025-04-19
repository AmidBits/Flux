namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonPrefix(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonPrefixLength(offset, predicate, maxTrimLength);

      return source.Remove(offset, length);
    }

    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonPrefix(this System.Text.StringBuilder source, int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonPrefixLength(offset, value, equalityComparer, maxTrimLength);

      return source.Remove(offset, length);
    }

    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonPrefix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      var length = source.CommonPrefixLength(offset, value, equalityComparer);

      return source.Remove(offset, length);
    }
  }
}
