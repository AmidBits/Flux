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

    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, int, bool> predicate, System.Func<T, int, T> replacementSelector)
      => source.Replace(predicate, replacementSelector, 0, source.Length);

    /// <summary>Replace all characters satisfying the <paramref name="predicate"/> with the <paramref name="replacement"/>.</summary>
    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, bool> predicate, T replacement, int startIndex, int count)
    {
      for (var index = startIndex + count - 1; index >= startIndex; index--)
        if (predicate(source[index]))
          source[index] = replacement;

      return source;
    }

    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, bool> predicate, T replacement)
      => source.Replace(predicate, replacement, 0, source.Length);
  }
}
