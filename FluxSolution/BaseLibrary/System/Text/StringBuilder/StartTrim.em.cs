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
    public static System.Text.StringBuilder StartTrim(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
      => source.Remove(0, source.StartMatchLength(predicate));

    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder StartTrim(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
      => source.Remove(0, source.StartMatchLength(target, equalityComparer));

    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed at the start.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder StartTrim(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
      => source.Remove(0, source.StartMatchLength(target, equalityComparer));
  }
}
