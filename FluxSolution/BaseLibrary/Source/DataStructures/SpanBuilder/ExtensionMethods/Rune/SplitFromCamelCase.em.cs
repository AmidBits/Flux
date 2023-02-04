namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static SpanBuilder<System.Text.Rune> SplitFromCamelCase(this SpanBuilder<System.Text.Rune> source, System.Text.Rune separator)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (System.Text.Rune.IsUpper(source[index]) && (!System.Text.Rune.IsUpper(source[index - 1]) || System.Text.Rune.IsLower(source[index + 1])))
          source.Insert(index, separator);

      return source;
    }
  }
}
