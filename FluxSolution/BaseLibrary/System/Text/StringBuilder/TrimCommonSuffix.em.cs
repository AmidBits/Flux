namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonSuffix(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, predicate, maxTrimLength);

      return source.Remove(source.Length - offset - length, length);
    }

    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements equal to <paramref name="target"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonSuffix(this System.Text.StringBuilder source, int offset, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonSuffixLength(offset, target, equalityComparer, maxTrimLength);

      return source.Remove(source.Length - offset - length, length);
    }

    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="offset"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonSuffix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      var length = source.CommonSuffixLength(offset, target, equalityComparer);

      return source.Remove(source.Length - offset - length, length);
    }
  }
}
