namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static SpanBuilder<System.Text.Rune> JoinToCamelCase(this SpanBuilder<System.Text.Rune> source, System.Func<System.Text.Rune, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            source[index] = System.Text.Rune.ToUpperInvariant(source[index]);
        }

      return source;
    }
    public static SpanBuilder<System.Text.Rune> JoinToCamelCase(this SpanBuilder<System.Text.Rune> source)
      => JoinToCamelCase(source, System.Text.Rune.IsWhiteSpace);
  }
}
