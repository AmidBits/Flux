namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes how many elements satisfying the <paramref name="predicate"/> is found in the <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int Count<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      var count = 0;

      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
          count++;

      return count;
    }

    /// <summary>
    /// <para>Computes how many elements of <paramref name="target"/> is found in the <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int Count<T>(this System.ReadOnlySpan<T> source, T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return Count(source, t => equalityComparer.Equals(t, target));
    }
  }
}
