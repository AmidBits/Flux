namespace Flux
{
  public static partial class Fx
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
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonPrefix(this System.Text.StringBuilder source, int offset, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, int maxTrimLength = int.MaxValue)
    {
      var length = source.CommonPrefixLength(offset, target, equalityComparer, maxTrimLength);

      return source.Remove(offset, length);
    }

    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder TrimCommonPrefix(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      var length = source.CommonPrefixLength(offset, target, equalityComparer);

      return source.Remove(offset, length);
    }
  }
}
