namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static SpanBuilder<System.Text.Rune> ToSpanBuilderRune(this SpanBuilder<char> source)
      => new(source.AsReadOnlySpan().ToListRune());
  }
}
