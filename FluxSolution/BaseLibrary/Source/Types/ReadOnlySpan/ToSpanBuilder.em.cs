namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.ReadOnlySpan<T> source) => new(source);
  }
}
