namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Creates a new SpanBuilder from the source.</summary>
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.ReadOnlySpan<T> source)
      => new(source);

    /// <summary>Creates a new SpanBuilder from the source.</summary>
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.Span<T> source)
      => new(source);
  }
}
