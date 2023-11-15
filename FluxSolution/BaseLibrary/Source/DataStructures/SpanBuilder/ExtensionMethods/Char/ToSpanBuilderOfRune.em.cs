namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    public static SpanBuilder<System.Text.Rune> ToSpanBuilderOfRune(this SpanBuilder<char> source)
      => new(source.AsReadOnlySpan().ToListOfRune());
  }
}
