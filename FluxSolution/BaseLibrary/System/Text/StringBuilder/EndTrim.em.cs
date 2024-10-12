namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder EndTrim(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
      => source.Remove(source.Length - source.EndMatchLength(predicate));

    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements equal to <paramref name="target"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder EndTrim(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
      => source.Remove(source.Length - source.EndMatchLength(target, equalityComparer));

    /// <summary>
    /// <para>Returns the <paramref name="source"/> with all consecutive elements satisfying the <paramref name="predicate"/> removed from the end. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static System.Text.StringBuilder EndTrim(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer)
      => source.Remove(source.Length - source.EndMatchLength(target, equalityComparer));
  }
}
