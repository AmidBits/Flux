namespace Flux
{
  public static partial class Spans
  {
    /// <summary>Replace all values in <paramref name="source"/> using the specified <paramref name="replacementSelector"/>.</summary>
    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, System.Func<T, int, T> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var item && predicate(item, index))
          source[index] = replacementSelector(item, index);

      return source;
    }

    /// <summary>
    /// <para>Replace all elements satisfying the <paramref name="predicate"/> with the <paramref name="replacement"/> if after <paramref name="startIndex"/> and <paramref name="length"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, T replacement)
      => source.Replace(predicate, (e, i) => replacement);
  }
}
