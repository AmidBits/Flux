namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new SpanBuilder from the source.</summary>
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.ReadOnlySpan<T> source)
      where T : notnull
      => new(source);

    /// <summary>Creates a new SpanBuilder from the source.</summary>
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.Span<T> source)
      where T : notnull
      => new(source);
  }
}
