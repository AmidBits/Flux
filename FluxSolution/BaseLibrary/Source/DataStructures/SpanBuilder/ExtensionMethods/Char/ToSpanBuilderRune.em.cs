namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    public static SpanBuilder<System.Text.Rune> ToSpanBuilderRune(this SpanBuilder<char> source)
      => new(source.AsReadOnlySpan().ToListRune());
  }
}
