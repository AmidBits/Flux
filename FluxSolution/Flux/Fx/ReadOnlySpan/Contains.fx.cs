using System.Drawing;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines if there are any elements satisfying the <paramref name="predicate"/> in the <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Contains<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index]))
          return true;

      return false;
    }

    public static bool Contains<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null) => source.ContainsAny(equalityComparer, value);

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ContainsAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, params T[] values) => source.Contains(e => values.Contains(e, equalityComparer));
  }
}
