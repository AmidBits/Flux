namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SpanBuilder<char> ToSpanBuilder(this string source)
      => new(source.AsSpan());
    public static SpanBuilder<char> ToSpanBuilder(this string source, int startIndex)
      => new(source.AsSpan()[startIndex..]);
    public static SpanBuilder<char> ToSpanBuilder(this string source, int startIndex, int length)
      => new(source.AsSpan().Slice(startIndex, length));
  }
}
