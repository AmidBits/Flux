namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.Span<T> source) => new SpanBuilder<T>(source);
  }
}
