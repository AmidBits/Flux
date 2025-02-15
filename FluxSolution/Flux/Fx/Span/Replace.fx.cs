namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Replace all values in <paramref name="source"/> using the specified <paramref name="replacementSelector"/>.</summary>
    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, System.Func<T, int, T> replacementSelector, int startIndex, int count)
    {
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var index = startIndex + count - 1; index >= startIndex; index--)
        if (source[index] is var item && predicate(item, index))
          source[index] = replacementSelector(item, index);

      return source;
    }

    /// <summary>
    /// <para>Replace all elements in <paramref name="source"/> satisfying the <paramref name="predicate"/> with the result from the <paramref name="replacementSelector"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, System.Func<T, int, T> replacementSelector)
      => source.Replace(predicate, replacementSelector, 0, source.Length);

    /// <summary>
    /// <para>Replace all elements satisfying the <paramref name="predicate"/> with the <paramref name="replacement"/> if after <paramref name="startIndex"/> and <paramref name="length"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, T replacement, int startIndex, int length)
      => source.Replace(predicate, (e, i) => replacement, startIndex, length);

    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, T replacement, Slice slice)
      => source.Replace(predicate, (e, i) => replacement, slice.Index, slice.Length);

    /// <summary>
    /// <para>Replace all elements satisfying the <paramref name="predicate"/> with the specified <paramref name="replacement"/> in the <see cref="System.Span{T}"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, T replacement)
      => source.Replace(predicate, (e, i) => replacement, 0, source.Length);
  }
}
