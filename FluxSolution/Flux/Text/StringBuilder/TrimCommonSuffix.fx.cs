namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="predicate"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonSuffix(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, predicate, maxTrimLength);

      return source.Remove(source.Length - offset - length, length);
    }

    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements equal to <paramref name="value"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonSuffix(this System.Text.StringBuilder source, int offset, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, value, equalityComparer, maxTrimLength);

      return source.Remove(source.Length - offset - length, length);
    }

    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="maxTrimLength"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonSuffix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, value, equalityComparer, maxTrimLength);

      return source.Remove(source.Length - offset - length, length);
    }
  }
}
