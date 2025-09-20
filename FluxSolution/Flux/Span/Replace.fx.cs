namespace Flux
{
  public static partial class Spans
  {
    #region Replace

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

    public static System.Span<T> Replace<T>(this System.Span<T> source, System.Func<T, bool> predicate, System.Func<T, T> replacementSelector)
      => source.Replace((e, i) => predicate(e), (e, i) => replacementSelector(e));

    #endregion
  }
}
