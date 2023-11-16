namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Create a new <see cref="SpanBuilder{T}"/> from the elements in <paramref name="source"/>.</summary>
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.Span<T> source) => new(source);
  }
}
