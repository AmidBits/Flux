namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SpanBuilder<char> ToSpanBuilder(this string source)
      => new(source);
    public static SpanBuilder<char> ToSpanBuilder(this string source, int startIndex, int length)
      => new(source.Substring(startIndex, length));
  }
}
