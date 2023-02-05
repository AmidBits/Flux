namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SpanBuilder<char> ToSpanBuilder(this System.Text.StringBuilder source) => new(source.ToString().AsSpan());
  }
}
