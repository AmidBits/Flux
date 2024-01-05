namespace Flux
{
  public static partial class Em
  {
    /// <summary>Replace all characters satisfying the <paramref name="predicate"/> with the result of the <paramref name="replacementSelector"/>.</summary>
    public static SpanBuilder<char> Replace(this Flux.SpanBuilder<char> source, System.Func<char, bool> predicate, System.Func<char, string> replacementSelector)
    {
      System.ArgumentNullException.ThrowIfNull(replacementSelector);

      for (var index = source.Length - 1; index >= 0; index--)
        if (source[index] is var c && predicate(c))
        {
          source.Remove(index, 1);
          source.Insert(index, replacementSelector(c), 1);
        }

      return source;
    }
  }
}
